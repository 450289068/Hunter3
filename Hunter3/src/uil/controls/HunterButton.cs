using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Hunter3
{
    public partial class HunterButton : Button
    {
        Image ImgNormal;
        Image ImgHover;
        Image ImgChecked;
        Image ImgCheckedHover;

        public Boolean AllowCheck { get; set; }
        private bool _Checked;
        public Boolean Checked {
            get
            {
                return _Checked;
            }
            set
            {
                _Checked = value;
                if ( CheckStateChanged != null ) CheckStateChanged(this, null);
                CheckChange();
            }
        
        }
        
        public event EventHandler CheckStateChanged;

        public HunterButton()
        {
            InitializeComponent();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HunterButton));
            AllowCheck = false;
            Checked = false;

            BackgroundImageLayout = ImageLayout.Stretch;
            ImgNormal = global::Hunter3.Properties.Resources.button;
            ImgHover = global::Hunter3.Properties.Resources.hover;
            ImgChecked = global::Hunter3.Properties.Resources.buttonchecked;
            ImgCheckedHover = global::Hunter3.Properties.Resources.buttoncheckedhover;
            ForeColor = Color.White;

            this.BackgroundImage = ImgNormal;
            this.MouseLeave += new EventHandler(HunterButton_MouseLeave);
            this.MouseEnter += new EventHandler(HunterButton_MouseEnter);
            this.Click +=new EventHandler(HunterButton_Click);
        }

        void  HunterButton_Click(object sender, EventArgs e)
        {
            if (AllowCheck)
            {
                Checked = !Checked;
            }
        }

        void CheckChange()
        {
            if (IsMouseOver())
            {
                HunterButton_MouseEnter(null, null);
            }
            else
            {
                HunterButton_MouseLeave(null, null);
            }
        }

        bool IsMouseOver()
        {
            return
                PointToClient(MousePosition).X < Size.Width &&
                PointToClient(MousePosition).Y < Size.Height;
        }

        void HunterButton_MouseEnter(object sender, EventArgs e)
        {
            if (Checked)
            {
                BackgroundImage = ImgCheckedHover;
            }
            else
            {
                BackgroundImage = ImgHover;
            }
        }

        void HunterButton_MouseLeave(object sender, EventArgs e)
        {
            if (Checked)
            {
                BackgroundImage = ImgChecked;
            }
            else
            {
                BackgroundImage = ImgNormal;
            }
        }


    }
}
