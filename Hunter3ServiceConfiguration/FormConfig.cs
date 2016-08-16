using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Diagnostics;
using System.Xml.Linq;
using System.IO;

namespace Hunter3ServerConfiguration
{
    public partial class FormConfig : Form
    {
        public FormConfig()
        {
            InitializeComponent();
            lbVersion.Text = Application.ProductVersion;
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            btnCreate.Enabled = false;

            String tb_keywords = @"CREATE TABLE IF NOT EXISTS `tb_keywords` (
  `key_id` int(11) NOT NULL AUTO_INCREMENT,
  `key_value` varchar(32) NOT NULL,
  `key_typeandengine` text DEFAULT NULL,
  PRIMARY KEY (`key_id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;";
            String tb_file_infos = @"CREATE TABLE IF NOT EXISTS `tb_file_infos` (
  `file_id` int(11) NOT NULL AUTO_INCREMENT,
  `file_ksotype` varchar(32) DEFAULT NULL,
  `file_type` varchar(32) DEFAULT NULL,
  `file_keyword` varchar(128) DEFAULT NULL,
  `file_link` text,
  `file_path` text,
  `file_name` text,
  `file_ip` varchar(16) DEFAULT NULL,
  `file_host` varchar(256) DEFAULT NULL,
  `file_md5` varchar(32) DEFAULT NULL,
  `file_search_lang` varchar(32) DEFAULT NULL,
  `file_date` varchar(17) DEFAULT NULL,
  `file_available` varchar(8) DEFAULT NULL,
  `file_reserved_1` varchar(32) DEFAULT NULL,
  `file_reserved_2` varchar(32) DEFAULT NULL,
  `file_reserved_3` varchar(32) DEFAULT NULL,
  `file_reserved_4` varchar(32) DEFAULT NULL,
  PRIMARY KEY (`file_id`),
  KEY `index_md5` (`file_md5`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;";

            String str_server = tbIP.Text;
            String str_database = tbDbname.Text;
            String str_uid = tbDbAccount.Text;
            String str_password = tbPwd.Text;
            String str_cache = tbCache.Text;
            String str_dest = tbDest.Text;
            String str_save = tbSavepath.Text;
            String str_connection = String.Format("SERVER={0};DATABASE={1};UID={2};PASSWORD={3};", str_server, str_database, str_uid, str_password);
            try
            {
                MySqlConnection con = new MySqlConnection(str_connection);
                MySqlCommand cmd = new MySqlCommand(tb_file_infos, con);
                con.Open();
                cmd.ExecuteNonQuery();
                cmd = new MySqlCommand(tb_keywords, con);
                cmd.ExecuteNonQuery();
                cmd = new MySqlCommand("SELECT COUNT(*) FROM tb_keywords", con);
                int c = int.Parse(cmd.ExecuteScalar().ToString());
                if (c <= 0)
                {
                    Process p = Process.Start("keywords.exe", String.Format("{0} {1} {2} {3} {4} {5}", str_server, str_database, str_uid, str_password, "dictionary.txt", "tb_keywords"));
                    p.WaitForExit();
                    if (p.ExitCode < 0)
                    {
                        MessageBox.Show("辞典添加发生错误，请重试。", "Hunter 3 Service Configuration", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        btnCreate.Enabled = true;
                        return;
                    }
                }
                con.Close();

                XDocument x_config = new XDocument(new XDeclaration("1.0", "utf-8", "yes"));
                x_config.Add(
                        new XElement("specimen",
                            new XElement("action", new XAttribute("mode", "monitor"), new XAttribute("interval", "3000"), new XAttribute("process", "4")),
                            new XElement("database", new XAttribute("ip", str_server), new XAttribute("usr", str_uid), new XAttribute("pwd", str_password), new XAttribute("db", str_database)),
                            new XElement("directory", new XAttribute("source", str_cache), new XAttribute("dest", str_dest), new XAttribute("cache", "500"), new XAttribute("sharepath", tbShare.Text )),
                            new XElement("type",
                                new XElement("match", new XAttribute("ext", ".doc"), new XAttribute("ksotype", "wps")),
                                new XElement("match", new XAttribute("ext", ".dot"), new XAttribute("ksotype", "wps")),
                                new XElement("match", new XAttribute("ext", ".wps"), new XAttribute("ksotype", "wps")),
                                new XElement("match", new XAttribute("ext", ".wpt"), new XAttribute("ksotype", "wps")),
                                new XElement("match", new XAttribute("ext", ".rtf"), new XAttribute("ksotype", "wps")),
                                new XElement("match", new XAttribute("ext", ".docx"), new XAttribute("ksotype", "wps")),
                                new XElement("match", new XAttribute("ext", ".dotx"), new XAttribute("ksotype", "wps")),
                                new XElement("match", new XAttribute("ext", ".docm"), new XAttribute("ksotype", "wps")),
                                new XElement("match", new XAttribute("ext", ".mht"), new XAttribute("ksotype", "wps-et")),
                                new XElement("match", new XAttribute("ext", ".mhtml"), new XAttribute("ksotype", "wps-et")),
                                new XElement("match", new XAttribute("ext", ".txt"), new XAttribute("ksotype", "wps-et")),
                                new XElement("match", new XAttribute("ext", ".xml"), new XAttribute("ksotype", "wps-et")),
                                new XElement("match", new XAttribute("ext", ".htm"), new XAttribute("ksotype", "wps-et")),
                                new XElement("match", new XAttribute("ext", ".html"), new XAttribute("ksotype", "wps-et")),
                                new XElement("match", new XAttribute("ext", ".xls"), new XAttribute("ksotype", "et")),
                                new XElement("match", new XAttribute("ext", ".xlsx"), new XAttribute("ksotype", "et")),
                                new XElement("match", new XAttribute("ext", ".et"), new XAttribute("ksotype", "et")),
                                new XElement("match", new XAttribute("ext", ".ett"), new XAttribute("ksotype", "et")),
                                new XElement("match", new XAttribute("ext", ".xlt"), new XAttribute("ksotype", "et")),
                                new XElement("match", new XAttribute("ext", ".dbf"), new XAttribute("ksotype", "et")),
                                new XElement("match", new XAttribute("ext", ".xlsm"), new XAttribute("ksotype", "et")),
                                new XElement("match", new XAttribute("ext", ".csv"), new XAttribute("ksotype", "et")),
                                new XElement("match", new XAttribute("ext", ".prn"), new XAttribute("ksotype", "et")),
                                new XElement("match", new XAttribute("ext", ".ppt"), new XAttribute("ksotype", "wpp")),
                                new XElement("match", new XAttribute("ext", ".dps"), new XAttribute("ksotype", "wpp")),
                                new XElement("match", new XAttribute("ext", ".dpt"), new XAttribute("ksotype", "wpp")),
                                new XElement("match", new XAttribute("ext", ".pot"), new XAttribute("ksotype", "wpp")),
                                new XElement("match", new XAttribute("ext", ".pps"), new XAttribute("ksotype", "wpp")),
                                new XElement("match", new XAttribute("ext", ".pptx"), new XAttribute("ksotype", "wpp")),
                                new XElement("match", new XAttribute("ext", ".ppsx"), new XAttribute("ksotype", "wpp"))
                            )
                        )
                    );
                if (!Directory.Exists(str_save)) Directory.CreateDirectory(str_save);
                x_config.Save( Path.Combine (str_save , "specimen.xml") );
                File.Copy ( "SpecimenSortServer.exe", Path.Combine ( str_save, "SpecimenSortServer.exe"), true);
                File.Copy ( "MySql.Data.dll", Path.Combine ( str_save, "MySql.Data.dll"), true);
                MessageBox.Show("配置生成完成，请运行" + Path.Combine (str_save, "SpecimenSortServer.exe") + "来开启样张分类服务。同时，Hunter 3 的远程辞典也已经打开了，可以在Hunter3中将remote_dictionary设置为true，从Mysql服务器上获取关键字了。"
                    , "Hunter 3 Service Configuration", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnCreate.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hunter 3 Service Configuration", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnCreate.Enabled = true;
            }
        }
    }
}
