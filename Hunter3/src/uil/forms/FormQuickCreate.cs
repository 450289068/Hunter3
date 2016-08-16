using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace Hunter3
{
    public partial class FormQuickCreate : HunterForm
    {
        public FormQuickCreate()
        {
            InitializeComponent();
            tPath.ForeColor = HunterConfig.ColorBarForeColor;
            Button b = new Button(); b.Click +=new EventHandler( (object sender, EventArgs e) => { Close(); });
            this.CancelButton = b;
            tPath.Focus();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            ProjectInfo pi = new ProjectInfo();
            try
            {
                if (tPath.Text.Trim() == "")
                {
                    MessageBox.Show("请输入保存的路径。", "Hunter 3", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if ( !Directory.Exists ( tPath.Text ) ) Directory.CreateDirectory ( tPath.Text ) ;

                //拷贝创建辞典
                String dictionaryPath =  Path.Combine(tPath.Text, "dictionary.dic");
                File.Copy("config\\dictionary.dic", Path.Combine(tPath.Text, dictionaryPath), true);

                //更改xml数据库路径
                pi.database = Path.Combine(tPath.Text, "database.xml");

                //更改辞典路径
                pi.dictionary = dictionaryPath;

                //更改下载目录
                pi.filefolder = Path.Combine(tPath.Text, "DownloadFiles");

                //网络参数
                pi.remote_dictionary = false;
                pi.mode = ProjectInfo.HunterMode.local;

                String savePath = Path.Combine(tPath.Text, "project.h3");
                pi.SaveProject(savePath);

                MessageBox.Show("项目生成完毕！", "Hunter 3", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Process.Start("explorer.exe", "/n, " + tPath.Text);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hunter 3", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void hbBrowse_Click(object sender, EventArgs e)
        {
            // 可以打开任何特定的对话框  
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.ShowNewFolderButton = true;
            dialog.Description = "请选择一个文件夹存放此项目配置";
            if (dialog.ShowDialog().Equals(DialogResult.OK))
            {
                tPath.Text = dialog.SelectedPath;
            }
        }

    }
}
