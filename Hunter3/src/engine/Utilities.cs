using System;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using Hunter3;
using Hunter3.Strategies;
using System.Xml.Linq;
using System.Runtime.InteropServices;

namespace Hunter3
{
    public class NetworkConnection
    {
        public enum ERROR_ID
        {
            ERROR_SUCCESS = 0,  // Success
            ERROR_BUSY = 170,
            ERROR_MORE_DATA = 234,
            ERROR_NO_BROWSER_SERVERS_FOUND = 6118,
            ERROR_INVALID_LEVEL = 124,
            ERROR_ACCESS_DENIED = 5,
            ERROR_INVALID_PASSWORD = 86,
            ERROR_INVALID_PARAMETER = 87,
            ERROR_BAD_DEV_TYPE = 66,
            ERROR_NOT_ENOUGH_MEMORY = 8,
            ERROR_NETWORK_BUSY = 54,
            ERROR_BAD_NETPATH = 53,
            ERROR_NO_NETWORK = 1222,
            ERROR_INVALID_HANDLE_STATE = 1609,
            ERROR_EXTENDED_ERROR = 1208,
            ERROR_DEVICE_ALREADY_REMEMBERED = 1202,
            ERROR_NO_NET_OR_BAD_PATH = 1203
        }

        public enum RESOURCE_SCOPE
        {
            RESOURCE_CONNECTED = 1,
            RESOURCE_GLOBALNET = 2,
            RESOURCE_REMEMBERED = 3,
            RESOURCE_RECENT = 4,
            RESOURCE_CONTEXT = 5
        }

        public enum RESOURCE_TYPE
        {
            RESOURCETYPE_ANY = 0,
            RESOURCETYPE_DISK = 1,
            RESOURCETYPE_PRINT = 2,
            RESOURCETYPE_RESERVED = 8,
        }

        public enum RESOURCE_USAGE
        {
            RESOURCEUSAGE_CONNECTABLE = 1,
            RESOURCEUSAGE_CONTAINER = 2,
            RESOURCEUSAGE_NOLOCALDEVICE = 4,
            RESOURCEUSAGE_SIBLING = 8,
            RESOURCEUSAGE_ATTACHED = 16,
            RESOURCEUSAGE_ALL = (RESOURCEUSAGE_CONNECTABLE | RESOURCEUSAGE_CONTAINER | RESOURCEUSAGE_ATTACHED),
        }

        public enum RESOURCE_DISPLAYTYPE
        {
            RESOURCEDISPLAYTYPE_GENERIC = 0,
            RESOURCEDISPLAYTYPE_DOMAIN = 1,
            RESOURCEDISPLAYTYPE_SERVER = 2,
            RESOURCEDISPLAYTYPE_SHARE = 3,
            RESOURCEDISPLAYTYPE_FILE = 4,
            RESOURCEDISPLAYTYPE_GROUP = 5,
            RESOURCEDISPLAYTYPE_NETWORK = 6,
            RESOURCEDISPLAYTYPE_ROOT = 7,
            RESOURCEDISPLAYTYPE_SHAREADMIN = 8,
            RESOURCEDISPLAYTYPE_DIRECTORY = 9,
            RESOURCEDISPLAYTYPE_TREE = 10,
            RESOURCEDISPLAYTYPE_NDSCONTAINER = 11
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NETRESOURCE
        {
            public RESOURCE_SCOPE dwScope;
            public RESOURCE_TYPE dwType;
            public RESOURCE_DISPLAYTYPE dwDisplayType;
            public RESOURCE_USAGE dwUsage;

            [MarshalAs(UnmanagedType.LPStr)]
            public string lpLocalName;

            [MarshalAs(UnmanagedType.LPStr)]
            public string lpRemoteName;

            [MarshalAs(UnmanagedType.LPStr)]
            public string lpComment;

            [MarshalAs(UnmanagedType.LPStr)]
            public string lpProvider;
        }


        [DllImport("mpr.dll")]
        public static extern int WNetAddConnection2A(NETRESOURCE[] lpNetResource, string lpPassword, string lpUserName, int dwFlags);

        [DllImport("mpr.dll")]
        public static extern int WNetCancelConnection2A(string sharename, int dwFlags, int fForce);


        public static int Connect(string remotePath, string localPath, string username, string password)
        {
            NETRESOURCE[] share_driver = new NETRESOURCE[1];
            share_driver[0].dwScope = RESOURCE_SCOPE.RESOURCE_GLOBALNET;
            share_driver[0].dwType = RESOURCE_TYPE.RESOURCETYPE_DISK;
            share_driver[0].dwDisplayType = RESOURCE_DISPLAYTYPE.RESOURCEDISPLAYTYPE_SHARE;
            share_driver[0].dwUsage = RESOURCE_USAGE.RESOURCEUSAGE_CONNECTABLE;
            share_driver[0].lpLocalName = localPath;
            share_driver[0].lpRemoteName = remotePath;

            Disconnect(localPath);
            int ret = WNetAddConnection2A(share_driver, password, username, 1);

            return ret;
        }

        public static int Disconnect(string localpath)
        {
            return WNetCancelConnection2A(localpath, 1, 1);
        }
    }

    public class HunterUtilities
    {
        public HunterUtilities()
        {

        }

        public static string GetIPFromPath(string path)
        {
            Regex reg = new Regex("\\\\\\\\(?<ip>((\\d){1,3}\\.){3}(\\d){1,3})\\\\");
            Match m = null;
            if ( (m = reg.Match(path)).Success )
            {
                return m.Result("${ip}");
            }
            return null;
        }
        /// <summary>
        /// 返回一个合法的文件名，不合法的字符用'-'代替
        /// </summary>
        /// <param name="filename">原始文件名</param>
        /// <returns>合法化后的文件名</returns>
        public static string LegalizeFile(string filename)
        {
            string result = filename;
            
            foreach (char iv in Path.GetInvalidFileNameChars())
            {
                result = result.Replace(iv.ToString(), "-");
            }

            return result;
        }


        public static string GetMD5Hash(string path)
        {
            FileStream get_file = null;
            System.Security.Cryptography.MD5CryptoServiceProvider get_md5 = null;
            try
            {
                get_file = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
                get_md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
                byte[] hash_byte = get_md5.ComputeHash(get_file);
                string result = System.BitConverter.ToString(hash_byte);
                result = result.Replace("-", "");
                get_md5.Clear();
                get_file.Close();
                return result;
            }
            catch (Exception)
            {
                try
                {
                    get_md5.Clear();
                    get_file.Close();
                }
                catch { throw; };
                //mHunterConsole.WriteException(e);
                throw;
                //return string.Empty;
            }
        }

        /// <summary>
        /// 从UriResource获得合适的保存文件名
        /// </summary>
        /// <param name="pInfo"></param>
        /// <param name="strategy"></param>
        /// <param name="uriRes"></param>
        /// <returns></returns>
        public static string GetFilenameFromUrl(ProjectInfo pInfo, Strategy strategy, UriResource uriRes)
        {
            /* 得到文件名的3套策略：
            * 1、获得超链接的标题
            * 2、如果超链接的标题有乱码，则获得网络路径
            * 3、如果不能正常匹配到网络路径，则以时间命名（一般不会出现这种情况）
            */

            //删除某些标记，并Html解码，然后合法化用户名

            string filepath, basicName = HunterUtilities.LegalizeFile(
                WebUtility.HtmlDecode(uriRes.Text)
                );

            if (strategy.HasConfusionString(basicName))   //如果含有违禁词（如乱码）
            {
                Regex regFilename = new Regex("^http://(.*)/(?<filename>.*)\\." + strategy.Filetype);
                Match mFilename = regFilename.Match(uriRes.Url);    //获得网络路径

                if (mFilename.Success)
                {
                    basicName = mFilename.Result("${filename}");
                }
                else
                {
                    basicName = DateTime.Now.ToFileTime().ToString();
                }
            }

            //文件判定重复措施
            filepath = Path.Combine(pInfo.filefolder , basicName + "." + strategy.Filetype);
            int i = 0;
            bool keepOriginal = true; string tempName = null;
            while (File.Exists(filepath))
            {
                i++;
                keepOriginal = false;
                tempName = basicName + "_" + i.ToString();
                filepath = Path.Combine(pInfo.filefolder , tempName + "." + strategy.Filetype);
            };
            if (!keepOriginal)
                basicName = tempName;   //出现重名，则在后面添加编号

            filepath = Path.Combine(pInfo.filefolder , basicName + "." + strategy.Filetype);

            return filepath;
            
        }

        public static void WriteDownloadFileXML(String url, String keyword, String specimenname, String language, String savepath)
        {
            try
            {
                XDocument xdoc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"));
                XElement xroot = new XElement("root");
                xdoc.Add(xroot);
                xroot.Add(new XElement("file", specimenname),
                    new XElement("url", url),
                    new XElement("keyword", keyword),
                    new XElement("search_language", language)
                    );
                xdoc.Save(savepath);
            }
            catch
            {
                throw;
            }
        }

        public static String AbsolutePath(String rel)
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            return rel.Contains(baseDir) ? rel : Path.Combine(baseDir, rel);
        }
    }


}
