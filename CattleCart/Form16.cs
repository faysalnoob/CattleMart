using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CattleCart
{
    public partial class Form16 : CattleCart.AppForm
    {
        public Form16()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            customerDashboard form9 = new customerDashboard();
            form9.Show(); // Change from Form12.Show() to form12.Show()
            this.Close();
        }

        private void Form16_Load(object sender, EventArgs e)
        {

        }
    }
}
