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
    public partial class RemoveUser : Form
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

        public RemoveUser()
        {
            InitializeComponent();
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            button2.Enabled = checkBox1.Checked && checkBox1.Enabled;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            var commandText = $"delete from teamz_users where user_barcode={textBox1.Text}";
            MySqlCommand command=new MySqlCommand(commandText,Connection);
            command.ExecuteNonQuery();
            MessageBox.Show("销户成功");
            this.Close();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            ChkCon();
            checkBox1.Checked = false;
            var commandText = $"select user_name from teamz_users where user_barcode='{textBox1.Text}'";
            var command = new MySqlCommand(commandText, Connection);
            var name = new object();
            var reader = command.ExecuteReader();
            name = new object();
            while (reader.Read())
            {
                name = reader[0];
            }
            reader.Close();
            if (Oreki.Temp == "0")
            {
                MessageBox.Show("用户不存在");
                return;
            }
            try
            {
                var xxx = (string)name;
            }
            catch (Exception)
            {
                MessageBox.Show("用户不存在");
                return;
            }
            label2.Text = $"姓名：{(string) name}";
            var commandText2 = $"select user_borrowed from teamz_users where user_barcode='{textBox1.Text}'";
            var command2 = new MySqlCommand(commandText2, Connection);
            var borrowed = new  object();
            var reader2 = command2.ExecuteReader();
            while (reader2.Read())
            {
                borrowed = reader2[0];
            }
            reader2.Close();
            label3.Text = $"应还书数：{(int)borrowed}";
            if ((int)borrowed==0)
            {
                checkBox1.Enabled = true;
            }
            else
            {
                checkBox1.Enabled = false;
            }
        }

        private void TextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar==Convert.ToChar(Keys.Enter))
            {
                button1.Focus();
                Button1_Click(sender,e);
            }
        }
    }
}
