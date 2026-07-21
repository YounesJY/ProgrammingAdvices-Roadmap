using DVLD_Business;
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
        private int _personID;
        public event Action<object, int> OnPersonCardDetailsUpdated;

        public ctrlPersonCard()
        {
            InitializeComponent();
        }

        public void loadPersonDetailsToCard(int personID)
        {
            this._personID = personID;
            Person person = Person.Find(personID);


            lblPersonIDValue.Text = person.PersonID.ToString();
            lblNationalNumberValue.Text = person.NationalNumber;
            lblNameValue.Text = person.Name;
            lblGenderValue.Text = (person.Gender == 0) ? "Male" : "Female";
            lblDateOfBirthValue.Text = person.DateOfBirth.ToString("dd/MM/yyyy");
            lblAddressValue.Text = person.Address;
            lblPhoneValue.Text = person.Phone;
            lblEmailValue.Text = person.Email;
            lblCountryValue.Text = Country.Find(person.CountryInfo.CountryID).CountryName;
            if (!string.IsNullOrEmpty(person.ProfilePhotoPath) && File.Exists(person.ProfilePhotoPath))
            {
                pbProfileImage.Image = Image.FromFile(person.ProfilePhotoPath);
                pbProfileImage.Tag = person.ProfilePhotoPath;
            }
        }
        private void lblEditPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AddUpdatePerson addUpdatePerson = new AddUpdatePerson(this._personID);
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
