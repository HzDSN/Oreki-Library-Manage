using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrekiLibraryManage.MDI
{
    public partial class Borrow : Form
    {
        public Borrow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Oreki.showInput("条码扫描","请扫描用户条码");
            //Oreki.sqlCon();
            string commandText = $"select user_name from teamz_users where user_barcode={Oreki.temp}";
        }
    }
}
