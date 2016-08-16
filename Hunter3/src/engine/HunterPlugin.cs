using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Windows.Forms;

namespace Hunter3
{
    public class HunterPlugin
    {
        public enum ParameterType { version };
        private Assembly assembly;
        private Type type;
        private object Instance { get; set; }
        public void LoadDll(String dllpath){
            assembly = Assembly.LoadFrom(dllpath);
            type = assembly.GetType("Hunter3Plugin.Plugin");
            Instance = Activator.CreateInstance(type);
        }

        public String GetTitle()
        {
            MethodInfo m = type.GetMethod("GetTitle");
            return m.Invoke(Instance, null).ToString();
        }

        public object Invoke(Object[] parameters)
        {
            MethodInfo m = type.GetMethod("Invoke");
            return m.Invoke(Instance, parameters);
        }

        public string GetParametersString()
        {
            MethodInfo m = type.GetMethod("SetParameters");
            object result = null;
            if ((result = m.Invoke(Instance, null)) != null)
            {
                return result.ToString();
            }
            return null;
        }

        //根据远程dll传入的parametertype字符串来传入参数
        public static object[] CreateArguments(string parametertype)
        {
            object[] result = new object[1];
            if (parametertype == null) return result;
            List<object> arguments = new List<object>();
            char[] sep = {';'};
            string[] para = parametertype.Split(sep);
            foreach (string s in para)
            {
                if ( s.ToLower() == ParameterType.version.ToString().ToLower())
                {
                    arguments.Add( Application.ProductVersion );
                }
            }
            result[0] = arguments.ToArray();
            return result;
        }
    }
}
