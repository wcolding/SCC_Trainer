using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SCC_Trainer
{
    static class Program
    {
        public static TextBox log;

        public static readonly string uplayDLL = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().GetName().CodeBase.Substring(8)) + @"\SC5_Uplay.dll";
        //public static readonly string steamDLL = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().GetName().CodeBase) + @"\SC5_Steam.dll";        

        public static void Log(string s, params object[] args)
        {
            log.AppendText(String.Format(s, args) + "\r\n");
        }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
