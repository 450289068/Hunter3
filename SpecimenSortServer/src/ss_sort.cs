using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using MySql.Data.MySqlClient;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text.RegularExpressions;
using System.Xml.Linq;
namespace SpecimenSort
{
    public class SpecimentSorter
    {
        static readonly String sTableFileInfo = "tb_file_infos";
        static readonly String sStrRegIpHost = @"(?<ip>(\d){0,3}\.(\d){0,3}\.(\d){0,3}\.(\d){0,3})(\s)*\((?<host>(.)*?)\)";
        static readonly Regex RegIpHost = new Regex ( sStrRegIpHost );
        public static String HOST_NAME
        {
            get { return Dns.GetHostName () ; }
        }

        public static string IP_ADDRESS
        {
            get
            {
                IPHostEntry IpEntry = Dns.GetHostEntry(HOST_NAME);
                for (int i = 0; i < IpEntry.AddressList.Length; i++)
                {
                    if (IpEntry.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
                    {
                        return IpEntry.AddressList[i].ToString();
                    }
                }
                return IpEntry.AddressList[0].ToString();
            }

        }

        String path_exe = AppDomain.CurrentDomain.BaseDirectory;
        Log mLog;
        Configuration mConfig;

        public Log Log { get { return mLog ; } }

        public SpecimentSorter()
        {
            mLog = new Log(path_exe);
            AppLogHelper.Log = mLog;
            mConfig = new Configuration ( Path.Combine (path_exe + "specimen.xml") , Log );
        }

        public void StartSort()
        {
            bool ExitApplication;
            mLog.Log_i ( "正在启动样张服务器 3.0 - By Froser: " + DateTime.Now.ToString ( "yyyy-MM-dd HH:mm:ss" ) );
            if ( !mConfig.LoadConfig(out ExitApplication))
            {
                mLog.Log_e("Open Database Failed. ");
                Console.ReadKey(true);
                return;
            }

            if (ExitApplication) return;

            string currentMode = mConfig.RunningMode == Configuration.Mode.Monitor ? "监控模式，监控间隔为 " + mConfig.Interval + "ms" : "运行一次";
            mLog.Log_i("运行模式：" + currentMode);
            mLog.Log_i("分析文件夹：" + mConfig.DirectorySource);
            while (true)
            {
                ParseDirectory();
                if (mConfig.RunningMode != Configuration.Mode.Monitor)
                    break;
                Thread.Sleep(mConfig.Interval);
            }
//             if (mConfig != null && mConfig.MySqlDbConnection != null)
//                 mConfig.MySqlDbConnection.Close();
        }

        public void ParseDirectory()
        {
            FileInfo[] files = null;
            try
            {
                if ( ! Directory.Exists ( mConfig.DirectorySource) ) Directory.CreateDirectory ( mConfig.DirectorySource );
                files = new DirectoryInfo(mConfig.DirectorySource).GetFiles ("*.*", SearchOption.AllDirectories);
            } catch (Exception e){
                AppLogHelper.Log_i(String.Format("{0, 76}", new String('-', 36)));
                mLog.Log_e("Parse Directory Failed: (" + mConfig.DirectorySource + ")" + e.Message);
            }
            try{
                int threadNum = 0;
                List<String> Md5List = new List<String>();
                foreach (var file in files)
                {
                    while (threadNum >= mConfig.Process)
                    {
                        Thread.Sleep(100);
                    }
                    Thread t = new Thread(new ParameterizedThreadStart(( object fullname) =>
                    {
                        try
                        {
                            FileOperate(fullname.ToString(), Md5List);
                        }
                        catch { }
                        finally
                        {
                            threadNum--;
                        }
                    }));
                    threadNum++;
                    t.Start(file.FullName);
                }
            }
            catch (Exception e)
            { 
                AppLogHelper.Log_i(String.Format("{0, 76}", new String('-', 36)));
                mLog.Log_e("Parse Directory Failed: " + e.Message);
            }
        }

        private String GetKsoType(String ext)
        {
            return (mConfig.DictionaryKsotype.ContainsKey(ext) ? mConfig.DictionaryKsotype[ext] : "other");
        }

        /// <summary>
        /// 对文件进行操作，按照规则放进相应的文件夹
        /// </summary>
        private void FileOperate(String filename, List<String> Md5List, bool loop = false)
        {
            try
            {
                if (Path.GetFileName(filename).StartsWith("$__")) return;
                Int32 folderID;
                String date = DateTime.Today.ToString("yyyyMMdd");
                String ext = Path.GetExtension(filename).ToLower();
                if (!mConfig.DictionaryKsotype.ContainsKey(ext)) return;

                if (ext.Trim() == "") ext = "-";

                String ksotype = GetKsoType(ext);

                String path = Path.Combine(mConfig.DirectoryDest, date, ksotype, ext);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                int folderCount = Directory.GetDirectories(path).Length;
                //Folder start with 1
                String finalPath = Path.Combine(path, "1");
                if (folderCount == 0)
                {
                    Directory.CreateDirectory(finalPath);
                }
                else
                {
                    folderID = folderCount;
                    finalPath = Path.Combine(path, folderID.ToString());
                    if (Directory.GetFiles(finalPath).Length >= mConfig.Cache)
                    {
                        folderID++;
                        finalPath = Path.Combine(path, folderID.ToString());
                        Directory.CreateDirectory(finalPath);
                    }

                }

                //思路：如果前面是MD5码，则去掉
                String fileMd5 = GetFileMD5(filename);
                String xml_filename = "";

                String originalFilename = Path.GetFileNameWithoutExtension(filename);
                String finalName;
                if (originalFilename.Length > 33 && originalFilename.Substring(0, 33) == fileMd5 + "_")
                {
                    finalName = originalFilename.Substring(33);
                }
                else
                {
                    finalName = originalFilename;
                }
                xml_filename = Path.Combine(Path.GetDirectoryName(filename), "$__" + Path.GetFileName(finalName)) + Path.GetExtension(filename) + ".xml";

                if (finalName.Length > 20)
                {
                    finalName = finalName.Substring(0, 20);
                }

                if (!IsDuplicateByMd5(fileMd5) && !Md5List.Contains(fileMd5))
                {
                    Md5List.Add(fileMd5);
                    if (Md5List.Count > 511)
                    {
                        lock (this)
                        {
                            try
                            {
                                Md5List.RemoveAt(0);
                                //mLog.Log_i("移除MD5列表底层项。");
                            }
                            catch { }
                        }
                    }
                    int num = 0;
                    // 汉字的正则表达式匹配 [\\u4e00-\\u9fa5]
                    Regex legalChar = new Regex("[A-Za-z0-9]|[\\u4e00-\\u9fa5]");
                    StringBuilder _sb = new StringBuilder();

                    foreach (char c in finalName)
                    {
                        if (legalChar.IsMatch(c.ToString()))
                            _sb.Append(c);
                    }

                    finalName = Path.Combine(finalPath, _sb.ToString() + Path.GetExtension(filename));

                    string _finalName;
                    if (File.Exists(finalName))
                    {
                        do
                        {
                            num++;
                            _finalName = Path.Combine(finalPath, Path.GetFileNameWithoutExtension(finalName) + "_" + num + Path.GetExtension(finalName));
                        } while (File.Exists(_finalName));
                        finalName = _finalName;
                    }

                    String ip = null;
                    String host = null;
                    Match matchIpHost = RegIpHost.Match(filename);
                    if (matchIpHost.Success)
                    {
                        ip = matchIpHost.Result("${ip}");
                        host = matchIpHost.Result("${host}");
                    }

                    ip = (ip == null || ip.Trim() == "") ? IP_ADDRESS : ip;
                    host = (host == null || host.Trim() == "") ? HOST_NAME : host;

                    String link = "", keyword = "", language = "";
                    try
                    {
                        XDocument xdoc = XDocument.Load(xml_filename);
                        XElement xroot = xdoc.Element("root");
                        link = xroot.Element("url").Value;
                        keyword = xroot.Element("keyword").Value;
                        if (xroot.Element("search_language") != null)
                        {
                            language = xroot.Element("search_language").Value;
                        }
                    }
                    catch { }
                    try
                    {
                        string replacePath = mConfig.SharePath;
                        if (finalName.EndsWith(@"\"))
                        {
                            if (!replacePath.EndsWith(@"\")) replacePath += @"\";
                        }
                        string sharepath = finalName.Replace(mConfig.DirectoryDest, replacePath);
                        InsertInformation(ext, sharepath, fileMd5, ip, host, link, keyword, language);
                        File.Delete(xml_filename);
                    }
                    catch (Exception ex)
                    { Log.Log_e(ex.Message); }

                    //如果是监视模式，则剪切
                    if (mConfig.RunningMode == Configuration.Mode.Once)
                    {
                        File.Copy(filename, finalName, true);
                        AppLogHelper.Log_o(" File Copied And Inserted -> " + filename + " -> " + finalName);
                    }
                    else
                    {
                        File.Move(filename, finalName);
                        AppLogHelper.Log_o(" File Moved And Inserted -> " + filename + " -> " + finalName);
                    }
                }
                else
                {
                    if (mConfig.RunningMode == Configuration.Mode.Monitor)
                    {
                        File.Delete(filename);
                        File.Delete(xml_filename);
                        AppLogHelper.Log_w(" Duplicated File And Deleted-> " + filename);
                    }
                    else
                    {
                        AppLogHelper.Log_w(" Duplicated File -> " + filename);
                    }
                }
                
            }
            catch (InvalidOperationException)
            {
                //再试一次
                if (!loop)
                    FileOperate(filename, Md5List, true);
            }
            catch (Exception e)
            {
                mLog.Log_e("Operation Failed: " + e.Message);
            }
        }

        private bool IsDuplicateByMd5(string md5)
        {
            String str_sql_cmd = "SELECT COUNT(*) FROM " + sTableFileInfo + " WHERE file_md5 = @file_md5";
            MySqlParameter msp_md5 = new MySqlParameter("@file_md5", MySqlDbType.VarChar, 32);
            msp_md5.Value = md5;

            MySqlConnection c = mConfig.MySqlDbConnection.NewConnectionInstance;
            c.Open();
            MySqlCommand mysql_cmd = new MySqlCommand(str_sql_cmd, c);
            mysql_cmd.Parameters.Add(msp_md5);
            object queryResult = mysql_cmd.ExecuteScalar();
            c.Close();
            if (queryResult.ToString() != "0") return true;
            return false;
        }

        private void InsertInformation(string ext, string sharepath, string md5, string ipaddress, string hostname, string link, string keyword, string search_language)
        {
            String str_sql_command = "INSERT INTO " + sTableFileInfo + " ( "  +
                                    "file_ksotype,          "   +
                                    "file_type,             "   +
                                    "file_keyword,          "   +
                                    "file_link,             "   +
                                    "file_path,             "   +
                                    "file_name,             "   +
                                    "file_ip,               "   +
                                    "file_host,             "   +
                                    "file_md5,              "   +
                                    "file_search_lang,              " +
                                    "file_date              "   +
                                    ") VALUES (             "   +
                                    "@file_ksotype,         "   +
                                    "@file_type,            " +
                                    "@file_keyword,         " +
                                    "@file_link,            " +
                                    "@file_path,            " +
                                    "@file_name,            " +
                                    "@file_ip,              " +
                                    "@file_host,            " +
                                    "@file_md5,             " +
                                    "@file_search_lang, " +
                                    "@file_date             " +
                                    ")";
            MySqlParameter msp_ksotype  = new MySqlParameter("@file_ksotype", MySqlDbType.VarChar, 32);
            MySqlParameter msp_type     = new MySqlParameter("@file_type", MySqlDbType.VarChar, 32);
            MySqlParameter msp_path     = new MySqlParameter("@file_path", MySqlDbType.Text);
            MySqlParameter msp_name     = new MySqlParameter("@file_name", MySqlDbType.Text);
            MySqlParameter msp_ip       = new MySqlParameter("@file_ip", MySqlDbType.VarChar, 12);
            MySqlParameter msp_host     = new MySqlParameter("@file_host", MySqlDbType.VarChar, 256);
            MySqlParameter msp_md5      = new MySqlParameter("@file_md5", MySqlDbType.VarChar, 32);
            MySqlParameter msp_search_language = new MySqlParameter("@file_search_lang", MySqlDbType.VarChar, 32);
            MySqlParameter msp_date     = new MySqlParameter("@file_date", MySqlDbType.VarChar, 8);
            MySqlParameter msp_keyword  = new MySqlParameter("@file_keyword", MySqlDbType.VarChar);
            MySqlParameter msp_link     = new MySqlParameter("@file_link", MySqlDbType.Text);

            msp_ksotype.Value = GetKsoType( ext );
            msp_type.Value = ext;
            msp_path.Value = sharepath;
            msp_name.Value = Path.GetFileName(sharepath);
            msp_ip.Value = ipaddress;
            msp_host.Value = hostname;
            msp_keyword.Value = keyword;
            msp_link.Value = link;
            msp_md5.Value = md5;
            msp_search_language.Value = search_language;
            msp_date.Value = DateTime.Now.ToString ("yyyyMMdd-HHmmss");

            MySqlConnection c = mConfig.MySqlDbConnection.NewConnectionInstance;
            c.Open ();
            MySqlCommand mysql_cmd = new MySqlCommand(str_sql_command, c);
            mysql_cmd.Parameters.Add ( msp_ksotype );
            mysql_cmd.Parameters.Add ( msp_type    );
            mysql_cmd.Parameters.Add ( msp_path    );
            mysql_cmd.Parameters.Add ( msp_name    );
            mysql_cmd.Parameters.Add ( msp_ip      );
            mysql_cmd.Parameters.Add ( msp_host    );
            mysql_cmd.Parameters.Add ( msp_md5     );
            mysql_cmd.Parameters.Add ( msp_search_language);
            mysql_cmd.Parameters.Add ( msp_date    );
            mysql_cmd.Parameters.Add ( msp_keyword );
            mysql_cmd.Parameters.Add ( msp_link     );

            mysql_cmd.ExecuteNonQuery();
            c.Close();
        }

        private string GetFileMD5(string filename)
        {
            FileStream get_file = null;
            System.Security.Cryptography.MD5CryptoServiceProvider get_md5 = null;
            try
            {
                get_file = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
                get_md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
                byte[] hash_byte = get_md5.ComputeHash(get_file);
                string result = System.BitConverter.ToString(hash_byte);
                result = result.Replace("-", "");
                return result;
            }
            catch (Exception e)
            {
                mLog.Log_e("Get File MD5 Failed: " + e.Message);
                return string.Empty;
            }
            finally
            {
                get_md5.Clear();
                get_file.Close();
            }
        }

    }
}
