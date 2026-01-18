using chooseImage.Properties;
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
    public partial class formChooseImage : Form
    {
        public formChooseImage()
        {
            InitializeComponent();
        }
        private void Form_Load(object sender, EventArgs e)
        {
            cbList.SelectedIndex = 0;
            imageLabel.Text = cbList.SelectedItem.ToString().ToUpper();
            pictureBox.Image = getImage();
        }


        private Image getImage()
        {
            switch (cbList.SelectedItem.ToString())
            {
                case "Boy":
                    return Resources.boyImage;
                case "Girl":
                    return Resources.girlImage;
                case "Books":
                    return Resources.bookImage;
                case "Pen":
                    return Resources.penImage;
                default:
                    return Resources.boyImage;
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            imageLabel.Text = cbList.Text.ToUpper();
            pictureBox.Image = getImage();
        }
    }
}
