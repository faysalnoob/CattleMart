using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CattleCart
{
    public partial class Form31 : CattleCart.AppForm
    {
        public Form31()
        {
            InitializeComponent();
            button1.Location = new Point(450,350); button1.Text = "Done";
            label2.Font = new Font("Segoe UI", 18);
            label2.Text = "Demo order #" + Session.OrderId + " saved. No money was charged.";
           
        }

      

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

          

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Session.CattleId = 0; new customerDashboard().Show(); Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
    

