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
        public string GetTitle()
        {
            return "从服务器获取配置文件(&D)";
        }

        public object Invoke(object[] args)
        {
            new FormConfig().ShowDialog();
            return null;
        }

        public string SetParameters()
        {
            return null;
        }
    }
}
