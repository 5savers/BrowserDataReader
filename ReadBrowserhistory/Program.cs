using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ReadBrowserhistory
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            

            bool result;
            var mutex = new System.Threading.Mutex(true, "SqlReader", out result);

            if (!result)
            {
                if (Environment.UserName.Contains("xception"))
                    MessageBox.Show("Another instance is already running.");
                return;
            }

            try { Application.Run(new Form1()); }catch{}

            GC.KeepAlive(mutex);   
        }
    }
}
