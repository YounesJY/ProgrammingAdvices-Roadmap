using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleEventWithPrameters
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void myUserControl1_OnCalculationComplete(object sender, MyUserControl.CalculationCompleteEventArgs e)
        {
            MessageBox.Show($"Results={e.Results} , val1 = {e.Val1}, vale2 = {e.Val2}");
        }
    }
}
