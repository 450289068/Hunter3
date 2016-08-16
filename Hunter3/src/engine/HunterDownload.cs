using System;
using System.Net;
using System.Timers;
using System.Threading;
using System.IO;
using System.Windows.Forms;
using Hunter3.Data;
using Hunter3.Strategies;

namespace Hunter3
{
    //从列表中取地址下载文件的类
    public class HunterDownload
    {
        Hunter hunterProject;
        XMLDatabase database;   //XML数据库
        HunterConsole hunterConsole;
        ProjectInfo pInfo;
        Strategy strategy = null;

        public static int count = 0;  //下载文件数目
        System.Timers.Timer flowCalculator = new System.Timers.Timer(); //流量计
        long receive1 = 0;  //第一次获得的数据
        long receive2 = 0;  //若干时间差后获得的数据

        /// <summary>
        /// 构造函数
        /// </summary>
        public HunterDownload(Hunter _p)
        {
            hunterProject = _p;
            hunterConsole = _p.mHunterConsole;
            database = _p.xmlDatabase;
            pInfo = _p.projectInfo;
            strategy = _p.projectInfo.strategy;
        }

        /// <summary>
        /// 下载指定uri中的所有文件。如果为null表示跳过下载，为Empty表示下载没有问题，为路径表示下载不成功。
        /// </summary>
        /// <param name="uri">文件资源定位</param>
        /// <returns>下载信息</returns>
        public DownloadInfo DownloadFile(UriResource uriRes, Hunter h, HunterDownloadThread thisThread)
        {
            Database db = null;
            if (h.projectInfo.DatabaseHelper != null)
            {
                db = h.projectInfo.DatabaseHelper.GetDatabaseInstance();
            }

            flowCalculator.Interval = 1000;
            flowCalculator.Elapsed += new ElapsedEventHandler(flowCalculator_Elapsed);
            try
            {
                HunterWebClient wc = new HunterWebClient();

                #region 判断文件是否重复
                bool isExist = false;
                //检查文件在本地是否重复
                isExist = database.LinkExists(uriRes.Url);
                //如果文件不重复，而又为网络模式，则要检查数据库内的内容

                if (!isExist && (h.projectInfo.CurrentMode == ProjectInfo.HunterMode.network ))   //网络模式需要对比数据库和HunterXML
                {
                    
                    if (db == null)
                    {
                        hunterConsole.WriteException(new Exception("数据库连接失败，使用本地模式判重：" + uriRes.Url));
                    }
                    else
                    {
                        bool OpenFailed = false;
                        try
                        {
                            db.DbOpen();
                        }
                        catch (Exception ex)
                        {
                            hunterConsole.WriteException(ex);
                        }
                        bool KRESULT = db.IsRecordExists("tb_file_infos", out isExist, new FieldValue("file_link", uriRes.Url.Replace("'", "\\'").Replace("\"", "\\\"") ) );
                        if (!KRESULT || OpenFailed)
                        {
                            //hunterConsole.WriteException(new Exceptions("数据库连接失败，使用本地模式判重：" + uriRes.Url));
                            isExist = false;
                        }
                        try
                        {
                            db.DbClose();
                        }
                        catch (Exception ex)
                        {
                            hunterConsole.WriteException(ex);
                        }
                    }

                }
                #endregion

                if (isExist)
                #region 链接重复对应的措施
                {
                    hunterConsole.ReportAbandonURI(uriRes, "链接重复");
                    return null;
                }
                #endregion
                else
                {
                    #region 下载部分
                    try
                    {
                        hunterConsole.WriteDownload("正在下载文件：" + uriRes.Url);
                        hunterConsole.WriteDownload(
                            "线程ID：" + Thread.CurrentThread.ManagedThreadId + Environment.NewLine +
                            "正在下载的文件：" + Environment.NewLine +
                            "下载地址：" + uriRes.Url + Environment.NewLine +
                            "下载的关键字：" + strategy.GetKeyword(uriRes.Keyword) + Environment.NewLine +
                            "下载的页面页码：" + uriRes.index);

                    }
                    catch (Exception ex)
                    {
                        hunterConsole.WriteException(ex);
                    }


                    wc.DownloadFileCompleted += new System.ComponentModel.AsyncCompletedEventHandler(wc_DownloadFileCompleted); //绑定文件下载事件
                    wc.DownloadProgressChanged += new DownloadProgressChangedEventHandler(wc_DownloadProgressChanged);  //绑定下载进度改变事件

                    //临时文件命名
                    string filepath = HunterUtilities.GetFilenameFromUrl(pInfo, strategy, uriRes); //获取合适的文件名

                    int timeout = 0;

                    flowCalculator.Start(); //开始计算流量
                    receive1 = 0;   //最开始第一次获得的数据量为0

                    wc.DownloadKeyword = strategy.GetKeyword(uriRes.Keyword);
                    wc.DownloadSource = uriRes.Url;
                    wc.DownloadDestination = filepath;
                    if (!Directory.Exists(Path.GetDirectoryName(filepath)))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(filepath));
                    }

                    wc.DownloadFileAsync(new Uri(uriRes.Url), filepath);    //开始下载

                    bool downloadProblem = false;
                    while (wc.IsBusy)
                    {
                        Thread.Sleep(1000); //使用进程休眠
                        timeout++;
                        if (timeout >= pInfo.timeout || h.DownloadCancelled) //如果超时或下载取消
                        {
                            wc.CancelAsync();
                            wc.Dispose();

                            if (!downloadProblem)
                            {
                                hunterConsole.WriteDownload(
                                    "线程ID：" + Thread.CurrentThread.ManagedThreadId + Environment.NewLine +
                                    "下载超时，取消下载。");
                                try
                                {
                                    hunterConsole.ReportAbandonDownloadInfo(new DownloadInfo("", uriRes.Keyword, uriRes.index, uriRes.Url, "", true, "超时", hunterProject.projectInfo.strategy.GetKeyword(uriRes.Keyword))
                                        , "超时");
                                }
                                catch { }
                            }
                            downloadProblem = true;
                        }

                    }
                    flowCalculator.Stop();
                    receive1 = 0;
                    receive2 = 0;   //清空流量计
                    hunterConsole.outputSpeedInfo(DateTime.Now, 0); //流量计清零

                    wc.Dispose();
                    #endregion

                    if (!downloadProblem)
                    {   //如果没有下载问题
                        string MD5 = string.Empty;
                        try
                        {
                            MD5 = HunterUtilities.GetMD5Hash(filepath);
                        }
                        catch (Exception e)
                        {
                            hunterConsole.WriteException(new Exception("无法获取MD5。"));
                            hunterConsole.WriteException(e);
                        }

                        #region 文件MD5是否重复
                        bool isDuplicate; //记录文件是否重复
                        //判断是否与本地XML重复
                        isDuplicate = database.isDuplicate(uriRes.Url, MD5);
                        if (!isDuplicate && (h.projectInfo.CurrentMode == ProjectInfo.HunterMode.network ))   //网络模式需要对比数据库和HunterXML
                        {
                            if (db == null)
                            {
                                hunterConsole.WriteException(new Exception("数据库连接失败，使用本地模式判重：" + MD5));
                            }
                            else
                            {
                                bool OpenFailed = false;
                                try
                                {
                                    db.DbOpen();
                                }
                                catch (Exception ex)
                                {
                                    hunterConsole.WriteException(ex);
                                }
                                bool KRESULT = db.IsFileExists("tb_file_infos", out isDuplicate, MD5);
                                if (!KRESULT || OpenFailed)
                                {
                                    hunterConsole.WriteException(new Exception("数据库连接失败，使用本地模式判重：" + MD5));
                                    isDuplicate = false;
                                }
                                try
                                {
                                    db.DbClose();
                                }
                                catch (Exception ex)
                                {
                                    hunterConsole.WriteException(ex);
                                }
                            }
                        }

                        #endregion

                        #region 文件重复、不重复对应的动作
                        if (!isDuplicate)
                        {  //检测是否重复。如果不重复则入库
                            wc.XMLFile = Path.Combine(hunterProject.projectInfo.filefolder, "$__" + Path.GetFileName(wc.DownloadDestination)) + ".xml";
                            try
                            {
                                HunterUtilities.WriteDownloadFileXML(wc.DownloadSource, wc.DownloadKeyword, Path.GetFileName(wc.DownloadDestination), (hunterProject.projectInfo.search_language == ProjectInfo.Language.none ? null : hunterProject.projectInfo.search_language.ToString()),
                                  wc.XMLFile);
                            }
                            catch (Exception ex)
                            {
                                hunterConsole.WriteException(ex);
                            }
                            database.addNewRecord(uriRes.Url, wc.DownloadKeyword, filepath, MD5);
                            DownloadInfo d = new DownloadInfo(filepath, uriRes.Keyword, uriRes.index, uriRes.Url, MD5, false, "已下载", hunterProject.projectInfo.strategy.GetKeyword(uriRes.Keyword));
                            //Network模式：自动上传样张
                            if (h.projectInfo.CurrentMode == ProjectInfo.HunterMode.network)
                            #region 网络模式上传样张
                            {
                                String t_md5 = HunterUtilities.GetMD5Hash(filepath);    //获得文件MD5码
                                String filename = t_md5 + "_" + Path.GetFileName(filepath);
                                String combinedPath = Path.Combine(pInfo.share_remote_path, "cache", ProjectInfo.IP_ADDRESS + " (" + pInfo.name + ")");
                                String combinedFullPath = Path.Combine(combinedPath, filename);
                                if (!Directory.Exists(combinedPath)) Directory.CreateDirectory(combinedPath);

                                bool fileMoveSuccess = false;
                                const int maxMoveCount = 5;
                                int moveCount = 0;

                                try
                                {
                                    if (File.Exists(wc.XMLFile))
                                    {
                                        File.Delete(Path.Combine(combinedPath, Path.GetFileName(wc.XMLFile)));
                                        File.Move(wc.XMLFile, Path.Combine(combinedPath, Path.GetFileName(wc.XMLFile)));
                                    }
                                }
                                catch (Exception e)
                                {
                                    hunterConsole.WriteException(e);
                                }

                                while ( !fileMoveSuccess )
                                {
                                    try
                                    {
                                        if (moveCount > maxMoveCount) break;
                                        File.Move(filepath, combinedFullPath);
                                        fileMoveSuccess = true;
                                    }
                                    catch (Exception e)
                                    {
                                        moveCount++;
                                        hunterConsole.WriteException(e);
                                    }
                                }

                            }
                            #endregion

                            count++;
                            hunterConsole.outputDownloadedFileNum(DateTime.Now, count);
                            hunterConsole.ReportDownloadInfo(d);
                            return d;
                        }
                        else
                        {
                            DownloadInfo d = new DownloadInfo(filepath, uriRes.Keyword,
                                uriRes.index, uriRes.Url, MD5, true, "MD5重复", hunterProject.projectInfo.strategy.GetKeyword(uriRes.Keyword));
                            hunterConsole.ReportAbandonDownloadInfo(d, "MD5重复");
                            return d;   //删除文件
                        }
                        #endregion
                    }
                    return new DownloadInfo(filepath, uriRes.Keyword, uriRes.index,
                        uriRes.Url, null, true, "重复", hunterProject.projectInfo.strategy.GetKeyword(uriRes.Keyword));   //删除文件
                }
                
            }
            catch (Exception e)
            {
                //*此处预留错误处理
                hunterConsole.WriteException(e);
                return null;
            }
        }

        void flowCalculator_Elapsed(object sender, ElapsedEventArgs e)
        {
            long byteBetween = receive2 - receive1; //时间差内的流量
            hunterConsole.outputSpeedInfo(DateTime.Now, (int)byteBetween / 1024);
            receive1 = receive2;
        }


        private void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            try
            {
                receive2 = e.BytesReceived;
                hunterConsole.SetProgressSingleMax(100);
                hunterConsole.refreshSingleProgressInfo(e.ProgressPercentage);
            }
            catch (Exception ex)
            {
                hunterConsole.WriteException(ex);
            }
        }

        private void wc_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            try
            {
                //downloadNext = true;
                if (e.Cancelled)
                {
                    hunterConsole.WriteDownload("来自" + ((HunterWebClient)sender).DownloadSource + "的下载任务取消。");
                }
                else
                {
                    hunterConsole.WriteDownload("从" + ((HunterWebClient)sender).DownloadSource + "下载的文件已成功下载至" + ((HunterWebClient)sender).DownloadDestination);
                }

            }
            catch (Exception ex)
            {
                hunterConsole.WriteException(ex);
            }
        }

    }

}


