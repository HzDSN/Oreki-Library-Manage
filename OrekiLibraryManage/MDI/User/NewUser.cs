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
        public static MySqlConnection connection = new MySqlConnection(Oreki.conStr);

        public static void sqlCon()
        {
            if (connection.State == ConnectionState.Open) connection.Close();
            connection.Open();
        }

        public static void chkCon()
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
        }
        #endregion

        public NewUser()
        {
            InitializeComponent();
        }

        private void chkEnable()
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
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            chkCon();
            var commandText = $"select user_name from teamz_users where user_barcode='{textBox1.Text}'";
            var command = new MySqlCommand(commandText, connection);
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
                addUser();
                return;
            }
            MessageBox.Show("已存在相同编号的用户");
        }

        private void addUser()
        {
            string commandText = $"insert into teamz_users values ('{textBox1.Text}','{textBox2.Text}','',0,{textBox3.Text})";
            var command=new MySqlCommand(commandText,connection);
            command.ExecuteNonQuery();
            MessageBox.Show("添加成功");
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            chkEnable();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            chkEnable();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            chkEnable();
        }
    }
}
