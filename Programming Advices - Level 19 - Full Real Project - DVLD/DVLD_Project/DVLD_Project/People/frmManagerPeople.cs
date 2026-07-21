using DVLD_Business;
using System;
using System.Data;
using System.Security.Policy;
using System.Windows.Forms;

namespace DVLD_Project.People
{
    public partial class frmManagerPeople : Form
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


        public frmManagerPeople()
        {
            InitializeComponent();
        }
        private void ManagerPeople_Load(object sender, EventArgs e)
        {
            peopleDataGridView.DataSource = Person.GetPeople();
            peopleDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            lblNumberOfRecordsValue.Text = peopleDataGridView.RowCount.ToString();
            cbFilterRows.SelectedIndex = 0;
        }


        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (peopleDataGridView.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = peopleDataGridView.SelectedRows[0];
                int PersonID = Convert.ToInt32(selectedRow.Cells["PersonID"].Value);
                frmPersonDetails form = new frmPersonDetails(PersonID);

                // Traditional null check (works in all versions)
                if (form.PersonCard != null)
                    form.PersonCard.OnPersonCardDetailsUpdated += RefreshHandler;
                form.ShowDialog();
                form.PersonCard.OnPersonCardDetailsUpdated -= RefreshHandler; // <- Remember to unsubscribe when the form closes
            }
        }
        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AddUpdatePerson().ShowDialog();
            refreshFormData();
        }
        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (peopleDataGridView.SelectedRows.Count > 0)
            {
                // Get the selected row
                DataGridViewRow selectedRow = peopleDataGridView.SelectedRows[0];

                // Get PersonID from the row (assuming PersonID is in column index 0 or use column name)
                int PersonID = Convert.ToInt32(selectedRow.Cells["PersonID"].Value);

                // Open the details form
                new AddUpdatePerson(PersonID).ShowDialog();
            }
            refreshFormData();
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
            string filterColumn = cbFilterRows.SelectedItem.ToString();
            string searchValue = mtbFilterSeach.Text.Trim();

            // If search is empty, show all
            if (string.IsNullOrEmpty(searchValue))
            {
                peopleDataGridView.DataSource = Person.GetPeople();
                lblNumberOfRecordsValue.Text = peopleDataGridView.RowCount.ToString();
                return;
            }

            DataTable dt = Person.GetPeople();
            DataView dv = dt.DefaultView;

            switch (filterColumn)
            {
                case "PersonID":
                    if (int.TryParse(searchValue, out int personId))
                        dv.RowFilter = $"PersonID = {personId}";
                    else
                        dv.RowFilter = "PersonID = -1";
                    break;

                case "Gender":
                    if (searchValue.ToLower() == "male")
                        dv.RowFilter = "Gender = 0";
                    else if (searchValue.ToLower() == "female")
                        dv.RowFilter = "Gender = 1";
                    else
                        dv.RowFilter = $"Gender LIKE '%{searchValue}%'";
                    break;
                default:
                    dv.RowFilter = $"{filterColumn} LIKE '%{searchValue}%'";
                    break;
            }

            peopleDataGridView.DataSource = dv;
            lblNumberOfRecordsValue.Text = dv.Count.ToString();
        }

        private void pbAddPerson_Click(object sender, EventArgs e)
        {
            addNewPersonToolStripMenuItem_Click(sender, e);
        }
        private void peopleDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            showDetailsToolStripMenuItem_Click(sender, e);
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void cbFilterRows_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterRows.SelectedItem.ToString() == enPeopleFilter.None.ToString())
            {
                peopleDataGridView.DataSource = Person.GetPeople();
                mtbFilterSeach.Visible = false;
                lblNumberOfRecordsValue.Text = peopleDataGridView.RowCount.ToString();
            }
            else
            {
                mtbFilterSeach.Visible = true;
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
        }
        private void mtbFilterSeach_TextChanged(object sender, EventArgs e)
        {
            FilterPeople();
        }
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Check if a row is selected
            if (peopleDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a person to delete.", "No Selection",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow selectedRow = peopleDataGridView.SelectedRows[0];
            int PersonID = Convert.ToInt32(selectedRow.Cells["PersonID"].Value);
            string PersonName = selectedRow.Cells["FirstName"].Value?.ToString() ?? "this person";

            // Confirm deletion
            DialogResult result = MessageBox.Show($"Are you sure you want to delete {PersonName}?\n\nThis action cannot be undone.", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                // Delete the person
                if (Person.Delete(PersonID))
                {
                    MessageBox.Show($"Person deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Refresh the DataGridView
                    peopleDataGridView.DataSource = Person.GetPeople();
                    lblNumberOfRecordsValue.Text = peopleDataGridView.RowCount.ToString();
                }
                else
                    MessageBox.Show("Failed to delete person.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void sendEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This feature is not implemented yet. Coming soon!",
                            "Feature Not Available",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
        }
        private void makeAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This feature is not implemented yet. Coming soon!",
                            "Feature Not Available",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
        }
    }
}
