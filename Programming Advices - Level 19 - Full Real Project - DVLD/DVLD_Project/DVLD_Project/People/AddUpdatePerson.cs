using System;
using System.Windows.Forms;
using DVLD_Business;

namespace DVLD_Project.People
{
    public partial class AddUpdatePerson : Form
    {
        public enum Mode { Add, Update };
        
        public Mode mode { get; private set; }

        public AddUpdatePerson()
        {
            InitializeComponent();
            mode = Mode.Add;
        }


        public AddUpdatePerson(int PersonID)
        {
            InitializeComponent();
            mode = Mode.Update;
            ctrlPersonDetails.fillWithPersonDetails(PersonID);
        }

        private void AddNewPerson_Load(object sender, EventArgs e)
        {
            setLabels();


            if (this.mode == Mode.Add)
            {
                ctrlPersonDetails.countriesList.DataSource = Country.getAllCountries();
                ctrlPersonDetails.countriesList.DisplayMember = "CountryName";  // The column name to display
                ctrlPersonDetails.countriesList.ValueMember = "CountryID";     // The column name to use as value
                ctrlPersonDetails.countriesList.SelectedIndex = ctrlPersonDetails.countriesList.FindString("Morocco");

                ctrlPersonDetails.dateOfBirth.MaxDate = DateTime.Now.AddYears(-18);
            }
        }

        private void setLabels()
        {
            lblFromLabel.Text = (mode == Mode.Add) ? "Add New Person" : "Update Person";
        }

    }
}
