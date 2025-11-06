using ImageExercice.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageExercice
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void rbImageBoy_CheckedChanged(object sender, EventArgs e)
        {
            imageLabel.Text = rbImageBoy.Text.ToUpper();
            pictureBox.Image = Resources.boyImage;
        }

        private void rbImageGirl_CheckedChanged(object sender, EventArgs e)
        {
            imageLabel.Text = rbImageGirl.Text.ToUpper();
            pictureBox.Image = Resources.girlImage;
        }

        private void rbImageBooks_CheckedChanged(object sender, EventArgs e)
        {
            imageLabel.Text = rbImageBooks.Text.ToUpper();
            pictureBox.Image = Resources.bookImage;
        }

        private void rbImagePen_CheckedChanged(object sender, EventArgs e)
        {
            imageLabel.Text = rbImagePen.Text.ToUpper();
            pictureBox.Image = Resources.penImage;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            rbImageBoy.Checked = true;
        }
    }
}
