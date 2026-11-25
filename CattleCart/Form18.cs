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
    public partial class Form18 : CattleCart.AppForm
    {
        public Form18()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            LoadCustomerData();
        }


        //to view data

        private void LoadCustomerData()
        {
            try
            {
                using (SqlConnection con = Class1.GetConnection())
                {
                    con.Open();
                    string query = "SELECT * FROM Customers";  // Adjust column names if needed

                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, con))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        dataGridView1.DataSource = dt; AppSupport.ConfigureGrid(dataGridView1);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load customer data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            adminDashboard form17 = new adminDashboard();
            form17.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0 || dataGridView1.SelectedRows[0].IsNewRow)
            {
                MessageBox.Show("Please select a Customers to delete.");
                return;
            }

            try
            {
                if (MessageBox.Show("Delete the selected customer?", "Confirm delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
                int selectedRowIndex = dataGridView1.SelectedRows[0].Index;
                int customerId = Convert.ToInt32(dataGridView1.Rows[selectedRowIndex].Cells["CustomerID"].Value);
                using (SqlConnection con = Class1.GetConnection())
                {
                    con.Open();
                    string query = "DELETE FROM Customers WHERE CustomerID = @CustomerID";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@CustomerID", customerId);
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Customer deleted successfully.");
                LoadCustomerData();  // Refresh the data grid
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting Customer: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
