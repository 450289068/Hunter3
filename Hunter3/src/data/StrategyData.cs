using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Reflection;

namespace Hunter3
{
    [XmlRoot("Strategy")]
    public class StrategyData
    {
        #region Data
        [XmlElement("Information")]
        public Information information { get; set; }
        [XmlElement("Configuration")]
        public Configuration configuration { get; set; }

        public StrategyData()
        {
            information = new Information();
            configuration = new Configuration();
        }

        public class Information
        {
            public String Title {get; set;}
            public String Version {get; set;} 
            public String Author {get; set;}
            public String Uri {get; set;}
            public String Message { get; set; }
            public Information()
            {
                Title = "Untitled";
                Version = "1.0";
                Author = "Anonymous";
                Uri = "";
                Message = "";
            }

        }

        public class Configuration
        {
            public Configuration()
            {
                UseIE = false;
                StartIndex = "0";
                IndexStep = "10";
                MaxIndex = "770";
                HttpHead = "http://www.baidu.com/s?tn=baiduhome_pg&ie=utf-8&cl=3";
                Regex = "href=\"(?<url>http(s)?://www.baidu.com/link\\?url=(.*?))\"(\\s*)(.*)(\\s*)>(?<text>(.*?))</a>";
                FirstURL = "{HttpHead}&wd=filetype:{filetype}+{keyword}&pn=0";
                SearchURL = "{HttpHead}&wd=filetype:{filetype}+{keyword}&pn={index}";
                Redirect = "true";
                confusionString = new ConfusionString("html鐗");
                forbiddenString = new ForbiddenString("百度文库");
                disguise = new Disguise();
            }

            public Boolean UseIE { get; set; }
            public String StartIndex { get; set; }
            public String IndexStep{get; set;}
            public String MaxIndex {get; set;}
            public String HttpHead {get; set;}
            public String Regex {get; set;}
            public String FirstURL {get; set;}
            public String SearchURL{get; set;}
            public String Redirect {get; set;}

            [XmlElement("ConfusionString")]
            public ConfusionString confusionString { get; set; }
            [XmlElement("ForbiddenString")]
            public ForbiddenString forbiddenString { get; set; }
            [XmlElement("Disguise")]
            public Disguise disguise { get; set; }

            public class ConfusionString
            {
                public List<String> String { get; set; }
                public ConfusionString() { String = new List<String>();  }
                public ConfusionString(String str)
                {
                    String = new List<String>();
                    String.Add(str);
                }
            }

            public class ForbiddenString
            {
                public List<String> String { get; set; }
                public ForbiddenString() { String = new List<String>();}
                public ForbiddenString(String str)
                {
                    String = new List<String>();
                    String.Add(str);
                }
            }

            public class Disguise
            {
                public Disguise()
                {
                    Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                    KeepAlive = "true";
                    UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:23.0) Gecko/20100101 Firefox/23.0";
                    Timeout = "10000";
                    AllowAutoRedirect = "true";
                    Cookie = "BAIDUID={RandomString(32)}:FG=1; BDUSS=wyQ0NqdDF-bngyRHZ1bmpsV0s5Ujl-STM2R0xiVmZUa21QZTZHQWxYejBuaWxTQVFBQUFBJCQAAAAAAAAAAAEAAAA7Q9oARnJvc2VyAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAPQRAlL0EQJSMm; BDRCVFR[gltLrB7qNCt]=mk3SLVN4HKm; H_PS_PSSID=2776_2976_2980_3109_3225; BDRCVFR[feWj1Vr5u3D]=mk3SLVN4HKm";
                }

                public String Accept {get;set;}
                public String KeepAlive{get;set;}
                public String UserAgent{get;set;}
                public String Timeout {get;set;}
                public String AllowAutoRedirect { get; set; }
                public String Cookie { get; set; }
            }
        }
        #endregion

        #region Actions

        public String GetStrategyInformation()
        {
            String result = "策略信息：" + Environment.NewLine;
            foreach (PropertyInfo p in information.GetType().GetProperties())
            {
                result += p.Name + ": " + p.GetValue(information, null).ToString() + Environment.NewLine;
            }
            result += "策略读取完毕。";
            return result;

        }

        #endregion
    }

}
