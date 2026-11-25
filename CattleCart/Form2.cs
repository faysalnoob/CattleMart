using CattleCarta;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CattleCart
{
    public partial class loginPage: CattleCart.AppForm
    {
        public loginPage()
        {
            InitializeComponent();
            Session.Clear();
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            AcceptButton = loginBtn;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        //hudai !!!
        //hudai !!!
        // nah hudai hah ekahn kaaj kortise!
        //MAINLY phone number ar kaj.
        //chaile ay errror handle korte parri or nicher ta 
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //Atta kinda advance type !!!
            //string input = textBox1.Text;

            //// Check if input starts with "01", is 11 digits, and contains only digits
            //if (!input.StartsWith("01") || input.Length != 11 || !input.All(char.IsDigit))
            //{
            //    textBox1.BackColor = Color.MistyRose; // Highlight error
            //                                          // Optionally, show a message only once or use an error provider
            //                                          // MessageBox.Show("Phone number must start with '01' and be exactly 11 digits.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
            //else
            //{
            //    textBox1.BackColor = Color.White; // Valid input
            //}
        }

        //Hudai
        private void label4_Click(object sender, EventArgs e)
        {

        }

        //Atta intro page a nihe jabe 
        //private void backBtn_Click(object sender, EventArgs e)
        //{

        //    intropage lmao = new intropage();
        //    lmao.Show();
        //    this.Close(); 
        //}
        private void backBtn_Click(object sender, EventArgs e)
        {
            
            intropage intro = new intropage();
            intro.Show();
            this.Close();
        }

        //error handle korbe (phone number , password)
        private void loginBtn_Click(object sender, EventArgs e)
        {

            string phone = textBox1.Text.Trim(), password = textBox2.Text;
            if (!AppSupport.IsPhone(phone)) { MessageBox.Show("Phone must be 11 digits and start with 01."); return; }
            if (string.IsNullOrWhiteSpace(password)) { MessageBox.Show("Please enter your password."); return; }
            string role = Convert.ToString(comboBox1.SelectedItem);
            string table = role == "Admin" ? "Admin" : role == "Farmer" ? "Farmers" : role == "Customer" ? "Customers" : null;
            if (table == null) { MessageBox.Show("Please select a role."); return; }
            try
            {
                string name = null, stored = null;
                using (var con = Class1.GetConnection())
                using (var cmd = new SqlCommand("SELECT Name, Password FROM " + table + " WHERE Contact=@contact", con))
                {
                    cmd.Parameters.AddWithValue("@contact", phone); con.Open();
                    using (var reader = cmd.ExecuteReader()) if (reader.Read()) { name = reader.GetString(0); stored = reader.GetString(1); }
                }
                if (stored == null || !Passwords.Verify(password, stored)) { MessageBox.Show("Invalid credentials.", "Login failed"); return; }
                Session.Clear(); Session.Name = name; Session.Contact = phone; Session.Role = role;
                Form next = role == "Admin" ? (Form)new adminDashboard() : role == "Farmer" ? new farmerPage(name, phone) : (Form)new customerDashboard();
                next.Show(); Close();
            }
            catch (Exception ex) { MessageBox.Show("Database error: " + ex.Message); }

        }
        private void loginPage_Load(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '*'; // Hide by default
        }

        private void signupBtn_Click(object sender, EventArgs e)
        {
            signuppage signupForm = new signuppage();
            signupForm.Show();
            this.Close(); // Optional: hides the current form
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

            //atta dile passwoed show / hide korbe .
            if (showpassword.Checked)
            {
                textBox2.PasswordChar = '\0'; // Show password
            }
            else
            {
                textBox2.PasswordChar = '*'; // Hide password
            }

        }

        //Ay part bad , but bad dile code high hohe jabe 
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ////Get the selected item as string
            //string selectedRole = comboBox1.SelectedItem?.ToString();

            //if (selectedRole == "Farmer")
            //{
            //    farmerPage farmerForm = new farmerPage(selectedRole);
            //    farmerForm.Show();
            //    this.Close();
            //}

            //else if (selectedRole == "Customer")
            //{
            //    customerDashboard customerForm = new customerDashboard();
            //    customerForm.Show();
            //    this.Close();
            //}
        }
    }
}
