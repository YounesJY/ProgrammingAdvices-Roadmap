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
    public partial class MyUserControl : UserControl
    {
        /*
            this is the old code....commented.
            // Define a custom event handler delegate with parameters
            public event Action<int> OnCalculationComplete;
            // Create a protected method to raise the event with a parameter
            protected virtual void CalculationComplete(int PersonID)
            {
                Action<int> handler = OnCalculationComplete;
                if (handler != null)
                {
                    handler(PersonID); // Raise the event with the parameter
                }
            }
        */

        public event EventHandler<CalculationCompleteEventArgs> OnCalculationComplete;
        public class CalculationCompleteEventArgs : EventArgs
        {
            public int Results { get; }
            public int Val1 { get; }
            public int Val2 { get; }


            public CalculationCompleteEventArgs(int Results, int Val1, int Val2)
            {
                this.Results = Results;
                this.Val1 = Val1;
                this.Val2 = Val2;
            }
        }


        public MyUserControl()
        {
            InitializeComponent();
        }


        protected void RaiseOnCalulationComplete(CalculationCompleteEventArgs e)
        {
            OnCalculationComplete?.Invoke(this, e);
        }
        public void RaiseOnCalulationComplete(int Results, int Val1, int Val2)
        {
            RaiseOnCalulationComplete(new CalculationCompleteEventArgs(Results, Val1, Val2));
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            int val1, val2;
            val1 = Convert.ToInt32(txtNumber1.Text);
            val2 = Convert.ToInt32(txtNumber2.Text);

            int result = val1 + val2;
            lblResult.Text = result.ToString();

            if (OnCalculationComplete != null)
                RaiseOnCalulationComplete(result, val1, val2);
        }
    }
}
