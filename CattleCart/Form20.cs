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
    public partial class Form20 : CattleCart.AppForm
    {
        public Form20()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            LoadAllAnimalData();
        }



        private void LoadAllAnimalData()
        {
            using (SqlConnection con = Class1.GetConnection())
            {
                try
                {
                    con.Open();

                    string query = "Select * from Cattle";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dataGridView1.DataSource = dt; AppSupport.ConfigureGrid(dataGridView1);

                    // Optional UI improvements
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dataGridView1.ReadOnly = true;
                    dataGridView1.AllowUserToAddRows = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading animal data: " + ex.Message);
                }
            }
        }

        private void Form20_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            adminDashboard form17 = new adminDashboard();   
            form17.Show(); // Change from Form12.Show() to form12.Show()'
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
