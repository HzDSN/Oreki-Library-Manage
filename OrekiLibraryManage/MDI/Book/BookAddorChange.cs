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
    public partial class BookAddorChange : Form
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

        public BookAddorChange()
        {
            InitializeComponent();
        }

        private void BookAddorChange_Load(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "添加")
            {
                AddBook();
            }
            else
            {
                ChangeBook();
            }
        }

        private void AddBook()
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
                /*图书不存在，可添加*/
                string commandText = $"insert into teamz_books values ('{textBox1.Text}','{textBox2.Text}','{textBox3.Text}','{textBox4.Text}','{textBox5.Text}',0)";
                MySqlCommand command = new MySqlCommand(commandText, Connection);
                command.ExecuteNonQuery();
                MessageBox.Show("添加成功");
                this.Close();
                return;
            }
            MessageBox.Show("图书已存在");
        }

        private void ChangeBook()
        {
            ChkCon();
            string commandText =
                $"update teamz_books set book_name='{textBox2.Text}',book_writer='{textBox3.Text}',book_press='{textBox4.Text}',book_isbn='{textBox5.Text}' where book_barcode='{textBox1.Text}'";
            MySqlCommand command=new MySqlCommand(commandText,Connection);
            command.ExecuteNonQuery();
            MessageBox.Show("修改成功");
            this.Close();
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            button1.Enabled = (textBox1.TextLength > 0) && (textBox2.TextLength > 0) && (textBox3.TextLength > 0) &&
                              (textBox4.TextLength > 0) && (textBox5.TextLength > 0);
        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {
            button1.Enabled = (textBox1.TextLength > 0) && (textBox2.TextLength > 0) && (textBox3.TextLength > 0) &&
                              (textBox4.TextLength > 0) && (textBox5.TextLength > 0);
        }

        private void TextBox3_TextChanged(object sender, EventArgs e)
        {
            button1.Enabled = (textBox1.TextLength > 0) && (textBox2.TextLength > 0) && (textBox3.TextLength > 0) &&
                              (textBox4.TextLength > 0) && (textBox5.TextLength > 0);
        }

        private void TextBox4_TextChanged(object sender, EventArgs e)
        {
            button1.Enabled = (textBox1.TextLength > 0) && (textBox2.TextLength > 0) && (textBox3.TextLength > 0) &&
                              (textBox4.TextLength > 0) && (textBox5.TextLength > 0);
        }

        private void TextBox5_TextChanged(object sender, EventArgs e)
        {
            button1.Enabled = (textBox1.TextLength > 0) && (textBox2.TextLength > 0) && (textBox3.TextLength > 0) &&
                              (textBox4.TextLength > 0) && (textBox5.TextLength > 0);
        }
    }
}
