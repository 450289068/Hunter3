using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace Hunter3.Data
{
    public class Database
    {
        private HunterConsole hunterConsole;
        private String username;
        private String ip;
        private String password;
        private String dbname;
        private String port;

        public Database(HunterConsole c, String _usr, String _pwd, String _ip, String _dbname, String _port)
        {
            hunterConsole = c;
            username = _usr;
            password = _pwd;
            ip = _ip;
            dbname = _dbname;
            port = _port;

            str_connection = String.Format("SERVER={0};DATABASE={1};UID={2};PASSWORD={3};PORT={4};charset=utf8;", ip, dbname, username, password, port);
        }

        public String str_connection;
        public MySqlConnection mysql_connection;

        public void DbOpen()
        {
            if (mysql_connection == null)
            {
                mysql_connection = new MySqlConnection(str_connection);
            }

            try
            {
                mysql_connection.Open();
            }
            catch
            {
                throw;
            }
        }

        public void DbClose()
        {
            try
            {
                mysql_connection.Close();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 表示给表格添加内容，按照默认顺序
        /// </summary>
        public void Insert(string tableName, params string[] contents)
        {
            string sqlCmd = "INSERT INTO " + tableName + " VALUES ";

            string c = "(";
            for (int i = 0; i < contents.Length; i++)
            {
                c += "'" + contents[i] + "'";

                if (i != contents.Length - 1)
                    c += ",";
                else
                    c += ")";
            }

            sqlCmd += c;

            hunterConsole.WriteDetails(sqlCmd);
            try
            {
                MySqlCommand sqlCommand = new MySqlCommand(sqlCmd, mysql_connection);
                sqlCommand.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                hunterConsole.WriteException(e);
            }
        }

        /// <summary>
        /// 表示给表格添加内容，按照指定顺序
        /// </summary>
        public void Insert(string tableName, params FieldValue[] contents)
        {
            string sqlCmd = "";
            string fields = "(";
            for (int i = 0; i < contents.Length; i++)
            {
                fields += contents[i].field;

                if (i != contents.Length - 1)
                    fields += ",";
                else
                    fields += ")";
            }

            string values = "(";
            for (int i = 0; i < contents.Length; i++)
            {
                values += "'" + contents[i].value + "'";

                if (i != contents.Length - 1)
                    values += ",";
                else
                    values += ")";
            }

            sqlCmd = "INSERT INTO " + tableName + " " + fields + " VALUES " + values;

            hunterConsole.WriteDetails(sqlCmd);
            try
            {
                MySqlCommand sqlCommand = new MySqlCommand(sqlCmd, mysql_connection);
                sqlCommand.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                hunterConsole.WriteException(e);
            }
        }

        /// <summary>
        /// 返回符合相等条件的条目是否存在，结果保存在result，返回操作是否成功
        /// </summary>
        public bool IsRecordExists(string tableName, out bool result, params FieldValue[] contents)
        {
            try
            {
                string sqlCmd = "SELECT * FROM " + tableName + " WHERE ";

                string t = "";
                for (int i = 0; i < contents.Length; i++)
                {
                    if (contents[i].value != null)
                        t += contents[i].field + "='" + contents[i].value + "'";
                    else
                        t += contents[i].field + " IS NULL ";

                    if (i != contents.Length - 1)
                        t += " AND ";
                }
                
                sqlCmd += t;
                MySqlCommand tempCommand = new MySqlCommand(sqlCmd, mysql_connection);

                hunterConsole.WriteDetails(sqlCmd);
                if (tempCommand.ExecuteScalar() != null) result = true; else result = false;

                //mysql.Close();
                return true;
            }
            catch (Exception e)
            {
                hunterConsole.WriteException(e);
                result = false;
                return false;
            }
        }

        /// <summary>
        /// 根据MD5判断是否文件已经在库中，结果为result，返回操作是否成功
        /// </summary>
        public bool IsFileExists(String table_name, out bool result, string MD5)
        {
            lock (this)
            {
                FieldValue fv = new FieldValue("file_md5", MD5);
                return IsRecordExists(table_name, out result, fv);
            }
        }

    }


    /// <summary>
    /// 表示一个域值对
    /// </summary>
    public struct FieldValue
    {
        public string field;
        public string value;
        public FieldValue(string _field, string _value)
        {
            field = _field;
            value = _value;
        }
    }

    /// <summary>
    /// 表示一个列定义
    /// </summary>
    public struct Column_definition
    {
        public string column_name;
        public string type_name;
        public bool null_able;      //表示是否可以使用空值
        public string extra;        //其他内容
        public string precision;
        public string def;
        /// <summary>
        /// 表示一个列定义的构造函数
        /// </summary>
        /// <param name="cname">列名</param>
        /// <param name="type">列类型</param>
        /// <param name="pre">精度，若为0则表示为默认</param>
        /// <param name="nul">是否可为空值</param>
        /// <param name="e">额外的内容</param>
        /// <param name="d">默认值，若为null则表示无默认值</param>
        public Column_definition(string cname, string type, int pre, bool nul, string d, string e)
        {
            column_name = cname;
            type_name = type;
            null_able = nul;
            extra = (e == null) ? "" : e;
            precision = (pre == 0) ? "" : "(" + pre.ToString() + ")";
            def = (d == null) ? "" : "DEFAULT '" + d + "'";
        }
    }

}
