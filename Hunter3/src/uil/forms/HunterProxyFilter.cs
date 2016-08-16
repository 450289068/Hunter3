using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Hunter3
{
    public partial class HunterProxyFilter : HunterForm
    {
        HunterConfig config;

        public HunterProxyFilter(HunterConfig c)
        {
            config = c;
            InitializeComponent();
            FormClosing += new FormClosingEventHandler(HunterProxyFilter_FormClosing);
            foreach (String s in config.ProxyFilterKeywords)
            {
                hContent.AppendText(s + Environment.NewLine);
            }
            Button e = new Button();
            e.Click += new EventHandler((object sender, EventArgs ex) =>
            {
                Close();
            });
            this.CancelButton = e;
        }

        void HunterProxyFilter_FormClosing(object sender, FormClosingEventArgs e)
        {
            miSave_Click(null, null);
        }

        private void miAbort_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void miSave_Click(object sender, EventArgs e)
        {
            config.ProxyFilterKeywords.Clear();
            foreach (String s in hContent.Lines)
            {
                if (s.Length <= 0) continue;
                //注释部分：NotifyClick.Substring(0, NotifyClick.IndexOf('\'') >= 0 ? NotifyClick.IndexOf('\'') : NotifyClick.Length);
                if (s.Trim() != "") 
                    config.ProxyFilterKeywords.Add(s);
            }
        }


        private void miPaste_Click(object sender, EventArgs e)
        {
            hContent.Paste(DataFormats.GetFormat(DataFormats.Text));
            RefreshUI();
        }

        private void miCopy_Click(object sender, EventArgs e)
        {
            RefreshUI();
            hContent.Copy();
        }

        private void miRedo_Click(object sender, EventArgs e)
        {
            hContent.Redo();
            RefreshUI();
        }

        private void miUndo_Click(object sender, EventArgs e)
        {
#warning 有个Undo Bug
            hContent.Undo();
            RefreshUI();
        }

        private void miAll_Click(object sender, EventArgs e)
        {
            hContent.SelectAll();
        }

        private void miCut_Click(object sender, EventArgs e)
        {
            RefreshUI();
            hContent.Cut();
        }

        private void RefreshUI()
        {
            miUndo.Enabled = hContent.CanUndo;
            miRedo.Enabled = hContent.CanRedo;

            if (hContent.SelectionLength > 0)
            {
                miCut.Enabled = true;
                miCopy.Enabled = true;
            }
            else
            {
                miCut.Enabled = false;
                miCopy.Enabled = false;
            }
            miPaste.Enabled = hContent.CanPaste(DataFormats.GetFormat(DataFormats.Text));
        }
        
    }
}
