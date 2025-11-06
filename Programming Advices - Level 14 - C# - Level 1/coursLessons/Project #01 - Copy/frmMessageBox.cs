using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project__01___Copy
{
    public partial class frmMessageBox : Form
    {
        public frmMessageBox()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Hi, this is C# message box");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Hi, this is C# message box", "Caption");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure to make this operation ?", "Verification", MessageBoxButtons.YesNo) == DialogResult.Yes)
                MessageBox.Show("Operation done successfuly");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure to make this operation ?", "Verification", MessageBoxButtons.YesNo, MessageBoxIcon.Hand ) == DialogResult.Yes)
                MessageBox.Show("Operation done successfuly");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure to make this operation ?", "Verification", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                MessageBox.Show("Operation done successfuly");
        }
    }
}
