using CattleCart;
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

namespace CattleCarta
{
    public partial class signuppage: CattleCart.AppForm
    {
        public signuppage()
        {
            InitializeComponent();
            textBox5.UseSystemPasswordChar = textBox6.UseSystemPasswordChar = true;
            //signupBtn.Click += signupBtn_Click; // Ensure the event is wired
            /*  this.StartPosition = FormStartPosition.CenterScreen;


              //Atta enter press korle next textbiox e jabe 
              this.KeyPreview = true;

              // Attach KeyDown event to each TextBox
              textBox1.KeyDown += TextBox_KeyDown;
              textBox2.KeyDown += TextBox_KeyDown;
              textBox3.KeyDown += TextBox_KeyDown;
              textBox4.KeyDown += TextBox_KeyDown;
              textBox5.KeyDown += TextBox_KeyDown;
              textBox6.KeyDown += TextBox_KeyDown;
            */
            this.StartPosition = FormStartPosition.CenterScreen;
            this.KeyPreview = true;
            this.AcceptButton = signupBtn; // Pressing Enter will trigger signup

            // Attach Enter key event to all TextBoxes
            textBox1.KeyDown += TextBox_KeyDown;
            textBox2.KeyDown += TextBox_KeyDown;
            textBox3.KeyDown += TextBox_KeyDown;
            textBox4.KeyDown += TextBox_KeyDown;
            textBox5.KeyDown += TextBox_KeyDown;
            textBox6.KeyDown += TextBox_KeyDown;
        }


        //Addi hudai
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void signuppage_Load(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Prevent the ding sound
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }
        //Aykhan thike code start
        //registerBtn
        //registerBtn
        //Atta actu utlaplata hoise thik ,kora lagbe 
        private void signupBtn_Click(object sender, EventArgs e)
        {

            string name = textBox1.Text.Trim(), email = textBox2.Text.Trim(), phone = textBox3.Text.Trim(), address = textBox4.Text.Trim();
            string password = textBox5.Text;
            if (string.IsNullOrWhiteSpace(name) || name.Length > 100) { MessageBox.Show("Enter your name (up to 100 characters)."); textBox1.Focus(); return; }
            try { var parsed = new System.Net.Mail.MailAddress(email); if (parsed.Address != email || email.Length > 254) throw new FormatException(); }
            catch (FormatException) { MessageBox.Show("Enter a valid email address."); textBox2.Focus(); return; }
            if (!AppSupport.IsPhone(phone)) { MessageBox.Show("Phone must be 11 digits and start with 01."); textBox3.Focus(); return; }
            if (string.IsNullOrWhiteSpace(address) || address.Length > 500) { MessageBox.Show("Enter your address (up to 500 characters)."); textBox4.Focus(); return; }
            if (password.Length < 8 || password.Length > 128 || !System.Text.RegularExpressions.Regex.IsMatch(password, @"^(?=.*[a-zA-Z])(?=.*\d)(?=.*[\W_]).+$"))
            { MessageBox.Show("Password must be 8-128 characters with a letter, number and special character."); return; }
            if (password != textBox6.Text) { MessageBox.Show("Passwords do not match."); return; }
            string table = radioButton1.Checked ? "Customers" : radioButton2.Checked ? "Farmers" : null;
            if (table == null) { MessageBox.Show("Please select Farmer or Customer."); return; }
            try
            {
                using (var con = Class1.GetConnection())
                using (var cmd = new SqlCommand("INSERT INTO " + table + " (Name,Email,Contact,Address,Password) VALUES (@name,@email,@phone,@address,@password)", con))
                {
                    cmd.Parameters.AddWithValue("@name", name); cmd.Parameters.AddWithValue("@email", email); cmd.Parameters.AddWithValue("@phone", phone);
                    cmd.Parameters.AddWithValue("@address", address); cmd.Parameters.AddWithValue("@password", Passwords.Hash(password));
                    con.Open(); cmd.ExecuteNonQuery();
                }
                MessageBox.Show("Registration successful. Please log in."); new loginPage().Show(); Close();
            }
            catch (SqlException ex) { MessageBox.Show(ex.Number == 2601 || ex.Number == 2627 ? "This phone is already registered for that role." : "Registration failed: " + ex.Message); }

        }

        private void backBtn_Click(object sender, EventArgs e)
        {
            loginPage loginForm = new loginPage();
            loginForm.Show();
            this.Close();
        }
    }
}
