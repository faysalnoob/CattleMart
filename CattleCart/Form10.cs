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
    public partial class Form10 : CattleCart.AppForm
    {
        public Form10()
        {
            InitializeComponent();
            button1.Click += (s,e) => { new customerDashboard().Show(); Close(); };
        }
    }
}
