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
using static DVLD_Project.People.ctrlPersonCardWithFilters;
using static DVLD_Project.People.frmManagerPeople;

namespace DVLD_Project.People
{
    public partial class ctrlPersonCardWithFilters : UserControl
    {
        public enum enFilterBy
        {
            PersonID,
            NationalNumber
        }
        public enum enMode { Default, Find }
        public event Action<object, int> OnPersonSelected;

        public int PersonId { get; private set; }
        public Person SelectedPerson { get => this.ctrlPersonCard.Person; private set { SelectedPerson = value; } }
        private enMode _mode = enMode.Default;


        public ctrlPersonCardWithFilters()
        {
            InitializeComponent();
            _mode = enMode.Default;
        }
        public ctrlPersonCardWithFilters(int personId)
        {
            InitializeComponent();
            _mode = enMode.Find;
            this.PersonId = personId;
            ctrlPersonCard.loadPersonDetailsToCard(this.PersonId);
        }
        private void ctrlPersonCardWithFilters_Load(object sender, EventArgs e)
        {
            resetToDefautValues();
            if (this._mode == enMode.Find)
                ctrlPersonCard.loadPersonDetailsToCard(PersonId);
        }

        private void resetToDefautValues()
        {
            cbFilterRows.SelectedIndex = (int)enFilterBy.PersonID;
            mtbFilterSeach.Clear();
            mtbFilterSeach.Focus();
        }
        private void handleNewPersonAdded(object sender, int personId)
        {
            MessageBox.Show($"Person with ID {personId} has been added/updated.");

            this.PersonId = personId;
            ctrlPersonCard.loadPersonDetailsToCard(personId);
            cbFilterRows.SelectedIndex = (int)enFilterBy.PersonID;
            mtbFilterSeach.Text = personId.ToString();
        }

        private void cbFilterRows_SelectedIndexChanged(object sender, EventArgs e)
        {
            mtbFilterSeach.Clear();
            if (cbFilterRows.SelectedItem.ToString() == enPeopleFilter.PersonID.ToString())
            {
                mtbFilterSeach.Mask = "00000000";
                mtbFilterSeach.Select(0, 0); // Cursor at first position
                mtbFilterSeach.Focus();
            }
            else
                mtbFilterSeach.Mask = "";
        }
        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Check if the pressed key is Enter (character code 13)
            if (e.KeyChar == (char)13)
                btnFind.PerformClick();

            //this will allow only digits if person id is selected
            if (cbFilterRows.SelectedItem.ToString() == enFilterBy.PersonID.ToString())
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void mtbFilterSeach_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(mtbFilterSeach.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider.SetError(mtbFilterSeach, "This field is required!");
            }
            else
                errorProvider.SetError(mtbFilterSeach, null);
        }
        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            AddUpdatePerson form = new AddUpdatePerson();
            form.OnPersonAddUpdate += handleNewPersonAdded;
            form.ShowDialog();
            form.OnPersonAddUpdate -= handleNewPersonAdded;
        }
        private void btnFind_Click(object sender, EventArgs e)
        {
            switch (cbFilterRows.SelectedIndex)
            {
                case (int)enFilterBy.PersonID:
                    if (int.TryParse(mtbFilterSeach.Text, out int personId))
                        ctrlPersonCard.loadPersonDetailsToCard(personId);
                    break;
                case (int)enFilterBy.NationalNumber:
                    ctrlPersonCard.loadPersonDetailsToCard(mtbFilterSeach.Text);
                    break;
                default:
                    break;
            }

            this.PersonId = SelectedPerson.PersonID;
            if (SelectedPerson != null)
            {
                MessageBox.Show($"Person found: {SelectedPerson.FirstName} {SelectedPerson.LastName}");
                PersonId = SelectedPerson.PersonID;
                //ctrlPersonCard.loadPersonDetailsToCard(person.PersonID);
                OnPersonSelected?.Invoke(this, SelectedPerson.PersonID);
            }
            else
                MessageBox.Show("Person not found.");
        }
    }
}
