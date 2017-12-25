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
    public partial class User : Form
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

        public User()
        {
            InitializeComponent();
            if (Oreki.Group != "Admin" && Oreki.Group != "Manager")//需同时满足这两个条件，用and
            {
                button2.Visible = false;
                button3.Visible = false;
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Oreki.ShowInput("条码扫描", "请扫描用户条码");
            if (Oreki.OkCancel == 0)
            {
                return;
            }
            ChkCon();
            var commandText = $"select user_name from teamz_users where user_barcode='{Oreki.Temp}'";
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
            try
            {
                label1.Text = $"当前用户：{(string)name}";
            }
            catch (Exception)
            {
                return;
            }
            tabControl1.Enabled = true;
            TabRefresh();
        }

        private void TabRefresh()
        {
            labelYHBH.Text = $"用户编号：{Oreki.Temp}";
            var commandText = $"select user_name from teamz_users where user_barcode='{Oreki.Temp}'";
            var command = new MySqlCommand(commandText, Connection);
            var name = new object();
            var reader = command.ExecuteReader();
            name = new object();
            while (reader.Read())
            {
                name = reader[0];
            }
            labelXM.Text = $"姓名：{(string)name}";
            reader.Close();
            var commandText2 = $"select user_borrowed from teamz_users where user_barcode='{Oreki.Temp}'";
            var command2 = new MySqlCommand(commandText2, Connection);
            var borrowed = new object();
            var reader2 = command2.ExecuteReader();
            borrowed = new object();
            while (reader2.Read())
            {
                borrowed = reader2[0];
            }
            labelCYTSS.Text = $"持有图书数：{(int)borrowed}"; ;
            reader2.Close();
            var commandText3 = $"select user_maxborrow from teamz_users where user_barcode='{Oreki.Temp}'";
            var command3 = new MySqlCommand(commandText3, Connection);
            var maxborrow = new object();
            var reader3 = command3.ExecuteReader();
            maxborrow = new object();
            while (reader3.Read())
            {
                maxborrow = reader3[0];
            }
            labelZDKJS.Text = $"最大可借数：{(int)maxborrow}";
            reader3.Close();
            var commandText4 = $"select * from teamz_books where book_owner='{Oreki.Temp}'";
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(commandText4, Oreki.ConStr);
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
            var commandText5 = $"select * from teamz_records where user_code='{Oreki.Temp}'";
            MySqlDataAdapter dataAdapter2 = new MySqlDataAdapter(commandText5, Oreki.ConStr);
            DataSet dataSet2 = new DataSet();
            dataAdapter2.Fill(dataSet2, "book_barcode");
            dataGridView2.DataSource = dataSet2;
            dataGridView2.DataMember = "book_barcode";
            dataGridView2.Columns[0].DataPropertyName = dataSet2.Tables[0].Columns[0].ToString();
            dataAdapter2.Fill(dataSet2, "book_name");
            dataGridView2.DataSource = dataSet2;
            dataGridView2.DataMember = "book_name";
            dataGridView2.Columns[1].DataPropertyName = dataSet2.Tables[1].Columns[1].ToString();
            dataAdapter2.Fill(dataSet2, "book_isbn");
            dataGridView2.DataSource = dataSet2;
            dataGridView2.DataMember = "book_isbn";
            dataGridView2.Columns[2].DataPropertyName = dataSet2.Tables[2].Columns[2].ToString();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            //Oreki.NewUser.Show();
            NewUser newUser = new NewUser
            {
                MdiParent = Oreki.Main
            };
            newUser.Show();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            RemoveUser removeUser = new RemoveUser
            {
                MdiParent = Oreki.Main
            };
            removeUser.Show();
        }
    }
}
