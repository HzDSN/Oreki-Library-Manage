using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrekiLibraryManage
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
            Application.Run(new Login());
        }
    }

    static class Oreki
    {
        public static string conStr = "server=115.159.157.220;uid=Oreki;pwd=6434;database=teamz";

        static void showInput(string title, string hint)
        {
            Inputbox inputbox = new Inputbox();
            inputbox.Text = title;
            inputbox.label1.Text = hint;
            inputbox.ShowDialog();
        }
    }
}
