using DVLD.Classes;
using DVLD_Business;
using DVLD_Project.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace DVLD_Project.People
{
    public partial class AddUpdatePerson : Form
    {
        public enum enMode { AddNew, Update };
        public event Action<object, int> OnPersonCreation;
        private Person _person;

        public int PersonID { get; private set; }
        public enMode Mode { get; private set; }


        public AddUpdatePerson()
        {
            InitializeComponent();
            Mode = enMode.AddNew;
            this._person = new Person();
        }
        public AddUpdatePerson(int PersonID)
        {
            InitializeComponent();
            Mode = enMode.Update;
            this.PersonID = PersonID;
        }
        private void AddNewPerson_Load(object sender, EventArgs e)
        {
            resetDefualtValues();
            if (this.Mode == enMode.Update)
                fillWithPersonDetails();
        }

        private void resetDefualtValues()
        {
            this._person = new Person();
            setLabels();
            fillCountriesInComoboBox();

            //set default image for the person.
            rbGenderMale.Checked = true;

            //hide/show the remove linke incase there is no image for the person.
            //llRemoveImage.Visible = (pbProfileImage.ImageLocation != null);

            //we set the max date to 18 years from today, and set the default value the same.
            dtpDateOfBirth.MaxDate = DateTime.Now.AddYears(-18);
            dtpDateOfBirth.Value = dtpDateOfBirth.MaxDate;
            //should not allow adding age more than 100 years
            dtpDateOfBirth.MinDate = DateTime.Now.AddYears(-100);

            txtFirstName.Text = "";
            txtSecondName.Text = "";
            txtThirdName.Text = "";
            txtLastName.Text = "";
            txtNationalNumber.Text = "";
            rbGenderMale.Checked = true;
            txtPhone.Text = "";
            txtEmail.Text = "";
            txtAddress.Text = "";
            cbCountries.SelectedIndex = cbCountries.FindString("Morocco");
        }
        private void setLabels()
        {
            lblFromLabel.Text = (Mode == enMode.AddNew) ? "Add New Person" : "Update Person";
        }
        private void fillWithPersonDetails()
        {
            _person = Person.Find(this.PersonID);

            if (_person == null)
            {
                MessageBox.Show("Person not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            lblPersonID.Text = _person.PersonID.ToString();
            txtNationalNumber.Text = _person.NationalNumber;
            txtFirstName.Text = _person.FirstName;
            txtSecondName.Text = _person.SecondName;
            txtThirdName.Text = _person.ThirdName;
            txtLastName.Text = _person.LastName;

            // Set Gender
            if (_person.Gender == (byte)Person.enGender.Male)
                rbGenderMale.Checked = true;
            else
                rbGenderFemale.Checked = true;

            dtpDateOfBirth.Value = _person.DateOfBirth;
            txtAddress.Text = _person.Address;
            txtPhone.Text = _person.Phone;
            txtEmail.Text = _person.Email;
            cbCountries.SelectedIndex = _person.CountryInfo.CountryID;

            // Set Image
            if (!string.IsNullOrEmpty(_person.ProfilePhotoPath) && File.Exists(_person.ProfilePhotoPath))
            {
                pbProfileImage.Image = Image.FromFile(_person.ProfilePhotoPath);
                pbProfileImage.Tag = _person.ProfilePhotoPath;
            }
            else
            {
                pbProfileImage.Image = (_person.Gender == (byte)Person.enGender.Male) ?
                    Resources.male : Resources.female;
                pbProfileImage.Tag = _person.Gender == (byte)Person.enGender.Male ? "male" : "female";
            }
        }
        private void fillCountriesInComoboBox()
        {
            cbCountries.DataSource = Country.getAllCountries();
            cbCountries.DisplayMember = "CountryName";  // The column name to display
            cbCountries.ValueMember = "CountryID";     // The column name to use as value
            cbCountries.SelectedIndex = cbCountries.FindString("Morocco");
        }


        private void EmptyTextBox_Validating(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (String.IsNullOrEmpty(textBox.Text))
            {
                errorProvider.SetError(textBox, "This field is required !.");
                textBox.Focus();
            }
            else
                errorProvider.SetError(textBox, "");
        }
        private void txtNationalNumber_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNationalNumber.Text))
            {
                errorProvider.SetError(txtNationalNumber, "National Number is required.");
                e.Cancel = true;
                return;
            }

            if (Person.IsPersonExist(txtNationalNumber.Text.Trim()) && this.Mode == AddUpdatePerson.enMode.AddNew)
            {
                errorProvider.SetError(txtNationalNumber, "A person with this National Number already exists.");
                e.Cancel = true;
            }

            else
                errorProvider.SetError(txtNationalNumber, "");

            if (txtNationalNumber.Text.Trim() != this._person.NationalNumber && Person.IsPersonExist(txtNationalNumber.Text.Trim()) && this.Mode == AddUpdatePerson.enMode.Update)
            {
                errorProvider.SetError(txtNationalNumber, "A person with this National Number already exists.");
                e.Cancel = true;
            }
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
        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtEmail.Text) && !Validatoin.ValidateEmail(txtEmail.Text))
            {
                errorProvider.SetError(txtEmail, "Please enter a valid email address.");
                e.Cancel = true; // Prevents leaving the control
                txtEmail.Focus();
            }
            else
                errorProvider.SetError(txtEmail, "");
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
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the error", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Check NationalNumber specifically (since it uses TextChanged, not Validating)
            if (!string.IsNullOrWhiteSpace(txtNationalNumber.Text.Trim()) && Person.IsPersonExist(txtNationalNumber.Text.Trim()) && this.Mode == enMode.AddNew)
            {
                MessageBox.Show("This National Number already exists. Please enter a unique number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNationalNumber.Focus();
                return;
            }

            Person person;
            person = (this.Mode == AddUpdatePerson.enMode.AddNew) ? new Person() : Person.Find(int.Parse(lblPersonID.Text));

            person.FirstName = txtFirstName.Text.Trim();
            person.SecondName = txtSecondName.Text.Trim();
            person.ThirdName = txtThirdName.Text.Trim();
            person.LastName = txtLastName.Text.Trim();
            person.NationalNumber = txtNationalNumber.Text.Trim();
            person.Gender = rbGenderMale.Checked ? Person.enGender.Male : Person.enGender.Female;
            person.DateOfBirth = dtpDateOfBirth.Value;
            person.Address = txtAddress.Text.Trim();
            person.Phone = txtPhone.Text.Trim();
            person.Email = txtEmail.Text.Trim();
            person.CountryInfo = Country.Find(cbCountries.SelectedIndex);
            person.ProfilePhotoPath = pbProfileImage.Tag?.ToString() ?? "";
            //    person.CreatedByUser = 1;

            if (person.Save())
            {
                MessageBox.Show("Person saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Mode = enMode.Update;
                setLabels();
                OnPersonCreation?.Invoke(this, person.PersonID);
            }
            else
                MessageBox.Show("Failed to save person.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
