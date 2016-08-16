using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Net;

namespace Hunter3Plugin
{
    public partial class PluginMain : Form
    {
        public PluginMain()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            textSrc.Text = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "proxy.hip");
            textDest.Text = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "available_proxy.hip");
        }

        public void Start()
        {
            try
            {
                String content = File.ReadAllText(textSrc.Text.Replace ( "\"", ""));
                List<HunterProxy> proxies = new List<HunterProxy>();
                proxies = HunterProxy.GetProxy(content);
                AnalyzeProxies(proxies, textDest.Text);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "Hunter 3", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnStart.Enabled = true;
            }
        }

        private void AnalyzeProxies(List<HunterProxy> proxies, String outputPath)
        {
            pb.Maximum = proxies.Count;
            int count = 0;
            int timeout = int.Parse(textTimeout.Text);
            foreach (HunterProxy hp in proxies)
            {
                try
                {
                    count++;
                    pb.Value = count;
                    HttpWebRequest wc = (HttpWebRequest)HttpWebRequest.Create(textTest.Text);
                    WebProxy wp = new WebProxy(hp.IPAndPort);
                    wc.Proxy = wp;
                
                    bool success = false;
                    Thread t = new Thread ( new ThreadStart( ( ) =>{
                        try
                        {
                            HttpWebResponse wr = (HttpWebResponse)wc.GetResponse();
                            if (wr.StatusCode == HttpStatusCode.OK)
                            {
                                if (textExceptedString.Text.Trim() == "")
                                    success = true;
                                else
                                {
                                    StreamReader sr = new StreamReader(wr.GetResponseStream(), Encoding.UTF8);
                                    String content = sr.ReadToEnd();
                                    if (content.Contains(textExceptedString.Text))
                                    {
                                        success = true;
                                    }
                                    else
                                    {
                                        success = false;
                                    }
                                    sr.Close();
                                }
                            }
                            else
                                success = false;
                            wr.Close();
                        }
                        catch { success = false; }
                    }));
                    t.Start();
                    if (t.Join(timeout) && success )
                    {
                        textConsole.AppendText("使用代理" + hp.IPAndPort + "成功，保存此代理。" + Environment.NewLine);
                        File.AppendAllText(outputPath, hp.ToString() + Environment.NewLine, Encoding.UTF8);
                    }
                    else
                    {
                        textConsole.AppendText("连接" + hp.IPAndPort + "失败或与预期值不符，丢弃此代理。" + Environment.NewLine);
                    }
                }
                catch (Exception)
                {
                    textConsole.AppendText("连接" + hp.IPAndPort + "失败，丢弃此代理。" + Environment .NewLine);
                }
            }
            MessageBox.Show("完成。", "Hunter 3", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnStart.Enabled = true;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            Thread t = new Thread(new ThreadStart(Start));
            t.Start();
            btnStart.Enabled = false;
        }

    }
}
