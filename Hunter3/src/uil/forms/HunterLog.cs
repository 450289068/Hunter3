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
    public partial class HunterLog : HunterForm
    {
        readonly int MAX = 20;
        Hunter hunter;
        public HunterLog(Hunter project, String caption)
        {
            Button cancel = new Button();
            cancel.Click +=new EventHandler( ( object sender, EventArgs e )=>{
                DialogResult = DialogResult.Cancel;
            });
            this.CancelButton = cancel;
            hunter = project;
            InitializeComponent();

            this.Text = caption;
            FormBorderStyle = FormBorderStyle.Sizable;
        }

        public void ShowContent(List<AbandonFile> arg )
        {
            Console.Clear();
            for (int i = 0; i != ( arg.Count > MAX ? MAX : arg.Count ) ; i++)
            {
                AbandonFile a = arg[arg.Count - 1 - i];
                Console.WriteLine("文件路径： " + a.Info.Filepath, HunterConfig.ColorMessage);
                Console.WriteLine("原因： " + a.Reason, HunterConfig.ColorException);
                Console.WriteLine("来源： " + a.Info.Url, HunterConfig.ColorMessage);
                Console.WriteLine("关键字： " + a.Keyword, HunterConfig.ColorMessage);
                Console.WriteLine("MD5： " + a.Info.Md5, HunterConfig.ColorMessage);
                Console.WriteLine("------------------------------", HunterConfig.ColorMessage);
            }
            this.ShowDialog();
        }

        public void ShowContent(List<AbandonUri> arg)
        {
            Console.Clear();
            //foreach (AbandonUri a in arg)
            for (int i=0; i != (arg.Count > MAX ? MAX : arg.Count ); i++)
            {
                AbandonUri a = arg[arg.Count - 1 - i];
                Console.WriteLine("链接： " + a.Info.Url, HunterConfig.ColorMessage);
                Console.WriteLine("原因： " + a.Reason, HunterConfig.ColorException);
                Console.WriteLine("标题： " + a.Info.Text, HunterConfig.ColorMessage);
                Console.WriteLine("关键字： " + a.Keyword, HunterConfig.ColorMessage);
                Console.WriteLine("------------------------------", HunterConfig.ColorMessage);
            }
            this.ShowDialog();
        }

        public void ShowContent(List<DownloadInfo> arg)
        {
            Console.Clear();
            for (int i = 0; i != (arg.Count > MAX ? MAX : arg.Count ); i++)
            {
                DownloadInfo a = arg[arg.Count - 1 - i];
                Console.WriteLine("文件路径： " + a.Filepath, HunterConfig.ColorMessage);
                Console.WriteLine("来源： " + a.Url, HunterConfig.ColorMessage);
                Console.WriteLine("关键字： " + a.Str_keyword, HunterConfig.ColorMessage);
                Console.WriteLine("MD5： " + a.Md5, HunterConfig.ColorMessage);
                Console.WriteLine("------------------------------", HunterConfig.ColorMessage);
            }
            this.ShowDialog();
        }

        public void ShowContent(List<String> arg)
        {
            Console.Clear();
            for (int i = 0; i != (arg.Count > MAX ? MAX : arg.Count); i++)
            {
                Console.WriteLine(arg[arg.Count - 1 - i], HunterConfig.ColorMessage);
                Console.WriteLine("------------------------------", HunterConfig.ColorMessage);
            }
            this.ShowDialog();
        }
    }
}
