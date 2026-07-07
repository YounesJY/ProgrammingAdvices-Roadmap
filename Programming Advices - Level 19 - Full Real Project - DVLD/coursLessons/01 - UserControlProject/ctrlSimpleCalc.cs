using System;
using System.Windows.Forms;

namespace MyFirstUserControlProject
{
    public partial class ctrlSimpleCalc : UserControl
    {
        public Double lblResult =>  Convert.ToDouble(lblResults.Text);

        public ctrlSimpleCalc()
        {
            InitializeComponent();
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            lblResults.Text = (int.Parse( textBox2.Text) + int.Parse(textBox2.Text)).ToString();
        }

        private void ctrlSimpleCalc_Load(object sender, EventArgs e)
        {
            
        }
        
    }
}
