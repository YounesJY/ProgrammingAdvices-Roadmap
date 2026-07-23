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
        enum enGender { Male, Female }
        private int _personID;
        public Person Person { get; private set; }
        public event Action<object, int> OnPersonCardDetailsUpdated;

        public ctrlPersonCard()
        {
            InitializeComponent();
        }
        public void loadPersonDetailsToCard(int personID)
        {
            this.Person = Person.Find(personID);
            if (Person == null)
            {
                resetPersonInfo();
                MessageBox.Show($"No Person with PersonID = {personID}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            fillPersonInfo();
        }
        public void loadPersonDetailsToCard(string nationalNumber)
        {
            this.Person = Person.Find(nationalNumber);
            if (Person == null)
            {
                resetPersonInfo();
                MessageBox.Show($"No Person with National No. = {nationalNumber}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            fillPersonInfo();
        }

        private void fillPersonInfo()
        {
            this._personID = this.Person.PersonID;
            lblPersonIDValue.Text = this.Person.PersonID.ToString();
            lblNationalNumberValue.Text = this.Person.NationalNumber;
            lblNameValue.Text = this.Person.Name;
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
        }
        public void resetPersonInfo()
        {
            this._personID = -1;
            lblPersonID.Text = "[????]";
            lblNationalNumber.Text = "[????]";
            lblName.Text = "[????]";
            pbGender.Image = Resources.Man_32;
            lblGender.Text = "[????]";
            lblEmail.Text = "[????]";
            lblPhone.Text = "[????]";
            lblDateOfBirth.Text = "[????]";
            lblCountry.Text = "[????]";
            lblAddress.Text = "[????]";
            pbProfileImage.Image = Resources.Male_512;
        }
        private void lblEditPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAddUpdatePerson addUpdatePerson = new frmAddUpdatePerson(this._personID);
            addUpdatePerson.OnPersonAddUpdate += refreshDataOnUpdate;
            addUpdatePerson.ShowDialog();
            addUpdatePerson.OnPersonAddUpdate -= refreshDataOnUpdate;
        }
        private void refreshDataOnUpdate(object sender, int PersonID)
        {
            this.loadPersonDetailsToCard(_personID);
            OnPersonCardDetailsUpdated?.Invoke(this, this._personID);
        }
    }
}
