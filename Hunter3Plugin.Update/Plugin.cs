using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Net;
using System.Windows.Forms;
using System.IO;

namespace Hunter3Plugin
{
    public class Plugin : IPlugin
    {
        public String SetParameters()
        {
            ParameterBuilder pb = new ParameterBuilder();
            pb.AppendParameter(ParameterBuilder.ParameterType.version);

            return pb.ToString();
        }

        public object Invoke(object[] args)
        {
            String UpdateHTTPRoot = "http://10.20.128.164:1204";
            String UpdateHTTPIndex = "/download/version.xml";
            try
            {
                XDocument result = XDocument.Load(UpdateHTTPRoot + UpdateHTTPIndex, LoadOptions.None);
                String currentVersion = args[0].ToString();
                String description = result.Element("info").Element("version").Element("description").Value;
                String latestVersion = result.Element("info").Element("version").Attribute("value").Value;
                String fileuri = result.Element("info").Element("version").Element("url").Value;

                if (!IsLatestVersion(currentVersion, latestVersion))
                {
                    DialogResult dr = MessageBox.Show(String.Format("当前最新版本为{0}，而您的版本为{1}，是否需要更新？" + Environment.NewLine + "更新信息：" + Environment.NewLine + "{2}", latestVersion, currentVersion, description), "Hunter 3", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        WebClient wc = new WebClient();
                        String downpath = Path.GetFileName(fileuri);
                        wc.DownloadFile(UpdateHTTPRoot + fileuri, downpath);
                        MessageBox.Show(String.Format("最新的Hunter 3已经下载到本程序目录下的{0}中。", downpath), "Hunter 3", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("您的Hunter 3为最新版本，无需更新。", "Hunter 3", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hunter 3", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return null;
        }

        public String GetTitle()
        {
            return "检查更新(&A)";
        }

        private static bool IsLatestVersion(String current, String latest)
        {
//             int cv = int.Parse(current.Replace(".", ""));
//             int lv = int.Parse(latest.Replace(".", ""));
            if (current == latest) return true;
            bool result = false;
            char[] sep = {'.'};
            string[] cv = current.Split(sep, StringSplitOptions.RemoveEmptyEntries);
            string[] lv = latest.Split(sep, StringSplitOptions.RemoveEmptyEntries);
            try
            {
                for ( int i=0; i < 4; i ++){
                    if (int.Parse(cv[i]) > int.Parse(lv[i]))
                        result = true;
                }
            }
            catch {} 
            return result;
        }

    }
}
