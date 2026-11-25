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
    public partial class Form30: CattleCart.AppForm
    {
        public Form30()
        {
            InitializeComponent();
            Text = "Demo checkout - no real payment";
            label1.Text = "DEMO checkout - use dummy details only";
            textBox3.UseSystemPasswordChar = true;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form27 form = new Form27();
            form.Show();
            this.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (!AppSupport.IsName(textBox1.Text.Trim())) { MessageBox.Show("Enter a valid name; spaces are allowed."); return; }
            if (!AppSupport.IsPhone(textBox2.Text.Trim())) { MessageBox.Show("Mobile number must be 11 digits and start with 01."); return; }
            string pin = textBox3.Text.Trim();
            if (pin.Length < 4 || pin.Length > 5 || !pin.All(char.IsDigit)) { MessageBox.Show("Enter a dummy 4-5 digit PIN for this demo."); return; }
            decimal amount;
            if (!decimal.TryParse(textBox4.Text, out amount) || amount != 5000) { MessageBox.Show("Demo delivery charge is 5000 Taka."); return; }
            Checkout.Complete(this, "bKash (demo)");

        }
    }
}
