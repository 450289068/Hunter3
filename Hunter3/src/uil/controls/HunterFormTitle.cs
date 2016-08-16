using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Hunter3
{
    public partial class HunterFormTitle : UserControl
    {
        public Form Owner { get; set; }

        public HunterFormTitle()
        {
            InitializeComponent();
        }

        public HunterFormTitle(Form owner)
        {
            InitializeComponent();
            Owner = owner;
        }

        public void AddMouseMove(MouseEventHandler e)
        {
            flowLayoutControl.MouseMove += e;
            flowLayoutTitle.MouseMove += e;
            Title.MouseMove += e;
            Icon.MouseMove += e;
        }

        public void AddMouseDown(MouseEventHandler e)
        {
            flowLayoutControl.MouseDown += e;
            flowLayoutTitle.MouseDown += e;
            Title.MouseDown += e;
            Icon.MouseDown += e;
        }

        public void AddMouseLeave(EventHandler e)
        {
            flowLayoutControl.MouseLeave += e;
            flowLayoutTitle.MouseLeave += e;
            Title.MouseLeave += e;
            Icon.MouseLeave += e;
        }

        public virtual void OnClose(object sender, EventArgs e)
        {
            Owner.Close();
        }

        public virtual void OnChangeWindowState(object sender, EventArgs e)
        {
            if (Owner != null)
            {
                if (Owner.WindowState == FormWindowState.Maximized)
                    Owner.WindowState = FormWindowState.Normal;
                else if (Owner.WindowState == FormWindowState.Normal)
                {
                    Owner.WindowState = FormWindowState.Maximized;
                }
            }
        }

        public virtual void OnMinimize(object sender, EventArgs e){
            if (Owner != null)
            {
                Owner.WindowState = FormWindowState.Minimized;
            }
        }

        public virtual void OnChangeWindowStateByDoubleClick(object sender, MouseEventArgs e)
        {
            OnChangeWindowState(sender, null);
        }
    }
}
