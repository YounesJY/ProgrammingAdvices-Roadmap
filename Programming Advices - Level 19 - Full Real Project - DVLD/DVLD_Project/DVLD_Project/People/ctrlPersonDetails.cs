using DVLD_Business;
using DVLD_Project.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project.People
{
    public partial class ctrlPersonDetails : UserControl
    {
        public ComboBox countriesList { get => cbCountries; }

        public ctrlPersonDetails()
        {
            InitializeComponent();
            rbGenderMale.Checked = true;
        }
        private void rbGenderMale_CheckedChanged(object sender, EventArgs e)
        {
            if (rbGenderMale.Checked)
            {
                pbProfileImage.Image = Properties.Resources.male;
            }
        }

        private void rbGenderFemale_CheckedChanged(object sender, EventArgs e)
        {
            if (rbGenderFemale.Checked)
            {
                pbProfileImage.Image = Properties.Resources.female;
            }
        }

        private void txtNationalNumber_TextChanged(object sender, EventArgs e)
        {
            if (Person.IsPersonExist(txtNationalNumber.Text.Trim()))
            {
                errorProvider.SetError(txtNationalNumber, "A person with this National Number already exists.");
            }
            else
            {
                errorProvider.SetError(txtNationalNumber, "");
            }
            txtNationalNumber.Focus();
        }


        private void txtFieldIsEmpty_Leave(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (String.IsNullOrEmpty(textBox.Text))
            {
                errorProvider.SetError(textBox, "This field is required !.");
                textBox.Focus();
            }
            else
            {
                errorProvider.SetError(txtNationalNumber, "");
            }
        }
    }
}
