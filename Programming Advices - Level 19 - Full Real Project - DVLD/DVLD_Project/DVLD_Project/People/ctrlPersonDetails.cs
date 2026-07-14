using DVLD_Business;
using DVLD_Project.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
            if (rbGenderMale.Checked && pbProfileImage.Tag?.ToString() == "female")
            {
                pbProfileImage.Image = Properties.Resources.male;
                pbProfileImage.Tag = "male";
            }
        }

        private void rbGenderFemale_CheckedChanged(object sender, EventArgs e)
        {
            if (rbGenderFemale.Checked && pbProfileImage.Tag?.ToString() == "male")
            {
                pbProfileImage.Image = Properties.Resources.female;
                pbProfileImage.Tag = "female";
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

        private void llSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Create OpenFileDialog
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
            openFileDialog.Title = "Select a Profile Image";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Get the selected file path
                    string sourceFilePath = openFileDialog.FileName;

                    // Create directory if it doesn't exist
                    string imagesDirectory = @"C:\DVLD-People_Images";
                    if (!Directory.Exists(imagesDirectory))
                        Directory.CreateDirectory(imagesDirectory);

                    // Generate GUID for the image name
                    string imageGuid = Guid.NewGuid().ToString();
                    string fileExtension = Path.GetExtension(sourceFilePath);
                    string newFileName = imageGuid + fileExtension;
                    string destinationFilePath = Path.Combine(imagesDirectory, newFileName);

                    // Copy the image to the destination folder
                    File.Copy(sourceFilePath, destinationFilePath, true);

                    // Save the path in the person object
                    pbProfileImage.Tag = destinationFilePath;

                    // Display the image in the PictureBox
                    pbProfileImage.Image = Image.FromFile(destinationFilePath);
                    pbProfileImage.SizeMode = PictureBoxSizeMode.Zoom;

                    MessageBox.Show("Image uploaded successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error uploading image: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool isFormFilledCorrectly()
        {
            return !String.IsNullOrEmpty(txtFirstName.Text) && !String.IsNullOrEmpty(txtLastName.Text)
            && !String.IsNullOrEmpty(txtEmail.Text) && !String.IsNullOrEmpty(txtAddress.Text);

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Force validation on all controls
            if (!ValidateChildren(ValidationConstraints.Enabled))
            {
                MessageBox.Show("Please fix all validation errors before saving.", "Validation Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Check NationalNumber specifically (since it uses TextChanged, not Validating)
            if (!string.IsNullOrWhiteSpace(txtNationalNumber.Text) && Person.IsPersonExist(txtNationalNumber.Text.Trim()))
            {
                MessageBox.Show("This National Number already exists. Please enter a unique number.", "Validation Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNationalNumber.Focus();
                return;
            }

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
            person.CountryID = (int)cbCountries.SelectedValue;
            person.ProfilePhotoPath = pbProfileImage.Tag?.ToString() ?? "";
            //    person.CreatedByUser = 1;

            if (person.Save())
            {
                MessageBox.Show("Person saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Failed to save person.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void txtNationalNumber_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNationalNumber.Text))
            {
                errorProvider.SetError(txtNationalNumber, "National Number is required.");
                e.Cancel = true;
                return;
            }

            if (Person.IsPersonExist(txtNationalNumber.Text.Trim()))
            {
                errorProvider.SetError(txtNationalNumber, "A person with this National Number already exists.");
                e.Cancel = true;
            }
            else
            {
                errorProvider.SetError(txtNationalNumber, "");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.ParentForm.Close();
        }
    }
}
