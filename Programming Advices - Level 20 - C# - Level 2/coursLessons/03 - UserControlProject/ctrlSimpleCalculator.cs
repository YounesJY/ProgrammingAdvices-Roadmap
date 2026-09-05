using System;
using System.Windows.Forms;

namespace MyFirstUserControlProject
{
    public partial class ctrlSimpleCalculator : UserControl
    {
        // public Double lblResult => Convert.ToDouble(lblResults.Text);
        public String OperationResult
        {
            get { return this.lblResult.Text; }
            set { this.lblResult.Text = value; }
        }


        public ctrlSimpleCalculator()
        {
            InitializeComponent();
        }


        private void btnCalculate_Click(object sender, EventArgs e)
        {
            lblResult.Text = (int.Parse(textBox2.Text) + int.Parse(textBox2.Text)).ToString();
        }
    }
}
