using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;

namespace OrekiLibraryManage
{
    public partial class Inputbox : Form
    {
        public Inputbox()
        {
            InitializeComponent();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                button1_Click(sender, e);
                
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Oreki.temp = textBox1.Text;
            Oreki.okCancel = 1;
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Inputbox_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Oreki.okCancel = 0;
            this.Close();
        }
    }
}
