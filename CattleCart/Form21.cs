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
    public partial class Form21 : CattleCart.AppForm
    {
        public Form21()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            LoadAllAnimalData();
        }



        private void LoadAllAnimalData()
        {
            try { dataGridView1.DataSource = AppSupport.Orders(false); AppSupport.ConfigureGrid(dataGridView1); }
            catch (Exception ex) { MessageBox.Show("Could not load orders: " + ex.Message); }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            adminDashboard form17 = new adminDashboard();
            form17.Show(); // Change from Form12.Show() to form12.Show()
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }
    }
}
