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
    public partial class Form8 : CattleCart.AppForm
    {
        public Form8()
        {
            InitializeComponent();
            try { dataGridView1.DataSource = AppSupport.Orders(true); AppSupport.ConfigureGrid(dataGridView1); }
            catch (Exception ex) { MessageBox.Show("Could not load orders: " + ex.Message); }
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
                   
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = Session.Name; // Replace with actual username if needed
            farmerPage farmer = new farmerPage(Session.Name,Session.Contact);
            farmer.Show();
            this.Close();
        }
    }
}
