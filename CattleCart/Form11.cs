using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CattleCart
{
    public partial class Form11 : CattleCart.AppForm
    {

        public Form11()
        {
            InitializeComponent();
            button4.Click += (s,e) => AppSupport.ShowSelected(this, dataGridView1, true);
            this.StartPosition = FormStartPosition.CenterScreen;
            LoadCattleData();
        }


        // show cow data

        private void LoadCattleData()
        {

            try
            {
                using (var con = Class1.GetConnection())
                using (var da = new SqlDataAdapter("SELECT CattleID,Breed,Weight,Age,Price,TypeAnimal FROM Cattle WHERE TypeAnimal=@type AND IsSold=0 ORDER BY CattleID", con))
                { da.SelectCommand.Parameters.AddWithValue("@type", "Cow"); var dt = new DataTable(); da.Fill(dt); dataGridView1.DataSource = dt; AppSupport.ConfigureGrid(dataGridView1); }
            }
            catch (Exception ex) { MessageBox.Show("Could not load animals: " + ex.Message); }

        }




        private void tb_search_KeyPress(object sender, KeyPressEventArgs e)
        {

           
        }


        private void button1_Click(object sender, EventArgs e)
        {
            customerDashboard form9 = new customerDashboard();
            form9.Show(); // Change from Form12.Show() to form12.Show()
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            AppSupport.ShowSelected(this, dataGridView1);
        }

        private void Form11_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter) { e.Handled = true; button2_Click(sender, EventArgs.Empty); }
        }

        private void ib_search_TextChanged(object sender, KeyPressEventArgs e)
        {
           
        }

        private void ib_search_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

            try
            {
                using (var con = Class1.GetConnection())
                using (var da = new SqlDataAdapter("SELECT CattleID,Breed,Weight,Age,Price,TypeAnimal FROM Cattle WHERE TypeAnimal='Cow' AND IsSold=0 AND (Breed LIKE @search OR CAST(CattleID AS nvarchar(20)) LIKE @search OR CAST(Price AS nvarchar(30)) LIKE @search)", con))
                { da.SelectCommand.Parameters.AddWithValue("@search", "%" + ib_search.Text.Trim() + "%"); var dt = new DataTable(); da.Fill(dt); dataGridView1.DataSource = dt; AppSupport.ConfigureGrid(dataGridView1); }
            }
            catch (Exception ex) { MessageBox.Show("Search failed: " + ex.Message); }

        }
    }
}
