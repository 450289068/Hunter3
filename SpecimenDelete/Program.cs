using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.IO;

namespace SpecimenDelete
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 5)
            {
                Console.WriteLine("specimendelete [ServerIP] [DatabaseName] [Username] [Password] [\"MysqlQuery\"]");
                return ;
            }
            try
            {
                String sql = args[4].Replace("\"","");
                String str_connection = String.Format("SERVER={0};DATABASE={1};UID={2};PASSWORD={3};", args[0], args[1], args[2], args[3]);
                MySqlConnection con = new MySqlConnection(str_connection);
                con.Open();
                Console.WriteLine("Database is opended.");
                MySqlCommand cmd = new MySqlCommand(sql, con);
                MySqlDataReader dr = cmd.ExecuteReader();
                string target;
                while (dr.Read())
                {
                    target = dr["file_path"].ToString();
                    try
                    {
                        File.Delete(target);
                        Console.WriteLine(target + "is disposed");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message); 
                    }
                }

                dr.Close();
                String[] sep = {"WHERE"};
                String[] split = sql.ToUpper().Split (sep, StringSplitOptions.RemoveEmptyEntries);
                String deleteSql = split[split.Length - 1];
                deleteSql = "DELETE FROM tb_file_infos WHERE " + deleteSql;
                cmd = new MySqlCommand(deleteSql, con);
                Console.WriteLine("Deleting data from database.");
                cmd.ExecuteNonQuery();
                Console.WriteLine("Done.");
                con.Close();
                return ;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey(true);
                return ;
            }
        }
    }
}
