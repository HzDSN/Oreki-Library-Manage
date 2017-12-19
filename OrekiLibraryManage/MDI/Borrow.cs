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

        public Borrow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Oreki.showInput("条码扫描","请扫描用户条码");
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
            label2.Text = $"姓名：{(string) name}";
            label1.Visible = true;
            label2.Visible = true;
            textBox1.Visible = true;
            textBox1.Focus();

        }
    }
}

