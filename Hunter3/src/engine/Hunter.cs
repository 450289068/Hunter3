using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Hunter3.Data;
using Hunter3.Strategies;
using System.Text.RegularExpressions;
namespace Hunter3
{
    /// <summary>
    /// 一个Hunter项目
    /// </summary>
    public class Hunter
    {
        private HunterForm MainForm;
        private Thread[] ProxyGetThreads;
        private HunterConfig mHunterConfig;
        private String ProxyText;
        private Queue<HunterProxy> AllProxies = new Queue<HunterProxy>();
        private List<HunterProxy> AvailableProxies = new List<HunterProxy>();
        public HunterProxyFetcher ProxyFetcher;
        public bool DownloadCancelled = false;
        public bool Error = false;

        /// <summary>
        /// 下载线程数目
        /// </summary>
        private int downloadThreadNum;

        /// <summary>
        /// 暂停和继续Hunter的开关
        /// </summary>
        private ManualResetEvent hunterSwitch = new ManualResetEvent(true);
        //private ManualResetEvent proxySwitch = new ManualResetEvent(true);

        /// <summary>
        /// Hunter下载线程组
        /// </summary>
        private HunterDownloadThread[] hunterThreads;

        /// <summary>
        /// 获取下载线程存活的数目
        /// </summary>
        private int AliveHunterThreadsCount
        {
            get
            {
                int result = 0;
                foreach (HunterDownloadThread t in hunterThreads)
                {
                    if (t.downloadThread.IsAlive)
                    {
                        result++;
                    }
                }
                return result;

            }
        }

        /// <summary>
        /// 捕获资源的线程
        /// </summary>
        private Thread thHuntUris;

        /// <summary>
        /// 猎手捕捉到的资源
        /// </summary>
        private HunterUri hUri;

        public bool isDownloadingUris = true;
        public bool isHuntingUri = true;

        private HunterDownload hDownload;

        /// <summary>
        /// 调用分配捕捉到的资源的多线程来下载
        /// </summary>
        private void threadDownloadUris(object _hunter)
        {
            HunterDownloadThread hunterDownloadThread = (HunterDownloadThread)_hunter;
            DownloadInfo downloadFile;

            while (isDownloadingUris)
            {
                Thread.Sleep(100);  //休眠，节约CPU资源
                while (uriQueue.Count > 0)
                {
                    hunterSwitch.WaitOne();
                    hDownload = new HunterDownload(this);

                    UriResource u;
                    lock (this)
                    {
                        if (uriQueue.Count > 0)
                            u = uriQueue.Dequeue();
                        else
                            break;
                    }

                    downloadFile = hDownload.DownloadFile(u, this, hunterDownloadThread);

                    if (downloadFile == null)   //如果没有下载这个文件
                    {
                        //什么也不做
                    }
                    else if (downloadFile.Remove) //如果没有入库成功，则删除这个下载到一半的文件
                    {
                        try
                        {
                            File.Delete(downloadFile.Filepath);
                        }
                        catch (Exception e)
                        {
                            mHunterConsole.WriteException(e);
                        }
                    }
                    else
                    {   //如果入库成功，则更新进度
                        projectInfo.strategy.RefreshProgress(downloadFile.Index, downloadFile.Keyword);
                    }
                }
            }

            //如果仅仅剩下此线程存活
            lock (this)
            {
                if (AliveHunterThreadsCount == 1)
                {
                    mHunterConsole.Done();
                }
            }
        }

        /// <summary>
        /// 调用捕捉资源多线程
        /// </summary>
        private void threadHuntUris()
        {
            string nextURL = null;
            List<string> lastResult = null;
            int failureTimes = 0;
            while (isHuntingUri)
            {
                Thread.Sleep(100);  //休眠，节约CPU资源
                hunterSwitch.WaitOne();
                if (uriQueue.Count < int.Parse(projectInfo.threadnum) * 2 + 1)
                {
                    List<string> thisResult =
                        hUri.HuntUris( mHunterConfig.UseProxy ? ProxyFetcher.Next() : null , MainForm);   //获取单次分析的结果

                    if (thisResult.Count == 1 && thisResult[0].Contains("{/WebException/}"))
                    {
                        //thisResult = hUri.HuntUris(mHunterConfig.UseProxy ? ProxyFetcher.Next() : null);   //再试一次，不行就拉倒
                        failureTimes++;
                        if (failureTimes >= mHunterConfig.FailureTimes)
                        {
                            mHunterConsole.WriteDetails("连续访问网页失败，正在进行治疗。网页分析线程将冷却" + (mHunterConfig.FailureCooldown ) + "秒后继续。");
                            Thread.Sleep(mHunterConfig.FailureCooldown * 1000);
                            failureTimes = 0;
                            continue;
                        }
                    }
                    else
                    {
                        failureTimes = 0;
                    }

                    if (lastResult != null) //如果是第一次运行
                    {
                        if (isSame(lastResult, thisResult) || thisResult.Count == 0)
                        {
                            nextURL = projectInfo.strategy.GetNextURL(true);
                        }
                        else
                        {
                            nextURL = projectInfo.strategy.GetNextURL(false);
                        }
                    }
                    else
                    {
                        if (thisResult.Count == 0)
                            nextURL = projectInfo.strategy.GetNextURL(true);
                        else
                            nextURL = projectInfo.strategy.GetNextURL(false);
                    }

                    lastResult = new List<string>(thisResult);
                    if (nextURL != null)
                        hUri.setUrlAdress(nextURL);   //更新URL
                    else
                    {
                        if (projectInfo.remote_dictionary)
                        {
                            projectInfo.strategy.UpdateKeywordToDatabase(projectInfo.strategy.GetCurrentKeyword(projectInfo.strategy.CurrentKeywordProgress), Strategy.DictionaryState.done, true);
                            projectInfo.strategy.LoadKeywords();    //重新读取新的关键字
                            if (projectInfo.strategy.Keywords.Count <= 0)
                            {
                                mHunterConsole.WriteMessage("已经搜索完所有范围内的网址，请等待下载结束。我已经帮您清空了搜索进度，您可以更新辞典进行新一轮下载了。");
                                projectInfo.strategy.RefreshProgress(0, 0);
                                isHuntingUri = false;
                                isDownloadingUris = false;
                                break;
                            }
                            hUri.setUrlAdress(projectInfo.strategy.GetSearchURL(0, projectInfo.strategy.Keywords[0]));
                        }
                        else
                        {
                            mHunterConsole.WriteMessage("已经搜索完所有范围内的网址，请等待下载结束。我已经帮您清空了搜索进度，您可以更新辞典进行新一轮下载了。");
                            projectInfo.strategy.RefreshProgress(0, 0);
                            isHuntingUri = false;
                            isDownloadingUris = false;
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 判断两个List是否一致
        /// </summary>
        private bool isSame(List<string> str1, List<string> str2)
        {
            for (int i = 0; i < Math.Min(str1.Count, str2.Count); i++)
            {
                if (str1[i] != str2[i])
                    return false;
            }
            return true;
        }

        public HunterConsole mHunterConsole;

        public ProjectInfo projectInfo;

        public XMLDatabase xmlDatabase;

        /// <summary>
        /// 需要下载的URI队列。此方法要被多个线程使用。
        /// </summary>
        public Queue<UriResource> uriQueue = new Queue<UriResource>();

        public Hunter(HunterConsole oh, HunterConfig config, ProjectInfo _pj, HunterForm main)
        {
            try
            {
                MainForm = main;
                Error = false;
                mHunterConsole = oh;
                projectInfo = _pj;
                mHunterConfig = config;
                ProxyFetcher = new HunterProxyFetcher(AvailableProxies);
                projectInfo = ProjectInfo.LoadProject(_pj.mHunterConsole, _pj.projectPath, _pj.strategyPath, true);

                downloadThreadNum = int.Parse(projectInfo.threadnum);
                hunterThreads = new HunterDownloadThread[downloadThreadNum];

                //获取代理的线程
                ProxyGetThreads = new Thread[downloadThreadNum];

                if (mHunterConfig.UseProxy == true)
                {
                    FileStream fs = new FileStream("proxy.hip", FileMode.Open,FileAccess.Read);
                    StreamReader sr = new StreamReader(fs);
                    ProxyText = sr.ReadToEnd();
                    sr.Close();
                    fs.Close();
                    AllProxies = HunterProxy.GetProxy(ProxyText, mHunterConfig.ProxyFilterKeywords);
                }

                mHunterConsole.WriteMessage(projectInfo.ConfigInformation());
                mHunterConsole.WriteMessage("");
                mHunterConsole.WriteMessage(projectInfo.strategy.GetStrategyInformation());

                xmlDatabase = new XMLDatabase(projectInfo.database, mHunterConsole);
                xmlDatabase.openDatabase();

                try
                {
                    if (downloadThreadNum <= 0)
                    {
                        mHunterConsole.WriteMessage("配置错误：下载线程数不能小于0。");
                        return;
                    }
                }
                catch (Exception ex)
                {
                    mHunterConsole.WriteException(ex);
                    return;
                }

                for (int i = 0; i < hunterThreads.Length; i++)
                {
                    hunterThreads[i] = new HunterDownloadThread();
                    hunterThreads[i].downloadThread = new Thread(threadDownloadUris);
                }

                if (mHunterConfig.UseProxy)
                {
                    for (int i = 0; i < ProxyGetThreads.Length; i++)
                    {
                        ProxyGetThreads[i] = new Thread(GetAvaliableProxies);
                    }
                }

                if (projectInfo.strategy.Keywords.Count <= 0)
                {
                    projectInfo.mHunterConsole.WriteMessage("没有找到关键字，任务取消。");
                    Error = true;
                    return;
                }

                thHuntUris = new Thread( threadHuntUris );
                thHuntUris.SetApartmentState(ApartmentState.STA);

                hUri = new HunterUri(this);
                projectInfo.strategy.RecordFirstWord();

                mHunterConsole.WriteMessage("下载线程总数：" + hunterThreads.Length);
                mHunterConsole.WriteMessage("读取配置完毕。");
                mHunterConsole.WriteMessage("正在运行任务...");
            }
            catch (Exception e)
            {
                mHunterConsole.WriteException(e);
            }
        }


        public void Start()
        {
            DownloadCancelled = false;
            try
            {
                if (projectInfo.mode == ProjectInfo.HunterMode.network)
                    projectInfo.CreateIPC();
                //记录辞典文件的MD5码
                string lastMD5 = projectInfo.LoadlastDicMD5();  //读取上次保存的辞典MD5
                projectInfo.SaveDicMD5();   //保存此次的辞典MD5
                if (lastMD5 != HunterUtilities.GetMD5Hash(projectInfo.dictionary))
                {
                    //如果两次MD5不一致，说明辞典文件已经改变。询问是否重置辞典
                    if (projectInfo.strategy.CurrentSearchProgress != 0 ||
                        projectInfo.strategy.CurrentKeywordProgress != 0)
                    {
                        DialogResult dr =
                        MessageBox.Show("您的辞典已经更新。要将搜索进度置零，重新开始搜索吗？", "Hunter 3", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (dr == DialogResult.Yes)
                        {
                            projectInfo.strategy.CurrentKeywordProgress = 0;
                            projectInfo.strategy.CurrentSearchProgress = 0;
                            projectInfo.strategy.RefreshProgress(0, 0);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                mHunterConsole.WriteException(e);
            }
// 
//             //以Mode来判断是否连接服务器
            try
            {
                if (projectInfo.CurrentMode == ProjectInfo.HunterMode.network)
                {
                    //尝试连接一次数据库
                    try
                    {
                        projectInfo.DatabaseHelper.database.DbOpen();
                        projectInfo.DatabaseHelper.database.DbClose();
                    }
                    catch
                    {
                        MessageBox.Show("连接数据库失败，本任务改为本地模式。", "Hunter 3", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        projectInfo.CurrentMode = ProjectInfo.HunterMode.local;
                    }

                }

                isHuntingUri = true;
                isDownloadingUris = true;

                mHunterConsole.outputStartInformation(DateTime.Now, projectInfo.timeout.ToString());

                for (int i = 0; i < hunterThreads.Length; i++)
                {
                    if (projectInfo.CurrentMode != ProjectInfo.HunterMode.local)
                    {
                        hunterThreads[i].databaseHelper = new HunterDatabaseHelper(projectInfo);
                        //hunterThreads[i].databaseHelper.connect();
                    }
                    hunterThreads[i].downloadThread.Start(hunterThreads[i]);
                }

                if (mHunterConfig.UseProxy)
                {
                    for (int i = 0; i < ProxyGetThreads.Length; i++)
                    {
                        ProxyGetThreads[i].Start();
                    }
                }

                thHuntUris.Start();

            }
            catch (Exception e)
            {
                mHunterConsole.WriteException(e);
            }
        }

        public void CloseHunter()
        {
            try
            {
                projectInfo.DisconnectIPC();
                HunterDownload.count = 0;
                isHuntingUri = false;
                isDownloadingUris = false;
                DownloadCancelled = true;

                foreach (HunterDownloadThread h in hunterThreads)
                {
                    try
                    {
                        h.databaseHelper.database.DbClose();
                    }
                    catch { }
                }
                if (thHuntUris != null && thHuntUris.IsAlive) thHuntUris.Abort();

                foreach (Thread t in ProxyGetThreads)
                {
                    if (t != null && t.IsAlive) t.Abort();
                }

                uriQueue.Clear();
                projectInfo.strategy.ClearDoingFromDatabase();
            }
            catch { }
            finally
            {
            }
        }

        public void Pause()
        {
            hunterSwitch.Reset();   //导致线程堵塞
            //proxySwitch.Reset();
            isRunning = false;
        }

        public void Resume()
        {
            hunterSwitch.Set();
            //proxySwitch.Set();
            isRunning = true;
        }

        public bool isRunning = true;

        public void GetAvaliableProxies()
        {
            //while (isHuntingUri)
            //{
                //proxySwitch.WaitOne();
                //Thread.Sleep(100);  //休眠，节约CPU资源
                while (AllProxies.Count > 0)
                {
                    HunterProxy p;
                    if (AllProxies.Count > 0)
                    {
                        lock(AllProxies){
                            p = AllProxies.Dequeue();
                        }
                        if (p.isAvaliable(mHunterConfig.PingTimeout))
                        {
                            mHunterConsole.WriteProxy("连接代理" + p.IPAndPort + "成功。");
                            AvailableProxies.Add(p);
                        }
                        else
                        {
                            mHunterConsole.WriteProxy("连接代理" + p.IPAndPort + "失败。");
                        }
                    }
                    else
                        break;
                }
            //}
        }
    }


}
