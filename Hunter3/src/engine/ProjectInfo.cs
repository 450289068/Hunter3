using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Net;
using System.Net.Sockets;
using Hunter3.Strategies;
using Hunter3.Data;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Text;
using System.Windows.Forms;
namespace Hunter3
{
    [XmlRoot("HunterProject")]
    public class ProjectInfo
    {
        #region 本地项目设置
        [XmlIgnore]
        public HunterConsole mHunterConsole;

        [CategoryAttribute("高级设置"), DescriptionAttribute("下载并行线程数")]
        public string threadnum { get; set; }
        [CategoryAttribute("常规设置"), DescriptionAttribute("上传用户名")]
        public string name { get; set; }                     //上传人的名字
        [CategoryAttribute("常规设置"), DescriptionAttribute("模式为local或network，表示是本地模式还是网络模式(访问数据库排重、上传)")]
        public HunterMode mode { get; set; }                 //模式类型：local, network
       
        [CategoryAttribute("常规设置"), DescriptionAttribute("设置共享路径")]
        public string share_remote_path { get; set; }
        [CategoryAttribute("常规设置"), DescriptionAttribute("共享文件夹的账号")]
        public string share_usr { get; set; }
        [CategoryAttribute("常规设置"), DescriptionAttribute("共享文件夹的密码")]
        public string share_pwd { get; set; }
        [CategoryAttribute("常规设置"), DescriptionAttribute("单个文件下载超时时间(毫秒)")]
        public int timeout { get; set; }
        [CategoryAttribute("常规设置"), DescriptionAttribute("本地XML数据库的路径"), EditorAttribute(typeof(PropertyGridSaveFileItem), typeof(System.Drawing.Design.UITypeEditor))]
        public string database { get; set; }              //HunterXML数据库路径
        [CategoryAttribute("常规设置"), DescriptionAttribute("下载的文件的本地保存路径。在远程模式下，文件上传后会从本地删除这个文件"), EditorAttribute(typeof(PropertyGridFolderItem), typeof(System.Drawing.Design.UITypeEditor))]
        public string filefolder { get; set; }              //存放样张文件夹，或网络模式下网络连接失败的文件夹
        [CategoryAttribute("常规设置"), DescriptionAttribute("搜索页码，从0开始")]
        public int index { get; set; }                      //起始的搜索页码
        [CategoryAttribute("常规设置"), DescriptionAttribute("搜索关键字序号，从0开始")]
        public int keywords { get; set; }                   //起始的关键字编号
        [CategoryAttribute("常规设置"), DescriptionAttribute("下载文件类型")]
        public string filetype { get; set; }                     //下载的文件类型

        [CategoryAttribute("高级设置"), DescriptionAttribute("搜索时每一个自动生成的关键字后都会添加此后缀进行搜索。如在谷歌中设置本属性为&&lr=lang_en可以下载英文的文件。")]
        public string dictionary_affix { get; set; }
        [CategoryAttribute("常规设置"), DescriptionAttribute("辞典文件路径"), EditorAttribute(typeof(PropertyGridOpenFileItem), typeof(System.Drawing.Design.UITypeEditor))]
        public string dictionary { get; set; }               //辞典文件路径
        [CategoryAttribute("高级设置"), DescriptionAttribute("搜索的语言，将会被记录进数据库，便于搜索特定语言的文件。值得注意的是，这仅仅是主观判断，并不一定准确。不指定语言，请选择none。")]
        public Language search_language { get; set; }

        [BrowsableAttribute (false)]
        public string dic_md5 { get; set; }                      //起始的搜索页码
        [BrowsableAttribute(false)]
        public string first { get; set; }

        [CategoryAttribute("高级设置"), DescriptionAttribute("是否从db_ip指定的数据库服务器中获取关键字进行爬虫任务。若为true，则放弃使用本地辞典文件，辞典信息全部从数据库获取。")]
        public bool remote_dictionary { get; set; }
        [CategoryAttribute("高级设置"), DescriptionAttribute("MySQL数据库的IP地址")]
        public string db_ip { get; set; }
        [CategoryAttribute("高级设置"), DescriptionAttribute("MySQL数据库的端口")]
        public string db_port { get; set; }
        [CategoryAttribute("高级设置"), DescriptionAttribute("MySQL数据库的用户名")]
        public string db_username { get; set; }
        [CategoryAttribute("高级设置"), DescriptionAttribute("MySQL数据库密码")]
        public string db_pwd { get; set; }
        [CategoryAttribute("高级设置"), DescriptionAttribute("MySQL数据库名，一般为db_specimen，无需修改")]
        public string db_dbname { get; set; }
        [CategoryAttribute("高级设置"), DescriptionAttribute("MySQL辞典表，默认为空。请联系服务器维护人员获得辞典表的列表，若输入错误则无法从服务器获取关键字。")]
        public string db_dictionary_table { get; set; }

        [XmlIgnore]
        public string strategyPath;                 //策略文件路径
        [XmlIgnore]
        [BrowsableAttribute(false)]
        public string projectPath { get; set;}                  //本项目文件路径

        public enum HunterMode { local, network };
        public enum Language { none, en_us, zh_cn, zh_tw, ja_jp, fr_fr};
        private HunterMode _currentMode;
        [BrowsableAttribute(false)]
        public HunterMode CurrentMode
        {
            get
            {
                return _currentMode;
            }

            set
            {
                if (mHunterConsole != null)
                {
                    mHunterConsole.ModeChange(ModeName(value));
                }
                _currentMode = value;
            }
        }

        [XmlIgnore]
        public Strategy strategy;
        [XmlIgnore]
        public HunterDatabaseHelper DatabaseHelper;

        public string ConfigInformation()
        {
            return "配置信息：" + Environment.NewLine +
                "下载超时时间：" + timeout + Environment.NewLine +
                "数据库文件：" + database + Environment.NewLine +
                "辞典文件：" + dictionary + Environment.NewLine +
                "保存地址：" + filefolder;
        }

        [XmlIgnore]
        public static string IP_ADDRESS
        {
            get
            {
                IPHostEntry IpEntry = Dns.GetHostEntry(Dns.GetHostName());
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
        #endregion

        private bool IPCConnected = false;

        public ProjectInfo(HunterConsole c, string pjPath, string _strategyPath)
        {
            mHunterConsole = c;
            projectPath = pjPath;
            strategyPath = _strategyPath;
        }

        public ProjectInfo()
        {
            threadnum = "16";
            name = "DemoUser";
            CurrentMode = HunterMode.local;
            timeout = 120;
            database = "D:\\database.xml";
            filefolder = "D:\\Hunter\\";
            db_ip = "10.20.175.199";
            db_port = "3306";
            db_username = "ktk";
            db_pwd = "+5688king";
            db_dbname = "db_specimen";
            dic_md5 = "";
            index = 0;
            keywords = 0;
            filetype = "doc";
            dictionary = "D:\\dictionary.dic";
            first = "";
            remote_dictionary = true;
            dictionary_affix = "";
            search_language = Language.none;
            db_dictionary_table = "";
            share_remote_path = @"\\10.20.172.203\Specimen";
            share_usr = "administrator";
            share_pwd = "kingsoft";
        }

        public void SaveProject(String path)
        {
            XmlSerializer ser = new XmlSerializer(typeof(ProjectInfo));
            
            FileStream f = new FileStream(path , FileMode.Create, FileAccess.Write);
            StreamWriter sw = new StreamWriter(f, Encoding.UTF8);
            ser.Serialize(sw, this);
            projectPath = path;
            f.Close();
        }

        public static ProjectInfo LoadProject(HunterConsole c, string pjPath, string _strategyPath, bool loadKeywords)
        {
            XmlSerializer ser = new XmlSerializer(typeof(ProjectInfo));
            FileStream f = new FileStream(pjPath, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(f, Encoding.UTF8);
            ProjectInfo result= (ProjectInfo)ser.Deserialize(sr);
            result.mHunterConsole = c;
            result.projectPath = pjPath;
            result.strategyPath = _strategyPath;
            result.strategy = new Strategy(result, loadKeywords);     //一个百度搜索策略
            result.CurrentMode = result.mode;
            f.Close();
            
            result.DatabaseHelper = new HunterDatabaseHelper(result);
            return result;
        }

        public void CreateIPC()
        {
            if (!IPCConnected)
            {
                int state = NetworkConnection.Connect("\\\\" + HunterUtilities.GetIPFromPath(share_remote_path), "", share_usr, share_pwd);
                if (state != (int)NetworkConnection.ERROR_ID.ERROR_SUCCESS)
                {
                    MessageBox.Show(String.Format("建立IPC连接出错，返回出错码为{0}", state), "Hunter 3", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    IPCConnected = true;
                }
            }
        }

        public void DisconnectIPC()
        {
            int state = NetworkConnection.Disconnect("\\\\" + HunterUtilities.GetIPFromPath(share_remote_path));
        }

        public void SaveDicMD5()
        {
            lock (this)
            {
                dic_md5 = HunterUtilities.GetMD5Hash(dictionary);
                SaveProject(projectPath);
            }
        }

        public string LoadlastDicMD5()
        {
            return dic_md5;
        }

        public string ModeName(HunterMode m)
        {
            switch (m)
            {
                case (HunterMode.local):
                    return "本地模式";
                case (HunterMode.network):
                    return "网络模式";
            }

            return "未知";
        }
    }


}
