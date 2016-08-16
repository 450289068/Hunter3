using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;
namespace Hunter3
{
    public partial class HunterRichTextBoxHTMLGetterBar : UserControl
    {
        private readonly int PaddingRight = 50;
        public Control BindingOutputControl { get; set; }
        private Color TextForeColor;

        public HunterRichTextBoxHTMLGetterBar()
        {
            InitializeComponent();
            Dock = DockStyle.Bottom;
            Height = sHTMLGetter.Height;
            this.Resize += new EventHandler(HunterRichTextBoxHTMLGetterBar_Resize);
            this.VisibleChanged += new EventHandler(HunterRichTextBoxHTMLGetterBar_VisibleChanged);
            hURL.KeyDown += new KeyEventHandler(hURL_KeyDown);
            hURL.TextChanged += new EventHandler(hURL_TextChanged);
        }

        void hURL_TextChanged(object sender, EventArgs e)
        {
            hURL.ForeColor = TextForeColor;
        }

        void hURL_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return || e.KeyCode == Keys.Enter)
            {
                hbGet_Click(null, null);
            }
        }

        void HunterRichTextBoxHTMLGetterBar_VisibleChanged(object sender, EventArgs e)
        {
            hURL.Width = Width - hURL.Left - PaddingRight;
        }

        void HunterRichTextBoxHTMLGetterBar_Resize(object sender, EventArgs e)
        {
            hURL.Width = Width - hURL.Left - PaddingRight;
        }

        public void Init(Control output, Color forecolor){
            BindingOutputControl = output;
            TextForeColor = forecolor;
            hURL.ForeColor = forecolor;
            hURL.Width = Width - hURL.Left - PaddingRight;
        }

        public String GetHTML(String url, bool getheaders)
        {
            try
            {
                WebRequest wrq = WebRequest.Create(url);
                wrq.Timeout = 10000;
                WebResponse wrp = wrq.GetResponse();
                Stream s = wrp.GetResponseStream();
                StringBuilder sb = new StringBuilder();
                StreamReader sr = new StreamReader(s, Encoding.UTF8);
                sb.AppendLine(sr.ReadToEnd());
                sb.AppendLine("");

                sb.AppendLine("ContentLength:" + wrp.ContentLength);
                sb.AppendLine("ContentType:" + wrp.ContentLength);

                sb.AppendLine("服务器响应头：");
                foreach (String str in wrp.Headers.AllKeys)
                {
                    sb.AppendLine(str + ": " + wrp.Headers.Get(str));
                }

                wrp.Close();
                return sb.ToString();

            }
            catch (WebException ex)
            {
                return ex.Message + Environment.NewLine;
            }
            catch (Exception ex)
            {
                return ex.Message + Environment.NewLine;
            }
        }

        private void hbGet_Click(object sender, EventArgs e)
        {
            String url = hURL.Text.StartsWith("http://") ? hURL.Text : "http://" + hURL.Text;
            BindingOutputControl.Text += GetHTML(url, true);
            TextBoxBase tbb = BindingOutputControl as TextBoxBase;
            if (tbb != null)
            {
                tbb.Select(tbb.TextLength, 0);
                tbb.ScrollToCaret();
            }
        }
    }
}
