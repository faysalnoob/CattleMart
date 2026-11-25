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
    public partial class Form27: CattleCart.AppForm
    {
        public Form27()
        {
            InitializeComponent();
            Text = "Demo checkout - no real payment";
            var notice = new Label { Text="Academic demo only. No real money is charged. Use dummy details.", AutoSize=true, Location=new Point(20,10), ForeColor=Color.DarkRed };
            Controls.Add(notice); notice.BringToFront();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form28 form = new Form28();
            form.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form30 form = new Form30();
            form.Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form29 form = new Form29();
            form.Show();
            this.Close();
        }

        private void Form27_Load(object sender, EventArgs e)
        {

        }
    }
}
