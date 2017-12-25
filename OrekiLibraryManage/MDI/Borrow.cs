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

        private string _temp;

        public Borrow()
        {
            InitializeComponent();
        }

        private void ButtonBorrow_Click(object sender, EventArgs e)
        {
            Oreki.ShowInput("条码扫描", "请扫描用户条码");
            if (Oreki.OkCancel==0)
            {
                return;
            }
            ChkCon();
            var commandText = $"select user_name from teamz_users where user_barcode='{Oreki.Temp}'";
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
            if (Oreki.Temp=="0")
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
            label1.Text = $"用户编号：{Oreki.Temp}";
            this._temp = Oreki.Temp;
            try
            {
                label2.Text = $"姓名：{(string)name}";
            }
            catch (Exception)
            {
                return;
            }
            var commandText3 = $"select user_borrowed from teamz_users where user_barcode='{Oreki.Temp}'";
            var commandText2 = $"select user_maxborrow from teamz_users where user_barcode='{Oreki.Temp}'";
            var command3 = new MySqlCommand(commandText3, Connection);
            var command2 = new MySqlCommand(commandText2, Connection);
            var borrowed = new object();
            try
            {
                var reade2R = command3.ExecuteReader();
                borrowed = new object();
                while (reade2R.Read())
                {
                    borrowed = reade2R[0];
                }
                reade2R.Close();
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

        private void Button2_Click(object sender, EventArgs e)
        {
            label1.Visible = false;
            label2.Visible = false;
            label4.Text = "还";
            label4.Visible = true;
            label5.Visible = false;
            textBox1.Visible = true;
            textBox1.Focus();
        }

        private void TextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter) && textBox1.Text != String.Empty)
            {
                if (label4.Text == "借")
                {
                    BorrowBook();
                }
                else if (label4.Text == "还")
                {
                    ReturnBook();
                }
            }
        }

        private void BorrowBook()
        {
            Oreki.Temp = this._temp;
            ChkCon();
            var commandText = $"select user_borrowed from teamz_users where user_barcode='{Oreki.Temp}'";
            var commandText2 = $"select user_maxborrow from teamz_users where user_barcode='{Oreki.Temp}'";
            var command = new MySqlCommand(commandText, Connection);
            var command2 = new MySqlCommand(commandText2, Connection);
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
                listBox1.Items.Add($"{DateTime.Now.ToLongTimeString()}：{Oreki.Temp}借书失败 原因：超过最大借阅量");
                textBox1.Text=String.Empty;
                return;
            }
            var commandText3 = $"select book_name from teamz_books where book_barcode='{textBox1.Text}'";
            var command3 = new MySqlCommand(commandText3, Connection);
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
            var command4 = new MySqlCommand(commandText4, Connection);
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
                listBox1.Items.Add($"{DateTime.Now.ToLongTimeString()}：{Oreki.Temp}借出{(string)bookname}失败 原因：已被借出");
                textBox1.Text=String.Empty;
                return;
            }
            var commandText5 = $"update teamz_users SET user_borrowed=user_borrowed+1 where user_barcode='{Oreki.Temp}';update teamz_books set book_owner='{Oreki.Temp}' where book_barcode='{textBox1.Text}';insert into teamz_records values ('{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}','{Oreki.Temp}','borrow','{textBox1.Text}')";
            var command5 = new MySqlCommand(commandText5, Connection);
            command5.ExecuteNonQuery();
            listBox1.Items.Add($"{DateTime.Now.ToLongTimeString()}：{Oreki.Temp}借出{(string)bookname}成功");
            textBox1.Text = string.Empty;
        }

        void ReturnBook()
        {
            ChkCon();
            var commandText3 = $"select book_name from teamz_books where book_barcode='{textBox1.Text}'";
            var command3 = new MySqlCommand(commandText3, Connection);
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
            var command4 = new MySqlCommand(commandText4, Connection);
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
            Oreki.Temp = (string) owner;
            var commandText = $"select user_borrowed from teamz_users where user_barcode='{Oreki.Temp}'";
            var command = new MySqlCommand(commandText, Connection);
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
                MessageBox.Show($"{Oreki.Temp}还书失败 原因：已借书0册");
            }
            var commandText1 =
                $"update teamz_users SET user_borrowed=user_borrowed-1 where user_barcode='{Oreki.Temp}';update teamz_books set book_owner='0' where book_barcode='{textBox1.Text}';insert into teamz_records values ('{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}','{Oreki.Temp}','return','{textBox1.Text}')";
            var command1=new MySqlCommand(commandText1,Connection);
            command1.ExecuteNonQuery();
            listBox1.Items.Add($"{DateTime.Now.ToLongTimeString()}：{Oreki.Temp}归还{(string)bookname}成功");
            textBox1.Text=String.Empty;
        }
    }
}

