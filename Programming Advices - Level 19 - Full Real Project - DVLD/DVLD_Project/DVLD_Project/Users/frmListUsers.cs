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
        public enum enUserActiveStatusFilter
        {
            No,
            Yes,
            All
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
            usersDataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.ColumnHeader);
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
            if (usersDataGridView.RowCount == 0)
            {
                MessageBox.Show("No user selected to show details.",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }

            int userID = Convert.ToInt32(usersDataGridView.CurrentRow.Cells["UserID"].Value);
            frmUserInfo userInfoForm = new frmUserInfo(userID);

            // [This teaches how to handle events for inner controls via Event Exposure pattern]
            try
            {
                if (userInfoForm != null)
                    userInfoForm.OnPersonCardDetailsUpdated += RefreshHandler;
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
                    userInfoForm.OnPersonCardDetailsUpdated -= RefreshHandler;
            }

        }
        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddEditUser frmAddEditUser = new frmAddEditUser();

            // this teaches how to handle events for inner controls via Event Exposure pattern
            // and also how to properly subscribe and unsubscribe to events to avoid memory leaks
            // [Always Remember] a form has it's own events + it can expose events from its inner controls to the outside world
            try
            {
                if (frmAddEditUser != null)
                {
                    frmAddEditUser.OnUserAddedOrUpdated += RefreshHandler;
                    frmAddEditUser.OnPersonDetailsUpdated += RefreshHandler;
                }
                frmAddEditUser.ShowDialog();
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
                if (frmAddEditUser != null)
                {
                    frmAddEditUser.OnUserAddedOrUpdated -= RefreshHandler;
                    frmAddEditUser.OnPersonDetailsUpdated -= RefreshHandler;
                }
            }
        }
        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (usersDataGridView.RowCount == 0)
            {
                MessageBox.Show("No user selected to edit.",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }

            int userID = Convert.ToInt32(usersDataGridView.CurrentRow.Cells["UserID"].Value);
            frmAddEditUser frmAddEditUser = new frmAddEditUser(userID);

            // this teaches how to handle events for inner controls via Event Exposure pattern
            // and also how to properly subscribe and unsubscribe to events to avoid memory leaks
            // [Always Remember] a form has it's own events + it can expose events from its inner controls to the outside world

            try
            {
                if (frmAddEditUser != null)
                {
                    frmAddEditUser.OnUserAddedOrUpdated += RefreshHandler;
                    frmAddEditUser.OnPersonDetailsUpdated += RefreshHandler;
                }
                frmAddEditUser.ShowDialog();
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
                if (frmAddEditUser != null)
                {
                    frmAddEditUser.OnUserAddedOrUpdated -= RefreshHandler;
                    frmAddEditUser.OnPersonDetailsUpdated -= RefreshHandler;
                }
            }
        }
        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (usersDataGridView.RowCount == 0)
            {
                MessageBox.Show("No user selected to change password.",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }

            int userID = Convert.ToInt32(usersDataGridView.CurrentRow.Cells["UserID"].Value);
            new frmChangeUserPassword(userID).ShowDialog();

            /*
                ===============================
                ===== VERY IMPORTANT NOTE =====
                ===============================
            
                you can also use the following code to handle events and refresh data after changing the password:
                but it's not necessary to refresh the data grid view after changing the password, since the password is not displayed in the data grid view.
                and the user can change the password without changing any other user information.
            
                this is just an example of how to handle events for inner controls via Event Exposure pattern and also how to properly subscribe and unsubscribe to events to avoid memory leaks
            */
            /*
                int UserID = Convert.ToInt32(usersDataGridView.CurrentRow.Cells["UserID"].Value);
                frmChangeUserPassword frmChangeUserPassword = new frmChangeUserPassword(UserID);

                // this teaches how to handle events for inner controls via Event Exposure pattern
                // and also how to properly subscribe and unsubscribe to events to avoid memory leaks
                // [Always Remember] a form has it's own events + it can expose events from its inner controls to the outside world
                try
                {
                    if (frmChangeUserPassword != null)
                    {
                        frmChangeUserPassword.OnPersonCardDetailsUpdated += RefreshHandler;
                        frmChangeUserPassword.OnUserPasswordChanged += RefreshHandler;
                    }
                    frmChangeUserPassword.ShowDialog();

                }
                catch (Exception)
                {
                    MessageBox.Show("An error occurred while changing the user's password.",
                                    "Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }
                finally
                {
                    if (frmChangeUserPassword != null)
                    {
                        frmChangeUserPassword.OnPersonCardDetailsUpdated -= RefreshHandler;
                        frmChangeUserPassword.OnUserPasswordChanged -= RefreshHandler;
                    }
                }
            */
        }
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (usersDataGridView.RowCount == 0)
            {
                MessageBox.Show("No user selected to delete.",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }

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
        private void FilterUsersByActiveStatus()
        {
            string searchValue = cbUserActiveStatus.SelectedItem.ToString().ToLower().Trim();
            DataTable dataTable = User.GetAllUsers();
            DataView dataView = dataTable.DefaultView;

            if (searchValue == enUserActiveStatusFilter.Yes.ToString().ToLower() || searchValue == enUserActiveStatusFilter.No.ToString().ToLower())
                dataView.RowFilter = $"IsActive = {(searchValue == enUserActiveStatusFilter.Yes.ToString().ToLower() ? (int)enUserActiveStatusFilter.Yes : (int)enUserActiveStatusFilter.No)}";

            usersDataGridView.DataSource = dataView;
            lblNumberOfRecords.Text = dataView.Count.ToString();
        }
        private void pbAddUser_Click(object sender, EventArgs e)
        {
            //  AddNewUser();
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
            else if (cbFilterRows.SelectedItem.ToString().ToLower() == enUsersFilter.IsActive.ToString().ToLower())
            {
                mtbFilterSearch.Visible = false;
                cbUserActiveStatus.Visible = true;
                cbUserActiveStatus.SelectedItem = enUserActiveStatusFilter.All.ToString(); // Default to "All"
                FilterUsersByActiveStatus();
            }
            else
            {
                mtbFilterSearch.Visible = true;
                cbUserActiveStatus.Visible = false;
                mtbFilterSearch.Clear();

                if (cbFilterRows.SelectedItem.ToString().ToLower() == enUsersFilter.UserID.ToString().ToLower() || cbFilterRows.SelectedItem.ToString().ToLower() == enUsersFilter.PersonID.ToString().ToLower())
                {
                    mtbFilterSearch.Mask = "00000000";
                    mtbFilterSearch.Select(0, 0);
                }
                else
                    mtbFilterSearch.Mask = string.Empty;

                mtbFilterSearch.Focus();
            }
        }
        private void cbUserActiveStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterUsersByActiveStatus();
        }
        private void mtbFilterSearch_TextChanged(object sender, EventArgs e)
        {
            FilterUsers();
        }
        private void usersDataGridView_DoubleClick(object sender, EventArgs e)
        {
            showDetailsToolStripMenuItem_Click(sender, e);
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}