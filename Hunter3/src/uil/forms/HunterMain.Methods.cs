using System;
using System.IO;
using System.Net;
using System.Windows.Forms;
using Hunter3.Strategies;
using System.Threading;
using System.Text;
namespace Hunter3
{
    partial class HunterMain
    {
        public WebBrowser IEBrowser = new WebBrowser();
        public void HideForm()
        {
            if (mHunterConfig.notifyOption.FirstMinimize)
            {
                HunterNotify.ShowBalloonTip(5, "Hunter 3", "Hunter 3 已经最小化到通知区域，请单击它还原窗口。", ToolTipIcon.Info);
            }
            Hide();
            mHunterConfig.notifyOption.FirstMinimize = false;
        }

        public void ShowForm()
        {
            Show();
            WindowState = FormWindowState.Normal;
            this.Activate();
        }

        private void NotifyClick(object sender, MouseEventArgs e)
        {

        }

        public void ShowProxyList()
        {
            String Filter = "Hunter 3 代理站点列表(*.hip)|*.hip";
            new HunterEditor(mHunterConsole, mHunterConfig, "proxy.hip", false, Filter, false, null, null, HunterRichTextBox.TextType.None).Show();
        }


        private void testProxy(object sender, EventArgs e)
        {

            WebProxy wp = new WebProxy("58.20.223.230:3128", true);
            WebRequest req = WebRequest.Create("http://www.9daili.com/");
            //req.Proxy = wp;
            WebResponse s = req.GetResponse();
            Stream ms = s.GetResponseStream();
            StreamReader sr = new StreamReader(ms, Encoding.UTF8);
            String sd = sr.ReadToEnd();
            WriteMessage(sd);
        }


        public void EditStrategy()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            String Filter = "Hunter 3 策略(*.h3s)|*.h3s";
            ofd.Filter = Filter;
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    new HunterEditor(mHunterConsole, mHunterConfig, ofd.FileName, false, Filter, false, typeof(StrategyData), null, HunterRichTextBox.TextType.Xml).Show();
                }
                catch (Exception ex)
                {
                    WriteException(ex);
                }
            }
            else
            {
                return;
            }
        }


        public void New()
        {
            new HunterEditor(mHunterConsole, mHunterConfig, null, false, "Hunter 3 项目(*.h3)|*.h3", true, typeof(ProjectInfo), new ProjectInfo(), HunterRichTextBox.TextType.Xml).Show();
        }

        public void Modify()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            String Filter = "Hunter 3 项目(*.h3)|*.h3";
            ofd.Filter = Filter;
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    new HunterEditor(mHunterConsole, mHunterConfig, ofd.FileName, false, Filter, true, typeof(ProjectInfo), ProjectInfo.LoadProject(mHunterConsole, ofd.FileName, mHunterConfig.CurrentStrategyFile, false), HunterRichTextBox.TextType.Xml).Show();
                }
                catch (Exception ex)
                {
                    WriteException(ex);
                    WriteException(new Exception("请尝试新建一个Hunter 3项目。"));
                }
            }
            else
            {
                return;
            }
        }


        public void ShowAbandonLinkList()
        {
            new HunterLog(hunter, "抛弃链接数").ShowContent(AbandonLinkList);
        }

        public void ShowAbandonFileList()
        {
            new HunterLog(hunter, "抛弃文件数").ShowContent(AbandonFileList);
        }

        public void ShowDownloadedList()
        {
            new HunterLog(hunter, "下载数").ShowContent(DownloadedList);
        }

        public void ShowExceptions()
        {
            new HunterLog(hunter, "异常数").ShowContent(ExceptionList);
        }


        public void Cls()
        {
            hunterTextBox.Clear();
        }

        public void Pause()
        {
            if (hunter != null)
            {
                hunter.Pause();
                Paused = true;
                WriteMessage("暂停任务。");
                Text = "Hunter - 暂停";
            }
        }

        public void Resume()
        {
            if (hunter != null)
            {
                hunter.Resume();
                Paused = false;
                WriteMessage("继续任务。");
                Text = "Hunter - 正在运行";
            }
        }

        public void Toogle()
        {
            if (!Paused)
            {
                miPause.Text = "继续(&R)";
                Pause();
            }
            else
            {
                miPause.Text = "暂停(&P)";
                Resume();
            }
        }


        private void LoadPluginList()
        {
            try
            {
                bool hasAddedSeparator = false;
                if (!Directory.Exists(mHunterConfig.PluginFolder)) Directory.CreateDirectory(HunterUtilities.AbsolutePath(mHunterConfig.PluginFolder));
                DirectoryInfo d = new DirectoryInfo(mHunterConfig.PluginFolder);
                FileInfo[] files = d.GetFiles();

                foreach (ToolStripMenuItem t in tsPlugins)
                {
                    miPluginList.DropDownItems.Remove(t);
                }

                foreach (FileInfo f in files)
                {
                    try
                    {
                        if (f.Extension != ".dll") continue;
                        HunterPlugin hp = new HunterPlugin();
                        hp.LoadDll(f.FullName);
                        String title = hp.GetTitle();

                        if (!hasAddedSeparator)
                        {
                            ToolStripSeparator s = new ToolStripSeparator();
                            miPluginList.DropDownItems.Add(s);
                            tsPlugins.Add(s);
                            hasAddedSeparator = true;
                        }

                        ToolStripMenuItem mi = new ToolStripMenuItem();
                        mi.Tag = hp;
                        mi.CheckOnClick = false;
                        mi.Click += new EventHandler((object sender, EventArgs e) =>
                        {
                            try
                            {
                                new Thread(new ThreadStart(() =>
                                {
                                    HunterPlugin _hp = mi.Tag as HunterPlugin;
                                    object[] parameters = HunterPlugin.CreateArguments(_hp.GetParametersString());
                                    if (_hp != null)
                                    {
                                        _hp.Invoke(parameters);
                                    }
                                })).Start();
                            }
                            catch { }
                        });
                        mi.Text = title;
                        miPluginList.DropDownItems.Add(mi);
                        tsPlugins.Add(mi);
                    }
                    catch(Exception ex)
                    {
                        WriteException(ex);
                    }
                }

            }
            catch (DirectoryNotFoundException ex)
            {
                mHunterConsole.WriteException(ex);
                mHunterConsole.WriteException(new Exception("请手动更改config.xml中的策略文件路径。"));
            }
            catch (Exception ex)
            {
                mHunterConsole.WriteException(ex);
            }
        }

        private void OpenProject(string pjpath)
        {
            ProjectInfo pj = new ProjectInfo(mHunterConsole, pjpath, mHunterConfig.CurrentStrategyFile);
            Downloads = 0;
            Exceptions = 0;
            AbandonFiles = 0;
            AbandonLinks = 0;

            lbDownloads.Text = strDownloads + Downloads;
            lbExceptions.Text = strExceptions + Exceptions;
            lbAbandonFiles.Text = strAbandonFiles + AbandonFiles;
            lbAbandonLinks.Text = strAbandonLinks + AbandonLinks;

            miPause.Enabled = true;
            hunter = new Hunter(mHunterConsole, mHunterConfig, pj, this);
            if (hunter.Error) return;
            hunter.Start();
            Text = "Hunter 3 - 正在运行";
        }

        private void WriteMessage(String str)
        {
            try
            {
                String[] sep = { Environment.NewLine };
                foreach (String s in str.Split(sep, StringSplitOptions.None))
                {
                    hunterTextBox.WriteLine("[消息 " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "] " + s, HunterConfig.ColorMessage);
                }
            }
            catch (Exception ex) { WriteException(ex); }
        }

        private void WriteException(Exception ex)
        {
            try
            {
                Exceptions++;
                lbExceptions.Text = strExceptions + Exceptions;

                String exMsg = "[异常 " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "] " + ex.Message;
                String exMsg2 = "[异常 " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "] " + ex.StackTrace;
                ExceptionList.Add(exMsg);
                if (ExceptionList.Count > 100)
                {
                    ExceptionList.RemoveAt(0);
                }
                if (cbViewExceptions.Checked)
                {
                    hunterTextBox.WriteLine(exMsg, HunterConfig.ColorException);
#if DEBUG
                    hunterTextBox.WriteLine(exMsg2, HunterConfig.ColorException);
#endif
                }
            }
            catch { }
        }

        private void WriteDetails(String str)
        {
            try
            {
                String[] sep = { Environment.NewLine };
                foreach (String s in str.Split(sep, StringSplitOptions.None))
                {
                    if (cbViewDetails.Checked)
                        hunterTextBox.WriteLine("[详细信息 " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "] " + s, HunterConfig.ColorDetails);
                }
            }
            catch (Exception ex) { WriteException(ex); }

        }

        private void WriteProxy(String str)
        {
            try
            {
                String[] sep = { Environment.NewLine };
                foreach (String s in str.Split(sep, StringSplitOptions.None))
                {
                    if (cbViewProxy.Checked)
                        hunterTextBox.WriteLine("[代理 " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "] " + s, HunterConfig.ColorProxy);
                }
            }
            catch (Exception ex) { WriteException(ex); }
        }

        private void WriteDownload(String str)
        {
            try
            {
                String[] sep = { Environment.NewLine };
                foreach (String s in str.Split(sep, StringSplitOptions.None))
                {
                    if (cbViewDownloads.Checked)
                        hunterTextBox.WriteLine("[下载 " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "] " + s, HunterConfig.ColorDownload);
                }
            }
            catch (Exception ex) { WriteException(ex); }
        }

        void ReportAbandonFile(DownloadInfo d, string reason)
        {
            try
            {
                AbandonFiles++;
                lbAbandonFiles.Text = strAbandonFiles + AbandonFiles;
                AbandonFile a = new AbandonFile();
                a.Info = d;
                a.Reason = reason;
                AbandonFileList.Add(a);
                if (AbandonFileList.Count > 100)
                {
                    AbandonFileList.RemoveAt(0);
                }
                try
                {
                    a.Keyword = hunter.projectInfo.strategy.GetKeyword(d.Keyword);
                }
                catch
                {
                    a.Keyword = "[获取出错]";
                }
                WriteAbandonFile(a);
            }
            catch (Exception ex) { WriteException(ex); }
        }

        void WriteAbandonFile(AbandonFile a)
        {
            try
            {
                if (cbViewAbandonFiles.Checked)
                {
                    hunterTextBox.WriteLine("[抛弃文件 " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "] " + a.Info.Url + "，关键字：\"" + hunter.projectInfo.strategy.GetKeyword(a.Info.Keyword) + "\"，原因：" + a.Reason
                    , HunterConfig.ColorAbandonFile);
                }
            }
            catch (Exception ex) { WriteException(ex); }
        }


        void ReportAbandonURI(UriResource u, string reason)
        {
            try
            {
                AbandonLinks++;
                lbAbandonLinks.Text = strAbandonLinks + AbandonLinks;
                AbandonUri a = new AbandonUri();
                a.Info = u;
                a.Reason = reason;
                AbandonLinkList.Add(a);
                if (AbandonLinkList.Count > 100)
                {
                    AbandonLinkList.RemoveAt(0);
                }

                try
                {
                    a.Keyword = hunter.projectInfo.strategy.GetKeyword(u.Keyword);
                }
                catch
                {
                    a.Keyword = "[获取出错]";
                }
                WriteAbandonUri(a);
            }
            catch (Exception ex) { WriteException(ex); }
        }

        void ReportDownloadedInfo(DownloadInfo d)
        {
            try
            {
                Downloads++;
                lbDownloads.Text = strDownloads + Downloads;
                DownloadedList.Add(d);

                if (DownloadedList.Count > 100)
                {
                    DownloadedList.RemoveAt(0);
                }
            }
            catch (Exception ex) { WriteException(ex); }
        }

        void WriteAbandonUri(AbandonUri a)
        {
            try
            {
                if (cbViewAbandonLinks.Checked)
                {
                    try
                    {
                        hunterTextBox.WriteLine("[抛弃链接 " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "] " + a.Info.Url + "，标题：\"" + a.Info.Text + "\"，关键字：\"" + hunter.projectInfo.strategy.GetKeyword(a.Info.Keyword) + "\"，原因：" + a.Reason
                        , HunterConfig.ColorAbandonLink);
                    }
                    catch
                    {
                        hunterTextBox.WriteLine("[抛弃链接 " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "] " + a.Info.Url + "，标题：\"" + a.Info.Text + "\"，关键字：\"[获取失败]\"，原因：" + a.Reason
                        , HunterConfig.ColorAbandonLink);
                    }
                }
            }
            catch (Exception ex) { WriteException(ex); }
        }

        void Done()
        {
            String[] sep = { Environment.NewLine };
            hunterTextBox.WriteLine("[消息 " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "] " + "完成", HunterConfig.ColorMessage);
            Text = "Hunter 3 - 已完成";
        }

        new public void Close()
        {
            base.Close();
        }

        public void Open()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Hunter 3 项目(*.h3)|*.h3";
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                OpenProject(ofd.FileName);
            }
            else
            {
                return;
            }
            miClose.Visible = true;
            miOpen.Visible = false;
            foreach (var m in tsStrategies)
            {
                m.Enabled = false;
            }
        }

        public void CloseProject()
        {

            if (hunter != null)
                hunter.CloseHunter();

            if (Paused)
            {
                Toogle();
            }

            hunter = null;
            miPause.Enabled = false;
            miOpen.Visible = true;
            miClose.Visible = false;

            foreach (var m in tsStrategies)
            {
                m.Enabled = true;
            }

            DownloadedList.Clear();
            AbandonFileList.Clear();
            AbandonLinkList.Clear();
            ExceptionList.Clear();
            WriteMessage("任务已关闭。");
            Text = "Hunter 3";
        }


        public StrategyData LoadStrategy(String strategyPath)
        {
            StrategyData result = null;
            try
            {
                strategyPath = HunterUtilities.AbsolutePath(strategyPath);
                result = StrategyReader.LoadStrategy(strategyPath);
                WriteMessage(result.GetStrategyInformation());
                mHunterConfig.CurrentStrategyFile = strategyPath;
            }
            catch
            {
                throw;
            }
            return result;
        }

        private void LoadStrategyList()
        {
            try
            {
                bool hasAddedSeparator = false;
                int addedCount = 0;
                DirectoryInfo d = new DirectoryInfo(HunterUtilities.AbsolutePath(mHunterConfig.StrategyFolder));
                FileInfo[] files = d.GetFiles();

                foreach (ToolStripMenuItem t in tsStrategies)
                {
                    miStrategyList.DropDownItems.Remove(t);
                }

                foreach (FileInfo f in files)
                {
                    try
                    {
                        StrategyData data = StrategyReader.LoadStrategy(f.FullName);
                        //出现在列表的条件：<Title>不为空
                        if (data.information.Title != "")
                        {
                            if (!hasAddedSeparator)
                            {
                                ToolStripSeparator s = new ToolStripSeparator();
                                miStrategyList.DropDownItems.Add(s);
                                tsStrategies.Add(s);
                                hasAddedSeparator = true;
                            }

                            addedCount++;
                            ToolStripMenuItem mi = new ToolStripMenuItem();
                            mi.Tag = f.FullName;
                            mi.CheckOnClick = false;
                            mi.Click += new EventHandler(mi_Click);
                            mi.Text = data.information.Title;
                            miStrategyList.DropDownItems.Add(mi);
                            tsStrategies.Add(mi);
                            addedCount++;

                            if (String.Compare(f.FullName, new FileInfo(HunterUtilities.AbsolutePath(mHunterConfig.CurrentStrategyFile)).FullName, true) == 0)
                            {
                                mi.Checked = true;
                            }
                            else
                            {
                                mi.Checked = false;
                            }
                        }
                    }
                    catch
                    {
                    }
                }

            }
            catch (DirectoryNotFoundException ex)
            {
                mHunterConsole.WriteException(ex);
                mHunterConsole.WriteException(new Exception("请手动更改config.xml中的策略文件路径。"));
            }
            catch (Exception ex)
            {
                mHunterConsole.WriteException(ex);
            }
        }

        void WriteHTML(string msg)
        {
            if (cbViewHTML.Checked)
                hunterTextBox.WriteLine("[HTML " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "] " + msg, HunterConfig.ColorHTML);
        }
    }

    
}
