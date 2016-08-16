using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Hunter3.Strategies
{
    public class StrategyReader
    {
        public StrategyReader()
        {
        }

        public static void CreateModel(String StrategyPath)
        {
            XmlSerializer ser = new XmlSerializer(typeof(StrategyData));
            FileStream fs = new FileStream(StrategyPath, FileMode.Create, FileAccess.Write);
            ser.Serialize(fs, new StrategyData());
            fs.Close();
        }

        public static StrategyData LoadStrategy(String StrategyPath)
        {
            try
            {
                XmlSerializer ser = new XmlSerializer(typeof(StrategyData));
                FileStream fs = new FileStream(StrategyPath, FileMode.Open, FileAccess.Read);
                StrategyData s = (StrategyData)ser.Deserialize(fs);
                Lexer(s.GetType(), s);
                fs.Close();
                return s;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 语法分析迭代器
        /// 规则如下：
        /// {标签}表示引用XML中的标签，在Lexer中处理
        /// {预设值}表示一些预设的值，如{filetype},{Keyword}，为变量，需要即时演算，在Lexer中不处理
        /// {RandomString(int)}为一个预设函数，需要即时演算，在Lexer中不处理
        /// </summary>
        private static void Lexer(Type t, object Obj)
        {
            Regex reg = new Regex("\\{(?<attr>(.*?))\\}");
            foreach (PropertyInfo p in t.GetProperties()){
                Match match;
                String value = p.GetValue(Obj, null).ToString();//.Split(sep)[p.GetValue(mStrategy, null).ToString().Split(sep).Length - 1];
                match = reg.Match(value);
                while ( match.Success)
                {
                    String replace = match.Value;
                    foreach (PropertyInfo _p in t.GetProperties())
                    {
                        if (_p.Name == match.Result("${attr}"))
                        {
                            p.SetValue(Obj, value.Replace(replace, _p.GetValue(Obj, null).ToString()), null);
                            value = p.GetValue(Obj, null).ToString();
                        }

                    }

                    match = match.NextMatch();
                    
                }

                //如果是一个类
                if (value.Contains("+") && p.PropertyType != typeof(String)) {
                    Lexer(p.PropertyType, p.GetValue(Obj, null) );
                }
            }
        }

    }
}
