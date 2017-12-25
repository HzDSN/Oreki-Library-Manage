using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
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

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(System.Windows.Forms.Keys.Enter))
            {
                Button1_Click(sender, e);
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Oreki.SqlCon();
            var commandText =
                $"select stuff_group from teamz_stuff where stuff_id='{textBox1.Text}' and stuff_password='{textBox2.Text}'";
            var command = new MySqlCommand(commandText, Oreki.Connection);
            var group = new object();
            try
            {
                var reader = command.ExecuteReader();
                group = new object();
                while (reader.Read())
                {
                    group = reader[0];
                }
                Oreki.Group = (string) group;
                if ((string)group == "Admin")
                {
                    Oreki.Main.Show();
                    this.Hide();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("登录失败");
                return;
            }
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(System.Windows.Forms.Keys.Enter))
            {
                textBox2.Focus();
            }
        }
    }
}
