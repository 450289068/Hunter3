namespace Hunter3
{
    partial class HunterRichTextBoxSearchBar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HunterRichTextBoxSearchBar));
            this.sFind = new System.Windows.Forms.FlowLayoutPanel();
            this.hbRegex = new Hunter3.HunterButton();
            this.hbCaseSensitive = new Hunter3.HunterButton();
            this.hbWholeWord = new Hunter3.HunterButton();
            this.hSearch = new Hunter3.HunterRichTextBox();
            this.hbFindPrev = new Hunter3.HunterButton();
            this.hbSearchNext = new Hunter3.HunterButton();
            this.tt = new System.Windows.Forms.ToolTip(this.components);
            this.sFind.SuspendLayout();
            this.SuspendLayout();
            // 
            // sFind
            // 
            this.sFind.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("sFind.BackgroundImage")));
            this.sFind.Controls.Add(this.hbRegex);
            this.sFind.Controls.Add(this.hbCaseSensitive);
            this.sFind.Controls.Add(this.hbWholeWord);
            this.sFind.Controls.Add(this.hSearch);
            this.sFind.Controls.Add(this.hbFindPrev);
            this.sFind.Controls.Add(this.hbSearchNext);
            this.sFind.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.sFind.Location = new System.Drawing.Point(0, 6);
            this.sFind.Margin = new System.Windows.Forms.Padding(0);
            this.sFind.Name = "sFind";
            this.sFind.Padding = new System.Windows.Forms.Padding(6);
            this.sFind.Size = new System.Drawing.Size(1158, 36);
            this.sFind.TabIndex = 23;
            this.sFind.WrapContents = false;
            // 
            // hbRegex
            // 
            this.hbRegex.AllowCheck = true;
            this.hbRegex.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("hbRegex.BackgroundImage")));
            this.hbRegex.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.hbRegex.Checked = false;
            this.hbRegex.FlatAppearance.BorderSize = 0;
            this.hbRegex.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.hbRegex.Font = new System.Drawing.Font("Microsoft YaHei", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.hbRegex.ForeColor = System.Drawing.Color.White;
            this.hbRegex.Location = new System.Drawing.Point(9, 9);
            this.hbRegex.Name = "hbRegex";
            this.hbRegex.Size = new System.Drawing.Size(24, 24);
            this.hbRegex.TabIndex = 0;
            this.hbRegex.Text = ".*";
            this.tt.SetToolTip(this.hbRegex, "正则表达式");
            this.hbRegex.UseVisualStyleBackColor = true;
            this.hbRegex.Click += new System.EventHandler(this.hbRegex_Click);
            // 
            // hbCaseSensitive
            // 
            this.hbCaseSensitive.AllowCheck = true;
            this.hbCaseSensitive.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("hbCaseSensitive.BackgroundImage")));
            this.hbCaseSensitive.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.hbCaseSensitive.Checked = false;
            this.hbCaseSensitive.FlatAppearance.BorderSize = 0;
            this.hbCaseSensitive.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.hbCaseSensitive.Font = new System.Drawing.Font("Microsoft YaHei", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.hbCaseSensitive.ForeColor = System.Drawing.Color.White;
            this.hbCaseSensitive.Location = new System.Drawing.Point(39, 9);
            this.hbCaseSensitive.Name = "hbCaseSensitive";
            this.hbCaseSensitive.Size = new System.Drawing.Size(24, 24);
            this.hbCaseSensitive.TabIndex = 1;
            this.hbCaseSensitive.Text = "Aa";
            this.tt.SetToolTip(this.hbCaseSensitive, "大小写敏感");
            this.hbCaseSensitive.UseCompatibleTextRendering = true;
            this.hbCaseSensitive.UseVisualStyleBackColor = true;
            this.hbCaseSensitive.Click += new System.EventHandler(this.hbCaseSensitive_Click);
            // 
            // hbWholeWord
            // 
            this.hbWholeWord.AllowCheck = true;
            this.hbWholeWord.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("hbWholeWord.BackgroundImage")));
            this.hbWholeWord.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.hbWholeWord.Checked = false;
            this.hbWholeWord.FlatAppearance.BorderSize = 0;
            this.hbWholeWord.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.hbWholeWord.Font = new System.Drawing.Font("Microsoft YaHei", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.hbWholeWord.ForeColor = System.Drawing.Color.White;
            this.hbWholeWord.Location = new System.Drawing.Point(69, 9);
            this.hbWholeWord.Name = "hbWholeWord";
            this.hbWholeWord.Size = new System.Drawing.Size(24, 24);
            this.hbWholeWord.TabIndex = 5;
            this.hbWholeWord.Text = "\"\"";
            this.tt.SetToolTip(this.hbWholeWord, "全字匹配(非正则表达式模式有效)");
            this.hbWholeWord.UseCompatibleTextRendering = true;
            this.hbWholeWord.UseVisualStyleBackColor = true;
            this.hbWholeWord.Click += new System.EventHandler(this.hbWholeWord_Click);
            // 
            // hSearch
            // 
            this.hSearch.AcceptsTab = true;
            this.hSearch.BackColor = System.Drawing.SystemColors.Control;
            this.hSearch.ContentType = Hunter3.HunterRichTextBox.TextType.Plain;
            this.hSearch.DetectUrls = false;
            this.hSearch.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hSearch.ForeColor = System.Drawing.Color.White;
            this.hSearch.ImeMode = System.Windows.Forms.ImeMode.On;
            this.hSearch.Location = new System.Drawing.Point(96, 6);
            this.hSearch.Margin = new System.Windows.Forms.Padding(0);
            this.hSearch.Multiline = false;
            this.hSearch.Name = "hSearch";
            this.hSearch.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.hSearch.Size = new System.Drawing.Size(928, 27);
            this.hSearch.TabIndex = 2;
            this.hSearch.Text = "";
            // 
            // hbFindPrev
            // 
            this.hbFindPrev.AllowCheck = false;
            this.hbFindPrev.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("hbFindPrev.BackgroundImage")));
            this.hbFindPrev.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.hbFindPrev.Checked = false;
            this.hbFindPrev.FlatAppearance.BorderSize = 0;
            this.hbFindPrev.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.hbFindPrev.Font = new System.Drawing.Font("Microsoft YaHei", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.hbFindPrev.ForeColor = System.Drawing.Color.White;
            this.hbFindPrev.Location = new System.Drawing.Point(1027, 9);
            this.hbFindPrev.Name = "hbFindPrev";
            this.hbFindPrev.Size = new System.Drawing.Size(24, 24);
            this.hbFindPrev.TabIndex = 4;
            this.hbFindPrev.Text = "<-";
            this.tt.SetToolTip(this.hbFindPrev, "上一个");
            this.hbFindPrev.UseCompatibleTextRendering = true;
            this.hbFindPrev.UseVisualStyleBackColor = true;
            this.hbFindPrev.Click += new System.EventHandler(this.hbFindPrev_Click);
            // 
            // hbSearchNext
            // 
            this.hbSearchNext.AllowCheck = false;
            this.hbSearchNext.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("hbSearchNext.BackgroundImage")));
            this.hbSearchNext.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.hbSearchNext.Checked = false;
            this.hbSearchNext.FlatAppearance.BorderSize = 0;
            this.hbSearchNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.hbSearchNext.Font = new System.Drawing.Font("Microsoft YaHei", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.hbSearchNext.ForeColor = System.Drawing.Color.White;
            this.hbSearchNext.Location = new System.Drawing.Point(1057, 9);
            this.hbSearchNext.Name = "hbSearchNext";
            this.hbSearchNext.Size = new System.Drawing.Size(24, 24);
            this.hbSearchNext.TabIndex = 3;
            this.hbSearchNext.Text = "->";
            this.tt.SetToolTip(this.hbSearchNext, "下一个");
            this.hbSearchNext.UseCompatibleTextRendering = true;
            this.hbSearchNext.UseVisualStyleBackColor = true;
            this.hbSearchNext.Click += new System.EventHandler(this.hbSearchNext_Click);
            // 
            // HunterRichTextBoxSearchBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.sFind);
            this.Name = "HunterRichTextBoxSearchBar";
            this.Size = new System.Drawing.Size(1158, 42);
            this.sFind.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel sFind;
        private HunterButton hbRegex;
        private HunterButton hbCaseSensitive;
        private HunterButton hbWholeWord;
        private HunterRichTextBox hSearch;
        private HunterButton hbFindPrev;
        private HunterButton hbSearchNext;
        private System.Windows.Forms.ToolTip tt;
    }
}
