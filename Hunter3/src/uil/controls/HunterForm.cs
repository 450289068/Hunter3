using System.Drawing;
using System.Windows.Forms;
using System;

namespace Hunter3
{
    public class HunterForm : Form
    {
        public Color DefaultColor = Color.FromArgb(111, 122, 204);

        private Color _borderColor;
        public Color BorderColor
        {
            get
            {
                return _borderColor;
            }
            set
            {
                _borderColor = value;
                BackColor = value ;
            } 
        }
        /// <summary>
        /// 设置或获取边框宽度
        /// </summary>
        public int BorderWidth { get; set; }
        private ToolStrip _mainToolStrip;
        private bool _showBorder { get; set; }

        public bool ShowBorder
        {
            set
            {
                _showBorder = value;
                if (!value)
                {
                    Padding = new Padding(0);
                }
                else
                {
                    Padding = new Padding(BorderWidth);
                }
            }
        }

        ///<Summary>
        ///设定主菜单栏，就可使用自定义Render
        /// </Summary>
        public ToolStrip MainToolStrip
        {
            set
            {
                _mainToolStrip = value;
                if (value != null)
                    ToolStripManager.Renderer = new HunterToolStripRender(new HunterProfessionalColors(), value);
            }

        }

        public HunterForm()
        {
            InitializeComponent();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HunterForm));
            
            this.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            BackColor = DefaultColor;
            ShowBorder = true;

        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HunterForm));
            this.SuspendLayout();
            // 
            // HunterForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "HunterForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);

        }

        protected Point WindowLocationOffset = Point.Empty;
        protected void ResetLocOffset(object sender, EventArgs e)
        {
            WindowLocationOffset = Point.Empty;
        }

        protected void MoveWindow(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point cur = this.PointToScreen(e.Location);

                if (WindowLocationOffset != Point.Empty)
                    this.Location = new Point(cur.X - WindowLocationOffset.X, cur.Y - WindowLocationOffset.Y);
            }
        }

        protected void SaveLocation(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point cur = this.PointToScreen(e.Location);
                WindowLocationOffset = new Point(cur.X - this.Left, cur.Y - this.Top);
            }
        }
    }
}
