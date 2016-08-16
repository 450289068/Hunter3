using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using MySql.Data.MySqlClient;

namespace Keywords
{
    class Program
    {
        static int Main(string[] args)
        {
            if (args.Length < 6){
                Console.WriteLine("keywords [ServerIP] [DatabaseName] [Username] [Password] [DictonaryFile] [TableName]");
                return -1;
            }
            try
            {
                String tb_keywords = @"CREATE TABLE IF NOT EXISTS `" + args[5] + @"` (
  `key_id` int(11) NOT NULL AUTO_INCREMENT,
  `key_value` varchar(32) NOT NULL,
  `key_typeandengine` text DEFAULT NULL,
  PRIMARY KEY (`key_id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;";
                FileStream fs = new FileStream(args[4], FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(fs);
                String str = "";
                StringBuilder sql = new StringBuilder();
                String str_connection = String.Format("SERVER={0};DATABASE={1};UID={2};PASSWORD={3};", args[0], args[1], args[2], args[3]);
                MySqlConnection con = new MySqlConnection(str_connection);
                con.Open();
                Console.WriteLine("Database opended..");
                MySqlCommand createTable = new MySqlCommand(tb_keywords, con);
                createTable.ExecuteNonQuery();
                int counter = 0;
                while ((str = sr.ReadLine()) != null)
                {
                    if (str.Trim() == "") continue;
                    sql.Append("INSERT INTO " + args[5] + " (key_value) VALUES ('" + str.Replace("'","\\'") + "');");
                    counter++;

                    if (counter > 2000)
                    {
                        counter = 0;
                        MySqlCommand cmd = new MySqlCommand(sql.ToString(), con);
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("2000 keywords have been inserted.");
                        sql = new StringBuilder();
                    }
                }

                if (counter < 2000)
                {
                    MySqlCommand cmd = new MySqlCommand(sql.ToString(), con);
                    cmd.ExecuteNonQuery();
                    sql = new StringBuilder();
                }
                con.Close();
                Console.WriteLine("Database closed..");
                Console.WriteLine("Done.");
                fs.Close();
                sr.Close();
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey(true);
                return -1;
            }
        }
    }
}
