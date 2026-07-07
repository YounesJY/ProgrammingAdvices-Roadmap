using System.Windows.Forms;

namespace SimpleEventWithPrameters
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void myUserControl1_OnCalculationComplete(int obj)
        {
            int Results = obj;
            MessageBox.Show($"Results = {obj}");
        }
    }
}
