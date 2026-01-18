using System;
using System.Windows.Forms;

namespace Project__01___Copy
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        private void MainForm_Load(object sender, EventArgs e)
        {

        }


        private void button1_Click(object sender, EventArgs e)
        {
            textBox2.Text = textBox1.Text;
        }

        private void button2_OnMousehover(object sender, EventArgs e)
        {
            textBox2.Text = textBox1.Text;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Enabled = false;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Enabled = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Visible = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox1.Visible = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form msgBoxFrom = new frmMessageBox();
            msgBoxFrom.ShowDialog();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox2.Text = textBox1.Text;
        }

        private void btnCloseMainForm_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
