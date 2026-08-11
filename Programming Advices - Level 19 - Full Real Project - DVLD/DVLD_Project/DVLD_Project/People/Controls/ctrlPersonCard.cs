using DVLD_Business;
using DVLD_Project.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project.People
{
    public partial class ctrlPersonCard : UserControl
    {
        public event Action<object, int> OnPersonCardDetailsUpdated;

        enum enGender { Male, Female }
        public Person Person { get; private set; }


        public ctrlPersonCard()
        {
            InitializeComponent();
        }
        public void LoadPersonDetailsToCard(int personID)
        {
            if (personID <= 0)
            {
                ResetPersonDetails();
                MessageBox.Show($"Invalid PersonID = {personID}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.Person = Person.Find(personID);
            if (Person == null)
            {
                ResetPersonDetails();
                MessageBox.Show($"No Person with PersonID = {personID}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            FillPersonDetails();
        }
        public void LoadPersonDetailsToCard(string nationalNumber)
        {
            if (string.IsNullOrWhiteSpace(nationalNumber))
            {
                ResetPersonDetails();
                MessageBox.Show($"Invalid National Number = {nationalNumber}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.Person = Person.Find(nationalNumber);
            if (Person == null)
            {
                ResetPersonDetails();

                MessageBox.Show($"No Person with National No. = {nationalNumber}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            FillPersonDetails();
        }

        public void ResetPersonDetails()
        {
            this.Person = null;
            lblPersonIDValue.Text = "[????]";
            lblNationalNumberValue.Text = "[????]";
            lblNameValue.Text = "[????]";
            pbGender.Image = Resources.Man_32;
            lblGenderValue.Text = "[????]";
            lblEmailValue.Text = "[????]";
            lblPhoneValue.Text = "[????]";
            lblDateOfBirthValue.Text = "[????]";
            lblCountryValue.Text = "[????]";
            lblAddressValue.Text = "[????]";
            pbProfileImage.Image = Resources.Male_512;
            lblEditPersonInfo.Visible = this.Person != null;
        }
        private void FillPersonDetails()
        {
            lblPersonIDValue.Text = this.Person.PersonID.ToString();
            lblNationalNumberValue.Text = this.Person.NationalNumber;
            lblNameValue.Text = this.Person.FullName;
            lblGenderValue.Text = (this.Person.Gender == Person.enGender.Male) ? "Male" : "Female";
            pbGender.Image = (this.Person.Gender == Person.enGender.Male) ? Resources.Man_32 : Resources.Woman_32;
            lblDateOfBirthValue.Text = this.Person.DateOfBirth.ToString("dd/MM/yyyy");
            lblAddressValue.Text = this.Person.Address;
            lblPhoneValue.Text = this.Person.Phone;
            lblEmailValue.Text = this.Person.Email;
            lblCountryValue.Text = Country.Find(this.Person.CountryInfo.CountryID).CountryName;


            if (!string.IsNullOrEmpty(this.Person.ProfilePhotoPath) && File.Exists(this.Person.ProfilePhotoPath))
            {
                pbProfileImage.Image = Image.FromFile(this.Person.ProfilePhotoPath);
                pbProfileImage.ImageLocation = this.Person.ProfilePhotoPath;
            }
            else
            {
                pbProfileImage.Image = (this.Person.Gender == Person.enGender.Male) ? Resources.Male_512 : Resources.Female_512;
                pbProfileImage.ImageLocation = null;
            }

            lblEditPersonInfo.Enabled = true;
        }

        private void lblEditPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAddUpdatePerson addUpdatePerson = new frmAddUpdatePerson(this.Person.PersonID);

            try
            {
                if (addUpdatePerson != null)
                    addUpdatePerson.OnPersonAddUpdate += refreshDataOnUpdate;
                addUpdatePerson.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while updating person info: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (addUpdatePerson != null)
                    addUpdatePerson.OnPersonAddUpdate -= refreshDataOnUpdate;
            }
        }
        private void refreshDataOnUpdate(object sender, int PersonID)
        {
            this.LoadPersonDetailsToCard(this.Person.PersonID);
            OnPersonCardDetailsUpdated?.Invoke(this, this.Person.PersonID);
        }
    }
}