﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace OrekiLibraryManage
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(System.Windows.Forms.Keys.Enter))
            {
                button1_Click(sender, e);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Oreki.sqlCon();
            var commandText =
                $"select stuff_group from teamz_stuff where stuff_id='{textBox1.Text}' and stuff_password='{textBox2.Text}'";
            var command = new MySqlCommand(commandText, Oreki.connection);
            var group = new object();
            try
            {
                var reader = command.ExecuteReader();
                group = new object();
                while (reader.Read())
                {
                    group = reader[0];
                }
                Oreki.@group = (string) group;
                if ((string)group == "Admin")
                {
                    Oreki.Main.Show();
                    this.Hide();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("登录失败");
                return;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(System.Windows.Forms.Keys.Enter))
            {
                textBox2.Focus();
            }
        }
    }
}
