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
    public partial class Form23: CattleCart.AppForm
    {
        public Form23()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            farmerPage form4 = new farmerPage(Session.Name,Session.Contact);
            form4.Show(); // Show the farmer page
            this.Close(); // Hide the current form
        }
    }
}
