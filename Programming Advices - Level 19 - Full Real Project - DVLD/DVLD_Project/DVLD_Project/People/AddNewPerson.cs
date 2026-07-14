using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD_Business;

namespace DVLD_Project.People
{
    public partial class AddNewPerson : Form
    {
        public AddNewPerson()
        {
            InitializeComponent();
        }

        private void AddNewPerson_Load(object sender, EventArgs e)
        {
            ComboBox cbCountries = ctrlPersonDetails.countriesList;
            
            cbCountries.DataSource = Country.getAllCountries();
            cbCountries.DisplayMember = "CountryName";  // The column name to display
            cbCountries.ValueMember = "CountryID";     // The column name to use as value
            cbCountries.SelectedIndex = cbCountries.FindString("Morocco");

            ctrlPersonDetails.dateOfBirth.MaxDate = DateTime.Now.AddYears(-18);
        }
    }
}
