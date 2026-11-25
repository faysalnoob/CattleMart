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
    public partial class Form14 : CattleCart.AppForm
    {
        private readonly string animalType;
        public Form14() : this("Camel") { }
        public Form14(string animal)
        {
            animalType = animal;
            InitializeComponent();
            label1.Text = animalType + " Dashboard";
            button2.Click += (s,e) => AppSupport.ShowSelected(this, dataGridView1);
            button3.Click += (s,e) => AppSupport.ShowSelected(this, dataGridView1, true);
            this.StartPosition = FormStartPosition.CenterScreen;
            LoadCattleData();
        }


        private void LoadCattleData()
        {

            try
            {
                using (var con = Class1.GetConnection())
                using (var da = new SqlDataAdapter("SELECT CattleID,Breed,Weight,Age,Price,TypeAnimal FROM Cattle WHERE TypeAnimal=@type AND IsSold=0 ORDER BY CattleID", con))
                { da.SelectCommand.Parameters.AddWithValue("@type", animalType); var dt = new DataTable(); da.Fill(dt); dataGridView1.DataSource = dt; AppSupport.ConfigureGrid(dataGridView1); }
            }
            catch (Exception ex) { MessageBox.Show("Could not load animals: " + ex.Message); }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            customerDashboard form9 = new customerDashboard();
            form9.Show(); // Change from Form9.Show() to form9.Show()
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
