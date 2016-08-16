using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;
using System.Net;
using System.Reflection;
using System.Text.RegularExpressions;
using Hunter3.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace Hunter3.Strategies
{
    public class Strategy
    {
        public ProjectInfo projectInfo;
        public List<String> Keywords = new List<string>();
        public StrategyData StrategyData;
        public String Filetype;    //文件类型：初始值为doc
        private int _currentKeywordProgress = 0;
        public int CurrentKeywordProgress
        {
            get
            {
                return _currentKeywordProgress;
            }
            set
            {
                _currentKeywordProgress = value;
            }
        }
        public int CurrentSearchProgress = 0;
        private List<int> searchedKeywordsInAutoDictionary = new List<int>();
        public enum DictionaryState { doing, done };

        #region 构造函数：传入一些基类的变量
        public Strategy(ProjectInfo projectInfo, bool loadKeywords)
        {
            this.projectInfo = projectInfo;
            StrategyData = StrategyReader.LoadStrategy(HunterUtilities.AbsolutePath(projectInfo.strategyPath));
            CurrentSearchProgress = projectInfo.index;
            CurrentKeywordProgress = projectInfo.keywords;
            Filetype = projectInfo.filetype;
            if (loadKeywords)
                LoadKeywords();
        }
        #endregion

        #region 公共变量：获得URL等相关信息的一些方法，它们都是重载函数

        public string GetFirstSearchURL()
        {
            String str_firstSearchUrl = StrategyData.configuration.FirstURL;
            str_firstSearchUrl = str_firstSearchUrl.Replace("{filetype}", Filetype).Replace("{keyword}", Keywords[CurrentKeywordProgress]) + projectInfo.dictionary_affix;
            return str_firstSearchUrl;
        }

        public string GetCurrentKeyword(int c)
        {
            return Keywords[c];
        }

        public string GetNextURL(bool nextKeyword)     //获得下一个URL
        {
            lock (this)
            {
                if (!MeetEnd())  //是否进度到了最后
                {
                    if (nextKeyword)   //如果直接指定跳到后一个关键字
                    {
                        if (CurrentKeywordProgress < Keywords.Count - 1)
                        {
                            CurrentSearchProgress = 0;
                            CurrentKeywordProgress++;
                            //UpdateKeywordToDatabase(GetCurrentKeyword(CurrentKeywordProgress), DictionaryState.doing);
                        }
                        else
                        {
                            CurrentSearchProgress = 0;
                            return null;    //如果没有下个关键字了，则返回null
                        }
                    }
                    else
                    {   //正常情况下的下一个进度
                        if (CurrentSearchProgress + int.Parse(StrategyData.configuration.IndexStep) <= int.Parse(StrategyData.configuration.MaxIndex))
                        {
                            CurrentSearchProgress += int.Parse(StrategyData.configuration.IndexStep);
                        }
                        else
                        {
                            CurrentSearchProgress = 0;
                            CurrentKeywordProgress++;
                            //UpdateKeywordToDatabase(GetCurrentKeyword(CurrentKeywordProgress), DictionaryState.doing);
                        }
                    }

                    return GetSearchURL(CurrentSearchProgress, Keywords[CurrentKeywordProgress]);
                }
                else
                {
                    CurrentSearchProgress = 0;
                    return null;
                }
            }
        }

        public void UpdateKeywordToDatabase(String keyword, DictionaryState state, bool replace = false)
        {
            if (projectInfo.remote_dictionary)
            {
                String dictionary_table = projectInfo.db_dictionary_table.Trim() == "" ? "tb_keywords" : projectInfo.db_dictionary_table.Trim();
                //网络辞典模式
                HunterDatabaseHelper hdh = new HunterDatabaseHelper(projectInfo);
                Database db = hdh.GetDatabaseInstance();
                try
                {
                    db.DbOpen();
                    StringBuilder keyword_condition = new StringBuilder();
                    keyword_condition.Append("SELECT key_typeandengine FROM " + dictionary_table + " WHERE key_value = \"" + GetKeyword(CurrentKeywordProgress).Replace("'","\\'") + "\" LIMIT 1;");
                    MySqlCommand cmd = new MySqlCommand(keyword_condition.ToString(), db.mysql_connection);
                    String result = cmd.ExecuteScalar().ToString();
                    StringBuilder sb_typeandengine = new StringBuilder(result);
                    if (result != null)
                    {
                        if (!replace)
                        {
                            sb_typeandengine.Append("(" + Filetype + ":" + StrategyData.information.Uri + ":" + state + ");");
                        }
                        else
                        {
                            sb_typeandengine.Replace("(" + Filetype + ":" + StrategyData.information.Uri + ":" + DictionaryState.doing + ");", "(" + Filetype + ":" + StrategyData.information.Uri + ":" + state + ");");
                        }

                        String str_cmd = String.Format("UPDATE {2} SET key_typeandengine = '{0}' WHERE key_value = '{1}'",
                            sb_typeandengine.ToString(), keyword.Replace("'", "\\'"), dictionary_table);
                        cmd = new MySqlCommand(str_cmd, db.mysql_connection);
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        String str_cmd = String.Format("INSERT INTO {2} (key_value, key_typeandengine) VALUES ('{0}','{1}')", keyword.Replace("'", "\\'"), "(" + Filetype + ":" + StrategyData.information.Uri + ":" + state + ");", dictionary_table);
                        cmd = new MySqlCommand(str_cmd, db.mysql_connection);
                        cmd.ExecuteNonQuery();
                    }
                    projectInfo.mHunterConsole.WriteDetails("辞典中的关键字信息成功添加到数据库中。");
                }
                catch (Exception ex)
                {
                    projectInfo.mHunterConsole.WriteException(ex);
                    projectInfo.mHunterConsole.WriteException(new Exception("上传关键字信息到数据库失败。"));
                }
                finally
                {
                    try { db.DbClose(); }
                    catch { }
                }
            }
        }

        //从数据库去除掉当前关键字的doing状态。一般用于关闭项目。
        public void ClearDoingFromDatabase()
        {
            try
            {
                String dictionary_table = projectInfo.db_dictionary_table.Trim() == "" ? "tb_keywords" : projectInfo.db_dictionary_table.Trim();
                HunterDatabaseHelper hdh = new HunterDatabaseHelper(projectInfo);
                Database db = hdh.GetDatabaseInstance();
                String keyword = GetCurrentKeyword(CurrentKeywordProgress);
                db.DbOpen();
                StringBuilder keyword_condition = new StringBuilder();
                keyword_condition.Append("SELECT key_typeandengine FROM " + dictionary_table + " WHERE key_value = \"" + GetKeyword(CurrentKeywordProgress).Replace("'", "\\'") + "\" LIMIT 1;");
                MySqlCommand cmd = new MySqlCommand(keyword_condition.ToString(), db.mysql_connection);
                String result = cmd.ExecuteScalar().ToString();
                StringBuilder sb_typeandengine = new StringBuilder(result);
                if (result != null)
                {
                    sb_typeandengine.Replace("(" + Filetype + ":" + StrategyData.information.Uri + ":" + DictionaryState.doing + ");", "");
                    String str_cmd = String.Format("UPDATE {2} SET key_typeandengine = '{0}' WHERE key_value = '{1}'",
                        sb_typeandengine.ToString(), keyword.Replace("'", "\\'"), dictionary_table);
                    cmd = new MySqlCommand(str_cmd, db.mysql_connection);
                    cmd.ExecuteNonQuery();
                }
                projectInfo.mHunterConsole.WriteDetails("辞典数据库已关闭。");
            }
            catch (Exception ex)
            {
                projectInfo.mHunterConsole.WriteException(ex);
            }
        }

        public string GetKeyword(int id)
        {
            try
            {
                return Keywords[id];
            }
            catch { throw; }
        }

        #endregion

        public string GetSearchURL(int start, string keyword)
        {
            String str_searchUrl = StrategyData.configuration.SearchURL;
            str_searchUrl = str_searchUrl.Replace("{filetype}", Filetype).Replace("{keyword}", WebUtility.HtmlEncode(keyword)).Replace("{index}", start.ToString()) + projectInfo.dictionary_affix;
            return str_searchUrl;
        }
        
        private bool MeetEnd()
        {
            if (CurrentSearchProgress + int.Parse(StrategyData.configuration.IndexStep) > int.Parse(StrategyData.configuration.MaxIndex) && CurrentKeywordProgress >= Keywords.Count - 1)
                return true;
            return false;
        }
        
        public void LoadKeywords()
        {
            Keywords.Clear();
            if (projectInfo.remote_dictionary)
            {
                String dictionary_table = projectInfo.db_dictionary_table.Trim() == "" ? "tb_keywords" : projectInfo.db_dictionary_table.Trim();
                //网络辞典模式
                HunterDatabaseHelper hdh = new HunterDatabaseHelper(projectInfo);
                Database db = hdh.GetDatabaseInstance();
                CurrentKeywordProgress = 0;
                try
                {
                    db.DbOpen();
                    StringBuilder keyword_condition = new StringBuilder();
                    String condition_type_engine_doing = "%(" + projectInfo.filetype + ":" + StrategyData.information.Uri + ":" + DictionaryState.doing + ");%";
                    String condition_type_engine_done = "%(" + projectInfo.filetype + ":" + StrategyData.information.Uri + ":" + DictionaryState.done + ");%";
                    keyword_condition.Append("SELECT * FROM " + dictionary_table + " WHERE (key_typeandengine NOT LIKE \"" + condition_type_engine_doing + "\" AND key_typeandengine NOT LIKE \"" + condition_type_engine_done + "\") OR key_typeandengine IS NULL LIMIT 1;");
                    MySqlCommand cmd = new MySqlCommand(keyword_condition.ToString(), db.mysql_connection);
                    MySqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        dr.Read();
                        Keywords.Add(dr["key_value"].ToString());   //读取一个词
                        UpdateKeywordToDatabase(Keywords[0], DictionaryState.doing);
                    }
                    else
                    {
                        //添加一个空词
                        projectInfo.mHunterConsole.WriteMessage("服务器上已经没有可用的关键字了。");
                    }
                    dr.Close();
                }
                catch (Exception ex)
                {
                    projectInfo.mHunterConsole.WriteException(ex);
                    projectInfo.mHunterConsole.WriteException(new Exception("连接远程辞典失败，因此开始重试连接远程辞典数据库。"));
                    //MessageBox.Show("连接远程辞典失败，以后将采用本地辞典进行下载。若要调整回去，请修改项目文件的remote_dictionary值。", "Hunter 3", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //projectInfo.remote_dictionary = false;
                    try { db.DbClose(); }
                    catch { }
                    LoadKeywords();
                }finally{
                    try{db.DbClose();}catch{}
                }
            }
            else
            {
                //手动辞典模式
                try
                {
                    FileStream fs = new FileStream(projectInfo.dictionary, FileMode.Open);
                    StreamReader sr = new StreamReader(fs, Encoding.UTF8);
                    Keywords.Clear();
                    string k = sr.ReadLine();
                    try
                    {
                        while (k != null)
                        {
                            Keywords.Add(k);
                            k = sr.ReadLine();
                        }
                        if (Keywords.Count == 0) Keywords.Add("");
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    finally
                    {
                        fs.Close();
                        sr.Close();
                    }
                }
                catch (FileNotFoundException)
                {
                    Keywords.Add("");
                    throw;
                }
                catch
                {
                    throw;
                }
            }

        }
        public String GetStrategyInformation()
        {
            return StrategyData.GetStrategyInformation();
        }

        public void RefreshProgress(int index, int keyword)
        {
            lock (this)
            {
                projectInfo.index = index;
                projectInfo.keywords = keyword;
                projectInfo.SaveProject(projectInfo.projectPath);
            }
        }

        public void RecordFirstWord()
        {
            lock (this)
            {
                projectInfo.first = Keywords[0];
                projectInfo.SaveProject(projectInfo.projectPath);
            }
        }

        public String GetLastFirstWord()
        {
            return projectInfo.first;
        }

        /// <summary>
        /// 是否含有违禁词语，对于百度，如果链接内出现“百度文库”则说明其很有
        /// 可能是链接到百度文库，从而放弃这个文件的下载
        /// </summary>
        /// <param name="sentence">要分析的字符串</param>
        /// <returns>是否含有违禁词语</returns>
        public bool HasForbiddenWord(string sentence)
        {
            foreach (String s in StrategyData.configuration.forbiddenString.String)
            {
                if (sentence.Contains(s))
                    return true;
            }
            return false;

        }

        /// <summary>
        /// 判断是否有乱码，如果有乱码，则应该换一种策略显示
        /// </summary>
        /// <param name="sentence"></param>
        /// <returns></returns>
        public bool HasConfusionString(string sentence)
        {
            foreach (String s in StrategyData.configuration.confusionString.String)
            {
                if (sentence.Contains(s))
                    return true;
            }
            return false;

        }

        #region 伪装
        public void Disguise(HttpWebRequest request)
        {
            request.Accept = StrategyData.configuration.disguise.Accept;
            request.KeepAlive = StrategyData.configuration.disguise.KeepAlive.ToLower() == "true" ? true : false;
            request.UserAgent = StrategyData.configuration.disguise.UserAgent;
            request.Timeout = int.Parse(StrategyData.configuration.disguise.Timeout);
            request.AllowAutoRedirect = StrategyData.configuration.disguise.AllowAutoRedirect.ToLower() == "true" ? true : false;
            //String randomString = GetRandomString(32);
            //foreach ( 
            Regex regRandomString = new Regex("\\{RandomString\\((?<param>(\\d)+)\\)\\}");
            Match m = regRandomString.Match(StrategyData.configuration.disguise.Cookie);
            String cookie = StrategyData.configuration.disguise.Cookie;
            while (m.Success)
            {
                int param = int.Parse(m.Result("${param}"));
                String randomString = GetRandomString(param);
                cookie = cookie.Remove(m.Index, m.Value.Length);
                cookie = cookie.Insert(m.Index, randomString);
                //cookie = cookie.Replace(m.Value, randomString);
                m = regRandomString.Match(cookie);
            }
            request.Headers.Add("Cookie", cookie);
        }
        /// <summary>
        /// 获取length长度的随机字符串
        /// </summary>
        private static string GetRandomString(int length)
        {
            string randomChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string result = "";
            Random rnd = new Random();
            for (int i = 0; i < length; i++)
            {
                result += randomChars[rnd.Next(randomChars.Length)];
            }

            return result;
        }
        #endregion

    }

}
