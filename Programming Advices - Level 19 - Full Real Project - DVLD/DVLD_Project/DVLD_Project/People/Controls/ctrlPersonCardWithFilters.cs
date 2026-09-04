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
using static DVLD_Project.People.frmListPeople;

namespace DVLD_Project.People
{
    public partial class ctrlPersonCardWithFilters : UserControl
    {
        public enum enFilterBy
        {
            PersonID,
            NationalNumber
        }
        public event Action<object, int> OnPersonSelected;
        /// <summary>
        /// Event Exposure Pattern: Expose inner control's event to external subscribers
        /// This allows forms to listen to person detail updates without accessing internal controls
        /// </summary>
        public event Action<object, int> OnPersonCardDetailsUpdated
        {
            add { this.ctrlPersonCard.OnPersonCardDetailsUpdated += value; }
            remove { this.ctrlPersonCard.OnPersonCardDetailsUpdated -= value; }
        }

        public int PersonId { get; private set; }
        public Person SelectedPerson { get { return this.ctrlPersonCard.Person; } }

        /*
            =========================================
            ========= HIGH PRIORITY NOTE ============
            =========================================

                Exposing these properties makes it even easiter to test control behaviors on compile time/edit phase
            without the need to create separate forms or link things and wait until reach that section on system to test it

                You can edit ditectly and see things on the fly changes here :) 
        */
        public int RowFilter
        {
            get { return this.cbFilterRows.SelectedIndex; }
            set { this.cbFilterRows.SelectedIndex = value; }
        }
        public string SearchFilter
        {
            get { return this.mtbFilterSeach.Text; }
            set
            {
                this.mtbFilterSeach.Text = value.ToString();
                this.btnFind_Click(this, null);
            }
        }

        public ctrlPersonCardWithFilters()
        {
            InitializeComponent();
        }
        private void ctrlPersonCardWithFilters_Load(object sender, EventArgs e)
        {
            resetToDefautValues();
        }
        public void loadPersonDetailsToCard(int personId)
        {
            this.PersonId = personId;
            cbFilterRows.SelectedIndex = (int)enFilterBy.PersonID;
            mtbFilterSeach.Text = this.PersonId.ToString();
            ctrlPersonCard.LoadPersonDetailsToCard(personId);
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
            ctrlPersonCard.LoadPersonDetailsToCard(personId);
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
                MessageBox.Show("Please enter a search value.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }
        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            frmAddUpdatePerson form = new frmAddUpdatePerson();
            form.OnPersonAddUpdate += handleNewPersonAdded;
            form.ShowDialog();
            form.OnPersonAddUpdate -= handleNewPersonAdded;
        }
        private void btnFind_Click(object sender, EventArgs e)
        {
            // Validate search input
            if (string.IsNullOrWhiteSpace(mtbFilterSeach.Text))
            {
                MessageBox.Show("Please enter a search value.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mtbFilterSeach.Focus();
                return;
            }

            switch (cbFilterRows.SelectedIndex)
            {
                case (int)enFilterBy.PersonID:
                    if (int.TryParse(mtbFilterSeach.Text, out int personId))
                        ctrlPersonCard.LoadPersonDetailsToCard(personId);
                    break;
                case (int)enFilterBy.NationalNumber:
                    ctrlPersonCard.LoadPersonDetailsToCard(mtbFilterSeach.Text);
                    break;
                default:
                    break;
            }

            if (SelectedPerson != null)
                this.PersonId = SelectedPerson.PersonID;

            if (SelectedPerson != null)
            {
                MessageBox.Show($"Person found: {SelectedPerson.FirstName} {SelectedPerson.LastName}");
                PersonId = SelectedPerson.PersonID;
                OnPersonSelected?.Invoke(this, SelectedPerson.PersonID);
            }
        }

        public void FilterFocus()
        {
            mtbFilterSeach.Focus();
        }
        public void ActivateFilter()
        {
            gbFilter.Enabled = true;
        }
        public void DisactiviteFilter()
        {
            gbFilter.Enabled = false;
        }
    }
}
