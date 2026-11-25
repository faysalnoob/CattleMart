using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;


namespace CattleCart
{
    public partial class Form6: CattleCart.AppForm
    {
        public Form6()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            LoadCattle();
        }



        //SQL

        private void LoadCattle(string searchQuery = "")
        {

            try
            {
                using (var con = Class1.GetConnection())
                using (var da = new SqlDataAdapter(@"SELECT CattleID,Breed,Weight,Age,Price,TypeAnimal,ImageURL,IsSold FROM Cattle WHERE FarmerContact=@contact AND
(@search='' OR Breed LIKE @like OR CAST(CattleID AS nvarchar(20)) LIKE @like OR CAST(Price AS nvarchar(30)) LIKE @like)", con))
                {
                    da.SelectCommand.Parameters.AddWithValue("@contact", Session.Contact ?? "");
                    da.SelectCommand.Parameters.AddWithValue("@search", searchQuery); da.SelectCommand.Parameters.AddWithValue("@like", "%" + searchQuery + "%");
                    var dt = new DataTable(); da.Fill(dt); dataGridView1.DataSource = dt; AppSupport.ConfigureGrid(dataGridView1); LoadImagesToGrid();
                }
            }
            catch (Exception ex) { MessageBox.Show("Could not load animals: " + ex.Message); }

        }

        private void LoadImagesToGrid()
        {

            const string name = "ImageColumn";
            if (!dataGridView1.Columns.Contains(name)) dataGridView1.Columns.Add(new DataGridViewImageColumn { Name=name, HeaderText="Image", ImageLayout=DataGridViewImageCellLayout.Zoom });
            dataGridView1.Columns[name].DefaultCellStyle.NullValue = null;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            { if (row.IsNewRow) continue; row.Cells[name].Value = AppSupport.DecodeImage(row.Cells["ImageURL"].Value); row.Height = 70; }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Optional: You can handle cell click logic here
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = Session.Name;
            string contact = Session.Contact;// Replace with actual username if needed
            farmerPage previousForm = new farmerPage(Session.Name,Session.Contact);
            previousForm.Show();
            this.Close();
        }
    }
}
