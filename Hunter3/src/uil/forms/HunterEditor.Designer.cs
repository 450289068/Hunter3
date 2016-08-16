namespace Hunter3
{
    partial class HunterEditor
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
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HunterEditor));
            this.sContainer = new System.Windows.Forms.SplitContainer();
            this.hTextBox = new Hunter3.HunterRichTextBox();
            this.propertyGrid = new System.Windows.Forms.PropertyGrid();
            this.hHTMLGetterBar = new Hunter3.HunterRichTextBoxHTMLGetterBar();
            this.hSearchBar = new Hunter3.HunterRichTextBoxSearchBar();
            this.sStrip = new System.Windows.Forms.FlowLayoutPanel();
            this.tsLabel = new System.Windows.Forms.Label();
            this.msMenu = new System.Windows.Forms.MenuStrip();
            this.文件FToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miNew = new System.Windows.Forms.ToolStripMenuItem();
            this.miOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.miSave = new System.Windows.Forms.ToolStripMenuItem();
            this.miSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.miQuit = new System.Windows.Forms.ToolStripMenuItem();
            this.编辑EToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miUndo = new System.Windows.Forms.ToolStripMenuItem();
            this.miRedo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.miCut = new System.Windows.Forms.ToolStripMenuItem();
            this.miCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.miPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.miAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.miFindToogle = new System.Windows.Forms.ToolStripMenuItem();
            this.助手AToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miHTMLGetter = new System.Windows.Forms.ToolStripMenuItem();
            this.语法SToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miSyntaxPlain = new System.Windows.Forms.ToolStripMenuItem();
            this.miSyntaxXML = new System.Windows.Forms.ToolStripMenuItem();
            this.xMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miToXMLCharacter = new System.Windows.Forms.ToolStripMenuItem();
            this.miToXMLEntity = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.sContainer)).BeginInit();
            this.sContainer.Panel1.SuspendLayout();
            this.sContainer.Panel2.SuspendLayout();
            this.sContainer.SuspendLayout();
            this.sStrip.SuspendLayout();
            this.msMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // sContainer
            // 
            this.sContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.sContainer.Location = new System.Drawing.Point(0, 25);
            this.sContainer.Name = "sContainer";
            // 
            // sContainer.Panel1
            // 
            this.sContainer.Panel1.Controls.Add(this.hTextBox);
            // 
            // sContainer.Panel2
            // 
            this.sContainer.Panel2.Controls.Add(this.propertyGrid);
            this.sContainer.Size = new System.Drawing.Size(1115, 555);
            this.sContainer.SplitterDistance = 891;
            this.sContainer.TabIndex = 26;
            // 
            // hTextBox
            // 
            this.hTextBox.AcceptsTab = true;
            this.hTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(40)))), ((int)(((byte)(34)))));
            this.hTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.hTextBox.ContentType = Hunter3.HunterRichTextBox.TextType.Plain;
            this.hTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hTextBox.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.hTextBox.ForeColor = System.Drawing.Color.White;
            this.hTextBox.HideSelection = false;
            this.hTextBox.ImeMode = System.Windows.Forms.ImeMode.On;
            this.hTextBox.Location = new System.Drawing.Point(0, 0);
            this.hTextBox.Margin = new System.Windows.Forms.Padding(0);
            this.hTextBox.Name = "hTextBox";
            this.hTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.hTextBox.ShortcutsEnabled = false;
            this.hTextBox.Size = new System.Drawing.Size(891, 555);
            this.hTextBox.TabIndex = 2;
            this.hTextBox.Text = "";
            // 
            // propertyGrid
            // 
            this.propertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid.Location = new System.Drawing.Point(0, 0);
            this.propertyGrid.Name = "propertyGrid";
            this.propertyGrid.Size = new System.Drawing.Size(220, 555);
            this.propertyGrid.TabIndex = 0;
            // 
            // hHTMLGetterBar
            // 
            this.hHTMLGetterBar.BindingOutputControl = null;
            this.hHTMLGetterBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.hHTMLGetterBar.Location = new System.Drawing.Point(0, 580);
            this.hHTMLGetterBar.Name = "hHTMLGetterBar";
            this.hHTMLGetterBar.Size = new System.Drawing.Size(1115, 36);
            this.hHTMLGetterBar.TabIndex = 25;
            this.hHTMLGetterBar.Visible = false;
            // 
            // hSearchBar
            // 
            this.hSearchBar.BindingOutputControl = null;
            this.hSearchBar.BindingRichTextBox = null;
            this.hSearchBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.hSearchBar.Location = new System.Drawing.Point(0, 616);
            this.hSearchBar.Name = "hSearchBar";
            this.hSearchBar.Size = new System.Drawing.Size(1115, 36);
            this.hSearchBar.TabIndex = 21;
            this.hSearchBar.Visible = false;
            // 
            // sStrip
            // 
            this.sStrip.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("sStrip.BackgroundImage")));
            this.sStrip.Controls.Add(this.tsLabel);
            this.sStrip.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.sStrip.Location = new System.Drawing.Point(0, 652);
            this.sStrip.Margin = new System.Windows.Forms.Padding(0);
            this.sStrip.Name = "sStrip";
            this.sStrip.Size = new System.Drawing.Size(1115, 22);
            this.sStrip.TabIndex = 20;
            // 
            // tsLabel
            // 
            this.tsLabel.AutoSize = true;
            this.tsLabel.BackColor = System.Drawing.Color.Transparent;
            this.tsLabel.ForeColor = System.Drawing.Color.White;
            this.tsLabel.Location = new System.Drawing.Point(3, 0);
            this.tsLabel.Name = "tsLabel";
            this.tsLabel.Padding = new System.Windows.Forms.Padding(5);
            this.tsLabel.Size = new System.Drawing.Size(39, 22);
            this.tsLabel.TabIndex = 0;
            this.tsLabel.Text = "就绪";
            // 
            // msMenu
            // 
            this.msMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件FToolStripMenuItem,
            this.编辑EToolStripMenuItem,
            this.助手AToolStripMenuItem});
            this.msMenu.Location = new System.Drawing.Point(0, 0);
            this.msMenu.Name = "msMenu";
            this.msMenu.Size = new System.Drawing.Size(1115, 25);
            this.msMenu.TabIndex = 0;
            // 
            // 文件FToolStripMenuItem
            // 
            this.文件FToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miNew,
            this.miOpen,
            this.toolStripSeparator5,
            this.miSave,
            this.miSaveAs,
            this.toolStripSeparator1,
            this.miQuit});
            this.文件FToolStripMenuItem.Name = "文件FToolStripMenuItem";
            this.文件FToolStripMenuItem.Size = new System.Drawing.Size(58, 21);
            this.文件FToolStripMenuItem.Text = "文件(&F)";
            // 
            // miNew
            // 
            this.miNew.Name = "miNew";
            this.miNew.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.miNew.Size = new System.Drawing.Size(206, 22);
            this.miNew.Text = "新建(&N)";
            this.miNew.Click += new System.EventHandler(this.miNew_Click);
            // 
            // miOpen
            // 
            this.miOpen.Name = "miOpen";
            this.miOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.miOpen.Size = new System.Drawing.Size(206, 22);
            this.miOpen.Text = "打开(&O)";
            this.miOpen.Click += new System.EventHandler(this.miOpen_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(203, 6);
            // 
            // miSave
            // 
            this.miSave.Name = "miSave";
            this.miSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.miSave.Size = new System.Drawing.Size(206, 22);
            this.miSave.Text = "保存(&S)";
            this.miSave.Click += new System.EventHandler(this.miSave_Click);
            // 
            // miSaveAs
            // 
            this.miSaveAs.Name = "miSaveAs";
            this.miSaveAs.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.miSaveAs.Size = new System.Drawing.Size(206, 22);
            this.miSaveAs.Text = "另存为(&A)";
            this.miSaveAs.Click += new System.EventHandler(this.miSaveAs_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(203, 6);
            // 
            // miQuit
            // 
            this.miQuit.Name = "miQuit";
            this.miQuit.Size = new System.Drawing.Size(206, 22);
            this.miQuit.Text = "退出(&Q)";
            this.miQuit.Click += new System.EventHandler(this.miQuit_Click);
            // 
            // 编辑EToolStripMenuItem
            // 
            this.编辑EToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miUndo,
            this.miRedo,
            this.toolStripSeparator2,
            this.miCut,
            this.miCopy,
            this.miPaste,
            this.toolStripSeparator3,
            this.miAll,
            this.toolStripSeparator4,
            this.miFindToogle});
            this.编辑EToolStripMenuItem.Name = "编辑EToolStripMenuItem";
            this.编辑EToolStripMenuItem.Size = new System.Drawing.Size(59, 21);
            this.编辑EToolStripMenuItem.Text = "编辑(&E)";
            // 
            // miUndo
            // 
            this.miUndo.Name = "miUndo";
            this.miUndo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.miUndo.Size = new System.Drawing.Size(145, 22);
            this.miUndo.Text = "撤销";
            this.miUndo.Click += new System.EventHandler(this.miUndo_Click);
            // 
            // miRedo
            // 
            this.miRedo.Name = "miRedo";
            this.miRedo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.miRedo.Size = new System.Drawing.Size(145, 22);
            this.miRedo.Text = "重复";
            this.miRedo.Click += new System.EventHandler(this.miRedo_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(142, 6);
            // 
            // miCut
            // 
            this.miCut.Name = "miCut";
            this.miCut.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.miCut.Size = new System.Drawing.Size(145, 22);
            this.miCut.Text = "剪切";
            this.miCut.Click += new System.EventHandler(this.miCut_Click);
            // 
            // miCopy
            // 
            this.miCopy.Name = "miCopy";
            this.miCopy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.miCopy.Size = new System.Drawing.Size(145, 22);
            this.miCopy.Text = "复制";
            this.miCopy.Click += new System.EventHandler(this.miCopy_Click);
            // 
            // miPaste
            // 
            this.miPaste.Name = "miPaste";
            this.miPaste.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.miPaste.Size = new System.Drawing.Size(145, 22);
            this.miPaste.Text = "粘贴";
            this.miPaste.Click += new System.EventHandler(this.miPaste_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(142, 6);
            // 
            // miAll
            // 
            this.miAll.Name = "miAll";
            this.miAll.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.miAll.Size = new System.Drawing.Size(145, 22);
            this.miAll.Text = "全选";
            this.miAll.Click += new System.EventHandler(this.miAll_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(142, 6);
            // 
            // miFindToogle
            // 
            this.miFindToogle.CheckOnClick = true;
            this.miFindToogle.Name = "miFindToogle";
            this.miFindToogle.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.miFindToogle.Size = new System.Drawing.Size(145, 22);
            this.miFindToogle.Text = "查找";
            this.miFindToogle.Click += new System.EventHandler(this.miFindToogle_Click);
            // 
            // 助手AToolStripMenuItem
            // 
            this.助手AToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miHTMLGetter,
            this.语法SToolStripMenuItem,
            this.xMLToolStripMenuItem});
            this.助手AToolStripMenuItem.Name = "助手AToolStripMenuItem";
            this.助手AToolStripMenuItem.Size = new System.Drawing.Size(60, 21);
            this.助手AToolStripMenuItem.Text = "助手(&A)";
            // 
            // miHTMLGetter
            // 
            this.miHTMLGetter.CheckOnClick = true;
            this.miHTMLGetter.Name = "miHTMLGetter";
            this.miHTMLGetter.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.G)));
            this.miHTMLGetter.Size = new System.Drawing.Size(211, 22);
            this.miHTMLGetter.Text = "网络流抓取器(&G)";
            this.miHTMLGetter.Click += new System.EventHandler(this.miHTMLGetter_Click);
            // 
            // 语法SToolStripMenuItem
            // 
            this.语法SToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miSyntaxPlain,
            this.miSyntaxXML});
            this.语法SToolStripMenuItem.Name = "语法SToolStripMenuItem";
            this.语法SToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.语法SToolStripMenuItem.Text = "语法(&S)";
            // 
            // miSyntaxPlain
            // 
            this.miSyntaxPlain.Name = "miSyntaxPlain";
            this.miSyntaxPlain.Size = new System.Drawing.Size(127, 22);
            this.miSyntaxPlain.Text = "纯文本(&T)";
            this.miSyntaxPlain.Click += new System.EventHandler(this.miSyntaxPlain_Click);
            // 
            // miSyntaxXML
            // 
            this.miSyntaxXML.Name = "miSyntaxXML";
            this.miSyntaxXML.Size = new System.Drawing.Size(127, 22);
            this.miSyntaxXML.Text = "XML(&X)";
            this.miSyntaxXML.Click += new System.EventHandler(this.miSyntaxXML_Click);
            // 
            // xMLToolStripMenuItem
            // 
            this.xMLToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miToXMLCharacter,
            this.miToXMLEntity});
            this.xMLToolStripMenuItem.Name = "xMLToolStripMenuItem";
            this.xMLToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.xMLToolStripMenuItem.Text = "XML(&X)";
            this.xMLToolStripMenuItem.Visible = false;
            // 
            // miToXMLCharacter
            // 
            this.miToXMLCharacter.Enabled = false;
            this.miToXMLCharacter.Name = "miToXMLCharacter";
            this.miToXMLCharacter.Size = new System.Drawing.Size(200, 22);
            this.miToXMLCharacter.Text = "实体引用转换为字符(&R)";
            this.miToXMLCharacter.Click += new System.EventHandler(this.miToXMLCharacter_Click);
            // 
            // miToXMLEntity
            // 
            this.miToXMLEntity.Enabled = false;
            this.miToXMLEntity.Name = "miToXMLEntity";
            this.miToXMLEntity.Size = new System.Drawing.Size(200, 22);
            this.miToXMLEntity.Text = "字符转换为实体引用(&E)";
            this.miToXMLEntity.Click += new System.EventHandler(this.miToXMLEntity_Click);
            // 
            // HunterEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1115, 674);
            this.Controls.Add(this.sContainer);
            this.Controls.Add(this.hHTMLGetterBar);
            this.Controls.Add(this.hSearchBar);
            this.Controls.Add(this.sStrip);
            this.Controls.Add(this.msMenu);
            this.MainMenuStrip = this.msMenu;
            this.Name = "HunterEditor";
            this.Text = "Hunter 3 编辑器 ";
            this.sContainer.Panel1.ResumeLayout(false);
            this.sContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.sContainer)).EndInit();
            this.sContainer.ResumeLayout(false);
            this.sStrip.ResumeLayout(false);
            this.sStrip.PerformLayout();
            this.msMenu.ResumeLayout(false);
            this.msMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip msMenu;
        private System.Windows.Forms.ToolStripMenuItem 文件FToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem miSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem miQuit;
        private System.Windows.Forms.ToolStripMenuItem 编辑EToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem miRedo;
        private System.Windows.Forms.ToolStripMenuItem miUndo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem miCopy;
        private System.Windows.Forms.ToolStripMenuItem miPaste;
        private System.Windows.Forms.ToolStripMenuItem miCut;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem miAll;
        private System.Windows.Forms.ToolStripMenuItem miSaveAs;
        private System.Windows.Forms.FlowLayoutPanel sStrip;
        private System.Windows.Forms.Label tsLabel;
        private HunterRichTextBoxSearchBar hSearchBar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem miFindToogle;
        private System.Windows.Forms.ToolStripMenuItem 助手AToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem miHTMLGetter;
        private HunterRichTextBoxHTMLGetterBar hHTMLGetterBar;
        private System.Windows.Forms.SplitContainer sContainer;
        private System.Windows.Forms.PropertyGrid propertyGrid;
        private System.Windows.Forms.ToolStripMenuItem miNew;
        private System.Windows.Forms.ToolStripMenuItem miOpen;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem 语法SToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem miSyntaxPlain;
        private System.Windows.Forms.ToolStripMenuItem miSyntaxXML;
        private System.Windows.Forms.ToolStripMenuItem xMLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem miToXMLEntity;
        private System.Windows.Forms.ToolStripMenuItem miToXMLCharacter;
        private HunterRichTextBox hTextBox;
    }
}