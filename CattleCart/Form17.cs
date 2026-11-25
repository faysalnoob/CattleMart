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
    public partial class adminDashboard : CattleCart.AppForm
    {
        public adminDashboard()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            loginPage loginPage = new loginPage();
            loginPage.Show(); // Show the login page
            this.Close(); // Hide the current form
        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form18 form18 = new Form18();
            form18.Show(); 
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form19 form19 = new Form19();
            form19.Show(); // Change from Form12.Show() to form12.Show()
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form20 form20 = new Form20();
            form20.Show(); // Change from Form12.Show() to form12.Show()
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form21 form21 = new Form21();
            form21.Show(); // Change from Form12.Show() to form12.Show()
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form22 form22 = new Form22();
            form22.Show(); // Change from Form12.Show() to form12.Show()
            this.Close();
        }

        private void Form17_Load(object sender, EventArgs e)
        {

        }
    }
}
