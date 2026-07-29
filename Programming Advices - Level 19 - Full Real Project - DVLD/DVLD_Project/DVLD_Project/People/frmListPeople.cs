using DVLD_Business;
using System;
using System.Data;
using System.IO;
using System.Security.Policy;
using System.Windows.Forms;

namespace DVLD_Project.People
{
    public partial class frmListPeople : Form
    {
        public enum enPeopleFilter
        {
            None,
            PersonID,
            NationalNumber,
            FirstName,
            SecondName,
            ThirdName,
            LastName,
            Gender,
            Address,
            Phone,
            Email,
            Nationality
        }


        public frmListPeople()
        {
            InitializeComponent();
        }
        private void ListPeople_Load(object sender, EventArgs e)
        {
            resetForm();
        }
        private void resetForm()
        {
            peopleDataGridView.DataSource = Person.GetPeople();
            peopleDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            lblNumberOfRecords.Text = peopleDataGridView.RowCount.ToString();
            cbFilterRows.SelectedIndex = 0;
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = Convert.ToInt32(peopleDataGridView.CurrentRow.Cells["PersonID"].Value);
            frmPersonDetails form = new frmPersonDetails(PersonID);

            try
            {
                /*
                    ====================================================================
                    EVENT SUBSCRIPTION PATTERN with Try/Finally for Cleanup
                    ====================================================================
                    WHAT: Subscribe to the OnPersonCardDetailsUpdated event from the form
                
                    WHY:  - Notification: When a person's details are updated in the 
                            detail form, we want to refresh our list
                        - Try/Finally: Ensures we ALWAYS unsubscribe, even if an 
                            exception occurs (prevents memory leaks)
                
                    HOW:  1. Create a form and subscribe to its event (in try block)
                        2. Show the form (user can update data)
                        3. Form fires the event when data is saved
                        4. Our RefreshHandler method is called
                        5. Finally block guarantees unsubscription (cleanup)
                
                    MEMORY LEAK PREVENTION: If we don't unsubscribe (-=), the event 
                    handler keeps a reference to this form, preventing garbage collection
                    ====================================================================
                */
                if (form.PersonCard != null)
                    form.PersonCard.OnPersonCardDetailsUpdated += RefreshHandler;

                form.ShowDialog();
            }
            finally
            {
                // Always unsubscribe to prevent memory leaks
                if (form.PersonCard != null)
                    form.PersonCard.OnPersonCardDetailsUpdated -= RefreshHandler;
            }
        }
        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdatePerson form = new frmAddUpdatePerson();

            try
            {
                if (form != null)
                    form.OnPersonAddUpdate += RefreshHandler;
                form.ShowDialog();
            }
            finally
            {
                if (form != null)
                    form.OnPersonAddUpdate -= RefreshHandler;
            }
        }
        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Get PersonID from the row (assuming PersonID is in column index 0 or use column name)
            int PersonID = Convert.ToInt32(peopleDataGridView.CurrentRow.Cells["PersonID"].Value);

            frmAddUpdatePerson form = new frmAddUpdatePerson(PersonID);
            try
            {
                if (form != null)
                    form.OnPersonAddUpdate += RefreshHandler;
                form.ShowDialog();
            }
            finally
            {
                if (form != null)
                    form.OnPersonAddUpdate -= RefreshHandler;
            }   
        }
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = Convert.ToInt32(peopleDataGridView.CurrentRow.Cells["PersonID"].Value);
            string PersonName = peopleDataGridView.CurrentRow.Cells["FirstName"].Value?.ToString() ?? "this person";
            string PersonImage = Person.Find(PersonID).ProfilePhotoPath;

            DialogResult result = MessageBox.Show($"Are you sure you want to delete {PersonName} ?\n\nThis action cannot be undone.", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                if (Person.Delete(PersonID))
                {
                    if (PersonImage != null)
                    {
                        File.Delete(PersonImage);
                    }
                    MessageBox.Show($"Person deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    resetForm();
                }
                else
                    MessageBox.Show("Person was not deleted because it has data linked to it.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void sendEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This feature is not implemented yet. Coming soon!",
                            "Feature Not Available",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
        }
        private void makeACallStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This feature is not implemented yet. Coming soon!",
                            "Feature Not Available",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
        }

        private void refreshFormData()
        {
            peopleDataGridView.DataSource = Person.GetPeople();
        }
        private void RefreshHandler(object sender, int personID)
        {
            MessageBox.Show("Person information updated and data refreshed.",
                "Success",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            refreshFormData();
        }
        private void FilterPeople()
        {
            string filterColumn = cbFilterRows.SelectedItem.ToString().ToLower();
            string searchValue = mtbFilterSearch.Text.Trim();

            // If search is empty, show all
            if (string.IsNullOrEmpty(searchValue))
            {
                peopleDataGridView.DataSource = Person.GetPeople();
                lblNumberOfRecords.Text = peopleDataGridView.RowCount.ToString();
                return;
            }

            /*
                Using DataView filtering instead of direct SQL queries allows us to:
                1. Filter already-loaded data without additional DB round-trips
                2. Maintain a consistent dataset in memory for the UI
                3. Provide real-time filtering as the user types without performance overhead
                4. Avoid SQL injection risks since we are not constructing raw SQL queries
            */
            DataTable datatable = Person.GetPeople();
            DataView dataview = datatable.DefaultView;

            switch (filterColumn)
            {
                // '=' for [numeric values] and 'LIKE' for [strings]
                case "personid":
                    if (int.TryParse(searchValue, out int personId))
                        dataview.RowFilter = $"PersonID = {personId}";
                    else
                        dataview.RowFilter = "PersonID = -1";
                    break;

                case "gender":
                    if (searchValue.ToLower() == "male")
                        dataview.RowFilter = "Gender = 0";
                    else if (searchValue.ToLower() == "female")
                        dataview.RowFilter = "Gender = 1";
                    else
                        dataview.RowFilter = $"Gender LIKE '%{searchValue}%'";
                    break;
                default:
                    dataview.RowFilter = $"{filterColumn} LIKE '%{searchValue}%'";
                    break;
            }

            peopleDataGridView.DataSource = dataview;
            lblNumberOfRecords.Text = dataview.Count.ToString();
        }

        private void pbAddPerson_Click(object sender, EventArgs e)
        {
            addNewPersonToolStripMenuItem_Click(sender, e);
        }
        private void peopleDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            showDetailsToolStripMenuItem_Click(sender, e);
        }
        private void cbFilterRows_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterRows.SelectedItem.ToString().ToLower() == enPeopleFilter.None.ToString().ToLower())
            {
                peopleDataGridView.DataSource = Person.GetPeople();
                mtbFilterSearch.Visible = false;
                lblNumberOfRecords.Text = peopleDataGridView.RowCount.ToString();
            }
            else
            {
                mtbFilterSearch.Visible = true;
                mtbFilterSearch.Clear();
                if (cbFilterRows.SelectedItem.ToString().ToLower() == enPeopleFilter.PersonID.ToString().ToLower())
                {
                    mtbFilterSearch.Mask = "00000000";
                    mtbFilterSearch.Select(0, 0); // Cursor at first position
                    mtbFilterSearch.Focus();
                }
                else
                    mtbFilterSearch.Mask = "";
            }
        }
        private void mtbFilterSeach_TextChanged(object sender, EventArgs e)
        {
            FilterPeople();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
