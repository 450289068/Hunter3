namespace Hunter3
{
    partial class HunterLog
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
            this.Console = new Hunter3.HunterRichTextBox();
            this.SuspendLayout();
            // 
            // Console
            // 
            this.Console.AcceptsTab = true;
            this.Console.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(40)))), ((int)(((byte)(34)))));
            this.Console.ContentType = Hunter3.HunterRichTextBox.TextType.Plain;
            this.Console.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Console.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Console.ForeColor = System.Drawing.Color.White;
            this.Console.ImeMode = System.Windows.Forms.ImeMode.On;
            this.Console.Location = new System.Drawing.Point(0, 0);
            this.Console.Margin = new System.Windows.Forms.Padding(0);
            this.Console.Name = "Console";
            this.Console.ReadOnly = true;
            this.Console.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.Console.Size = new System.Drawing.Size(1177, 562);
            this.Console.TabIndex = 0;
            this.Console.Text = "";
            // 
            // HunterLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1177, 562);
            this.Controls.Add(this.Console);
            this.Name = "HunterLog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "日志";
            this.ResumeLayout(false);

        }

        #endregion

        private HunterRichTextBox Console;
    }
}