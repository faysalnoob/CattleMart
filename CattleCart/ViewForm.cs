using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CattleCart
{
    public partial class ViewForm: CattleCart.AppForm
    {
        public ViewForm()
        {
            InitializeComponent();
            foreach (var box in new[] { textBreed,textBox1,textBox2,textBox3,textBox4 }) box.ReadOnly = true;
        }

        public int CattleId { get; set; }
        public string Display { get; set; }
        public override string ToString() => Display;




        private void imageuploadBox_Click(object sender, EventArgs e)
        {

        }

        private void textBreed_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form27 form27 = new Form27();
            form27.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new customerDashboard().Show(); Close();
        }

        private void ViewForm_Load(object sender, EventArgs e)
        {

            button1.Enabled = false;
            if (CattleId <= 0) { MessageBox.Show("Please select an animal from the list."); return; }
            try
            {
                using (var con = Class1.GetConnection())
                using (var cmd = new SqlCommand("SELECT * FROM Cattle WHERE CattleID=@id AND IsSold=0", con))
                {
                    cmd.Parameters.AddWithValue("@id", CattleId); con.Open();
                    using (var r = cmd.ExecuteReader())
                    {
                        if (!r.Read()) { MessageBox.Show("This animal is no longer available."); return; }
                        textBreed.Text = Convert.ToString(r["Breed"]); textBox2.Text = Convert.ToString(r["Weight"]);
                        textBox1.Text = Convert.ToString(r["Age"]); textBox3.Text = Convert.ToString(r["Price"]); textBox4.Text = Convert.ToString(r["TypeAnimal"]);
                        imageuploadBox.Image = AppSupport.DecodeImage(r["ImageURL"]); Session.CattleId = CattleId; button1.Enabled = true;
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Could not load animal: " + ex.Message); }

        }





    }
}
