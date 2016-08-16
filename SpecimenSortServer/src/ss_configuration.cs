using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.IO;
using MySql.Data.MySqlClient;
using System.Diagnostics;

namespace SpecimenSort
{
    public class Configuration
    {
        public enum Mode { Monitor, Once };
        static readonly String s_root = "specimen"      ;
        static readonly String s_action = "action"      ;
        static readonly String a_mode = "mode"          ;
        static readonly String a_process = "process"   ;
        static readonly String a_interval = "interval"  ;
        static readonly String s_database = "database"  ;
        static readonly String a_ip = "ip"              ;
        static readonly String a_usr = "usr"            ;
        static readonly String a_pwd = "pwd"            ;
        static readonly String a_db = "db"              ;
        static readonly String s_dir = "directory"      ;
        static readonly String a_source = "source"      ;
        static readonly String a_dest = "dest"          ;
        static readonly String a_cache = "cache"        ;
        static readonly String a_sharepath = "sharepath";
        static readonly String s_type = "type"          ;
        static readonly String s_match = "match"        ;
        static readonly String a_ext = "ext"            ;
        static readonly String a_ksotype = "ksotype"    ;

        Mode mRunningMode;
        String mMode;
        Int32 mInterval;
        Int32 mProcess;
        String mConfigPath;
        String mDir_src;
        String mDir_dest;
        String mIp;
        String mUsr;
        String mPwd;
        String mDatabase;
        String mSharePath;
        Int32 mCache;
        Dictionary<String, String> mDicType = new Dictionary<String, String>();
        Log mlog;
        MySqlDBConnection mConnection;

        public Mode RunningMode { get { return mRunningMode; } set { value = mRunningMode ; }}
        public Int32 Interval { get { return mInterval; } }
        public String IP { get { return mIp; } }
        public String User { get { return mUsr; } }
        public String Password { get { return mPwd; } }
        public Int32 Cache { get { return mCache; } }
        public String ConfigPath { get { return mConfigPath; } }
        public String DirectorySource { get { return mDir_src; } }
        public String DirectoryDest { get { return mDir_dest; } }
        public Dictionary<String, String> DictionaryKsotype { get { return mDicType; } }
        public MySqlDBConnection MySqlDbConnection { get { return mConnection; } }
        public Int32 Process { get { return mProcess; } }
        public String SharePath { get { return mSharePath; } }

        public Configuration(String _configpath, Log log)
        {
            mConfigPath = _configpath ;
            mlog = log;
        }

        public bool LoadConfig(out bool exitApplication)
        {
            exitApplication = false;
            try
            {
                if (!File.Exists(mConfigPath))
                {
                    CreateConfig();
                    exitApplication = true;
                    return true;
                }

                XDocument x_config = XDocument.Load(mConfigPath);
                XElement x_root = x_config.Element(s_root);
                mMode = x_root.Element(s_action).Attribute(a_mode).Value;
                mInterval = int.Parse(x_root.Element(s_action).Attribute(a_interval).Value);
                mProcess = int.Parse(x_root.Element(s_action).Attribute(a_process).Value);

                mRunningMode = mMode == "monitor" ? Mode.Monitor : Mode.Once;

                mIp = x_root.Element(s_database).Attribute(a_ip).Value;
                mUsr = x_root.Element(s_database).Attribute(a_usr).Value;
                mPwd = x_root.Element(s_database).Attribute(a_pwd).Value;
                mDatabase = x_root.Element(s_database).Attribute(a_db).Value;
                mDir_src = x_root.Element(s_dir).Attribute(a_source).Value;
                mDir_dest = x_root.Element(s_dir).Attribute(a_dest).Value;
                mSharePath = x_root.Element(s_dir).Attribute(a_sharepath).Value;
                mCache = int.Parse(x_root.Element(s_dir).Attribute(a_cache).Value);

                foreach (var match in x_root.Element(s_type).Elements(s_match))
                {
                    if (!mDicType.ContainsKey(match.Attribute(a_ext).Value))
                    {
                        mDicType.Add(match.Attribute(a_ext).Value.ToLower(), match.Attribute(a_ksotype).Value);
                    }
                }

                mConnection = new MySqlDBConnection(mIp, mDatabase, mUsr, mPwd, AppLogHelper.Log);

                if (!mConnection.TestOpenClose())
                {
                    return false;
                }

                return true;
            }
            catch (Exception e)
            {
                AppLogHelper.Log_i(String.Format("{0, 76}", new String('-', 36)));
                mlog.Log_e("Parse Config File Failed: " + mConfigPath + " -> " + e.Message);
                return false;
            }
        }

        public void CreateConfig()
        {
            try
            {
                XDocument x_config = new XDocument(new XDeclaration("1.0", "utf-8", "yes"));
                x_config.Add(
                        new XElement(s_root,
                            new XElement(s_action, new XAttribute(a_mode, "monitor"), new XAttribute(a_interval, "1000"), new XAttribute(a_process, "4")),
                            new XElement(s_database, new XAttribute(a_ip, "127.0.0.1"), new XAttribute(a_usr, "root"), new XAttribute ( a_pwd, "pwd" ), new XAttribute (a_db, "db_specimen") ),
                            new XElement(s_dir, new XAttribute(a_source, "D:\\"), new XAttribute(a_dest, "D:\\"), new XAttribute (a_cache, "500" ), new XAttribute(a_sharepath, "您的共享路径") ),
                            new XElement(s_type,
                                new XElement(s_match, new XAttribute(a_ext, ".doc"), new XAttribute(a_ksotype, "wps"))
                            )
                        )
                    );
                x_config.Save(mConfigPath);
                mlog.Log_i ( "This is first time you run this application. We just made a config file for you. Please modify it and restart this application again." );
                Console.ReadKey (true);
                
            }
            catch (Exception e)
            {
                AppLogHelper.Log_i(String.Format("{0, 76}", new String('-', 36)));
                mlog.Log_e("Create Config File Failed: " + mConfigPath + " -> " + e.Message);
            }
        }

    }
}
