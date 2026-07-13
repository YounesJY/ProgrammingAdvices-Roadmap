using DVLD_Business;
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
    public partial class ctrlPersonCard : UserControl
    {
        public ctrlPersonCard()
        {
            InitializeComponent();
        }

        public void loadPersonDetailsToCard(int PersonID)
        {
            Person person = Person.Find(PersonID);

            lblPersonIDValue.Text = person.PersonID.ToString();
            lblNationalNumberValue.Text = person.NationalNumber;
            lblNameValue.Text = person.Name;
            lblGenderValue.Text = (person.Gender == 0) ? "Male" : "Female";
            lblDateOfBirthValue.Text = person.DateOfBirth.ToString("dd/MM/yyyy");
            lblAddressValue.Text = person.Address;
            lblPhoneValue.Text = person.Phone;
            lblEmailValue.Text = person.Email;
            lblCountryValue.Text = Country.Find(person.CountryID).CountryName;
        }

    }
}
