using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Hunter3
{
    public class WindowsAPI
    {
        [DllImport("user32")]
        private static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, IntPtr lParam);
        private const int WM_SETREDRAW = 0xB;

        //禁止控件重绘.
        public static void BeginPaint(Control c)
        {
            SendMessage(c.Handle, WM_SETREDRAW, 0, IntPtr.Zero);
        }
        //允许控件重绘.
        public static void EndPaint(Control c)
        {
            SendMessage(c.Handle, WM_SETREDRAW, 1, IntPtr.Zero);
            c.Refresh();
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Winapi)]
        internal static extern IntPtr GetFocus();
        //获取当前焦点
        public static Control GetFocusedControl()
        {
            Control FocusedControl = null;
            IntPtr FocusedHandle = GetFocus();

            if (FocusedHandle != IntPtr.Zero)
                FocusedControl = Control.FromHandle(FocusedHandle);
            return FocusedControl;
        }
    }
}
