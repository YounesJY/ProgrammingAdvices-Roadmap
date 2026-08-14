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


        /*
            * ====================================================================================
            * === EVENT PROPAGATION CHAINING - ANTI-PATTERN WARNING ===
            * ====================================================================================
            * 
            * This code demonstrates an educational example of Event Propagation Chaining
            * (also known as Event Bubbling or Deep Event Chaining).
            * 
            * 
            * ====================================================================================
            * === CURRENT IMPLEMENTATION - MULTIPLE CHAINS ===
            * ====================================================================================
            * 
            * This anti-pattern exists in multiple places throughout the project:
            * 
            * 
            * -------------------- CHAIN 1: PERSON → APPLICATION → LDA --------------------
            * 
            *   ctrlPersonCard.OnPersonCardDetailsUpdated
            *       ↓ (exposed via Event Exposure Pattern)
            *   frmPersonDetails.OnPersonCardDetailsUpdated
            *       ↓ (subscribed in ctrlApplicationDetails)
            *   ctrlApplicationDetails (listens and forwards to parent)
            *       ↓ (exposed via Event Exposure Pattern)
            *   ctrlLocalDrivingApplicationDetails.OnApplicationCardDetailsUpdated
            *       ↓ (exposed via Event Exposure Pattern)
            *   frmLocalDrivingLicenseApplicationDetails.OnApplicationCardDetailsUpdated
            *       ↓ (subscribed in frmListLocalDrivingLicenseApplications)
            *   frmListLocalDrivingLicenseApplications (receives the event)
            * 
            * 
            * -------------------- CHAIN 2: PERSON → USER --------------------------------
            * 
            *   ctrlPersonCard.OnPersonCardDetailsUpdated
            *       ↓ (subscribed in ctrlUserCard)
            *   ctrlUserCard (listens and forwards to parent)
            *       ↓ (exposed via Event Exposure Pattern)
            *   frmUserInfo.OnPersonCardDetailsUpdated
            *       ↓ (subscribed in frmListUsers)
            *   frmListUsers (receives the event)
            * 
            * 
            * -------------------- CHAIN 3: APPLICATION → LDA (EDIT FORM) ---------------
            * 
            *   frmAddEditLocalDrivingLicenseApplication.OnNewApplicationCreated
            *       ↓ (subscribed in ctrlApplicationDetails)
            *   ctrlApplicationDetails (listens and forwards to parent)
            *       ↓ (exposed via Event Exposure Pattern)
            *   ctrlLocalDrivingApplicationDetails.OnApplicationCardDetailsUpdated
            *       ↓ (exposed via Event Exposure Pattern)
            *   frmLocalDrivingLicenseApplicationDetails.OnApplicationCardDetailsUpdated
            *       ↓ (subscribed in frmListLocalDrivingLicenseApplications)
            *   frmListLocalDrivingLicenseApplications (receives the event)
            * 
            * 
            * ====================================================================================
            * === WHY THIS IS A PROBLEM ===
            * ====================================================================================
            * 
            * 1. BRITTLE CODE:
            *    - Changing one control breaks the entire chain
            *    - Adding/removing a layer requires updating all event exposures
            *    - Hard to refactor or restructure UI components
            * 
            * 2. MEMORY LEAKS:
            *    - Each layer holds references to the previous layer
            *    - Prevents proper garbage collection
            *    - Can lead to memory exhaustion in long-running applications
            * 
            * 3. DEBUGGING NIGHTMARE:
            *    - Tracing where an event originated is extremely difficult
            *    - Stack traces are misleading and hard to follow
            *    - "Who raised this event?" becomes a mystery
            *    - With multiple chains, tracking the source is even harder
            * 
            * 4. DUPLICATE SUBSCRIPTIONS:
            *    - Easy to accidentally subscribe multiple times
            *    - Same handler executes multiple times
            *    - Leads to unexpected behavior and performance issues
            * 
            * 5. TIGHT COUPLING:
            *    - Every layer must know about the event chain
            *    - Violates the Law of Demeter (Principle of Least Knowledge)
            *    - Makes unit testing nearly impossible
            * 
            * 6. SINGLE POINT OF FAILURE:
            *    - If one layer fails to forward the event, the entire chain breaks
            *    - Silent failures are hard to detect
            * 
            * 7. EVENT EXPOSURE PATTERN ABUSE:
            *    - The Event Exposure Pattern is being used for deep chaining
            *    - This pattern is meant for simple, one-level exposure (Form → Control)
            *    - Not for multi-level event propagation
            * 
            * 
            * ====================================================================================
            * === REAL-WORLD CONSEQUENCES ===
            * ====================================================================================
            * 
            * In a real production application, this pattern can cause:
            * 
            * - Application crashes due to memory leaks
            * - UI freezes from duplicate event handlers
            * - Data inconsistencies from events firing multiple times
            * - Hours wasted debugging event propagation issues
            * - Difficulty adding new features or refactoring
            * - Increased maintenance costs
            * - Developer frustration and burnout
            * 
            * 
            * ====================================================================================
            * === BETTER SOLUTIONS (FOR FUTURE REFERENCE) ===
            * ====================================================================================
            * 
            * 1. EVENT AGGREGATOR PATTERN (Recommended for WinForms):
            *    - Central event hub that decouples publishers and subscribers
            *    - Events flow through a single point
            *    - Easy to debug, test, and maintain
            *    - No deep chaining required
            *    
            *    Example:
            *    ```
            *    public static class EventAggregator
            *    {
            *        public static event Action<int> PersonUpdated;
            *        public static event Action<int> ApplicationUpdated;
            *        public static event Action<int> UserUpdated;
            *        
            *        public static void OnPersonUpdated(int personId) 
            *            => PersonUpdated?.Invoke(personId);
            *        // ... etc
            *    }
            *    ```
            * 
            * 2. MEDIATOR PATTERN:
            *    - Central communication bus
            *    - Encapsulates interaction logic
            *    - Reduces direct dependencies
            * 
            * 3. SERVICE LOCATOR WITH EVENTS:
            *    - Global event service
            *    - Register once, use everywhere
            *    - Simple and effective for small-to-medium projects
            * 
            * 4. OBSERVER PATTERN (Proper Implementation):
            *    - Use standard event handlers without deep chaining
            *    - Each component subscribes directly to the source
            *    - Avoid forwarding events through multiple layers
            * 
            * 5. MESSAGE BUS:
            *    - Publish/subscribe model
            *    - Messages are strongly typed
            *    - Supports complex scenarios
            * 
            * 
            * ====================================================================================
            * === SPECIFIC FIX FOR EACH CHAIN ===
            * ====================================================================================
            * 
            * To refactor this for production:
            * 
            * 1. Remove all event forwarding in:
            *    - ctrlLocalDrivingApplicationDetails (remove OnApplicationCardDetailsUpdated exposure)
            *    - frmLocalDrivingLicenseApplicationDetails (remove OnApplicationCardDetailsUpdated exposure)
            *    - frmUserInfo (remove OnPersonCardDetailsUpdated exposure)
            *    - Any other forms/controls exposing forwarded events
            * 
            * 2. Each control should publish directly to EventAggregator:
            *    - ctrlPersonCard: EventAggregator.OnPersonUpdated(PersonID)
            *    - ctrlUserCard: EventAggregator.OnUserUpdated(UserID)
            *    - frmAddEditLocalDrivingLicenseApplication: EventAggregator.OnApplicationUpdated(AppID)
            * 
            * 3. Each form should subscribe directly to EventAggregator:
            *    - frmListPeople: EventAggregator.PersonUpdated += RefreshHandler
            *    - frmListUsers: EventAggregator.UserUpdated += RefreshHandler
            *    - frmListLocalDrivingLicenseApplications: EventAggregator.ApplicationUpdated += RefreshHandler
            * 
            * 
            * ====================================================================================
            * === LESSON LEARNED ===
            * ====================================================================================
            * 
            * This educational example demonstrates why event propagation chaining is
            * considered an anti-pattern in enterprise applications.
            * 
            * While this approach works for small, simple applications, it doesn't scale
            * and becomes a maintenance nightmare in larger projects.
            * 
            * Key Takeaway: 
            *    - The Event Exposure Pattern is for ONE level of exposure (Form → Control)
            *    - It should NOT be chained through multiple layers
            *    - Use EventAggregator for cross-component communication
            * 
            * 
            * ====================================================================================
            * === RECOMMENDED REFACTORING PATH ===
            * ====================================================================================
            * 
            * If this code were to be refactored for production:
            * 
            * 1. Remove all event forwarding chains (ALL of them!)
            * 2. Implement an Event Aggregator or Message Bus
            * 3. Each control/form publishes events directly to the aggregator
            * 4. Each control/form subscribes directly to events they care about
            * 5. Use weak event handlers or proper unsubscribe patterns
            * 6. Consider using a library like Prism's EventAggregator (for WPF)
            * 7. Or implement a simple custom EventAggregator (for WinForms)
            * 8. Document the event flow clearly for the team
            * 
            * 
            * ====================================================================================
            * === RESOURCES ===
            * ====================================================================================
            * 
            * - "Event Aggregator Pattern" - Martin Fowler
            *   https://martinfowler.com/eaaDev/EventAggregator.html
            * 
            * - "Mediator Pattern" - Gang of Four
            * 
            * - "Law of Demeter" - Principle of Least Knowledge
            * 
            * - "Memory Leaks in .NET Events" - Microsoft Docs
            * 
            * - "C# Event Best Practices" - Official Documentation
            * 
            * - "Prism EventAggregator" - Microsoft Patterns & Practices
            *   https://prismlibrary.com/docs/event-aggregator.html
            * 
            * 
            * ====================================================================================
            * === NOTE ===
            * ====================================================================================
            * 
            * This code is intentionally left with this pattern for EDUCATIONAL PURPOSES
            * to demonstrate the anti-pattern and its consequences.
            * 
            * In a real production project, this would be refactored to use one of
            * the recommended solutions above.
            * 
            * The three chains identified in this project serve as excellent examples
            * of how this anti-pattern can spread throughout an application.
            * 
            * ====================================================================================
        */
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