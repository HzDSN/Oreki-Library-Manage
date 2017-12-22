using System.Data;
using MySql.Data.MySqlClient;

namespace OrekiLibraryManage
{
    static class Oreki
    {
        public static string conStr = "server=115.159.157.220;uid=Oreki;pwd=6434;database=teamz";

        public static void showInput(string title, string hint)
        {
            var inputbox = new Inputbox();
            inputbox.Text = title;
            inputbox.label1.Text = hint;
            inputbox.ShowDialog();
        }

        public static string temp;

        public static string group;

        public static int okCancel = 0;

        public static MySqlConnection connection = new MySqlConnection(Oreki.conStr);

        public  static  NewUser NewUser = new NewUser();

        public  static Main Main = new Main();

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
