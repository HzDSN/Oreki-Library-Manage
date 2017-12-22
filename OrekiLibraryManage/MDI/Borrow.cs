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

        private string temp;

        public Borrow()
        {
            InitializeComponent();
        }

        private void buttonBorrow_Click(object sender, EventArgs e)
        {
            Oreki.showInput("条码扫描", "请扫描用户条码");
            if (Oreki.okCancel==0)
            {
                return;
            }
            chkCon();
            var commandText = $"select user_name from teamz_users where user_barcode='{Oreki.temp}'";
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
            if (Oreki.temp=="0")
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
            label1.Text = $"用户编号：{Oreki.temp}";
            this.temp = Oreki.temp;
            try
            {
                label2.Text = $"姓名：{(string)name}";
            }
            catch (Exception)
            {
                return;
            }
            var commandText3 = $"select user_borrowed from teamz_users where user_barcode='{Oreki.temp}'";
            var commandText2 = $"select user_maxborrow from teamz_users where user_barcode='{Oreki.temp}'";
            var command3 = new MySqlCommand(commandText3, connection);
            var command2 = new MySqlCommand(commandText2, connection);
            var borrowed = new object();
            try
            {
                var reade2r = command3.ExecuteReader();
                borrowed = new object();
                while (reade2r.Read())
                {
                    borrowed = reade2r[0];
                }
                reade2r.Close();
            }
            catch (InvalidCastException)
            {
                MessageBox.Show("参数错误");
                textBox1.Text = String.Empty;
                return;
            }
            var maxborrow = new object();
            try
            {
                var reader2 = command2.ExecuteReader();
                maxborrow = new object();
                while (reader2.Read())
                {
                    maxborrow = reader2[0];
                }
                reader2.Close();
            }
            catch (InvalidCastException)
            {
                MessageBox.Show("参数错误");
                textBox1.Text = String.Empty;
                return;
            }
            label5.Visible = true;
            label5.Text = $"剩余可借：{(int) maxborrow - (int) borrowed}";
            label1.Visible = true;
            label2.Visible = true;
            label4.Text = "借";
            label4.Visible = true;
            textBox1.Visible = true;
            textBox1.Focus();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            label1.Visible = false;
            label2.Visible = false;
            label4.Text = "还";
            label4.Visible = true;
            label5.Visible = false;
            textBox1.Visible = true;
            textBox1.Focus();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter) && textBox1.Text != String.Empty)
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
            Oreki.temp = this.temp;
            chkCon();
            var commandText = $"select user_borrowed from teamz_users where user_barcode='{Oreki.temp}'";
            var commandText2 = $"select user_maxborrow from teamz_users where user_barcode='{Oreki.temp}'";
            var command = new MySqlCommand(commandText, connection);
            var command2 = new MySqlCommand(commandText2, connection);
            var borrowed = new object();
            try
            {
                var reader = command.ExecuteReader();
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
                textBox1.Text=String.Empty;
                return;
            }
            var maxborrow = new object();
            try
            {
                var reader = command2.ExecuteReader();
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
                textBox1.Text=String.Empty;
                return;
            }
            if ((int)borrowed >= (int)maxborrow)
            {
                listBox1.Items.Add($"{DateTime.Now.ToLongTimeString()}：{Oreki.temp}借书失败 原因：超过最大借阅量");
                textBox1.Text=String.Empty;
                return;
            }
            var commandText3 = $"select book_name from teamz_books where book_barcode='{textBox1.Text}'";
            var command3 = new MySqlCommand(commandText3, connection);
            var bookname = new object();
            {
                var reader = command3.ExecuteReader();
                bookname = new object();
                while (reader.Read())
                {
                    bookname = reader[0];
                }
                reader.Close();
            }
            try
            {
                var aaa = (string)bookname;
            }
            catch (Exception)
            {
                listBox1.Items.Add($"{DateTime.Now.ToLongTimeString()}：系统中无此书");
                textBox1.Text=String.Empty;
                return;
            }
            var commandText4 = $"select book_owner from teamz_books where book_barcode='{textBox1.Text}'";
            var command4 = new MySqlCommand(commandText4, connection);
            var owner = new object();
            try
            {
                var reader = command4.ExecuteReader();
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
                textBox1.Text = string.Empty;
                return;
            }
            if ((string)owner != "0")
            {
                listBox1.Items.Add($"{DateTime.Now.ToLongTimeString()}：{Oreki.temp}借出{(string)bookname}失败 原因：已被借出");
                textBox1.Text=String.Empty;
                return;
            }
            var commandText5 = $"update teamz_users SET user_borrowed=user_borrowed+1 where user_barcode='{Oreki.temp}';update teamz_books set book_owner='{Oreki.temp}' where book_barcode='{textBox1.Text}';insert into teamz_records values ('{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}','{Oreki.temp}','borrow','{textBox1.Text}')";
            var command5 = new MySqlCommand(commandText5, connection);
            command5.ExecuteNonQuery();
            listBox1.Items.Add($"{DateTime.Now.ToLongTimeString()}：{Oreki.temp}借出{(string)bookname}成功");
            textBox1.Text = string.Empty;
        }

        void returnBook()
        {
            chkCon();
            var commandText3 = $"select book_name from teamz_books where book_barcode='{textBox1.Text}'";
            var command3 = new MySqlCommand(commandText3, connection);
            var bookname = new object();
            {
                var reader = command3.ExecuteReader();
                bookname = new object();
                while (reader.Read())
                {
                    bookname = reader[0];
                }
                reader.Close();
            }
            try
            {
                var aaa = (string)bookname;
            }
            catch (Exception)
            {
                listBox1.Items.Add($"{DateTime.Now.ToLongTimeString()}：系统中无此书");
                textBox1.Text = String.Empty;
                return;
            }
            var commandText4 = $"select book_owner from teamz_books where book_barcode='{textBox1.Text}'";
            var command4 = new MySqlCommand(commandText4, connection);
            var owner = new object();
            try
            {
                var reader = command4.ExecuteReader();
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
                textBox1.Text = string.Empty;
                return;
            }
            if ((string)owner == "0")
            {
                listBox1.Items.Add($"{DateTime.Now.ToLongTimeString()}：归还{(string)bookname}失败 原因：未被借出");
                textBox1.Text = String.Empty;
                return;
            }
            Oreki.temp = (string) owner;
            var commandText = $"select user_borrowed from teamz_users where user_barcode='{Oreki.temp}'";
            var command = new MySqlCommand(commandText, connection);
            var borrowed = new object();
            try
            {
                var reader = command.ExecuteReader();
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
                textBox1.Text = String.Empty;
                return;
            }
            if ((int)borrowed == 0)
            {
                MessageBox.Show($"{Oreki.temp}还书失败 原因：已借书0册");
            }
            var commandText1 =
                $"update teamz_users SET user_borrowed=user_borrowed-1 where user_barcode='{Oreki.temp}';update teamz_books set book_owner='0' where book_barcode='{textBox1.Text}';insert into teamz_records values ('{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}','{Oreki.temp}','return','{textBox1.Text}')";
            var command1=new MySqlCommand(commandText1,connection);
            command1.ExecuteNonQuery();
            listBox1.Items.Add($"{DateTime.Now.ToLongTimeString()}：{Oreki.temp}归还{(string)bookname}成功");
            textBox1.Text=String.Empty;
        }
    }
}

