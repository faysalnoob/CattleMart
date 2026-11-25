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
    public partial class customerDashboard : CattleCart.AppForm
    {
        
        // New constructor with contact parameter
        public customerDashboard()
        {
            InitializeComponent();
            var buffalo = new Button { Text="Browse Buffalo", Location=new Point(10,470), Size=new Size(200,45), BackColor=Color.NavajoWhite };
            buffalo.Click += (s,e) => { new Form14("Buffalo").Show(); Close(); };
            Controls.Add(buffalo); buffalo.BringToFront();
            this.StartPosition = FormStartPosition.CenterScreen;
        
        }

        private void Form9_Load(object sender, EventArgs e)
        {
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form11 form11 = new Form11();
            form11.Show(); // Change from Form11.Show() to form11.Show()            
            this.Close();
        }
        

        private void button5_Click_1(object sender, EventArgs e)
        {
            Form12 form12 = new Form12();
            form12.Show(); // Change from Form12.Show() to form12.Show()
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            
            Form13 form13 = new Form13();
            form13.Show(); // Change from Form13.Show() to form13.Show()
            this.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            
            Form14 form14 = new Form14();
            form14.Show(); // Change from Form14.Show() to form14.Show()
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form15 form15 = new Form15();
            form15.Show(); // Change from Form15.Show() to form15.Show()
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form16 form16 = new Form16();
            form16.Show(); // Change from Form16.Show() to form16.Show()
            this.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            loginPage loginform  = new loginPage();
            loginform.Show(); // Change from loginPage.Show() to login.Show()
            this.Close();
        }
    }
}
