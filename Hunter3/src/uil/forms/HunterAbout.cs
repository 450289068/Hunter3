using System;
using System.Windows.Forms;

namespace Hunter3
{
    public partial class HunterAbout : HunterForm
    {
        public HunterAbout()
        {
            InitializeComponent();
        }

        private void AboutKTS_Load(object sender, EventArgs e)
        {
            lbVersion.Text = Application.ProductVersion;
        }

    }
}
