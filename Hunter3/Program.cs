using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Hunter3
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(String[] args)
        {
            if (args.Length == 0)
                Application.Run(new HunterMain(null));
            else
            {

                string finalArg = "";
                foreach (string arg in args)
                {
                    finalArg += arg + " ";
                }

                HunterMain hunterMain = new HunterMain(finalArg);
                Application.Run(hunterMain);

            }

        }
    }
}
