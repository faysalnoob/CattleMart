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
    public partial class Form22 : CattleCart.AppForm
    {
        public Form22()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            LoadReportsData();
        }

        // show cow data

        private void LoadReportsData()
        {
            using (SqlConnection con = Class1.GetConnection()) // assuming Class1.GetConnection() gives you the valid connection
            {
                try
                {
                    con.Open();
                    string query = "SELECT * FROM Reports"; // fetch all reports records

                    SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dataGridView1.DataSource = dt; AppSupport.ConfigureGrid(dataGridView1);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading Reports data: " + ex.Message);
                }
            }
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
