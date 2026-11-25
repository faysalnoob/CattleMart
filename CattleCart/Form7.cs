using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace CattleCart
{
    public partial class Form7: CattleCart.AppForm
    {
        private string Contact;
        private string selectedRole;


        private string farmerContact; // store contact globally
       
        public Form7(string role, string phoneNumber)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Contact = phoneNumber.Trim();//new
            this.selectedRole = role;
            this.farmerContact = phoneNumber;
            Console.WriteLine(selectedRole);
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            LoadFarmerData();
            SetEditable(false);

            // Set initial button visibility
            button1.Visible = true;  // Edit button
            button2.Visible = false; // Save button

            //Console.WriteLine(selectedRole);
        }

        private void LoadFarmerData()
        {
            using (SqlConnection con = Class1.GetConnection())
            {
                try
                {
                    con.Open();
                    string query = "SELECT * FROM Farmers WHERE Contact = @Contact";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        // Trim the contact to avoid trailing space issues
                        cmd.Parameters.AddWithValue("@Contact", farmerContact.Trim());
                        Console.WriteLine("Searching for contact: " + farmerContact);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                textBox1.Text = reader["Name"].ToString();
                                textBox2.Text = reader["Contact"].ToString();
                                textBox3.Text = reader["Email"].ToString();
                                textBox4.Text = reader["Address"].ToString();

                            //Optional: Load animal types(if stored)
                            //        string animals = reader["AnimalTypes"]?.ToString() ?? "";
                            //    //  checkBox1.Checked = animals.Contains("Cow");
                            //    checkBox2.Checked = animals.Contains("Goat");
                            //    checkBox3.Checked = animals.Contains("Sheep");
                            //    checkBox4.Checked = animals.Contains("Buffalo");
                            }
                            else
                            {
                                MessageBox.Show("Farmer not found for contact: " + farmerContact);
                                this.Close();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading data: " + ex.Message);
                }
            }
        }


       // private bool setEditMode = false;
        private bool SetEditable(bool enable)
        {
            textBox1.ReadOnly =! enable;
            textBox2.ReadOnly = true; // Contact shouldn't be changed
            textBox3.ReadOnly =! enable;
            textBox4.ReadOnly =! enable;
          
            //query for auto fill


            // Return true to indicate the method executed successfully
            return true;
        }




        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Pass the username from textBox1 to the farmerPage constructor
            farmerPage previousForm = new farmerPage(textBox1.Text,Contact);
            previousForm.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SetEditable(true);
            button1.Visible = false;
            button2.Visible = true;
            MessageBox.Show("You can now edit your information.", "Edit Mode", MessageBoxButtons.OK, MessageBoxIcon.Information);


        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            //string selectedAnimals = "";
            //if (checkBox1.Checked) selectedAnimals += "Cow,";
            //if (checkBox2.Checked) selectedAnimals += "Goat,";
            //if (checkBox3.Checked) selectedAnimals += "Sheep,";
            //if (checkBox4.Checked) selectedAnimals += "Buffalo,";
           // selectedAnimals = selectedAnimals.TrimEnd(',');

            using (SqlConnection con = Class1.GetConnection())
            {
                try
                {
                    con.Open();
                    string updateQuery = @"UPDATE Farmers 
                                           SET Name = @Name, Email = @Email, Address = @Address 
                                           WHERE Contact = @Contact";

                    SqlCommand cmd = new SqlCommand(updateQuery, con);
                    cmd.Parameters.AddWithValue("@Name", textBox1.Text.Trim());
                    cmd.Parameters.AddWithValue("@Email", textBox3.Text.Trim());
                    cmd.Parameters.AddWithValue("@Address", textBox4.Text.Trim());
                   // cmd.Parameters.AddWithValue("@Animals", selectedAnimals);
                    cmd.Parameters.AddWithValue("@Contact", Contact);
                    if (cmd.ExecuteNonQuery() != 1) { MessageBox.Show("Profile was not found. Please log in again."); return; }
                    Session.Name = textBox1.Text.Trim();

                    MessageBox.Show("Profile updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SetEditable(false);
                    button2.Visible = false;
                    button1.Visible = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saving data: " + ex.Message);
                }
            }
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Please enter Name.");
                return false;
            }

            if (textBox2.Text.Length != 11 || !textBox2.Text.All(char.IsDigit) || !textBox2.Text.StartsWith("01"))
            {
                MessageBox.Show("Contact must be 11 digits and start with '01'.");
                return false;
            }

            try { var address = new System.Net.Mail.MailAddress(textBox3.Text.Trim()); if (address.Address != textBox3.Text.Trim()) throw new FormatException(); }
            catch (FormatException) { MessageBox.Show("Enter a valid email address."); return false; }

            if (string.IsNullOrWhiteSpace(textBox4.Text))
            {
                MessageBox.Show("Please enter Address.");
                return false;
            }

            //if (!checkBox1.Checked && !checkBox2.Checked && !checkBox3.Checked && !checkBox4.Checked)
            //{
            //    MessageBox.Show("Please select at least one animal type.");
            //    return false;
            //}

            return true;
        }



        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
