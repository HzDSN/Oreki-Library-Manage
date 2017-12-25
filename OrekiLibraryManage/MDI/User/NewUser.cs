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
    public partial class NewUser : Form
    {
        #region MySQL Connection
        public static MySqlConnection Connection = new MySqlConnection(Oreki.ConStr);

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
        #endregion

        public NewUser()
        {
            InitializeComponent();
        }

        private void ChkEnable()
        {
            if (textBox1.TextLength > 0 && textBox2.TextLength > 0 && textBox3.TextLength > 0)
            {
                int ii = 0;
                for (int i = 0; i < textBox3.TextLength; i++)
                {
                    if (Convert.ToChar(textBox3.Text.Substring(i, 1)) >= '0' && Convert.ToChar(textBox3.Text.Substring(i, 1)) <= '9')
                    {
                        ii++;
                    }
                }
                if (ii == textBox3.TextLength) button1.Enabled = true;
                else
                {
                    button1.Enabled = false;
                }
            }
            else
            {
                button1.Enabled = false;
            }
        }
        private void TextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {

            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            ChkCon();
            var commandText = $"select user_name from teamz_users where user_barcode='{textBox1.Text}'";
            var command = new MySqlCommand(commandText, Connection);
            var name = new object();
            //try
            //{
            var reader = command.ExecuteReader();
            name = new object();
            while (reader.Read())
            {
                name = reader[0];
            }
            reader.Close();
            try
            {
                var xxx = (string)name;
            }
            catch (Exception)
            {
                AddUser();
                return;
            }
            MessageBox.Show("已存在相同编号的用户");
        }

        private void AddUser()
        {
            string commandText = $"insert into teamz_users values ('{textBox1.Text}','{textBox2.Text}','',0,{textBox3.Text})";
            var command=new MySqlCommand(commandText,Connection);
            command.ExecuteNonQuery();
            MessageBox.Show("添加成功");
            this.Close();
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            ChkEnable();
        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {
            ChkEnable();
        }

        private void TextBox3_TextChanged(object sender, EventArgs e)
        {
            ChkEnable();
        }
    }
}
