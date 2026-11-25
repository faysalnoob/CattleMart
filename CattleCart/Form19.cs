using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace CattleCart
{
    public partial class Form19 : CattleCart.AppForm
    {
        public Form19()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            LoadFarmerData();
        }

        //show data code

        private void LoadFarmerData()
        {
            try
            {
                using (SqlConnection con = Class1.GetConnection())
                {
                    con.Open();
                    string query = "SELECT * FROM Farmers";  // Farmer table

                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, con))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        dataGridView1.DataSource = dt; AppSupport.ConfigureGrid(dataGridView1);  // Bind to grid
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading farmer data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0 || dataGridView1.SelectedRows[0].IsNewRow)
            {
                MessageBox.Show("Please select a Farmer to delete.");
                return;
            }

            try
            {
                if (MessageBox.Show("Delete the selected farmer?", "Confirm delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
                int selectedRowIndex = dataGridView1.SelectedRows[0].Index;
                int farmerId = Convert.ToInt32(dataGridView1.Rows[selectedRowIndex].Cells["FarmerID"].Value);
                using (SqlConnection con = Class1.GetConnection())
                {
                    con.Open();
                    string query = "DELETE FROM Farmers WHERE FarmerID = @FarmerID";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@FarmerID", farmerId);
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Farmer deleted successfully.");
                LoadFarmerData();  // Refresh the data grid
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting farmer: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
        }
    }
}
