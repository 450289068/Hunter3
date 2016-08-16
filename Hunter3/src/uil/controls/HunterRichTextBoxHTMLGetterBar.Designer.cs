namespace Hunter3
{
    partial class HunterRichTextBoxHTMLGetterBar
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HunterRichTextBoxHTMLGetterBar));
            this.sHTMLGetter = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.tt = new System.Windows.Forms.ToolTip(this.components);
            this.hURL = new Hunter3.HunterRichTextBox();
            this.hbGet = new Hunter3.HunterButton();
            this.sHTMLGetter.SuspendLayout();
            this.SuspendLayout();
            // 
            // sHTMLGetter
            // 
            this.sHTMLGetter.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("sHTMLGetter.BackgroundImage")));
            this.sHTMLGetter.Controls.Add(this.label1);
            this.sHTMLGetter.Controls.Add(this.hURL);
            this.sHTMLGetter.Controls.Add(this.hbGet);
            this.sHTMLGetter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.sHTMLGetter.Location = new System.Drawing.Point(0, 6);
            this.sHTMLGetter.Margin = new System.Windows.Forms.Padding(0);
            this.sHTMLGetter.Name = "sHTMLGetter";
            this.sHTMLGetter.Padding = new System.Windows.Forms.Padding(6);
            this.sHTMLGetter.Size = new System.Drawing.Size(1158, 36);
            this.sHTMLGetter.TabIndex = 23;
            this.sHTMLGetter.WrapContents = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(9, 6);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(5);
            this.label1.Size = new System.Drawing.Size(42, 27);
            this.label1.TabIndex = 5;
            this.label1.Text = "URL";
            // 
            // hURL
            // 
            this.hURL.AcceptsTab = true;
            this.hURL.BackColor = System.Drawing.SystemColors.Control;
            this.hURL.ContentType = Hunter3.HunterRichTextBox.TextType.Plain;
            this.hURL.DetectUrls = false;
            this.hURL.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hURL.ForeColor = System.Drawing.Color.White;
            this.hURL.ImeMode = System.Windows.Forms.ImeMode.On;
            this.hURL.Location = new System.Drawing.Point(54, 6);
            this.hURL.Margin = new System.Windows.Forms.Padding(0);
            this.hURL.Multiline = false;
            this.hURL.Name = "hURL";
            this.hURL.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.hURL.Size = new System.Drawing.Size(985, 27);
            this.hURL.TabIndex = 2;
            this.hURL.Text = "";
            // 
            // hbGet
            // 
            this.hbGet.AllowCheck = false;
            this.hbGet.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("hbGet.BackgroundImage")));
            this.hbGet.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.hbGet.Checked = false;
            this.hbGet.FlatAppearance.BorderSize = 0;
            this.hbGet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.hbGet.Font = new System.Drawing.Font("Microsoft YaHei", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.hbGet.ForeColor = System.Drawing.Color.White;
            this.hbGet.Location = new System.Drawing.Point(1042, 9);
            this.hbGet.Name = "hbGet";
            this.hbGet.Size = new System.Drawing.Size(24, 24);
            this.hbGet.TabIndex = 3;
            this.hbGet.Text = "H";
            this.tt.SetToolTip(this.hbGet, "获取HTML代码和请求信息");
            this.hbGet.UseVisualStyleBackColor = true;
            this.hbGet.Click += new System.EventHandler(this.hbGet_Click);
            // 
            // HunterRichTextBoxHTMLGetterBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.sHTMLGetter);
            this.Name = "HunterRichTextBoxHTMLGetterBar";
            this.Size = new System.Drawing.Size(1158, 42);
            this.sHTMLGetter.ResumeLayout(false);
            this.sHTMLGetter.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel sHTMLGetter;
        private HunterRichTextBox hURL;
        private HunterButton hbGet;
        private System.Windows.Forms.ToolTip tt;
        private System.Windows.Forms.Label label1;
    }
}
