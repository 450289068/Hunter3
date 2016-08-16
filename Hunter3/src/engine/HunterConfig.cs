using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Drawing;

namespace Hunter3
{
    public class HunterConfig
    {
        public enum Core { Default, IE, WebRequest };
        [XmlIgnore]
        public static readonly Color ColorMessage = Color.White;
        [XmlIgnore]
        public static readonly Color ColorException = Color.FromArgb(249, 38, 114);
        [XmlIgnore]
        public static readonly Color ColorDetails = Color.FromArgb(102, 217, 239);
        [XmlIgnore]
        public static readonly Color ColorDownload = Color.FromArgb(166, 226, 46);
        [XmlIgnore]
        public static readonly Color ColorAbandonFile = Color.FromArgb(249, 38, 114);
        [XmlIgnore]
        public static readonly Color ColorAbandonLink = Color.FromArgb(253, 151, 31);
        [XmlIgnore]
        public static readonly Color ColorProxy = Color.FromArgb(174, 129, 255);
        [XmlIgnore]
        public static readonly Color ColorHTML = Color.FromArgb(117, 113, 94);
        [XmlIgnore]
        public static readonly Color ColorPlain = Color.White;
        [XmlIgnore]
        public static readonly Color ColorXMLPlain = Color.White;
        [XmlIgnore]
        public static readonly Color ColorXMLElement = Color.FromArgb(249, 38, 114);
        [XmlIgnore]
        public static readonly Color ColorXMLAttribute = Color.FromArgb(166, 226, 46);
        [XmlIgnore]
        public static readonly Color ColorXMLString = Color.FromArgb(230, 219, 116);
        [XmlIgnore]
        public static readonly Color ColorXMLComment = Color.FromArgb(117, 113, 94);
        [XmlIgnore]
        public static readonly Color ColorXMLEscape = Color.FromArgb(174, 129, 255);
        [XmlIgnore]
        public static readonly Color ColorProxyFilterPlain = Color.White;
        [XmlIgnore]
        public static readonly Color ColorProxyFilterComment = Color.FromArgb(117, 113, 94);
        [XmlIgnore]
        public static readonly Color ColorBarForeColor = Color.FromArgb(29, 29, 28);
        
        [XmlElement("View")]
        public ViewOption viewOption { get; set;}

        [XmlElement("Notify")]
        public NotifyOption notifyOption { get; set; }

        public class ViewOption{
            public bool DownloadItems = true;
            public bool AbandonLinks = true;
            public bool AbandonFiles = true;
            public bool Details = false;
            public bool Exceptions = false;
            public bool Proxies = false;
            public bool HTML = false;
        }

        public class NotifyOption
        {
            public bool FirstBallon = true;
            public bool FirstMinimize = true;
        }

        public String StrategyFolder { get; set; }
        public String PluginFolder { get; set; }
        public String CurrentStrategyFile { get; set; }
        public bool UseProxy { get; set; }
        public int PingTimeout { get; set; }    //代理Ping超时时间
        public List<String> ProxyFilterKeywords { get; set; }
        public String UpdateHTTPRoot { get; set; }
        public String UpdateHTTPIndex { get; set; }
        public int FailureCooldown { get; set; }    //访问网页失败后的冷却时间
        public int FailureTimes { get; set; }   //连续失败访问多少次以后放弃治疗，进入冷却
        public Core HunterCore { get; set; }

        public HunterConfig()
        {
            StrategyFolder = HunterUtilities.AbsolutePath("strategy\\");
            PluginFolder = HunterUtilities.AbsolutePath("plugins\\");
            CurrentStrategyFile = HunterUtilities.AbsolutePath("strategy\\bing.h3s");
            UseProxy = false;
            PingTimeout = 3000;
            viewOption = new ViewOption();
            notifyOption = new NotifyOption();
            ProxyFilterKeywords = new List<String>();
            UpdateHTTPRoot = "http://10.20.128.164:1204";
            UpdateHTTPIndex = "/download/version.xml";
            FailureCooldown = 60;
            FailureTimes = 10;
            HunterCore = Core.Default;
        }

        public static HunterConfig Load()
        {
            try
            {
                HunterConfig hConfig;
                XmlSerializer ser = new XmlSerializer(typeof(HunterConfig));
                FileStream fs = new FileStream("config.xml", FileMode.Open, FileAccess.Read);
                hConfig = (HunterConfig)ser.Deserialize(fs);
                fs.Close();
                return hConfig;
            }
            catch
            {
                HunterConfig t = new HunterConfig();
                t.ProxyFilterKeywords.Add("'请把需要使用的代理信息输入在此，如输入\"广州\"，Hunter将会使用所有代理描述中含有\"广州\"二字的代理服务器。不支持空格分词，若什么都不输入，则表示使用所有可用的代理服务器。关闭此窗口，将自动保存内容。");
                t.Save();
                return t;
            }
        }

        public void Save()
        {
            FileStream fs = new FileStream("config.xml", FileMode.Create, FileAccess.Write);
            XmlSerializer ser = new XmlSerializer(typeof(HunterConfig));
            ser.Serialize(fs, this);
            fs.Close();
        }


    }
}
