namespace Hunter3
{
    partial class HunterFormTitle
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HunterFormTitle));
            this.splitTitle = new System.Windows.Forms.SplitContainer();
            this.flowLayoutTitle = new System.Windows.Forms.FlowLayoutPanel();
            this.Icon = new System.Windows.Forms.PictureBox();
            this.Title = new System.Windows.Forms.Label();
            this.flowLayoutControl = new System.Windows.Forms.FlowLayoutPanel();
            this.msControlbox = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitTitle)).BeginInit();
            this.splitTitle.Panel1.SuspendLayout();
            this.splitTitle.Panel2.SuspendLayout();
            this.splitTitle.SuspendLayout();
            this.flowLayoutTitle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Icon)).BeginInit();
            this.flowLayoutControl.SuspendLayout();
            this.msControlbox.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitTitle
            // 
            this.splitTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(40)))), ((int)(((byte)(34)))));
            this.splitTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitTitle.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitTitle.IsSplitterFixed = true;
            this.splitTitle.Location = new System.Drawing.Point(0, 0);
            this.splitTitle.Name = "splitTitle";
            // 
            // splitTitle.Panel1
            // 
            this.splitTitle.Panel1.Controls.Add(this.flowLayoutTitle);
            // 
            // splitTitle.Panel2
            // 
            this.splitTitle.Panel2.Controls.Add(this.flowLayoutControl);
            this.splitTitle.Size = new System.Drawing.Size(1111, 32);
            this.splitTitle.SplitterDistance = 903;
            this.splitTitle.SplitterWidth = 1;
            this.splitTitle.TabIndex = 0;
            // 
            // flowLayoutTitle
            // 
            this.flowLayoutTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(40)))), ((int)(((byte)(34)))));
            this.flowLayoutTitle.Controls.Add(this.Icon);
            this.flowLayoutTitle.Controls.Add(this.Title);
            this.flowLayoutTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutTitle.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutTitle.Name = "flowLayoutTitle";
            this.flowLayoutTitle.Size = new System.Drawing.Size(903, 32);
            this.flowLayoutTitle.TabIndex = 4;
            this.flowLayoutTitle.WrapContents = false;
            this.flowLayoutTitle.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.OnChangeWindowStateByDoubleClick);
            // 
            // Icon
            // 
            this.Icon.Image = ((System.Drawing.Image)(resources.GetObject("Icon.Image")));
            this.Icon.Location = new System.Drawing.Point(5, 5);
            this.Icon.Margin = new System.Windows.Forms.Padding(5);
            this.Icon.Name = "Icon";
            this.Icon.Size = new System.Drawing.Size(24, 24);
            this.Icon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.Icon.TabIndex = 0;
            this.Icon.TabStop = false;
            // 
            // Title
            // 
            this.Title.AutoSize = true;
            this.Title.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Title.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.Title.Location = new System.Drawing.Point(41, 7);
            this.Title.Margin = new System.Windows.Forms.Padding(7);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(58, 17);
            this.Title.TabIndex = 1;
            this.Title.Text = "Hunter 3";
            // 
            // flowLayoutControl
            // 
            this.flowLayoutControl.Controls.Add(this.msControlbox);
            this.flowLayoutControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutControl.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutControl.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutControl.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutControl.Name = "flowLayoutControl";
            this.flowLayoutControl.Size = new System.Drawing.Size(207, 32);
            this.flowLayoutControl.TabIndex = 3;
            this.flowLayoutControl.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.OnChangeWindowStateByDoubleClick);
            // 
            // msControlbox
            // 
            this.msControlbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.msControlbox.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem4,
            this.toolStripMenuItem3,
            this.toolStripMenuItem1});
            this.msControlbox.Location = new System.Drawing.Point(115, 0);
            this.msControlbox.Name = "msControlbox";
            this.msControlbox.Size = new System.Drawing.Size(92, 24);
            this.msControlbox.TabIndex = 0;
            this.msControlbox.Text = "menuStrip1";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Font = new System.Drawing.Font("黑体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.toolStripMenuItem4.ForeColor = System.Drawing.Color.White;
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(24, 20);
            this.toolStripMenuItem4.Text = "_";
            this.toolStripMenuItem4.Click += new System.EventHandler(this.OnMinimize);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Font = new System.Drawing.Font("黑体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.toolStripMenuItem3.ForeColor = System.Drawing.Color.White;
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(30, 20);
            this.toolStripMenuItem3.Text = "□";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.OnChangeWindowState);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Font = new System.Drawing.Font("黑体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.toolStripMenuItem1.ForeColor = System.Drawing.Color.White;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(30, 20);
            this.toolStripMenuItem1.Text = "×";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.OnClose);
            // 
            // HunterFormTitle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitTitle);
            this.Name = "HunterFormTitle";
            this.Size = new System.Drawing.Size(1111, 32);
            this.splitTitle.Panel1.ResumeLayout(false);
            this.splitTitle.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitTitle)).EndInit();
            this.splitTitle.ResumeLayout(false);
            this.flowLayoutTitle.ResumeLayout(false);
            this.flowLayoutTitle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Icon)).EndInit();
            this.flowLayoutControl.ResumeLayout(false);
            this.flowLayoutControl.PerformLayout();
            this.msControlbox.ResumeLayout(false);
            this.msControlbox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuStrip msControlbox;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        public System.Windows.Forms.PictureBox Icon;
        public System.Windows.Forms.Label Title;
        public System.Windows.Forms.SplitContainer splitTitle;
        public System.Windows.Forms.FlowLayoutPanel flowLayoutTitle;
        public System.Windows.Forms.FlowLayoutPanel flowLayoutControl;
    }
}
