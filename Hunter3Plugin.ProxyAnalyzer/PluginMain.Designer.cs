namespace Hunter3Plugin
{
    partial class PluginMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PluginMain));
            this.label1 = new System.Windows.Forms.Label();
            this.pb = new System.Windows.Forms.ProgressBar();
            this.textSrc = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textDest = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textTest = new System.Windows.Forms.TextBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.textConsole = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textTimeout = new System.Windows.Forms.TextBox();
            this.textExceptedString = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(13, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "原始HIP文件路径：";
            // 
            // pb
            // 
            this.pb.Location = new System.Drawing.Point(10, 262);
            this.pb.Name = "pb";
            this.pb.Size = new System.Drawing.Size(469, 23);
            this.pb.TabIndex = 1;
            // 
            // textSrc
            // 
            this.textSrc.Location = new System.Drawing.Point(15, 29);
            this.textSrc.Name = "textSrc";
            this.textSrc.Size = new System.Drawing.Size(467, 21);
            this.textSrc.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(13, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(245, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "生成新的HIP文件路径(请替换掉proxy.hip)：";
            // 
            // textDest
            // 
            this.textDest.Location = new System.Drawing.Point(15, 83);
            this.textDest.Name = "textDest";
            this.textDest.Size = new System.Drawing.Size(467, 21);
            this.textDest.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(13, 118);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "测试站点：";
            // 
            // textTest
            // 
            this.textTest.Location = new System.Drawing.Point(15, 133);
            this.textTest.Name = "textTest";
            this.textTest.Size = new System.Drawing.Size(467, 21);
            this.textTest.TabIndex = 2;
            this.textTest.Text = "http://www.baidu.com";
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(384, 211);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(95, 45);
            this.btnStart.TabIndex = 4;
            this.btnStart.Text = "开始(&S)";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // textConsole
            // 
            this.textConsole.Location = new System.Drawing.Point(10, 292);
            this.textConsole.Multiline = true;
            this.textConsole.Name = "textConsole";
            this.textConsole.ReadOnly = true;
            this.textConsole.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textConsole.Size = new System.Drawing.Size(469, 95);
            this.textConsole.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(13, 157);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "PING超时(ms)：";
            // 
            // textTimeout
            // 
            this.textTimeout.Location = new System.Drawing.Point(15, 172);
            this.textTimeout.Name = "textTimeout";
            this.textTimeout.Size = new System.Drawing.Size(467, 21);
            this.textTimeout.TabIndex = 3;
            this.textTimeout.Text = "3000";
            // 
            // textExceptedString
            // 
            this.textExceptedString.Location = new System.Drawing.Point(15, 211);
            this.textExceptedString.Multiline = true;
            this.textExceptedString.Name = "textExceptedString";
            this.textExceptedString.Size = new System.Drawing.Size(363, 45);
            this.textExceptedString.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(13, 196);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(221, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "预期值(返回的HTML)中应该包含的字符串";
            // 
            // PluginMain
            // 
            this.AcceptButton = this.btnStart;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 399);
            this.Controls.Add(this.textExceptedString);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textConsole);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.textTimeout);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textTest);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textDest);
            this.Controls.Add(this.textSrc);
            this.Controls.Add(this.pb);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PluginMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "代理分析器";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar pb;
        private System.Windows.Forms.TextBox textSrc;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textDest;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textTest;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.TextBox textConsole;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textTimeout;
        private System.Windows.Forms.TextBox textExceptedString;
        private System.Windows.Forms.Label label5;
    }
}