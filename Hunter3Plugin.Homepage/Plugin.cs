using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Net;
using System.Windows.Forms;
using System.Diagnostics;
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
            try{
                Process.Start("http://10.20.128.164:1204");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hunter 3", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return null;
        }

        public String GetTitle()
        {
            return "访问主页(&G)";
        }

        private static bool IsLatestVersion(String current, String latest)
        {
            int cv = int.Parse(current.Replace(".", ""));
            int lv = int.Parse(latest.Replace(".", ""));
            return cv >= lv;
        }

    }
}
