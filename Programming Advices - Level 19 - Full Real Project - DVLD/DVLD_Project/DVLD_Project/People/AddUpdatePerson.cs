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
        public event Action<object, int> OnPersonAddUpdate;

        private Person _person = null;
        private int _personId = -1;


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
            this._personId = PersonID;
        }
        private void AddNewPerson_Load(object sender, EventArgs e)
        {
            resetDefualtValues();
            if (this.Mode == enMode.Update)
                fillWithPersonDetails();
        }

        private void setLabels()
        {
            lblFromLabel.Text = (Mode == enMode.AddNew) ? "Add New Person" : "Update Person";
        }
        private void resetDefualtValues()
        {
            this._person = new Person();
            setLabels();
            fillCountriesInComoboBox();

            //set default image for the person.
            rbGenderMale.Checked = true;
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
            cbCountry.SelectedIndex = cbCountry.FindString("Morocco");
            pbProfileImage.ImageLocation = null;
            llRemoveImage.Visible = false;
        }
        private void fillWithPersonDetails()
        {
            _person = Person.Find(this._personId);

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
            cbCountry.SelectedIndex = _person.CountryInfo.CountryID;

            // Set Image
            if (!string.IsNullOrEmpty(_person.ProfilePhotoPath) && File.Exists(_person.ProfilePhotoPath))
                pbProfileImage.ImageLocation = _person.ProfilePhotoPath;
            else
            {
                pbProfileImage.Image = (_person.Gender == (byte)Person.enGender.Male) ? Resources.male : Resources.female;
                pbProfileImage.ImageLocation = null;
            }
            //hide/show the remove linke incase there is no image for the person.
            llRemoveImage.Visible = (pbProfileImage.ImageLocation != null);
        }
        private void fillCountriesInComoboBox()
        {
            cbCountry.DataSource = Country.getAllCountries();
            cbCountry.DisplayMember = "CountryName";  // The column name to display
            cbCountry.ValueMember = "CountryID";     // The column name to use as value
            cbCountry.SelectedIndex = cbCountry.FindString("Morocco");
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
                return;
            }

            if (txtNationalNumber.Text.Trim() != this._person.NationalNumber && Person.IsPersonExist(txtNationalNumber.Text.Trim()) && this.Mode == AddUpdatePerson.enMode.Update)
            {
                errorProvider.SetError(txtNationalNumber, "A person with this National Number already exists.");
                e.Cancel = true;
                return;
            }
        }
        private void rbGenderMale_CheckedChanged(object sender, EventArgs e)
        {
            if (rbGenderMale.Checked && pbProfileImage.ImageLocation == null)
            {
                pbProfileImage.Image = Properties.Resources.male;
                pbProfileImage.ImageLocation = null;
            }
        }
        private void rbGenderFemale_CheckedChanged(object sender, EventArgs e)
        {
            if (rbGenderFemale.Checked && pbProfileImage.ImageLocation == null)
            {
                pbProfileImage.Image = Properties.Resources.female;
                pbProfileImage.ImageLocation = null;
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
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Process the selected file
                string selectedFilePath = openFileDialog.FileName;
                //pbProfileImage.Load(selectedFilePath); // this can cause image lock for delation
                pbProfileImage.ImageLocation = selectedFilePath;

                llRemoveImage.Visible = true;
            }
        }
        private void llRemoveImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pbProfileImage.ImageLocation = null;
            pbProfileImage.Image = (rbGenderMale.Checked) ? Resources.male : Resources.female;
            llRemoveImage.Visible = false;
        }
        private bool handlePersonImage()
        {
            //this procedure will handle the person image,
            //it will take care of deleting the old image from the folder
            //in case the image changed. and it will rename the new image with guid and 
            // place it in the images folder.


            //_Person.ProfilePhotoPath contains the old Image, we check if it changed then we copy the new image
            if (_person.ProfilePhotoPath != pbProfileImage.ImageLocation)
            {
                // this case means that the person still holds the old profile image that's been loadoed from DB
                // and it's not the current one in the control
                if (!String.IsNullOrEmpty(_person.ProfilePhotoPath))
                {
                    //first we delete the old image from the folder in case there is any.

                    try
                    {
                        File.Delete(_person.ProfilePhotoPath);
                    }
                    catch (IOException)
                    {
                        // We could not delete the file.
                        //log it later   
                    }
                }

                if (!String.IsNullOrEmpty(pbProfileImage.ImageLocation))
                {
                    //then we copy the new image to the image folder after we rename it
                    string SourceImageFile = pbProfileImage.ImageLocation.ToString();

                    if (Utils.CopyImageToProjectImagesFolder(ref SourceImageFile))
                    {
                        pbProfileImage.ImageLocation = SourceImageFile;
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Error Copying Image File", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }

            }
            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the error", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!handlePersonImage())
                return;

            // Check NationalNumber specifically (since it uses TextChanged, not Validating)
            if (!string.IsNullOrWhiteSpace(txtNationalNumber.Text.Trim()) && Person.IsPersonExist(txtNationalNumber.Text.Trim()) && this.Mode == enMode.AddNew)
            {
                MessageBox.Show("This National Number already exists. Please enter a unique number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNationalNumber.Focus();
                return;
            }

            _person = (this.Mode == AddUpdatePerson.enMode.AddNew) ? new Person() : Person.Find(int.Parse(lblPersonID.Text));

            _person.NationalNumber = txtNationalNumber.Text.Trim();
            _person.FirstName = txtFirstName.Text.Trim();
            _person.SecondName = txtSecondName.Text.Trim();
            _person.ThirdName = txtThirdName.Text.Trim();
            _person.LastName = txtLastName.Text.Trim();
            _person.NationalNumber = txtNationalNumber.Text.Trim();
            _person.Gender = rbGenderMale.Checked ? Person.enGender.Male : Person.enGender.Female;
            _person.DateOfBirth = dtpDateOfBirth.Value;
            _person.Address = txtAddress.Text.Trim();
            _person.Phone = txtPhone.Text.Trim();
            _person.Email = txtEmail.Text.Trim();
            _person.CountryInfo = Country.Find(cbCountry.SelectedIndex);
            _person.ProfilePhotoPath = pbProfileImage.ImageLocation; ;
            //    person.CreatedByUser = 1;

            if (_person.Save())
            {
                MessageBox.Show("Person saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                setLabels();
                this.Mode = enMode.Update;
                lblPersonID.Text = _person.PersonID.ToString();

                OnPersonAddUpdate?.Invoke(this, _person.PersonID);
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
