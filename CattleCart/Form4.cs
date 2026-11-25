using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CattleCart
{
    public partial class farmerPage : CattleCart.AppForm
    {
        private string username;// Declare the 'username' variable
        private string phoneNumber;

        public farmerPage(string username,string phoneNumber) // Add a constructor to accept 'username'
        {
            InitializeComponent();
            this.username = Session.Name ?? username; // Initialize the 'username' variable
            this.StartPosition = FormStartPosition.CenterScreen;
            this.phoneNumber = Session.Contact ?? phoneNumber;
            Console.WriteLine(phoneNumber);
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        //logout btn , atta login page a nihe jabe 
        private void button6_Click(object sender, EventArgs e)
        {
            loginPage loginForm = new loginPage();
            loginForm.Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form5 form = new Form5(phoneNumber);
            form.Show();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form6 form = new Form6();
            form.Show();
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //Profile form = new Profile("username");//change
            //form.Show();
            //this.Close();
            //string username = "username"; // Replace with the actual username value as needed.
            //Form7 form = new Form7(); // Pass the 'username' variable correctly  
            //form.Show();
            //this.Close();
             // Replace with the actual contact value as needed
            Form7 form = new Form7(username, phoneNumber); // Pass both 'username' and 'contact' correctly
            form.Show();
            this.Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            // Fix the Label creation and assignment
            Label label = new Label();
            label.Text = "Welcome, " + username + "!";
            label.Text = "User : " + username; // Set the text of the label
            label.Location = new Point(10, 10); // Set a location for the label
            this.Controls.Add(label); // Add the label to the form
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form23 contact = new Form23();
            contact.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form8 form = new Form8();
            form.Show();
            this.Close();
        }
    }
}
