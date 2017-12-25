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
using OrekiLibraryManage.MDI;

namespace OrekiLibraryManage
{
    public partial class BookManage : Form
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

        public BookManage()
        {
            InitializeComponent();
            BookSearch();
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            BookSearch();
           // button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
        }

        private void BookSearch()
        {
            string commandText = $"select * from teamz_books where book_barcode='{textBox1.Text}' or book_name like '%{textBox1.Text}%' or book_writer like '%{textBox1.Text}%' or book_isbn='{textBox1.Text}'";
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(commandText, Oreki.ConStr);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet, "book_barcode");
            dataGridView1.DataSource = dataSet;
            dataGridView1.DataMember = "book_barcode";
            dataGridView1.Columns[0].DataPropertyName = dataSet.Tables[0].Columns[0].ToString();
            dataAdapter.Fill(dataSet, "book_name");
            dataGridView1.DataSource = dataSet;
            dataGridView1.DataMember = "book_name";
            dataGridView1.Columns[1].DataPropertyName = dataSet.Tables[1].Columns[1].ToString();
            dataAdapter.Fill(dataSet, "book_isbn");
            dataGridView1.DataSource = dataSet;
            dataGridView1.DataMember = "book_isbn";
            dataGridView1.Columns[2].DataPropertyName = dataSet.Tables[2].Columns[2].ToString();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            BookAddorChange bookAddorChange = new BookAddorChange
            {
                MdiParent = this,
                Text = "图书添加"
            };
            bookAddorChange.button1.Text = "添加";
            bookAddorChange.textBox1.Text = "";
            bookAddorChange.textBox1.Enabled = true;
            bookAddorChange.ShowDialog();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            BookAddorChange bookAddorChange = new BookAddorChange
            {
                MdiParent = this,
                Text = "图书修改"
            };
            bookAddorChange.button1.Text = "修改";
            bookAddorChange.textBox1.Text = Oreki.Bookbarcode;
            bookAddorChange.textBox1.Enabled = false;
            bookAddorChange.ShowDialog();
        }

        private void DataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            Oreki.Bookbarcode = Convert.ToString(dataGridView1.CurrentRow.Cells[0].Value);

        }
    }
}
