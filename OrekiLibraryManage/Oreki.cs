using System.Data;
using MySql.Data.MySqlClient;

namespace OrekiLibraryManage
{
    static class Oreki
    {
        public static string ConStr = "server=115.159.157.220;uid=Oreki;pwd=6434;database=teamz";

        public static void ShowInput(string title, string hint)
        {
            var inputbox = new Inputbox
            {
                Text = title
            };
            inputbox.label1.Text = hint;
            inputbox.ShowDialog();
        }

        public static string Temp;

        public static string Group;

        public static int OkCancel = 0;

        public static MySqlConnection Connection = new MySqlConnection(Oreki.ConStr);

        public static NewUser NewUser = new NewUser();

        public static Main Main = new Main();

        public static string Bookbarcode = "";

            public static void SqlCon()
        {
            if (Connection.State == ConnectionState.Open) Connection.Close();
            Connection.Open();
        }

        public static void ChkCon()
        {
            if (Connection.State == ConnectionState.Closed)
            {
                Connection.Open();
            }
        }
    }
}
