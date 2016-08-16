using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.IO;
using System.Linq;

/* XML记录文件的说明
 * 节点：
 * root 根节点
 * link 从哪个链接上下载的
 * mark 取前N个字符作为样式对照
 * path 存放路径
 */

namespace Hunter3.Data
{
    public class XMLDatabase : XMLDefinition
    {
        public string xmlDatabasePath;
        public int markedContent;   //表示文档作为关键字的字符个数
        public HunterConsole mHunterConsole; //输出操控器
        XDocument database; //表示读取的XML整个数据库

        public XMLDatabase(string path, HunterConsole mHunterConsole)
        {
            xmlDatabasePath = path;
            this.mHunterConsole = mHunterConsole;
            
        }

        public void openDatabase()
        {
            try
            {
                lock (this)
                {
                    database = XDocument.Load(xmlDatabasePath);
                }
                mHunterConsole.WriteMessage("打开数据库成功。");
            }
            catch (FileNotFoundException)
            {
                mHunterConsole.WriteMessage("没有找到Hunter XML数据库文件，准备新建一个。");
                lock (this)
                {
                    createDatabase();   //如果不存在文件
                }
            }
            catch (Exception)
            {
                mHunterConsole.WriteMessage("Hunter XML数据库文件异常，我们将备份原有Hunter XML文件并创建一个新的。");
                lock (this)
                {
                    File.Move(xmlDatabasePath, xmlDatabasePath + ".bak");
                    createDatabase();   //如果不存在文件
                }
            }
        }

        public void createDatabase()
        {
            try
            {
                XDocument xdoc = new XDocument(new XDeclaration("1,0", "utf-8", "yes"),
                    new XElement(XMLDefinition.ElementName.root.ToString()));
                if (xmlDatabasePath.Length <= 0)
                    mHunterConsole.WriteMessage("XML数据库文件名为空！");
                else
                {
                    xdoc.Save(xmlDatabasePath);
                    mHunterConsole.WriteMessage("建立XML数据库成功。");
                    openDatabase();
                }
            }
            catch (Exception e) { mHunterConsole.WriteException(e); }
        }

        /// <summary>
        /// 判断链接是否在数据库中存在
        /// </summary>
        /// <param name="link">链接地址</param>
        /// <returns>返回是否存在</returns>
        public bool LinkExists(string link)
        {
            /*
            try
            {
                openDatabase();
            }
            catch (Exceptions e)
            {
                mHunterConsole.WriteException(e);
            }
            */

            IEnumerable<XElement> sample =
                from eLink in database.Descendants(ElementName.root.ToString()).Elements(ElementName.sample.ToString())
                select eLink;   //选中所有LINK

            foreach (XElement s in sample)
            {
                if (s.Element(ElementName.link.ToString()).Value == link)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 这条记录的链接、MD5是否重复
        /// </summary>
        /// <param name="link">访问链接</param>
        /// <param name="Md5">MD5</param>
        /// <returns>返回是否重复</returns>
        public bool isDuplicate(string link, string md5)
        {
            /*
            try
            {
                openDatabase();
            }
            catch (Exceptions e)
            {
                mHunterConsole.WriteException(e);
            }
            */

            IEnumerable<XElement> sample =
                from eLink in database.Descendants(ElementName.root.ToString()).Elements(ElementName.sample.ToString())
                select eLink;   //选中所有LINK

            foreach (XElement s in sample)
            {
                if (s.Element(ElementName.link.ToString()).Value == link ||
                    s.Element(ElementName.md5.ToString()).Value == md5)
                {
                    return true;
                }
            }

            return false;

        }

        /// <summary>
        /// 添加不重复的样张属性，并返回结果
        /// </summary>
        /// <param name="link">访问链接</param>
        /// <param name="path">保存路径</param>
        /// <param name="Md5">MD5</param>
        public void addNewRecord(string url, string keyword, string path, string md5)
        {
            try
            {
                lock (this)
                {
                    database.Element(ElementName.root.ToString()).Add
                        (new XElement(ElementName.sample.ToString(),
                            new XElement(ElementName.link.ToString(), url),
                            new XElement(ElementName.keyword.ToString(), keyword),
                            new XElement(ElementName.path.ToString(), path),
                            new XElement(ElementName.md5.ToString(), md5)
                        )
                    );
                    database.Save(xmlDatabasePath);
                    
                    mHunterConsole.WriteDownload("写入XML数据库成功。");
                }
            }
            catch (Exception e)
            {
                mHunterConsole.WriteException(e);
            }

        }
    }
}
