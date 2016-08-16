using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;

namespace Hunter3
{
    public class HunterProxyFetcher
    {
        List<HunterProxy> proxyList = new List<HunterProxy>();

        public HunterProxyFetcher(List<HunterProxy> proxy)
        {
            proxyList = proxy;
            //proxy.Insert(0, null);
        }

        /// <summary>
        /// 随机获取下一个代理
        /// </summary>
        public HunterProxy Next()
        {
            int p = new Random().Next(proxyList.Count);
            if (proxyList.Count <= 0) return null;
            return proxyList[p]; ;
        }
    }

    public class HunterProxy
    {
        public String IPAndPort;
        public String Type;
        public String Speed;
        public String Description;
        public readonly static String StrRegex = "(?<ipandport>((\\d){0,3}\\.){3}(\\d){0,3}:(\\d)+)(\\s)+(?<type>(\\w)+)(\\s)+(?<speed>(.*?))(\\s)+(?<description>(.*))";

        public static Queue<HunterProxy> GetProxy(String r, List<String> pfilter)
        {
            Queue<HunterProxy> ResultList = new Queue<HunterProxy>();
            Regex regex = new Regex(StrRegex);
            Match m =  regex.Match(r);

            List<String> CommentRemovedFilter = new List<String>();
            foreach (String s in pfilter)
            {
                String key = s.Substring(0, s.IndexOf('\'') >= 0 ? s.IndexOf('\'') : s.Length);
                if (key.Trim() == "") continue;
                CommentRemovedFilter.Add(key);
            }

            while (m.Success){
                bool Legal = true;
                HunterProxy tempProxy = new HunterProxy();
                tempProxy.IPAndPort = m.Result("${ipandport}");
                tempProxy.Type = m.Result("${type}");
                tempProxy.Speed = m.Result("${speed}");
                tempProxy.Description = m.Result("${description}").Replace("\n","").Replace("\r","").Trim();

                if (CommentRemovedFilter.Count > 0)
                {
                    //过滤器有内容，则进行筛选。同时要去除以'开头的注释
                    foreach (String f in CommentRemovedFilter)
                    {
                        if (tempProxy.Description.Contains(f.Trim()))
                        {
                            Legal = true;
                            break;
                        }
                        Legal = false;
                    }
                }
                if (Legal)
                    ResultList.Enqueue(tempProxy);
                m = m.NextMatch();
            }
            return ResultList;
        }

        public bool isAvaliable(int timeout)
        {
            Ping ping = new Ping();
            PingReply result;
            try
            {
                result = ping.Send(IPAndPort.Substring(0, IPAndPort.IndexOf(':')), timeout);
            }
            catch
            {
                return false;
                throw;
            }
            return result.Status == IPStatus.Success;
        }


        public struct Struct_INTERNET_PROXY_INFO
        {
            public int dwAccessType;
            public IntPtr proxy;
            public IntPtr proxyBypass;
        };

        [DllImport("wininet.dll", SetLastError = true)]
        private static extern bool InternetSetOption(IntPtr hInternet, int dwOption, IntPtr lpBuffer, int lpdwBufferLength);

        public static void InternetSetOption(string strProxy)
        {

            //设置代理选项
            const int INTERNET_OPTION_PROXY = 38;
            //设置代理类型
            const int INTERNET_OPEN_TYPE_PROXY = 3;
            //设置代理类型，直接访问，不需要通过代理服务器了
            const int INTERNET_OPEN_TYPE_DIRECT = 1;
            Struct_INTERNET_PROXY_INFO struct_IPI;
            // Filling in structure 
            struct_IPI.dwAccessType = INTERNET_OPEN_TYPE_PROXY;
            //把代理地址设置到非托管内存地址中 
            struct_IPI.proxy = Marshal.StringToHGlobalAnsi(strProxy);
            //代理通过本地连接到代理服务器上
            struct_IPI.proxyBypass = Marshal.StringToHGlobalAnsi("local");
            // Allocating memory 
            //关联到内存
            IntPtr intptrStruct = Marshal.AllocCoTaskMem(Marshal.SizeOf(struct_IPI));
            if (string.IsNullOrEmpty(strProxy) || strProxy.Trim().Length == 0)
            {
                strProxy = string.Empty;
                struct_IPI.dwAccessType = INTERNET_OPEN_TYPE_DIRECT;
            }
            // Converting structure to IntPtr 
            //把结构体转换到句柄
            Marshal.StructureToPtr(struct_IPI, intptrStruct, true);
            bool iReturn = InternetSetOption(IntPtr.Zero, INTERNET_OPTION_PROXY, intptrStruct, Marshal.SizeOf(struct_IPI));
        } 
    }
}
