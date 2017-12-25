using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OrekiLibraryManage.MDI;

namespace OrekiLibraryManage
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            Oreki.NewUser.MdiParent = this;
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void 图书借阅ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var borrow = new Borrow
            {
                MdiParent = this
            };
            borrow.Show();
        }

        private void 用户管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var user = new User
            {
                MdiParent = this
            };
            user.Show();
        }

        private void 图书管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BookManage book = new BookManage
            {
                MdiParent = this
            };
            book.Show();
        }
    }
}
