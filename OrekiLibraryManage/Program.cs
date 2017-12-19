using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

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
            //Login login = new Login();
            //login.ShowDialog();
        }
    }

    static class Oreki
    {
        public static string conStr = "server=115.159.157.220;uid=Oreki;pwd=6434;database=teamz";

        public static void showInput(string title, string hint)
        {
            Inputbox inputbox = new Inputbox();
            inputbox.Text = title;
            inputbox.label1.Text = hint;
            inputbox.ShowDialog();
        }

        public static string temp;

        public static MySqlConnection connection = new MySqlConnection(Oreki.conStr);

        public static void sqlCon()
        {
            if (connection.State == ConnectionState.Open) connection.Close();
            connection.Open();
        }

        public static void chkCon()
        {
            if (connection.State==ConnectionState.Closed)
            {
                connection.Open();
            }
        }
    }
}
