using System;
using System.Windows.Forms;

namespace SendDataBackToFromUsingDelgate
{
    public partial class Form2 : Form
    {
        /*
        // Declare a delegate, a delegate must include the minimum necessary information that handlers/subscribers can decide what to do based on
        public delegate void DataBackEventHandler(object sender, int PersonID);
        // Declare an event using the delegate
        public event DataBackEventHandler OnDataBack;
        */
        // Or use a built-in Generic Delegate 
        public event Action<object, int> OnDataBack;


        public Form2()
        {
            InitializeComponent();
        }


        private void SendDataBack_Click(object sender, EventArgs e)
        {
            int PersonID = int.Parse(txtPersonID.Text);

            // Trigger the event to send data back to Form1
            // This will notify all subscribers
            // The ".?" is a shorthand to check for the existance/absence of any handlers/subscribers for this event
            OnDataBack?.Invoke(this, PersonID);

            this.Close();
        }
    }
}
