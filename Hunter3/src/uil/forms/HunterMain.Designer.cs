namespace Hunter3
{
    partial class HunterMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing && (components != null))
                {
                    components.Dispose();
                }
                base.Dispose(disposing);
            }
            catch { }
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HunterMain));
            this.HunterNotify = new System.Windows.Forms.NotifyIcon(this.components);
            this.hunterFormTitle = new Hunter3.HunterFormTitle();
            this.msMenu = new System.Windows.Forms.MenuStrip();
            this.miQuest = new System.Windows.Forms.ToolStripMenuItem();
            this.miQuick = new System.Windows.Forms.ToolStripMenuItem();
            this.miNew = new System.Windows.Forms.ToolStripMenuItem();
            this.miOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.miClose = new System.Windows.Forms.ToolStripMenuItem();
            this.miModify = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.miQuit = new System.Windows.Forms.ToolStripMenuItem();
            this.miStrategyList = new System.Windows.Forms.ToolStripMenuItem();
            this.miNewStrategy = new System.Windows.Forms.ToolStripMenuItem();
            this.miEditStrategy = new System.Windows.Forms.ToolStripMenuItem();
            this.miConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.miProxyList = new System.Windows.Forms.ToolStripMenuItem();
            this.miFilter = new System.Windows.Forms.ToolStripMenuItem();
            this.miToogleProxy = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.内核CToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miCoreAuto = new System.Windows.Forms.ToolStripMenuItem();
            this.miCoreIE = new System.Windows.Forms.ToolStripMenuItem();
            this.miCoreHunter = new System.Windows.Forms.ToolStripMenuItem();
            this.tsCommand = new System.Windows.Forms.ToolStripMenuItem();
            this.miCls = new System.Windows.Forms.ToolStripMenuItem();
            this.miDownloaded = new System.Windows.Forms.ToolStripMenuItem();
            this.miAbandonLinks = new System.Windows.Forms.ToolStripMenuItem();
            this.miAbandonFiles = new System.Windows.Forms.ToolStripMenuItem();
            this.miExceptions = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.miPause = new System.Windows.Forms.ToolStripMenuItem();
            this.miPluginList = new System.Windows.Forms.ToolStripMenuItem();
            this.miHunter3Editor = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助HToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.flowMain = new System.Windows.Forms.FlowLayoutPanel();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.HContainer = new Hunter3.HunterSplitContainer();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.cbViewDownloads = new System.Windows.Forms.CheckBox();
            this.cbViewAbandonLinks = new System.Windows.Forms.CheckBox();
            this.cbViewAbandonFiles = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbViewDetails = new System.Windows.Forms.CheckBox();
            this.cbViewProxy = new System.Windows.Forms.CheckBox();
            this.cbViewHTML = new System.Windows.Forms.CheckBox();
            this.cbViewExceptions = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lbDownloads = new System.Windows.Forms.Label();
            this.lbAbandonLinks = new System.Windows.Forms.Label();
            this.lbAbandonFiles = new System.Windows.Forms.Label();
            this.lbExceptions = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lbMode = new System.Windows.Forms.Label();
            this.msMenu.SuspendLayout();
            this.flowMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HContainer)).BeginInit();
            this.HContainer.Panel2.SuspendLayout();
            this.HContainer.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // HunterNotify
            // 
            this.HunterNotify.Icon = ((System.Drawing.Icon)(resources.GetObject("HunterNotify.Icon")));
            this.HunterNotify.Text = "Hunter 3";
            this.HunterNotify.Visible = true;
            // 
            // hunterFormTitle
            // 
            this.hunterFormTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.hunterFormTitle.Location = new System.Drawing.Point(0, 0);
            this.hunterFormTitle.Name = "hunterFormTitle";
            this.hunterFormTitle.Owner = null;
            this.hunterFormTitle.Size = new System.Drawing.Size(1042, 32);
            this.hunterFormTitle.TabIndex = 19;
            // 
            // msMenu
            // 
            this.msMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miQuest,
            this.miStrategyList,
            this.miConfig,
            this.tsCommand,
            this.miPluginList,
            this.帮助HToolStripMenuItem});
            this.msMenu.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.msMenu.Location = new System.Drawing.Point(0, 32);
            this.msMenu.Name = "msMenu";
            this.msMenu.Size = new System.Drawing.Size(1042, 25);
            this.msMenu.TabIndex = 20;
            // 
            // miQuest
            // 
            this.miQuest.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miQuick,
            this.miNew,
            this.miOpen,
            this.miClose,
            this.miModify,
            this.toolStripMenuItem2,
            this.miQuit});
            this.miQuest.Name = "miQuest";
            this.miQuest.Size = new System.Drawing.Size(59, 21);
            this.miQuest.Text = "任务(&S)";
            // 
            // miQuick
            // 
            this.miQuick.Name = "miQuick";
            this.miQuick.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.miQuick.Size = new System.Drawing.Size(223, 22);
            this.miQuick.Text = "快速新建任务(&I)";
            // 
            // miNew
            // 
            this.miNew.Name = "miNew";
            this.miNew.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.N)));
            this.miNew.Size = new System.Drawing.Size(223, 22);
            this.miNew.Text = "新建任务(&N)";
            // 
            // miOpen
            // 
            this.miOpen.ForeColor = System.Drawing.SystemColors.ControlText;
            this.miOpen.Name = "miOpen";
            this.miOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.miOpen.Size = new System.Drawing.Size(223, 22);
            this.miOpen.Text = "打开任务(&O)";
            // 
            // miClose
            // 
            this.miClose.Name = "miClose";
            this.miClose.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
            this.miClose.Size = new System.Drawing.Size(223, 22);
            this.miClose.Text = "关闭项目(&C)";
            this.miClose.Visible = false;
            // 
            // miModify
            // 
            this.miModify.Name = "miModify";
            this.miModify.Size = new System.Drawing.Size(223, 22);
            this.miModify.Text = "修改任务(&D)";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(220, 6);
            // 
            // miQuit
            // 
            this.miQuit.Name = "miQuit";
            this.miQuit.Size = new System.Drawing.Size(223, 22);
            this.miQuit.Text = "退出(&Q)";
            // 
            // miStrategyList
            // 
            this.miStrategyList.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miNewStrategy,
            this.miEditStrategy});
            this.miStrategyList.Name = "miStrategyList";
            this.miStrategyList.Size = new System.Drawing.Size(60, 21);
            this.miStrategyList.Text = "策略(&R)";
            // 
            // miNewStrategy
            // 
            this.miNewStrategy.Name = "miNewStrategy";
            this.miNewStrategy.Size = new System.Drawing.Size(139, 22);
            this.miNewStrategy.Text = "新建策略(&S)";
            // 
            // miEditStrategy
            // 
            this.miEditStrategy.Name = "miEditStrategy";
            this.miEditStrategy.Size = new System.Drawing.Size(139, 22);
            this.miEditStrategy.Text = "编辑策略(&E)";
            // 
            // miConfig
            // 
            this.miConfig.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miProxyList,
            this.miFilter,
            this.miToogleProxy,
            this.toolStripSeparator2,
            this.内核CToolStripMenuItem});
            this.miConfig.Name = "miConfig";
            this.miConfig.Size = new System.Drawing.Size(58, 21);
            this.miConfig.Text = "设置(&F)";
            // 
            // miProxyList
            // 
            this.miProxyList.Name = "miProxyList";
            this.miProxyList.Size = new System.Drawing.Size(207, 22);
            this.miProxyList.Text = "管理代理站点(&S)";
            // 
            // miFilter
            // 
            this.miFilter.Name = "miFilter";
            this.miFilter.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.miFilter.Size = new System.Drawing.Size(207, 22);
            this.miFilter.Text = "代理站点筛选(&F)";
            // 
            // miToogleProxy
            // 
            this.miToogleProxy.CheckOnClick = true;
            this.miToogleProxy.Name = "miToogleProxy";
            this.miToogleProxy.Size = new System.Drawing.Size(207, 22);
            this.miToogleProxy.Text = "启用代理进行下载(&R)";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(204, 6);
            // 
            // 内核CToolStripMenuItem
            // 
            this.内核CToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miCoreAuto,
            this.miCoreIE,
            this.miCoreHunter});
            this.内核CToolStripMenuItem.Name = "内核CToolStripMenuItem";
            this.内核CToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.内核CToolStripMenuItem.Text = "内核(&C)";
            // 
            // miCoreAuto
            // 
            this.miCoreAuto.Name = "miCoreAuto";
            this.miCoreAuto.Size = new System.Drawing.Size(209, 22);
            this.miCoreAuto.Text = "自动(&A)";
            this.miCoreAuto.Click += new System.EventHandler(this.miCoreInherit_Click);
            // 
            // miCoreIE
            // 
            this.miCoreIE.Name = "miCoreIE";
            this.miCoreIE.Size = new System.Drawing.Size(209, 22);
            this.miCoreIE.Text = "强制使用IE内核(&E)";
            this.miCoreIE.Click += new System.EventHandler(this.miCoreIE_Click);
            // 
            // miCoreHunter
            // 
            this.miCoreHunter.Name = "miCoreHunter";
            this.miCoreHunter.Size = new System.Drawing.Size(209, 22);
            this.miCoreHunter.Text = "强制使用Hunter3内核(&T)";
            this.miCoreHunter.Click += new System.EventHandler(this.miCoreHunter_Click);
            // 
            // tsCommand
            // 
            this.tsCommand.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miCls,
            this.miDownloaded,
            this.miAbandonLinks,
            this.miAbandonFiles,
            this.miExceptions,
            this.toolStripSeparator1,
            this.miPause});
            this.tsCommand.Name = "tsCommand";
            this.tsCommand.Size = new System.Drawing.Size(60, 21);
            this.tsCommand.Text = "命令(&C)";
            // 
            // miCls
            // 
            this.miCls.Name = "miCls";
            this.miCls.Size = new System.Drawing.Size(181, 22);
            this.miCls.Text = "清空内容";
            // 
            // miDownloaded
            // 
            this.miDownloaded.Name = "miDownloaded";
            this.miDownloaded.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.miDownloaded.Size = new System.Drawing.Size(181, 22);
            this.miDownloaded.Text = "显示已下载项目";
            // 
            // miAbandonLinks
            // 
            this.miAbandonLinks.Name = "miAbandonLinks";
            this.miAbandonLinks.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.miAbandonLinks.Size = new System.Drawing.Size(181, 22);
            this.miAbandonLinks.Text = "显示已抛弃链接";
            // 
            // miAbandonFiles
            // 
            this.miAbandonFiles.Name = "miAbandonFiles";
            this.miAbandonFiles.ShortcutKeys = System.Windows.Forms.Keys.F4;
            this.miAbandonFiles.Size = new System.Drawing.Size(181, 22);
            this.miAbandonFiles.Text = "显示已抛弃文件";
            // 
            // miExceptions
            // 
            this.miExceptions.Name = "miExceptions";
            this.miExceptions.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.miExceptions.Size = new System.Drawing.Size(181, 22);
            this.miExceptions.Text = "显示异常";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(178, 6);
            // 
            // miPause
            // 
            this.miPause.Name = "miPause";
            this.miPause.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.miPause.Size = new System.Drawing.Size(181, 22);
            this.miPause.Text = "暂停(&P)";
            // 
            // miPluginList
            // 
            this.miPluginList.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miHunter3Editor});
            this.miPluginList.Name = "miPluginList";
            this.miPluginList.Size = new System.Drawing.Size(95, 21);
            this.miPluginList.Text = "工具及插件(&T)";
            // 
            // miHunter3Editor
            // 
            this.miHunter3Editor.Name = "miHunter3Editor";
            this.miHunter3Editor.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.miHunter3Editor.Size = new System.Drawing.Size(225, 22);
            this.miHunter3Editor.Text = "Hunter 3 编辑器(&E)";
            // 
            // 帮助HToolStripMenuItem
            // 
            this.帮助HToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miAbout});
            this.帮助HToolStripMenuItem.Name = "帮助HToolStripMenuItem";
            this.帮助HToolStripMenuItem.Size = new System.Drawing.Size(61, 21);
            this.帮助HToolStripMenuItem.Text = "帮助(&H)";
            // 
            // miAbout
            // 
            this.miAbout.Name = "miAbout";
            this.miAbout.Size = new System.Drawing.Size(116, 22);
            this.miAbout.Text = "关于(&A)";
            // 
            // flowMain
            // 
            this.flowMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(40)))), ((int)(((byte)(34)))));
            this.flowMain.Controls.Add(this.picLogo);
            this.flowMain.Controls.Add(this.HContainer);
            this.flowMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowMain.Location = new System.Drawing.Point(0, 57);
            this.flowMain.Name = "flowMain";
            this.flowMain.Size = new System.Drawing.Size(1042, 652);
            this.flowMain.TabIndex = 21;
            // 
            // picLogo
            // 
            this.picLogo.Image = ((System.Drawing.Image)(resources.GetObject("picLogo.Image")));
            this.picLogo.Location = new System.Drawing.Point(3, 3);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(560, 90);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picLogo.TabIndex = 3;
            this.picLogo.TabStop = false;
            // 
            // HContainer
            // 
            this.HContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.HContainer.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.HContainer.IsSplitterFixed = true;
            this.HContainer.Location = new System.Drawing.Point(3, 99);
            this.HContainer.Name = "HContainer";
            // 
            // HContainer.Panel2
            // 
            this.HContainer.Panel2.Controls.Add(this.flowLayoutPanel1);
            this.HContainer.Size = new System.Drawing.Size(1026, 605);
            this.HContainer.SplitterDistance = 850;
            this.HContainer.SplitterWidth = 1;
            this.HContainer.TabIndex = 2;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(88)))), ((int)(((byte)(88)))));
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Controls.Add(this.cbViewDownloads);
            this.flowLayoutPanel1.Controls.Add(this.cbViewAbandonLinks);
            this.flowLayoutPanel1.Controls.Add(this.cbViewAbandonFiles);
            this.flowLayoutPanel1.Controls.Add(this.label3);
            this.flowLayoutPanel1.Controls.Add(this.cbViewDetails);
            this.flowLayoutPanel1.Controls.Add(this.cbViewProxy);
            this.flowLayoutPanel1.Controls.Add(this.cbViewHTML);
            this.flowLayoutPanel1.Controls.Add(this.cbViewExceptions);
            this.flowLayoutPanel1.Controls.Add(this.label2);
            this.flowLayoutPanel1.Controls.Add(this.lbDownloads);
            this.flowLayoutPanel1.Controls.Add(this.lbAbandonLinks);
            this.flowLayoutPanel1.Controls.Add(this.lbAbandonFiles);
            this.flowLayoutPanel1.Controls.Add(this.lbExceptions);
            this.flowLayoutPanel1.Controls.Add(this.label4);
            this.flowLayoutPanel1.Controls.Add(this.lbMode);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(5);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(175, 605);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(8, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "显示内容";
            // 
            // cbViewDownloads
            // 
            this.cbViewDownloads.AutoSize = true;
            this.cbViewDownloads.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbViewDownloads.ForeColor = System.Drawing.Color.White;
            this.cbViewDownloads.Location = new System.Drawing.Point(8, 25);
            this.cbViewDownloads.Name = "cbViewDownloads";
            this.cbViewDownloads.Size = new System.Drawing.Size(75, 21);
            this.cbViewDownloads.TabIndex = 1;
            this.cbViewDownloads.Text = "下载项目";
            this.cbViewDownloads.UseVisualStyleBackColor = true;
            // 
            // cbViewAbandonLinks
            // 
            this.cbViewAbandonLinks.AutoSize = true;
            this.cbViewAbandonLinks.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbViewAbandonLinks.ForeColor = System.Drawing.Color.White;
            this.cbViewAbandonLinks.Location = new System.Drawing.Point(8, 52);
            this.cbViewAbandonLinks.Name = "cbViewAbandonLinks";
            this.cbViewAbandonLinks.Size = new System.Drawing.Size(87, 21);
            this.cbViewAbandonLinks.TabIndex = 1;
            this.cbViewAbandonLinks.Text = "被抛弃链接";
            this.cbViewAbandonLinks.UseVisualStyleBackColor = true;
            // 
            // cbViewAbandonFiles
            // 
            this.cbViewAbandonFiles.AutoSize = true;
            this.cbViewAbandonFiles.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbViewAbandonFiles.ForeColor = System.Drawing.Color.White;
            this.cbViewAbandonFiles.Location = new System.Drawing.Point(8, 79);
            this.cbViewAbandonFiles.Name = "cbViewAbandonFiles";
            this.cbViewAbandonFiles.Size = new System.Drawing.Size(99, 21);
            this.cbViewAbandonFiles.TabIndex = 1;
            this.cbViewAbandonFiles.Text = "被抛弃的文件";
            this.cbViewAbandonFiles.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(8, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 17);
            this.label3.TabIndex = 0;
            this.label3.Text = "调试";
            // 
            // cbViewDetails
            // 
            this.cbViewDetails.AutoSize = true;
            this.cbViewDetails.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbViewDetails.ForeColor = System.Drawing.Color.White;
            this.cbViewDetails.Location = new System.Drawing.Point(8, 123);
            this.cbViewDetails.Name = "cbViewDetails";
            this.cbViewDetails.Size = new System.Drawing.Size(99, 21);
            this.cbViewDetails.TabIndex = 1;
            this.cbViewDetails.Text = "显示详细信息";
            this.cbViewDetails.UseVisualStyleBackColor = true;
            // 
            // cbViewProxy
            // 
            this.cbViewProxy.AutoSize = true;
            this.cbViewProxy.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbViewProxy.ForeColor = System.Drawing.Color.White;
            this.cbViewProxy.Location = new System.Drawing.Point(8, 150);
            this.cbViewProxy.Name = "cbViewProxy";
            this.cbViewProxy.Size = new System.Drawing.Size(51, 21);
            this.cbViewProxy.TabIndex = 2;
            this.cbViewProxy.Text = "代理";
            this.cbViewProxy.UseVisualStyleBackColor = true;
            // 
            // cbViewHTML
            // 
            this.cbViewHTML.AutoSize = true;
            this.cbViewHTML.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbViewHTML.ForeColor = System.Drawing.Color.White;
            this.cbViewHTML.Location = new System.Drawing.Point(8, 177);
            this.cbViewHTML.Name = "cbViewHTML";
            this.cbViewHTML.Size = new System.Drawing.Size(61, 21);
            this.cbViewHTML.TabIndex = 3;
            this.cbViewHTML.Text = "HTML";
            this.cbViewHTML.UseVisualStyleBackColor = true;
            // 
            // cbViewExceptions
            // 
            this.cbViewExceptions.AutoSize = true;
            this.cbViewExceptions.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbViewExceptions.ForeColor = System.Drawing.Color.White;
            this.cbViewExceptions.Location = new System.Drawing.Point(8, 204);
            this.cbViewExceptions.Name = "cbViewExceptions";
            this.cbViewExceptions.Size = new System.Drawing.Size(51, 21);
            this.cbViewExceptions.TabIndex = 1;
            this.cbViewExceptions.Text = "异常";
            this.cbViewExceptions.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(8, 228);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "数量";
            // 
            // lbDownloads
            // 
            this.lbDownloads.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbDownloads.ForeColor = System.Drawing.Color.White;
            this.lbDownloads.Location = new System.Drawing.Point(8, 245);
            this.lbDownloads.Name = "lbDownloads";
            this.lbDownloads.Padding = new System.Windows.Forms.Padding(3);
            this.lbDownloads.Size = new System.Drawing.Size(150, 23);
            this.lbDownloads.TabIndex = 5;
            this.lbDownloads.Text = "下载数(F2)：";
            // 
            // lbAbandonLinks
            // 
            this.lbAbandonLinks.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbAbandonLinks.ForeColor = System.Drawing.Color.White;
            this.lbAbandonLinks.Location = new System.Drawing.Point(8, 268);
            this.lbAbandonLinks.Name = "lbAbandonLinks";
            this.lbAbandonLinks.Padding = new System.Windows.Forms.Padding(3);
            this.lbAbandonLinks.Size = new System.Drawing.Size(150, 23);
            this.lbAbandonLinks.TabIndex = 6;
            this.lbAbandonLinks.Text = "抛弃链接数(F3)：";
            // 
            // lbAbandonFiles
            // 
            this.lbAbandonFiles.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbAbandonFiles.ForeColor = System.Drawing.Color.White;
            this.lbAbandonFiles.Location = new System.Drawing.Point(8, 291);
            this.lbAbandonFiles.Name = "lbAbandonFiles";
            this.lbAbandonFiles.Padding = new System.Windows.Forms.Padding(3);
            this.lbAbandonFiles.Size = new System.Drawing.Size(150, 23);
            this.lbAbandonFiles.TabIndex = 7;
            this.lbAbandonFiles.Text = "抛弃文件数(F4)：";
            // 
            // lbExceptions
            // 
            this.lbExceptions.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbExceptions.ForeColor = System.Drawing.Color.White;
            this.lbExceptions.Location = new System.Drawing.Point(8, 314);
            this.lbExceptions.Name = "lbExceptions";
            this.lbExceptions.Padding = new System.Windows.Forms.Padding(3);
            this.lbExceptions.Size = new System.Drawing.Size(150, 23);
            this.lbExceptions.TabIndex = 8;
            this.lbExceptions.Text = "异常数(F5)：";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(8, 337);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 17);
            this.label4.TabIndex = 4;
            this.label4.Text = "模式";
            // 
            // lbMode
            // 
            this.lbMode.AutoSize = true;
            this.lbMode.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbMode.ForeColor = System.Drawing.Color.White;
            this.lbMode.Location = new System.Drawing.Point(8, 354);
            this.lbMode.Name = "lbMode";
            this.lbMode.Padding = new System.Windows.Forms.Padding(3);
            this.lbMode.Size = new System.Drawing.Size(19, 23);
            this.lbMode.TabIndex = 8;
            this.lbMode.Text = "-";
            // 
            // HunterMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1042, 709);
            this.Controls.Add(this.flowMain);
            this.Controls.Add(this.msMenu);
            this.Controls.Add(this.hunterFormTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "HunterMain";
            this.Text = "Hunter 3";
            this.Load += new System.EventHandler(this.HunterMain_Load);
            this.msMenu.ResumeLayout(false);
            this.msMenu.PerformLayout();
            this.flowMain.ResumeLayout(false);
            this.flowMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.HContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.HContainer)).EndInit();
            this.HContainer.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon HunterNotify;
        private HunterFormTitle hunterFormTitle;
        private System.Windows.Forms.MenuStrip msMenu;
        private System.Windows.Forms.ToolStripMenuItem miQuest;
        private System.Windows.Forms.ToolStripMenuItem miQuick;
        private System.Windows.Forms.ToolStripMenuItem miNew;
        private System.Windows.Forms.ToolStripMenuItem miOpen;
        private System.Windows.Forms.ToolStripMenuItem miClose;
        private System.Windows.Forms.ToolStripMenuItem miModify;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem miQuit;
        private System.Windows.Forms.ToolStripMenuItem miStrategyList;
        private System.Windows.Forms.ToolStripMenuItem miNewStrategy;
        private System.Windows.Forms.ToolStripMenuItem miEditStrategy;
        private System.Windows.Forms.ToolStripMenuItem miConfig;
        private System.Windows.Forms.ToolStripMenuItem miProxyList;
        private System.Windows.Forms.ToolStripMenuItem miFilter;
        private System.Windows.Forms.ToolStripMenuItem miToogleProxy;
        private System.Windows.Forms.ToolStripMenuItem tsCommand;
        private System.Windows.Forms.ToolStripMenuItem miCls;
        private System.Windows.Forms.ToolStripMenuItem miDownloaded;
        private System.Windows.Forms.ToolStripMenuItem miAbandonLinks;
        private System.Windows.Forms.ToolStripMenuItem miAbandonFiles;
        private System.Windows.Forms.ToolStripMenuItem miExceptions;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem miPause;
        private System.Windows.Forms.ToolStripMenuItem miPluginList;
        private System.Windows.Forms.ToolStripMenuItem miHunter3Editor;
        private System.Windows.Forms.ToolStripMenuItem 帮助HToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem miAbout;
        private System.Windows.Forms.FlowLayoutPanel flowMain;
        private System.Windows.Forms.PictureBox picLogo;
        private HunterSplitContainer HContainer;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cbViewDownloads;
        private System.Windows.Forms.CheckBox cbViewAbandonLinks;
        private System.Windows.Forms.CheckBox cbViewAbandonFiles;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox cbViewDetails;
        private System.Windows.Forms.CheckBox cbViewProxy;
        private System.Windows.Forms.CheckBox cbViewHTML;
        private System.Windows.Forms.CheckBox cbViewExceptions;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbDownloads;
        private System.Windows.Forms.Label lbAbandonLinks;
        private System.Windows.Forms.Label lbAbandonFiles;
        private System.Windows.Forms.Label lbExceptions;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbMode;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem 内核CToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem miCoreAuto;
        private System.Windows.Forms.ToolStripMenuItem miCoreIE;
        private System.Windows.Forms.ToolStripMenuItem miCoreHunter;





    }
}

