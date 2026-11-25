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
    public partial class intropage : CattleCart.AppForm
    {
        public intropage()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            // Ensure that Form2 is defined in the same namespace or imported correctly.  
            // If Form2 is in another namespace, add the appropriate using directive.  
            loginPage loginForm = new loginPage();
            loginForm.Show();
            this.Close(); // Optional: hides the current form  
        }

        private void intopage_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            AllRightsReserved allRightsReserved = new AllRightsReserved();
            allRightsReserved.Show();
            this.Close(); // Optional: hides the current form
        }
    }
}
