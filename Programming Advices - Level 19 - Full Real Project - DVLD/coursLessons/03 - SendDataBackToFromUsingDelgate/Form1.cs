using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SendDataBackToFromUsingDelgate
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void btnOpenForm2_Click(object sender, EventArgs e)
        {
            Form2 frm2 = new Form2();

            // Passing data back from Form2 to Form1(two - way communication)
            // Subscribe to the event using a multicast delegate
            frm2.OnDataBack += Form2_DataBack;
            /*
            this can be written using a lambda expression as :
            frm2.DataBack += (s, PersonID) => textBox1.Text = PersonID.ToString();
            */
            frm2.ShowDialog();
        }

        private void Form2_DataBack(object sender, int PersonID)
        {
            // Handle the data received from Form2
            textBox1.Text = PersonID.ToString();
        }
    }
}
