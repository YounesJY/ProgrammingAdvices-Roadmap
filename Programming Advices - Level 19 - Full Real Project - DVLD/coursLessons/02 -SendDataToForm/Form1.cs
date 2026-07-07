using System;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void btnSendData_Click(object sender, EventArgs e)
        {
            int PersonID = -1;

            if (int.TryParse(txtPersonID.Text, out PersonID))
            {
                Form frm = new Form2(PersonID);
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("PersonID should be a number!");
                txtPersonID.Focus();
            }
        }
    }
}
