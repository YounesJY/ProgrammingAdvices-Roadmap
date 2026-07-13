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
        public DateTimePicker dateOfBirth { get => dtpDateOfBirth; }
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
                txtNationalNumber.Focus();
            }
            else
            {
                errorProvider.SetError(txtNationalNumber, "");
            }
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
                errorProvider.SetError(textBox, "");
            }
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            Person person = new Person();

            person.FirstName = txtFirstName.Text.Trim();
            person.SecondName = txtSecondName.Text.Trim();
            person.ThirdName = txtThirdName.Text.Trim();
            person.LastName = txtLastName.Text.Trim();
            person.NationalNumber = txtNationalNumber.Text.Trim();
            person.Gender = rbGenderMale.Checked ? (byte)0 : (byte)1;
            person.DateOfBirth = dtpDateOfBirth.Value;
            person.Address = txtAddress.Text.Trim();
            person.Phone = txtPhone.Text.Trim();
            person.Email = txtEmail.Text.Trim();
            person.CountryID = (int)cbCountries.SelectedIndex;
            person.ProfilePhotoPath = pbProfileImage.Tag?.ToString() ?? "";
            // person.CreatedByUser = 1; // Current logged-in user ID, [not yet implemented therefore shouldn't be used for the current time]

            if (person.Save())
            {
                MessageBox.Show("Person saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Failed to save person.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtEmail.Text) && !IsValidEmail(txtEmail.Text))
            {
                errorProvider.SetError(txtEmail, "Please enter a valid email address.");
                e.Cancel = true; // Prevents leaving the control
                txtEmail.Focus();
            }
            else
            {
                errorProvider.SetError(txtEmail, "");
            }
        }
        private bool IsValidEmail(string email)
        {
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return System.Text.RegularExpressions.Regex.IsMatch(email, pattern);
        }
    }
}
