using DVLD_Business;
using System;
using System.Data;
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

            // Check if a row is selected
            if (peopleDataGridView.SelectedRows.Count > 0)
            {
                // Get the selected row
                DataGridViewRow selectedRow = peopleDataGridView.SelectedRows[0];

                // Get PersonID from the row (assuming PersonID is in column index 0 or use column name)
                int PersonID = Convert.ToInt32(selectedRow.Cells["PersonID"].Value);

                // Open the details form
                new frmPersonDetails(PersonID).ShowDialog();
            }

        }

        private void peopleDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            showDetailsToolStripMenuItem_Click(sender, e);
        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AddNewPerson().ShowDialog();
            peopleDataGridView.DataSource = Person.GetPeople();
            peopleDataGridView.Refresh();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            addNewPersonToolStripMenuItem_Click(sender, e);
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
                mtbFilterSeach.Visible = true;
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

        private void mtbFilterSeach_TextChanged(object sender, EventArgs e)
        {
            FilterPeople();
        }
    }
}
