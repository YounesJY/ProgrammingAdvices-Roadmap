using ImageExercice.Properties;
using System;
using System.Windows.Forms;

namespace ImageExercice
{
    public partial class ImageForm : Form
    {
        public ImageForm()
        {
            InitializeComponent();
        }
        private void Form_Load(object sender, EventArgs e)
        {
            rbImageBoy.Checked = true;
        }

        private void rbImageBoy_CheckedChanged(object sender, EventArgs e)
        {
            imageLabel.Text = rbImageBoy.Tag.ToString().ToUpper();
            pictureBox.Image = Resources.boyImage;
        }
        private void rbImageGirl_CheckedChanged(object sender, EventArgs e)
        {
            imageLabel.Text = rbImageGirl.Tag.ToString().ToUpper();;
            pictureBox.Image = Resources.girlImage;
        }
        private void rbImageBooks_CheckedChanged(object sender, EventArgs e)
        {
            imageLabel.Text = rbImageBooks.Tag.ToString().ToUpper();;
            pictureBox.Image = Resources.bookImage;
        }
        private void rbImagePen_CheckedChanged(object sender, EventArgs e)
        {
            imageLabel.Text = rbImagePen.Tag.ToString().ToUpper();;
            pictureBox.Image = Resources.penImage;
        }
    }
}
