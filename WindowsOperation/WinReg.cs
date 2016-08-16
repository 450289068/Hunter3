using System.Windows.Forms;
using Microsoft.Win32;

namespace Ktk.WindowsOperation
{
    public struct RightClickMenu
    {
        public const string EDIT = "Edit";
        public string menuType;
        public string menuText;
        public string shellCmd;
        public RightClickMenu(string _menuType, string _menuText, string _shellCmd)
        {
            menuType = _menuType;
            menuText = _menuText;
            shellCmd = _shellCmd;
        }
    }

    /// <summary>
    /// 对系统进行操作的类
    /// </summary>
    public class WinReg
    {
        
        /// <summary>
        /// 给某类型文件设置文件关联，自动添加Open关联
        /// </summary>
        /// <param name="fileExdName">扩展名</param>
        /// <param name="declaration">描述</param>
        /// <param name="filetype">文件类型</param>
        public static void SaveRelevant(string fileExdName, string filetype, string declaration)
        {
            RegistryKey rkClassesRoot = Registry.ClassesRoot.OpenSubKey("", true); //ClassesRoot节点      

            RegistryKey rk = rkClassesRoot.OpenSubKey(fileExdName, RegistryKeyPermissionCheck.ReadWriteSubTree, 
                System.Security.AccessControl.RegistryRights.FullControl); //尝试打开文件类型的键 如.hunt"

            if (rk != null) rkClassesRoot.DeleteSubKey(fileExdName, true);    //如果存在这个键，则删除
            rkClassesRoot.Flush();

            rk = rkClassesRoot.CreateSubKey(fileExdName, RegistryKeyPermissionCheck.ReadWriteSubTree);   //建立一个ClassesRoot下的键，为文件类型
            rk.SetValue("", filetype);   //增加描述 如 huntertaskFile
            rk.Flush();

            rk = rkClassesRoot.OpenSubKey(filetype, true);     //建立一个子键
            if (rk != null) rkClassesRoot.DeleteSubKeyTree (filetype);
            rkClassesRoot.Flush();

            rkClassesRoot.CreateSubKey(filetype).SetValue("", declaration);

            //获得一个可以操作子键的实例
            rk = rkClassesRoot.OpenSubKey(filetype, true);
            //以下为依次建立键值
            rk.CreateSubKey("DefaultIcon").SetValue("", Application.ExecutablePath + ",0");//建立关联图标
            rk.CreateSubKey("Shell");
            rk.Flush();
            rk = rk.OpenSubKey("Shell", true);                   
            rk.CreateSubKey("Open");
            rk.Flush();
            rk = rk.OpenSubKey("Open", true);
            rk.CreateSubKey("Command");
            rk.Flush();
            rk = rk.OpenSubKey("Command", true);
            string _PathString = Application.ExecutablePath+  " %1";
            rk.SetValue("", _PathString);                             
            rk.Flush();

            rkClassesRoot.Flush();
        }
        
        /// <summary>
        /// 给某类型文件设置右键菜单关联，需要自己添加Open、Edit等关联
        /// </summary>
        /// <param name="fileExdName">扩展名</param>
        /// <param name="declaration">描述</param>
        /// <param name="filetype">文件类型</param>
        /// <param name="rcm">右键关联菜单</param>
        public static void SaveRelevant(string fileExdName, string filetype, string declaration, string DefaultIconPath, params RightClickMenu[] rcm)
        {
            RegistryKey rkClassesRoot = Registry.ClassesRoot.OpenSubKey("", true); //ClassesRoot节点      

            RegistryKey rk = rkClassesRoot.OpenSubKey(fileExdName, RegistryKeyPermissionCheck.ReadWriteSubTree,
                System.Security.AccessControl.RegistryRights.FullControl); //尝试打开文件类型的键 如.hunt"

            if (rk != null) rkClassesRoot.DeleteSubKey(fileExdName, true);    //如果存在这个键，则删除
            rkClassesRoot.Flush();

            rk = rkClassesRoot.CreateSubKey(fileExdName, RegistryKeyPermissionCheck.ReadWriteSubTree);   //建立一个ClassesRoot下的键，为文件类型
            rk.SetValue("", filetype);   //增加描述 如 huntertaskFile
            rk.Flush();

            rk = rkClassesRoot.OpenSubKey(filetype, true);     //建立一个子键
            if (rk != null) rkClassesRoot.DeleteSubKeyTree(filetype);
            rkClassesRoot.Flush();

            rkClassesRoot.CreateSubKey(filetype).SetValue("", declaration);

            //获得一个可以操作子键的实例
            rk = rkClassesRoot.OpenSubKey(filetype, true);
            //以下为依次建立键值
            rk.CreateSubKey("DefaultIcon").SetValue("", DefaultIconPath + ",0");//建立关联图标
            rk.CreateSubKey("Shell");
            rk.Flush();
            
            foreach (RightClickMenu _r in rcm)
            {
                RegistryKey _rk = rk.OpenSubKey("Shell", true);
                _rk.CreateSubKey(_r.menuType);
                _rk.Flush();
                _rk = _rk.OpenSubKey(_r.menuType, true);
                if (_r.menuText != "") _rk.SetValue("", _r.menuText);
                _rk.CreateSubKey("Command");
                _rk.Flush();
                _rk = _rk.OpenSubKey("Command", true);
                string _PathString = _r.shellCmd + " %1";
                _rk.SetValue("", _PathString);
                _rk.Flush();
            }

            rkClassesRoot.Flush();
        }
        
        /// <summary>
        /// 检测是否在注册表中有正确关联
        /// </summary>
        /// <param name="fileExdName">扩展名</param>
        /// <param name="declaration">描述</param>
        /// <param name="filetype">文件类型</param>
        public static bool isRelevant(string fileExdName, string filetype, string declaration)
        {
            try
            {
                RegistryKey rkClassesRoot = Registry.ClassesRoot.OpenSubKey("", true); //ClassesRoot节点
                if (rkClassesRoot.OpenSubKey(fileExdName) == null) return false;
                if (rkClassesRoot.OpenSubKey(fileExdName).GetValue("").ToString()
                    != filetype) return false;
                if (rkClassesRoot.OpenSubKey(filetype) == null ||
                    rkClassesRoot.OpenSubKey(filetype).OpenSubKey("Shell") == null ||
                    rkClassesRoot.OpenSubKey(filetype).OpenSubKey("Shell").OpenSubKey("Open") == null ||
                    rkClassesRoot.OpenSubKey(filetype).OpenSubKey("Shell").OpenSubKey("Open").OpenSubKey("Command") == null ||
                    rkClassesRoot.OpenSubKey(filetype).OpenSubKey("Shell").OpenSubKey("Open").OpenSubKey("Command")
                        .GetValue("").ToString() != Application.ExecutablePath + " %1") return false;
            }
            catch { return false; }
            return true;

        }

        /// <summary>
        /// 检测是否在注册表中的Open有正确关联到特定的correctShellCmd中
        /// </summary>
        /// <param name="fileExdName">扩展名</param>
        /// <param name="declaration">描述</param>
        /// <param name="filetype">文件类型</param>
        public static bool isRelevant(string fileExdName, string filetype, string declaration, string correctShellCmd)
        {
            try
            {
                RegistryKey rkClassesRoot = Registry.ClassesRoot.OpenSubKey("", true); //ClassesRoot节点
                if (rkClassesRoot.OpenSubKey(fileExdName) == null) return false;
                if (rkClassesRoot.OpenSubKey(fileExdName).GetValue("").ToString()
                    != filetype) return false;
                if (rkClassesRoot.OpenSubKey(filetype) == null ||
                    rkClassesRoot.OpenSubKey(filetype).OpenSubKey("Shell") == null ||
                    rkClassesRoot.OpenSubKey(filetype).OpenSubKey("Shell").OpenSubKey("Open") == null ||
                    rkClassesRoot.OpenSubKey(filetype).OpenSubKey("Shell").OpenSubKey("Open").OpenSubKey("Command") == null ||
                    rkClassesRoot.OpenSubKey(filetype).OpenSubKey("Shell").OpenSubKey("Open").OpenSubKey("Command")
                        .GetValue("").ToString() != correctShellCmd + " %1") return false;
            }
            catch { return false; }
            return true;

        }

        /// <summary>
        /// 给某类型文件删除文件关联
        /// </summary>
        /// <param name="fileExdName">扩展名</param>
        /// <param name="declaration">描述</param>
        /// <param name="filetype">文件类型</param>
        public static void DeleteRelevant(string fileExdName, string filetype)
        {
            RegistryKey rkClassesRoot = Registry.ClassesRoot.OpenSubKey("", true); //ClassesRoot节点
            RegistryKey rk = rkClassesRoot.OpenSubKey(fileExdName);
            if (rk != null)
                rkClassesRoot.DeleteSubKeyTree(fileExdName);    //删除后缀名关联

            rk = rkClassesRoot.OpenSubKey(filetype);
            if (rk != null)
                rkClassesRoot.DeleteSubKeyTree(filetype);       //删除文件类型关联
        }

    }

    
}
