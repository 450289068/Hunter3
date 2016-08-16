using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Hunter3Plugin
{
    public class HunterProxy
    {
        public String IPAndPort;
        public String Type;
        public String Speed;
        public String Description;
        public readonly static String StrRegex = "(?<ipandport>((\\d){0,3}\\.){3}(\\d){0,3}:(\\d)+)(\\s)+(?<type>(\\w)+)(\\s)+(?<speed>(.*?))(\\s)+(?<description>(.*))";

        public static List<HunterProxy> GetProxy(String r)
        {
            List<HunterProxy> ResultList = new List<HunterProxy>();
            Regex regex = new Regex(StrRegex);
            Match m = regex.Match(r);

            while (m.Success)
            {
                bool Legal = true;
                HunterProxy tempProxy = new HunterProxy();
                tempProxy.IPAndPort = m.Result("${ipandport}");
                tempProxy.Type = m.Result("${type}");
                tempProxy.Speed = m.Result("${speed}");
                tempProxy.Description = m.Result("${description}").Replace("\n", "").Replace("\r", "").Trim();

                if (Legal)
                    ResultList.Add(tempProxy);
                m = m.NextMatch();
            }

            return ResultList;
        }

        public override string ToString()
        {
            return IPAndPort + " " + Type + " " + Speed + " " + Description;
        }
    }
}
