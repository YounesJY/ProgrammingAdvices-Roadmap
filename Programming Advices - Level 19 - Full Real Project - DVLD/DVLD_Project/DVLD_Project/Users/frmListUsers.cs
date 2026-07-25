using DVLD_Business;
using System;
using System.Data;
using System.Windows.Forms;

namespace DVLD_Project.Users
{
    public partial class frmListUsers : Form
    {
        public enum enUsersFilter
        {
            None,
            UserID,
            PersonID,
            FullName,
            UserName,
            IsActive
        }
        public enum enUsersFilterByActive
        {
            All,
            Active,
            Inactive
        }

        public frmListUsers()
        {
            InitializeComponent();
        }
        private void ManagerUsers_Load(object sender, EventArgs e)
        {
            ResetForm();
        }
        private void ResetForm()
        {
            usersDataGridView.DataSource = User.GetAllUsers();
            usersDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            usersDataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);

            lblNumberOfRecords.Text = usersDataGridView.RowCount.ToString();
            cbFilterRows.SelectedIndex = 0;
            mtbFilterSearch.Visible = false;
            cbUserActiveStatus.Visible = false;
        }

        private void RefreshFormData()
        {
            usersDataGridView.DataSource = User.GetAllUsers();
        }

        private void FilterUsers()
        {
            int filterColumn = cbFilterRows.SelectedIndex;
            string searchValue = mtbFilterSearch.Text.Trim();

            // If search is empty, show all
            if (string.IsNullOrEmpty(searchValue))
            {
                usersDataGridView.DataSource = User.GetAllUsers();
                lblNumberOfRecords.Text = usersDataGridView.RowCount.ToString();
                return;
            }

            /*
                Using DataView filtering instead of direct SQL queries allows us to:
                1. Filter already-loaded data without additional DB round-trips
                2. Maintain a consistent dataset in memory for the UI
                3. Provide real-time filtering as the user types without performance overhead
                4. Avoid SQL injection risks since we are not constructing raw SQL queries
            */
            DataTable dataTable = User.GetAllUsers();
            DataView dataView = dataTable.DefaultView;

            switch (filterColumn)
            {
                // '=' for [numeric values] and 'LIKE' for [strings]
                case (int)enUsersFilter.UserID:
                    if (int.TryParse(searchValue, out int userId))
                        dataView.RowFilter = $"UserID = {userId}";
                    else
                        dataView.RowFilter = "UserID = -1";
                    break;

                case (int)enUsersFilter.PersonID:
                    if (int.TryParse(searchValue, out int personId))
                        dataView.RowFilter = $"PersonID = {personId}";
                    else
                        dataView.RowFilter = "PersonID = -1";
                    break;

                case (int)enUsersFilter.IsActive:
                    if (cbUserActiveStatus.SelectedIndex == (int)enUsersFilterByActive.Active)
                        dataView.RowFilter = "IsActive = 1";
                    else if (cbUserActiveStatus.SelectedIndex == (int)enUsersFilterByActive.Inactive)
                        dataView.RowFilter = "IsActive = 0";
                    break;
                default: // FullName, UserName
                    dataView.RowFilter = $"{cbFilterRows.SelectedItem.ToString()} LIKE '%{searchValue}%'";
                    break;
            }

            usersDataGridView.DataSource = dataView;
            lblNumberOfRecords.Text = dataView.Count.ToString();
        }

        private void cbFilterRows_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterRows.SelectedIndex == (int)enUsersFilter.None)
            {
                usersDataGridView.DataSource = User.GetAllUsers();
                mtbFilterSearch.Visible = false;
                cbUserActiveStatus.Visible = false;
                lblNumberOfRecords.Text = usersDataGridView.RowCount.ToString();
            }
            else
            {
                mtbFilterSearch.Visible = !cbFilterRows.SelectedItem.ToString().ToLower().Equals(enUsersFilter.IsActive.ToString().ToLower());
                cbUserActiveStatus.Visible = cbFilterRows.SelectedItem.ToString().ToLower().Equals(enUsersFilter.IsActive.ToString().ToLower());
                mtbFilterSearch.Clear();

                if (cbFilterRows.SelectedIndex == (int)enUsersFilter.UserID || cbFilterRows.SelectedIndex == (int)enUsersFilter.PersonID)
                {
                    mtbFilterSearch.Mask = "00000000";
                    mtbFilterSearch.Select(0, 0);
                    mtbFilterSearch.Focus();
                }
                else
                    mtbFilterSearch.Mask = "";
            }
        }
        private void mtbFilterSearch_TextChanged(object sender, EventArgs e)
        {
            FilterUsers();
        }
        private void pbAddUser_Click(object sender, EventArgs e)
        {
            // TODO: Implement add new user
        }
        private void usersDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // TODO: Implement show user details
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
