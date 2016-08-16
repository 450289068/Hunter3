using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hunter3Plugin
{
    public class Plugin : IPlugin
    {
        public String SetParameters()
        {
            return null;
        }

        public object Invoke(object[] args)
        {
            new PluginMain().ShowDialog();
            return null;
        }

        public String GetTitle()
        {
            return "代理分析器";
        }


    }
}
