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
        public enum enUserActiveStatus
        {
            All,
            Yes,
            No
        }


        public frmListUsers()
        {
            InitializeComponent();
        }
        private void ListUsers_Load(object sender, EventArgs e)
        {
            ResetForm();
        }

        private void ResetForm()
        {
            usersDataGridView.DataSource = User.GetAllUsers();
            usersDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            lblNumberOfRecords.Text = usersDataGridView.RowCount.ToString();
            cbFilterRows.SelectedIndex = (int)enUsersFilter.None;
        }
        private void RefreshFormData()
        {
            usersDataGridView.DataSource = User.GetAllUsers();
            usersDataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);
            lblNumberOfRecords.Text = usersDataGridView.RowCount.ToString();
        }
        private void RefreshHandler(object sender, int userID)
        {
            MessageBox.Show("User information updated and data refreshed.",
                "Success",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            RefreshFormData();
        }


        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int UserID = Convert.ToInt32(usersDataGridView.CurrentRow.Cells["UserID"].Value);
            frmUserInfo userInfoForm = new frmUserInfo(UserID);

            try
            {
                if (userInfoForm != null)
                    userInfoForm.OnUserInfoUpdated += RefreshHandler;
                userInfoForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while showing user details: {ex.Message}",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
            finally
            {
                if (userInfoForm != null)
                    userInfoForm.OnUserInfoUpdated -= RefreshHandler;
            }

        }
        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddNewUpdateUser frmAddNewUpdateUser = new frmAddNewUpdateUser();

            try
            {
                if (frmAddNewUpdateUser != null)
                    frmAddNewUpdateUser.OnUserAddedOrUpdated += RefreshHandler;
                frmAddNewUpdateUser.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while adding a new user: {ex.Message}",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
            finally
            {
                if (frmAddNewUpdateUser != null)
                    frmAddNewUpdateUser.OnUserAddedOrUpdated -= RefreshHandler;
            }
        }
        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int UserID = Convert.ToInt32(usersDataGridView.CurrentRow.Cells["UserID"].Value);
            frmAddNewUpdateUser frmAddNewUpdateUser = new frmAddNewUpdateUser(UserID);

            try
            {
                if (frmAddNewUpdateUser != null)
                    frmAddNewUpdateUser.OnUserAddedOrUpdated += RefreshHandler;
                frmAddNewUpdateUser.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while editing user: {ex.Message}",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
            finally
            {
                if (frmAddNewUpdateUser != null)
                    frmAddNewUpdateUser.OnUserAddedOrUpdated -= RefreshHandler;
            }
        }
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int userID = Convert.ToInt32(usersDataGridView.CurrentRow.Cells["UserID"].Value);
            string username = usersDataGridView.CurrentRow.Cells["Username"].Value?.ToString() ?? "this user";

            if (MessageBox.Show("Are you sure you want to delete this user?\n\nThis action cannot be undone.",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                if (User.Delete(userID))
                {
                    MessageBox.Show($"User '{username}' has been deleted successfully.",
                                    "User Deleted",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                    RefreshFormData();
                }
                else
                {
                    MessageBox.Show($"Failed to delete user '{username}' due to data relationship constraints.",
                                    "Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }
            }

        }
        private void sendEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This feature is not implemented yet. Coming soon!",
                            "Feature Not Available",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
        }
        private void makeACalllStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This feature is not implemented yet. Coming soon!",
                            "Feature Not Available",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
        }

        private void FilterUsers()
        {
            string filterColumn = cbFilterRows.SelectedItem.ToString().ToLower();
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
                case "userid":
                    if (int.TryParse(searchValue, out int userId))
                        dataView.RowFilter = $"UserID = {userId}";
                    else
                        dataView.RowFilter = "UserID = -1";
                    break;

                case "personid":
                    if (int.TryParse(searchValue, out int personId))
                        dataView.RowFilter = $"PersonID = {personId}";
                    else
                        dataView.RowFilter = "PersonID = -1";
                    break;

                default:
                    dataView.RowFilter = $"{filterColumn} LIKE '%{searchValue}%'";
                    break;
            }

            usersDataGridView.DataSource = dataView;
            lblNumberOfRecords.Text = dataView.Count.ToString();
        }
        private void FilterUserStatus()
        {
            string searchValue = cbUserActiveStatus.SelectedItem.ToString().ToLower().Trim();
            DataTable dataTable = User.GetAllUsers();
            DataView dataView = dataTable.DefaultView;

            if (searchValue == enUserActiveStatus.Yes.ToString().ToLower() || searchValue == enUserActiveStatus.No.ToString().ToLower())
                dataView.RowFilter = $"IsActive = {(searchValue == enUserActiveStatus.Yes.ToString().ToLower() ? (int)enUserActiveStatus.Yes : (int)enUserActiveStatus.No)}";

            usersDataGridView.DataSource = dataView;
            lblNumberOfRecords.Text = dataView.Count.ToString();
        }
        private void pbAddUser_Click(object sender, EventArgs e)
        {
            //  AddNewUser();
        }

        private void usersDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int UserID = Convert.ToInt32(usersDataGridView.CurrentRow.Cells["UserID"].Value);
                //ShowUserDetails(UserID);
            }
        }
        private void cbFilterRows_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterRows.SelectedItem.ToString().ToLower() == enUsersFilter.None.ToString().ToLower())
            {
                usersDataGridView.DataSource = User.GetAllUsers();
                mtbFilterSearch.Visible = false;
                cbUserActiveStatus.Visible = false;
                lblNumberOfRecords.Text = usersDataGridView.RowCount.ToString();
            }
            else
            {
                mtbFilterSearch.Visible = cbFilterRows.SelectedItem.ToString().ToLower() != enUsersFilter.IsActive.ToString().ToLower();
                cbUserActiveStatus.Visible = cbFilterRows.SelectedItem.ToString().ToLower() == enUsersFilter.IsActive.ToString().ToLower();
                mtbFilterSearch.Clear();

                if (cbFilterRows.SelectedItem.ToString().ToLower() == enUsersFilter.UserID.ToString().ToLower() || cbFilterRows.SelectedItem.ToString().ToLower() == enUsersFilter.PersonID.ToString().ToLower())
                {
                    mtbFilterSearch.Mask = "00000000";
                    mtbFilterSearch.Select(0, 0);
                    mtbFilterSearch.Focus();
                }
                else
                    mtbFilterSearch.Mask = "";
            }
        }
        private void cbUserActiveStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterUserStatus();
        }


        private void mtbFilterSearch_TextChanged(object sender, EventArgs e)
        {
            FilterUsers();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void usersDataGridView_DoubleClick(object sender, EventArgs e)
        {
            showDetailsToolStripMenuItem_Click(sender, e);
        }
    }
}
