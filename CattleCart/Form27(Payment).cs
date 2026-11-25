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
    public partial class Form27_Payment_: CattleCart.AppForm
    {
        public Form27_Payment_()
        {
            InitializeComponent();
            button1.Click += (s,e) => { new Form27().Show(); Close(); };
            button2.Click += (s,e) => { new ViewForm { CattleId=Session.CattleId }.Show(); Close(); };
            button3.Click += (s,e) => { new customerDashboard().Show(); Close(); };
        }
    }
}
