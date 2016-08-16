using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing;

namespace Hunter3
{
    public partial class HunterMain : HunterForm
    {
        internal HunterConfig mHunterConfig;
        private static Color ColorHunterReady = Color.FromArgb(0, 122, 204);
        private static Color ColorHunterNetworkRunning = Color.FromArgb(76, 174, 76);
        private static Color ColorHunterLocalRunning = Color.FromArgb(202, 81, 0);

        private readonly String AppPath = AppDomain.CurrentDomain.BaseDirectory;
        private List<ToolStripItem> tsStrategies = new List<ToolStripItem>();
        private List<ToolStripItem> tsPlugins = new List<ToolStripItem>();
        private readonly int splitterdistance = 850;
        private HunterRichTextBox hunterTextBox = new HunterRichTextBox();
        private Hunter hunter;
        private HunterConsole mHunterConsole = new HunterConsole();
        private List<String> ExceptionList = new List<String>();
        private List<DownloadInfo> DownloadedList = new List<DownloadInfo>();
        private List<AbandonFile> AbandonFileList = new List<AbandonFile>();
        private List<AbandonUri> AbandonLinkList = new List<AbandonUri>();
        private bool Paused = false;
        private int Downloads = 0; String strDownloads = "下载数(F2):";
        private int AbandonLinks = 0; String strAbandonLinks = "抛弃链接数(F3):";
        private int AbandonFiles = 0; String strAbandonFiles = "抛弃文件数(F4):";
        private int Exceptions = 0; String strExceptions = "异常数(F5):";

        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                hunterFormTitle.Title.Text = value;
                base.Text = value;

                if (hunter != null && hunter.isRunning)
                {
                    if (hunter.projectInfo.mode == ProjectInfo.HunterMode.network)
                    {
                        BorderColor = ColorHunterNetworkRunning;
                    }
                    else if (hunter.projectInfo.mode == ProjectInfo.HunterMode.local)
                    {
                        BorderColor = ColorHunterLocalRunning;
                    }
                }
                else
                {
                    BorderColor = ColorHunterReady;
                }
            }
        }

        private static string StartupPath
        {
            get
            {
                string path = Application.StartupPath;
                return path.EndsWith("\\") ? path : path + "\\";
            }
        }

        public HunterMain(string file_open)
        {
            
            Resize += new EventHandler(HunterMain_Resize);
            InitializeComponent();
            
            //边框
            BorderWidth = 2;
            BorderColor = ColorHunterReady;
            ShowBorder = true;

            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            CheckForIllegalCrossThreadCalls = false;

            //设置主菜单栏，绑定Render
            MainToolStrip = msMenu;

            //控制台输出定向
            mHunterConsole.onWriteMessage += new HunterConsole.dWriteMessage(WriteMessage);
            mHunterConsole.onWriteException += new HunterConsole.dWriteException(WriteException);
            mHunterConsole.onWriteDetails += new HunterConsole.dWriteMessage(WriteDetails);
            mHunterConsole.onWriteDownload += new HunterConsole.dWriteMessage(WriteDownload);
            mHunterConsole.onWriteProxy += new HunterConsole.dWriteMessage(WriteProxy);
            mHunterConsole.onWriteHTML += new HunterConsole.dWriteMessage(WriteHTML);
            mHunterConsole.onDone += new HunterConsole.dEvents(Done);
            mHunterConsole.onReportAbandonDownloadInfo += new HunterConsole.dReportAbandonDownloadInfo(ReportAbandonFile);
            mHunterConsole.onReportAbandonURI += new HunterConsole.dReportAbandonURI(ReportAbandonURI);
            mHunterConsole.onReportDownloadedInfo += new HunterConsole.dReportDownloadInfo(ReportDownloadedInfo);
            mHunterConsole.onModeChange += new HunterConsole.dModeChange(mHunterConsole_onModeChange);

            FormClosing += new FormClosingEventHandler(HunterMain_FormClosing);
            TextChanged += new EventHandler(HunterMain_TextChanged);

            if (file_open != null)
            {
                OpenProject(file_open);
                miClose.Visible = true;
                miOpen.Visible = false;
            }
        }

        const int WM_NCHITTEST = 0x0084;
        const int HTLEFT = 10;
        const int HTRIGHT = 11;
        const int HTTOP = 12;
        const int HTTOPLEFT = 13;
        const int HTTOPRIGHT = 14;
        const int HTBOTTOM = 15;
        const int HTBOTTOMLEFT = 0x10;
        const int HTBOTTOMRIGHT = 17;

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            switch (m.Msg)
            {
                case WM_NCHITTEST:
                    Point vPoint = new Point((int)m.LParam & 0xFFFF,
                        (int)m.LParam >> 16 & 0xFFFF);
                    vPoint = PointToClient(vPoint);
                    if (vPoint.X <= 5)
                        if (vPoint.Y <= 5)
                            m.Result = (IntPtr)HTTOPLEFT;
                        else if (vPoint.Y >= ClientSize.Height - 5)
                            m.Result = (IntPtr)HTBOTTOMLEFT;
                        else m.Result = (IntPtr)HTLEFT;
                    else if (vPoint.X >= ClientSize.Width - 5)
                        if (vPoint.Y <= 5)
                            m.Result = (IntPtr)HTTOPRIGHT;
                        else if (vPoint.Y >= ClientSize.Height - 5)
                            m.Result = (IntPtr)HTBOTTOMRIGHT;
                        else m.Result = (IntPtr)HTRIGHT;
                    else if (vPoint.Y <= 5)
                        m.Result = (IntPtr)HTTOP;
                    else if (vPoint.Y >= ClientSize.Height - 5)
                        m.Result = (IntPtr)HTBOTTOM;
                    break;
            }
        }

        void HunterMain_TextChanged(object sender, EventArgs e)
        {
            StringBuilder notifyText = new StringBuilder();
            notifyText.AppendLine(Text);
            notifyText.AppendLine("当前成功下载：" + Downloads);
            notifyText.AppendLine("当前抛弃链接：" + AbandonLinks);
            notifyText.AppendLine("当前抛弃文件：" + AbandonFiles);
            HunterNotify.Text = notifyText.ToString();
        }

        void mHunterConsole_onModeChange(string changedMode)
        {
            lbMode.Text = changedMode;
            if (hunter != null && hunter.isRunning)
            {
                if (hunter.projectInfo.mode == ProjectInfo.HunterMode.network)
                {
                    BorderColor = ColorHunterNetworkRunning;
                }
                else if (hunter.projectInfo.mode == ProjectInfo.HunterMode.local)
                {
                    BorderColor = ColorHunterLocalRunning;
                }
            }
        }


        void HunterMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (hunter != null)
                hunter.CloseHunter();
            try
            {
                {
                    mHunterConfig.viewOption.HTML = cbViewHTML.Checked;
                    mHunterConfig.viewOption.AbandonFiles = cbViewAbandonFiles.Checked;
                    mHunterConfig.viewOption.AbandonLinks = cbViewAbandonLinks.Checked;
                    mHunterConfig.viewOption.DownloadItems = cbViewDownloads.Checked;
                    mHunterConfig.viewOption.Proxies = cbViewProxy.Checked;
                    mHunterConfig.viewOption.Details = cbViewDetails.Checked;
                    mHunterConfig.viewOption.Exceptions = cbViewExceptions.Checked;
                }
                mHunterConfig.Save();
            }
            catch (Exception ex)
            {
                WriteException(ex);
            }
        }

        private void HunterMain_Load(object sender, EventArgs e)
        {
            Init();
        }

        void HunterMain_Resize(object sender, EventArgs e)
        {
            //if (HContainer.SplitterDistance != splitterdistance) HContainer.SplitterDistance = splitterdistance;
            if (WindowState != FormWindowState.Minimized)
            {
                HContainer.Width = flowMain.Width;
                HContainer.Height = flowMain.Height - HContainer.Top;
            }
            else
            {
                HideForm();
            }
        }


        private void Init()
        {
            hunterFormTitle.Owner = this;
            HunterNotify.MouseDoubleClick += NotifyDbClick;
            HunterNotify.MouseMove += NotifyMove;

            //绑定事件
            this.miQuick.Click += new System.EventHandler(this.miQuick_Click);
            this.miNew.Click += new System.EventHandler(this.miNew_Click);
            this.miOpen.Click += new System.EventHandler(this.miOpen_Click);
            this.miClose.Click += new System.EventHandler(this.miClose_Click);
            this.miModify.Click += new System.EventHandler(this.miModify_Click);
            this.miQuit.Click += new System.EventHandler(this.miQuit_Click);
            this.miNewStrategy.Click += new System.EventHandler(this.miNewStrategy_Click);
            this.miEditStrategy.Click += new System.EventHandler(this.miEditStrategy_Click);
            this.miConfig.Size = new System.Drawing.Size(60, 21);
            this.miProxyList.Click += new System.EventHandler(this.miProxyList_Click);
            this.miFilter.Click += new System.EventHandler(this.miFilter_Click);
            this.miToogleProxy.Click += new System.EventHandler(this.miToogleProxy_Click);
            this.miCls.Click += new System.EventHandler(this.miCls_Click);
            this.miDownloaded.Click += new System.EventHandler(this.miDownloaded_Click);
            this.miAbandonLinks.Click += new System.EventHandler(this.miAbandonLinks_Click);
            this.miAbandonFiles.Size = new System.Drawing.Size(181, 22);
            this.miAbandonFiles.Text = "显示已抛弃文件";
            this.miAbandonFiles.Click += new System.EventHandler(this.miAbandonFiles_Click);
            this.miExceptions.Click += new System.EventHandler(this.miExceptions_Click);
            this.miPause.Click += new System.EventHandler(this.miPause_Click);
            this.miHunter3Editor.Click += new System.EventHandler(this.miOpenH3Editor_Click);
            this.miAbout.Click += new System.EventHandler(this.miAbout_Click);

            //---

            cbViewAbandonFiles.ForeColor = HunterConfig.ColorAbandonFile;
            cbViewDetails.ForeColor = HunterConfig.ColorDetails;
            cbViewDownloads.ForeColor = HunterConfig.ColorDownload;
            cbViewExceptions.ForeColor = HunterConfig.ColorException;
            cbViewAbandonLinks.ForeColor = HunterConfig.ColorAbandonLink;
            cbViewProxy.ForeColor = HunterConfig.ColorProxy;
            cbViewHTML.ForeColor = HunterConfig.ColorHTML;

            lbDownloads.ForeColor = HunterConfig.ColorDownload;
            lbExceptions.ForeColor = HunterConfig.ColorException;
            lbAbandonFiles.ForeColor = HunterConfig.ColorAbandonFile;
            lbAbandonLinks.ForeColor = HunterConfig.ColorAbandonLink;

            hunterTextBox.Dock = DockStyle.Fill;
            hunterTextBox.ReadOnly = true;
            hunterTextBox.ScrollBars = RichTextBoxScrollBars.Vertical;
            hunterTextBox.BorderStyle = BorderStyle.None;
            HContainer.Panel1.Controls.Add(hunterTextBox);
            HContainer.SplitterDistance = splitterdistance;


            lbDownloads.Text = strDownloads + Downloads;
            lbExceptions.Text = strExceptions+ Exceptions;
            lbAbandonFiles.Text = strAbandonFiles + AbandonFiles;
            lbAbandonLinks.Text = strAbandonLinks + AbandonLinks;

            mHunterConfig = HunterConfig.Load();
            cbViewAbandonFiles.Checked = mHunterConfig.viewOption.AbandonFiles;
            cbViewAbandonLinks.Checked = mHunterConfig.viewOption.AbandonLinks;
            cbViewDownloads.Checked = mHunterConfig.viewOption.DownloadItems;
            cbViewDetails.Checked = mHunterConfig.viewOption.Details;
            cbViewExceptions.Checked = mHunterConfig.viewOption.Exceptions;
            cbViewProxy.Checked = mHunterConfig.viewOption.Proxies;
            cbViewHTML.Checked = mHunterConfig.viewOption.HTML;
            miToogleProxy.Checked = mHunterConfig.UseProxy;
            miPause.Enabled = false;

            switch (mHunterConfig.HunterCore)
            {
                case HunterConfig.Core.Default :
                    miCoreAuto.Checked = true;
                    break;
                case HunterConfig.Core.IE:
                    miCoreIE.Checked = true;
                    break;
                case HunterConfig.Core.WebRequest:
                    miCoreHunter.Checked = true;
                    break;
            }

            WriteMessage("搜索策略文件：" + mHunterConfig.CurrentStrategyFile);
            try
            {
                LoadStrategy(mHunterConfig.CurrentStrategyFile).GetStrategyInformation();
                WriteMessage("Hunter 准备就绪，可以打开任务进行爬虫下载了！");
            }
            catch (Exception ex)
            {
                WriteMessage("错误：" + ex.Message);
            }

            LoadStrategyList();
            LoadPluginList();

            if (mHunterConfig.notifyOption.FirstBallon)
            {
                HunterNotify.ShowBalloonTip(2, "Hunter 3", "这里是Hunter 3 的通知区域图标，单击它可以隐藏窗口。", ToolTipIcon.Info);
                mHunterConfig.notifyOption.FirstBallon = false;
            }

            flowMain.MouseDown += SaveLocation;
            flowMain.MouseMove += MoveWindow;
            flowMain.MouseLeave += ResetLocOffset;

            picLogo.MouseDown += SaveLocation;
            picLogo.MouseMove += MoveWindow;
            picLogo.MouseLeave += ResetLocOffset;

            msMenu.MouseDown += SaveLocation;
            msMenu.MouseMove += MoveWindow;
            msMenu.MouseLeave += ResetLocOffset;

            hunterFormTitle.AddMouseDown(SaveLocation);
            hunterFormTitle.AddMouseMove(MoveWindow);
            hunterFormTitle.AddMouseLeave(ResetLocOffset);
        }

        void mi_Click(object sender, EventArgs e)
        {
            try
            {
                mHunterConfig.CurrentStrategyFile = ((ToolStripMenuItem)sender).Tag.ToString();
                WriteMessage("更换搜索策略...");
                LoadStrategy(((ToolStripMenuItem)sender).Tag.ToString());
                foreach (ToolStripItem ts in tsStrategies)
                {
                    ToolStripMenuItem tsmi = ts as ToolStripMenuItem;
                    if (tsmi != null)
                    {
                        if (tsmi.Tag.ToString() == new FileInfo(mHunterConfig.CurrentStrategyFile).FullName)
                        {
                            tsmi.Checked = true;
                        }
                        else
                        {
                            tsmi.Checked = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                WriteException(ex);
            }
        }

        private void miQuit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void miOpen_Click(object sender, EventArgs e)
        {
            Open();
        }

        private void miClose_Click(object sender, EventArgs e)
        {
            CloseProject();
        }

        private void miNew_Click(object sender, EventArgs e)
        {
            New();
        }

        private void miModify_Click(object sender, EventArgs e)
        {
            Modify();
        }

        private void miNewStrategy_Click(object sender, EventArgs e)
        {
            NewStrategy();
        }

        public void NewStrategy()
        {
            new HunterEditor(mHunterConsole, mHunterConfig, "Strategy\\model.h3s", true, "Hunter 3 策略(*.h3s)|*.h3s", false, null, null, HunterRichTextBox.TextType.Xml).Show();
        }

        private void miEditStrategy_Click(object sender, EventArgs e)
        {
            EditStrategy();
        }


        private void miPause_Click(object sender, EventArgs e)
        {
            Toogle();
        }

        private void miExceptions_Click(object sender, EventArgs e)
        {
            ShowExceptions();
        }

        private void miAbandonFiles_Click(object sender, EventArgs e)
        {
            ShowAbandonFileList();
        }

        private void miAbandonLinks_Click(object sender, EventArgs e)
        {
            ShowAbandonLinkList();
        }

        private void miDownloaded_Click(object sender, EventArgs e)
        {
            ShowDownloadedList();
        }

        private void miCls_Click(object sender, EventArgs e)
        {
            Cls();
        }

        private void miProxyList_Click(object sender, EventArgs e)
        {
            ShowProxyList();
        }

        private void miToogleProxy_Click(object sender, EventArgs e)
        {
            miToogleProxy.Checked = !mHunterConfig.UseProxy;
            mHunterConfig.UseProxy = !mHunterConfig.UseProxy;
        }

        private void miFilter_Click(object sender, EventArgs e)
        {
            new HunterProxyFilter(mHunterConfig).ShowDialog();
        }

        private void miAbout_Click(object sender, EventArgs e)
        {
            new HunterAbout().ShowDialog();
        }

        private void miOpenH3Editor_Click(object sender, EventArgs e)
        {
            OpenHunter3Editor();
        }

        void OpenHunter3Editor()
        {
            String Filter = "所有格式 (*.*)|*.*";
            new HunterEditor(mHunterConsole, mHunterConfig, null, false, Filter, false, null, null, HunterRichTextBox.TextType.Plain).Show();
        }

        private void miHunter3Editor_Click(object sender, EventArgs e)
        {
            OpenHunter3Editor();
        }

        private void miQuick_Click(object sender, EventArgs e)
        {
            new FormQuickCreate().Show();
        }

        private void NotifyDbClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (Visible == false)
                {
                    ShowForm();
                }
                else
                {
                    HideForm();
                }
            }
        }

        private void NotifyMove(object sender, MouseEventArgs e)
        {
            StringBuilder notifyText = new StringBuilder();
            notifyText.AppendLine(Text);
            notifyText.AppendLine("当前成功下载：" + Downloads);
            notifyText.AppendLine("当前抛弃链接：" + AbandonLinks);
            notifyText.AppendLine("当前抛弃文件：" + AbandonFiles);
            HunterNotify.Text = notifyText.ToString();
        }

        private void miCoreInherit_Click(object sender, EventArgs e)
        {
            mHunterConfig.HunterCore = HunterConfig.Core.Default;
            miCoreAuto.Checked = true;
            miCoreIE.Checked = false;
            miCoreHunter.Checked = false;
        }

        private void miCoreIE_Click(object sender, EventArgs e)
        {
            mHunterConfig.HunterCore = HunterConfig.Core.IE;
            miCoreAuto.Checked = false;
            miCoreIE.Checked = true;
            miCoreHunter.Checked = false;
        }

        private void miCoreHunter_Click(object sender, EventArgs e)
        {
            mHunterConfig.HunterCore = HunterConfig.Core.WebRequest;
            miCoreAuto.Checked = false;
            miCoreIE.Checked = false;
            miCoreHunter.Checked = true;
        }

    }
}
