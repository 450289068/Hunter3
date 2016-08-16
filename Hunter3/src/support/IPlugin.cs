using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hunter3Plugin
{
    // 若要编写Hunter3插件，需要继承此接口(IPlugin)。
    public interface IPlugin
    {
        // 返回接口的标题，用于显示在菜单栏上
        String GetTitle();

        // 设置Hunter3返回给本插件的参数，本质上为一个格式化的字符串，可以用ParameterBuilder来创建
        String SetParameters();

        // 激活接口时触发的方法，args表示传入的参数数组
        Object Invoke(object[] args);
    }

    public class ParameterBuilder
    {
        public enum ParameterType { version };
        public StringBuilder Parameters { get; set; }
        public ParameterBuilder()
        {
            Parameters = new StringBuilder();
        }

        public void AppendParameter(ParameterType t)
        {
            Parameters.Append(t.ToString() + ";");
        }

        public override String ToString(){
            return Parameters.ToString();
        }
    }
}
