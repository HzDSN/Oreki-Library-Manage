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

namespace OrekiLibraryManage.MDI
{
    public partial class Borrow : Form
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

        public Borrow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Oreki.showInput("条码扫描", "请扫描用户条码");
            chkCon();
            string commandText = $"select user_name from teamz_users where user_barcode='{Oreki.temp}'";
            MySqlCommand command = new MySqlCommand(commandText, connection);
            object name = new object();
            try
            {
                MySqlDataReader reader = command.ExecuteReader();
                name = new object();
                while (reader.Read())
                {
                    name = reader[0];
                }
                reader.Close();
            }
            catch (InvalidCastException)
            {
                MessageBox.Show("查无此人");
                return;
            }
            label1.Text = $"用户编号：{Oreki.temp}";
            label2.Text = $"姓名：{(string)name}";
            label1.Visible = true;
            label2.Visible = true;
            label4.Text = "借";
            textBox1.Visible = true;
            textBox1.Focus();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            label1.Visible = false;
            label2.Visible = false;
            label4.Text = "还";
            textBox1.Focus();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (label4.Text == "借")
                {
                    borrow();
                }
                else if (label4.Text == "还")
                {
                    returnBook();
                }
            }
        }

        void borrow()
        {
            chkCon();
            string commandText = $"select user_borrowed from teamz_users where user_barcode='{Oreki.temp}'";
            string commandText2 = $"select user_maxborrow from teamz_users where user_barcode='{Oreki.temp}'";
            MySqlCommand command = new MySqlCommand(commandText, connection);
            MySqlCommand command2 = new MySqlCommand(commandText2, connection);
            object borrowed = new object();
            try
            {
                MySqlDataReader reader = command.ExecuteReader();
                borrowed = new object();
                while (reader.Read())
                {
                    borrowed = reader[0];
                }
                reader.Close();
            }
            catch (InvalidCastException)
            {
                MessageBox.Show("参数错误");
                return;
            }
            object maxborrow = new object();
            try
            {
                MySqlDataReader reader = command2.ExecuteReader();
                maxborrow = new object();
                while (reader.Read())
                {
                    maxborrow = reader[0];
                }
                reader.Close();
            }
            catch (InvalidCastException)
            {
                MessageBox.Show("参数错误");
                return;
            }
            if ((int)borrowed >= (int)maxborrow)
            {
                MessageBox.Show("超过最大借阅量");
            }
            string commandText3 = $"select book_name from teamz_books where book_barcode='{textBox1.Text}'";
            MySqlCommand command3 = new MySqlCommand(commandText3,connection);
            object bookname = new object();
            try
            {
                MySqlDataReader reader = command3.ExecuteReader();
                bookname = new object();
                while (reader.Read())
                {
                    bookname = reader[0];
                }
                reader.Close();
            }
            catch (InvalidCastException)
            {
                MessageBox.Show("无此书");
                return;
            }
            string commandText4 = $"select book_name from teamz_books where book_barcode='{textBox1.Text}'";
            MySqlCommand command4 = new MySqlCommand(commandText4, connection);
            object owner = new object();
            try
            {
                MySqlDataReader reader = command4.ExecuteReader();
                owner = new object();
                while (reader.Read())
                {
                    owner = reader[0];
                }
                reader.Close();
            }
            catch (InvalidCastException)
            {
                MessageBox.Show("参数错误");
                return;
            }
            if ((string) owner != "0")
            {
                listBox1.Items.Add($"{DateTime.Now.ToLongTimeString()}：{Oreki.temp}借出{(string) bookname}失败 原因：已被借出");
                return;
            }
            string commandText5= $"update teamz_users SET user_borrowed=user_borrowed+1 where user_barcode='{Oreki.temp}';update teamz_books set book_owner='{Oreki.temp}';insert into teamz_records values ('{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}','{Oreki.temp}','borrow','{textBox1.Text}')";
            MySqlCommand command5=new MySqlCommand(commandText5,connection);
            command5.ExecuteNonQuery();
            listBox1.Items.Add($"{DateTime.Now.ToLongTimeString()}：{Oreki.temp}借出{(string) bookname}成功");
        }

        void returnBook()
        {

        }
    }
}

