using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace Hunter3
{
    public class HunterWebClient : WebClient
    {
        public String DownloadSource { get; set; }
        public String DownloadDestination { get; set; }
        public String DownloadKeyword { get; set; }
        public String XMLFile { get; set; }
    }
}
