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
    public partial class Form12 : CattleCart.AppForm
    {
        public Form12()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            LoadCattleData();
        }

        // Show goat data
        private void LoadCattleData()
        {

            try
            {
                using (var con = Class1.GetConnection())
                using (var da = new SqlDataAdapter("SELECT CattleID,Breed,Weight,Age,Price,TypeAnimal FROM Cattle WHERE TypeAnimal=@type AND IsSold=0 ORDER BY CattleID", con))
                { da.SelectCommand.Parameters.AddWithValue("@type", "Goat"); var dt = new DataTable(); da.Fill(dt); dataGridView1.DataSource = dt; AppSupport.ConfigureGrid(dataGridView1); }
            }
            catch (Exception ex) { MessageBox.Show("Could not load animals: " + ex.Message); }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            customerDashboard form9 = new customerDashboard();
            form9.Show(); // Change from Form9.Show() to form9.Show()
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            AppSupport.ShowSelected(this, dataGridView1);
        }
    }
}
