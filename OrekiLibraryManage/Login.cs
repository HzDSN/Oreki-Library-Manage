using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace OrekiLibraryManage
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(System.Windows.Forms.Keys.Enter))
            {
                button1_Click(sender, e);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MySqlConnection connection = new MySqlConnection(Oreki.conStr);
            if (connection.State == ConnectionState.Open) connection.Close();
            connection.Open();
            string commandText = $"select stuff_group from teamz_stuff where stuff_id='{textBox1.Text}' and stuff_password='{textBox2.Text}'";
            MySqlCommand command = new MySqlCommand(commandText,connection);
            MySqlDataReader reader = command.ExecuteReader();
            object group = new object();
            while (reader.Read())
            {
                group = reader[0];
            }
            MessageBox.Show((string)group);
        }
    }
}
