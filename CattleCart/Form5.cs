using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using static System.Net.Mime.MediaTypeNames;
using System.Data.Common;
using System.Xml.Linq;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CattleCart
{
     
    public partial class Form5: CattleCart.AppForm
    {

        private string imageLocation = ""; //Src : youtube
        private string contact;
        // public PictureBox image1;
        public Form5(string cont)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            // LoadCattle();
            this.contact = cont; 


        }

      


        private void Form5_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

            using (var dialog = new OpenFileDialog { Filter = "Images|*.jpg;*.jpeg;*.png;*.bmp;*.gif" })
            {
                if (dialog.ShowDialog() != DialogResult.OK) return;
                try
                {
                    if (new FileInfo(dialog.FileName).Length > 10 * 1024 * 1024) { MessageBox.Show("Choose an image smaller than 10 MB."); return; }
                    var image = AppSupport.DecodeImage(File.ReadAllBytes(dialog.FileName));
                    if (image == null) { MessageBox.Show("This file is not a valid image."); return; }
                    if (imageuploadBox.Image != null) imageuploadBox.Image.Dispose();
                    imageuploadBox.Image = image; imageLocation = dialog.FileName;
                }
                catch (Exception ex) { MessageBox.Show("Could not open image: " + ex.Message); }
            }

        }

        //Atta upload button error / message box handle korbe 
        private void button1_Click(object sender, EventArgs e)
        {

            decimal weight, age, price;
            if (Session.Role != "Farmer" || !AppSupport.IsPhone(Session.Contact)) { MessageBox.Show("Please log in as a farmer."); return; }
            if (string.IsNullOrWhiteSpace(textBreed.Text) || textBreed.Text.Trim().Length > 100) { MessageBox.Show("Enter a breed (up to 100 characters)."); return; }
            if (!decimal.TryParse(textWeight.Text, out weight) || weight <= 0 || weight > 9999999999.99m ||
                !decimal.TryParse(textAge.Text, out age) || age <= 0 || age > 999999.99m ||
                !decimal.TryParse(textPrice.Text, out price) || price <= 0 || price > 999999999999.99m)
            { MessageBox.Show("Enter valid positive weight, age and price."); return; }
            string[] animals = { "Cow", "Goat", "Sheep", "Camel", "Buffalo" };
            string type = animals.FirstOrDefault(a => a.Equals(textTypeAnimal.Text.Trim(), StringComparison.OrdinalIgnoreCase));
            if (type == null) { MessageBox.Show("Animal type must be Cow, Goat, Sheep, Camel or Buffalo."); return; }
            try
            {
                byte[] bytes = File.Exists(imageLocation) ? File.ReadAllBytes(imageLocation) : null;
                using (var con = Class1.GetConnection())
                using (var cmd = new SqlCommand("INSERT INTO Cattle(Breed,Weight,Age,Price,TypeAnimal,ImageURL,FarmerContact) VALUES(@breed,@weight,@age,@price,@type,@image,@contact)", con))
                {
                    cmd.Parameters.AddWithValue("@breed", textBreed.Text.Trim()); cmd.Parameters.AddWithValue("@weight", weight);
                    cmd.Parameters.AddWithValue("@age", age); cmd.Parameters.AddWithValue("@price", price); cmd.Parameters.AddWithValue("@type", type);
                    cmd.Parameters.Add("@image", SqlDbType.VarBinary, -1).Value = (object)bytes ?? DBNull.Value;
                    cmd.Parameters.AddWithValue("@contact", Session.Contact); con.Open(); cmd.ExecuteNonQuery();
                }
                MessageBox.Show("Animal uploaded successfully.");
                textBreed.Clear(); textWeight.Clear(); textAge.Clear(); textPrice.Clear(); textTypeAnimal.Clear();
                if (imageuploadBox.Image != null) imageuploadBox.Image.Dispose(); imageuploadBox.Image = null; imageLocation = "";
            }
            catch (Exception ex) { MessageBox.Show("Upload failed: " + ex.Message); }

        }
            /*
             * akhan image upload ar kaj korle hobe 
             */

            // Shob from fill korle ay message show korbe 
          //  MessageBox.Show("All fields are filled. Upload successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //}

        //back btn ar kaj atta 
        private void button2_Click(object sender, EventArgs e)
        {
            string username = Session.Name; // Replace with the actual username value as needed.
           // string  contact = "defaultContact"; // Replace with the actual contact value as needed.
            farmerPage farmer = new farmerPage(Session.Name,Session.Contact);
            farmer.Show();
            this.Close();
        }

        private void imageuploadBox_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            // Fix for CS7036: Pass the required 'username' parameter to the farmerPage constructor.
            string username = Session.Name;
          //  string contact= "defaultContact";// Replace with the actual username value as needed.
            farmerPage form = new farmerPage(Session.Name,Session.Contact);

            // Fix for CS0120: Use the object reference 'form' to call the Show method.
            form.Show();

            // Hide the current form.
            this.Close();
        }
        //no need
        private void textBreed_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {

        }

        //bad atta
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //string[] allowedAnimals = { "Cow", "Goat", "Buffalo", "Sheep" ,"cow","goat","buffalo","sheep"};
            //string input = textTypeAnimal.Text.Trim();

            //if (!string.IsNullOrWhiteSpace(input) &&
            //    !allowedAnimals.Contains(input, StringComparer.OrdinalIgnoreCase))
            //{
            //    MessageBox.Show("Please enter a valid animal type: Cow, Goat, Buffalo, or Sheep.",
            //        "Invalid Animal Type", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    textTypeAnimal.Clear();
            //    textTypeAnimal.Focus();
            //}
        }
    }
    
}
