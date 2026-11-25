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
    public partial class Form28: CattleCart.AppForm
    {
        public Form28()
        {
            InitializeComponent();
            Text = "Demo checkout - no real payment";
            label1.Text = "DEMO checkout - use dummy details only";
            textBox4.UseSystemPasswordChar = true;
            label6.Text = "Security Code :";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form27 form = new Form27();
            form.Show();
            this.Close();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            string card = textBox1.Text.Replace(" ", "").Replace("-", "");
            if (card.Length < 13 || card.Length > 19 || !card.All(char.IsDigit)) { MessageBox.Show("Enter a dummy card number of 13-19 digits."); return; }
            DateTime expiry;
            if (!DateTime.TryParseExact(textBox2.Text.Trim(), "MM/yy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out expiry) || expiry.AddMonths(1) <= DateTime.Today)
            { MessageBox.Show("Enter a valid future expiry date in MM/yy format."); return; }
            if (!AppSupport.IsName(textBox3.Text.Trim())) { MessageBox.Show("Enter the name on the card."); return; }
            string code = textBox4.Text.Trim();
            if (code.Length < 3 || code.Length > 4 || !code.All(char.IsDigit)) { MessageBox.Show("Enter a dummy 3-4 digit security code."); return; }
            decimal amount;
            if (!decimal.TryParse(textBox5.Text, out amount) || amount != 5000) { MessageBox.Show("Demo delivery charge is 5000 Taka."); return; }
            Checkout.Complete(this, "Card (demo)");

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
