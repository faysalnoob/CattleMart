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
    public partial class AllRightsReserved: CattleCart.AppForm
    {
        public AllRightsReserved()
        {
            InitializeComponent();
            var back = new Button { Text="Back", Location=new Point(10,10), AutoSize=true };
            back.Click += (s,e) => { new intropage().Show(); Close(); }; Controls.Add(back); back.BringToFront();
        }

        private void AllRightsReserved_Load(object sender, EventArgs e)
        {

        }
    }
}
