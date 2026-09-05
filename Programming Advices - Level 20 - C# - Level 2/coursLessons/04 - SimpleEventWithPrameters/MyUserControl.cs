using System;
using System.Windows.Forms;


namespace SimpleEventWithPrameters
{
    public partial class MyUserControl : UserControl
    {
        // Define a custom event handler delegate with parameters
        public event Action<int> OnCalculationComplete;


        public MyUserControl()
        {
            InitializeComponent();
        }


        // Create a protected method to raise the event with a parameter
        // WHY SEPARATE METHOD? Encapsulation, reusability, inheritance, maintainability, and standard .NET pattern.
        protected virtual void CalculationComplete(int Result)
        {
            // Passing data from UserControl to Form1(child → parent communication)
            // WHY COPY TO LOCAL? Thread safety (prevents race condition null refs).
            Action<int> handler = OnCalculationComplete;
            handler?.Invoke(Result); // Raise the event with the parameter
        }
        private void btnCalculate_Click(object sender, EventArgs e)
        {
            int Result = Convert.ToInt32(txtNumber1.Text) + Convert.ToInt32(txtNumber2.Text);

            lblResult.Text = Result.ToString();
            if (OnCalculationComplete != null)
                CalculationComplete(Result);
        }
    }

}


