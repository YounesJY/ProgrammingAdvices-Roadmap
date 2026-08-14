using DVLD_Business;
using DVLD_Project.Tests;
using DVLD_Project.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DVLD_Project.Applications.LocalDrivingLicense
{
    public partial class frmListLocalDrivingLicenseApplications : Form
    {
        public enum enLocalDrivingLicenseApplicationsFilter
        {
            None,
            ApplicationID,
            NationalNumber,
            FullName,
            Status
        }
        public enum enLocalDrivingLicenseApplicationsStatusFilter
        {
            New,
            Cancelled,
            Completed
        }


        public frmListLocalDrivingLicenseApplications()
        {
            InitializeComponent();
        }
        private void frmListLocalDrivingLicenseApplications_Load(object sender, EventArgs e)
        {
            ResetForm();
        }


        private void ResetForm()
        {
            dgvLocalDrivingLicenseApplications.DataSource = LocalDrivingLicenseApplication.GetAllLocalDrivingLicenseApplications();
            dgvLocalDrivingLicenseApplications.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLocalDrivingLicenseApplications.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            lblNumberOfRecords.Text = dgvLocalDrivingLicenseApplications.RowCount.ToString();
            cbFilterRows.SelectedIndex = (int)enLocalDrivingLicenseApplicationsFilter.None;
        }
        private void RefreshFormData()
        {
            dgvLocalDrivingLicenseApplications.DataSource = LocalDrivingLicenseApplication.GetAllLocalDrivingLicenseApplications();
            dgvLocalDrivingLicenseApplications.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.ColumnHeader);
            lblNumberOfRecords.Text = dgvLocalDrivingLicenseApplications.RowCount.ToString();
        }
        private void RefreshHandler(object sender, int userID)
        {
            MessageBox.Show("Local driving license applications has been updated and data refreshed successfully.",
                "Information",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );

            RefreshFormData();
        }

        private void FilterLocalDrivingApplications()
        {
            string filterColumn = cbFilterRows.SelectedItem.ToString().ToLower();
            string searchValue = mtbFilterSearch.Text.Trim();

            // If search is empty, show all
            if (string.IsNullOrEmpty(searchValue))
            {
                dgvLocalDrivingLicenseApplications.DataSource = LocalDrivingLicenseApplication.GetAllLocalDrivingLicenseApplications();
                lblNumberOfRecords.Text = dgvLocalDrivingLicenseApplications.RowCount.ToString();
                return;
            }

            /*
                Using DataView filtering instead of direct SQL queries allows us to:
                1. Filter already-loaded data without additional DB round-trips
                2. Maintain a consistent dataset in memory for the UI
                3. Provide real-time filtering as the user types without performance overhead
                4. Avoid SQL injection risks since we are not constructing raw SQL queries
            */
            DataTable dataTable = LocalDrivingLicenseApplication.GetAllLocalDrivingLicenseApplications();
            DataView dataView = dataTable.DefaultView;

            switch (filterColumn)
            {
                // '=' for [numeric values] and 'LIKE' for [strings]
                case "applicationid":
                    if (int.TryParse(searchValue, out int applicationID))
                        dataView.RowFilter = $"ApplicationID = {applicationID}";
                    else
                        dataView.RowFilter = "ApplicationID = -1";
                    break;
                default: // NationalNumber & FullName
                    dataView.RowFilter = $"{filterColumn} LIKE '%{searchValue}%'";
                    break;
            }

            dgvLocalDrivingLicenseApplications.DataSource = dataView;
            lblNumberOfRecords.Text = dataView.Count.ToString();
        }
        private void FilterLocalDrivingApplicationsByStatus()
        {
            string searchValue = cbApplicationStatus.SelectedItem.ToString().ToLower().Trim();
            DataTable dataTable = LocalDrivingLicenseApplication.GetAllLocalDrivingLicenseApplications();
            DataView dataView = dataTable.DefaultView;

            dataView.RowFilter = $"Status = '{searchValue}'";

            dgvLocalDrivingLicenseApplications.DataSource = dataView;
            lblNumberOfRecords.Text = dataView.Count.ToString();
        }

        private void cbFilterRows_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cbFilterRows.SelectedItem.ToString().ToLower() == enLocalDrivingLicenseApplicationsFilter.None.ToString().ToLower())
            {
                dgvLocalDrivingLicenseApplications.DataSource = LocalDrivingLicenseApplication.GetAllLocalDrivingLicenseApplications();
                mtbFilterSearch.Visible = false;
                cbApplicationStatus.Visible = false;
                lblNumberOfRecords.Text = dgvLocalDrivingLicenseApplications.RowCount.ToString();
            }
            else if (cbFilterRows.SelectedItem.ToString().ToLower() == enLocalDrivingLicenseApplicationsFilter.Status.ToString().ToLower())
            {
                mtbFilterSearch.Visible = false;
                cbApplicationStatus.Visible = true;
                cbApplicationStatus.SelectedItem = enLocalDrivingLicenseApplicationsStatusFilter.New.ToString();
                FilterLocalDrivingApplicationsByStatus();
            }
            else
            {
                cbApplicationStatus.Visible = false;
                mtbFilterSearch.Visible = true;
                mtbFilterSearch.Clear();

                if (cbFilterRows.SelectedItem.ToString().ToLower() == enLocalDrivingLicenseApplicationsFilter.ApplicationID.ToString().ToLower())
                {
                    mtbFilterSearch.Mask = "00000000";
                    mtbFilterSearch.Select(0, 0);
                }
                else
                    mtbFilterSearch.Mask = string.Empty;

                mtbFilterSearch.Focus();
            }

        }
        private void cbApplicationStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterLocalDrivingApplicationsByStatus();
        }
        private void mtbFilterSearch_TextChanged(object sender, EventArgs e)
        {
            FilterLocalDrivingApplications();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        /*
            * ====================================================================================
            * === EVENT PROPAGATION CHAINING - ANTI-PATTERN WARNING ===
            * ====================================================================================
            * 
            * This code demonstrates an educational example of Event Propagation Chaining
            * (also known as Event Bubbling or Deep Event Chaining).
            * 
            * ====================================================================================
            * === THE PROBLEM ===
            * ====================================================================================
            * 
            * We have a deep chain of events being exposed and forwarded through multiple layers:
            * 
            *   ctrlPersonCard.OnPersonCardDetailsUpdated
            *       ↓ (exposed)
            *   frmPersonDetails.OnPersonCardDetailsUpdated
            *       ↓ (subscribed)
            *   ctrlApplicationDetails (listens and forwards)
            *       ↓ (exposed)
            *   ctrlLocalDrivingApplicationDetails.OnPersonDetailsUpdated
            *       ↓ (exposed)
            *   frmLocalDrivingLicenseApplicationDetails.OnPersonDetailsUpdated
            *       ↓ (subscribed)
            *   frmListLocalDrivingLicenseApplications (subscribes)
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
            * 
            * 
            * ====================================================================================
            * === BETTER SOLUTIONS (FOR FUTURE REFERENCE) ===
            * ====================================================================================
            * 
            * 1. EVENT AGGREGATOR PATTERN (Recommended):
            *    - Central event hub that decouples publishers and subscribers
            *    - Events flow through a single point
            *    - Easy to debug, test, and maintain
            *    - No deep chaining required
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
            * === LESSON LEARNED ===
            * ====================================================================================
            * 
            * This educational example demonstrates why event propagation chaining is
            * considered an anti-pattern in enterprise applications.
            * 
            * While this approach works for small, simple applications, it doesn't scale
            * and becomes a maintenance nightmare in larger projects.
            * 
            * 
            * ====================================================================================
            * === RECOMMENDED REFACTORING PATH ===
            * ====================================================================================
            * 
            * If this code were to be refactored for production:
            * 
            * 1. Remove all event forwarding chains
            * 2. Implement an Event Aggregator or Message Bus
            * 3. Each control/form publishes events directly
            * 4. Each control/form subscribes directly to events they care about
            * 5. Use weak event handlers or proper unsubscribe patterns
            * 6. Consider using a library like Prism's EventAggregator (for WPF)
            * 7. Or implement a simple custom EventAggregator (for WinForms)
            * 
            * 
            * ====================================================================================
            * === RESOURCES ===
            * ====================================================================================
            * 
            * - "Event Aggregator Pattern" - Martin Fowler
            * - "Mediator Pattern" - Gang of Four
            * - "Law of Demeter" - Principle of Least Knowledge
            * - "Memory Leaks in .NET Events" - Microsoft Docs
            * - "C# Event Best Practices" - Official Documentation
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
            * ====================================================================================
        */
        private void DisableAllContextMenuItems()
        {
            tsmiCancelApplicationDetails.Enabled = false;
            tsmiDeleteApplicationDetails.Enabled = false;
            tsmiEditApplication.Enabled = false;
            tsmiShowApplicationDetails.Enabled = false;

            tsmiScheduleTests.Enabled = false;
            tsmiIssueDrivingLicenseFirstTime.Enabled = false;
            tsmiShowLicenseDetails.Enabled = false;
            tsmiShowPersonLicenseHistory.Enabled = false;
        }
        private void cmsLocalDrivingLicenseApplications_Opening(object sender, CancelEventArgs e)
        {
            if (dgvLocalDrivingLicenseApplications.RowCount == 0 || dgvLocalDrivingLicenseApplications.CurrentRow == null)
            {
                DisableAllContextMenuItems();
                return;
            }

            int localDrivingApplicationID = Convert.ToInt32(dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value);
            LocalDrivingLicenseApplication localDrivingLicenseApplication = LocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(localDrivingApplicationID);


            if (localDrivingLicenseApplication == null)
            {
                DisableAllContextMenuItems();
                tsmiShowApplicationDetails.Enabled = true; // Still show details if found
                return;
            }

            ApplicationInfo.enApplicationStatus status = localDrivingLicenseApplication.ApplicationStatusID;
            bool isNew = status == ApplicationInfo.enApplicationStatus.New;
            bool isCancelled = status == ApplicationInfo.enApplicationStatus.Cancelled;
            bool isCompleted = status == ApplicationInfo.enApplicationStatus.Completed;

            tsmiShowApplicationDetails.Enabled = true;
            tsmiEditApplication.Enabled = isNew;
            tsmiCancelApplicationDetails.Enabled = isNew;
            tsmiDeleteApplicationDetails.Enabled = isNew || isCancelled;
            tsmiScheduleTests.Enabled = isNew;
            tsmiIssueDrivingLicenseFirstTime.Enabled = isCompleted;
            tsmiShowLicenseDetails.Enabled = isCompleted;
            tsmiShowPersonLicenseHistory.Enabled = isCompleted;
        }

        private void tsmiShowApplicationDetails_Click(object sender, EventArgs e)
        {
            if (dgvLocalDrivingLicenseApplications.RowCount == 0)
            {
                MessageBox.Show("No application selected to show details.",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }


            int localDrivingApplicationID = Convert.ToInt32(dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value);
            frmLocalDrivingLicenseApplicationDetails frmLocalDrivingLicenseApplicationDetails = new frmLocalDrivingLicenseApplicationDetails(localDrivingApplicationID);

            // [This teaches how to handle events for inner controls via Event Exposure pattern]
            try
            {
                if (frmLocalDrivingLicenseApplicationDetails != null)
                    frmLocalDrivingLicenseApplicationDetails.OnApplicationCardDetailsUpdated += RefreshHandler;
                frmLocalDrivingLicenseApplicationDetails.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while showing application details: {ex.Message}",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
            finally
            {
                if (frmLocalDrivingLicenseApplicationDetails != null)
                    frmLocalDrivingLicenseApplicationDetails.OnApplicationCardDetailsUpdated -= RefreshHandler;
            }
        }
        private void pbAddNewLocalDrivingLicenseApplication_Click(object sender, EventArgs e)
        {
            frmAddEditLocalDrivingLicenseApplication frmAddEditLocalDrivingLicenseApplication = new frmAddEditLocalDrivingLicenseApplication();

            try
            {
                if (frmAddEditLocalDrivingLicenseApplication != null)
                    frmAddEditLocalDrivingLicenseApplication.OnNewLocalDrivingLicenseApplicationCreated += RefreshHandler;
                frmAddEditLocalDrivingLicenseApplication.ShowDialog();
            }
            catch (Exception)
            {
                MessageBox.Show("An error occurred while trying to add a new local driving license application.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                throw;
            }
            finally
            {
                if (frmAddEditLocalDrivingLicenseApplication != null)
                    frmAddEditLocalDrivingLicenseApplication.OnNewLocalDrivingLicenseApplicationCreated -= RefreshHandler;
            }
        }
        private void tsmiEditApplication_Click(object sender, EventArgs e)
        {
            if (dgvLocalDrivingLicenseApplications.RowCount == 0)
            {
                MessageBox.Show("No application selected to show details.",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }


            int localDrivingApplicationID = Convert.ToInt32(dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value);
            frmAddEditLocalDrivingLicenseApplication frmAddEditLocalDrivingLicenseApplication = new frmAddEditLocalDrivingLicenseApplication(localDrivingApplicationID);

            // [This teaches how to handle events for inner controls via Event Exposure pattern]
            try
            {
                if (frmAddEditLocalDrivingLicenseApplication != null)
                {
                    frmAddEditLocalDrivingLicenseApplication.OnApplicationUpdate += RefreshHandler;
                    frmAddEditLocalDrivingLicenseApplication.OnPersonDetailsUpdated += RefreshHandler;
                }
                frmAddEditLocalDrivingLicenseApplication.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while showing application details: {ex.Message}",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
            finally
            {
                if (frmAddEditLocalDrivingLicenseApplication != null)
                {
                    frmAddEditLocalDrivingLicenseApplication.OnApplicationUpdate -= RefreshHandler;
                    frmAddEditLocalDrivingLicenseApplication.OnPersonDetailsUpdated -= RefreshHandler;
                }
            }
        }
        private void tmsiCancelApplicationDetails_Click(object sender, EventArgs e)
        {
            if (dgvLocalDrivingLicenseApplications.RowCount == 0)
            {
                MessageBox.Show("No application selected to delete.",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }


            int localDrivingApplicationID = Convert.ToInt32(dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value);
            LocalDrivingLicenseApplication localDrivingLicenseApplication = LocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(localDrivingApplicationID);

            if (MessageBox.Show("Are you sure you want to cancel this application?\n\nThis action cannot be undone.",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                if (localDrivingLicenseApplication.Cancel())
                {
                    MessageBox.Show($"The application has been canceled successfully.",
                                    "User Deleted",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                    RefreshFormData();
                }
                else
                {
                    MessageBox.Show($"Failed to cancel the application due to data relationship constraints.",
                                    "Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }
            }
        }
        private void tmsiDeleteApplicationDetails_Click(object sender, EventArgs e)
        {
            if (dgvLocalDrivingLicenseApplications.RowCount == 0)
            {
                MessageBox.Show("No application selected to delete.",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }

            int localDrivingApplicationID = Convert.ToInt32(dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value);
            LocalDrivingLicenseApplication localDrivingLicenseApplication = LocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(localDrivingApplicationID);

            if (MessageBox.Show("Are you sure you want to delete this application?\n\nThis action cannot be undone.",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                if (localDrivingLicenseApplication.Delete())
                {
                    MessageBox.Show($"The application has been deleted successfully.",
                                    "User Deleted",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                    RefreshFormData();
                }
                else
                {
                    MessageBox.Show($"Failed to delete the application due to data relationship constraints.",
                                    "Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }
            }
        }

        private void dgvLocalDrivingLicenseApplications_DoubleClick(object sender, EventArgs e)
        {
            tsmiShowApplicationDetails_Click(sender, e);
        }
        private void scheduleVisionTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvLocalDrivingLicenseApplications.RowCount == 0)
            {
                MessageBox.Show("No application selected to show details.",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }

            int localDrivingApplicationID = Convert.ToInt32(dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value);
            frmListTestAppointments frmListTestAppointments = new frmListTestAppointments(localDrivingApplicationID, TestType.enTestType.VisionTest);

            try
            {
                if (frmListTestAppointments != null)
                    frmListTestAppointments.OnApplicationCardDetailsUpdated += RefreshHandler;
                frmListTestAppointments.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while showing application details: {ex.Message}",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
            finally
            {
                if (frmListTestAppointments != null)
                    frmListTestAppointments.OnApplicationCardDetailsUpdated -= RefreshHandler;
            }
        }

        void DisableAllTestMenuItems()
        {
            tsmiScheduleVisionTestToolStripMenuItem.Enabled = false;
            tsmiScheduleWrittenTestToolStripMenuItem.Enabled = false;
            tsmiScheduleStreetTestToolStripMenuItem.Enabled = false;
        }

        private void tsmiScheduleTests_DropDownOpening(object sender, EventArgs e)
        {
            if (dgvLocalDrivingLicenseApplications.RowCount == 0 || dgvLocalDrivingLicenseApplications.CurrentRow == null)
            {
                DisableAllTestMenuItems();
                return;
            }

            int localDrivingApplicationID = Convert.ToInt32(dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value);
            LocalDrivingLicenseApplication localDrivingLicenseApplication = LocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(localDrivingApplicationID);

            if (localDrivingLicenseApplication == null)
            {
                DisableAllTestMenuItems();
                return;
            }

            bool visionPassed = localDrivingLicenseApplication.DoesPassTestType(TestType.enTestType.VisionTest);
            bool writtenPassed = localDrivingLicenseApplication.DoesPassTestType(TestType.enTestType.WrittenTest);
            bool streetPassed = localDrivingLicenseApplication.DoesPassTestType(TestType.enTestType.StreetTest);

            tsmiScheduleVisionTestToolStripMenuItem.Enabled = !visionPassed;
            tsmiScheduleWrittenTestToolStripMenuItem.Enabled = visionPassed && !writtenPassed;
            tsmiScheduleStreetTestToolStripMenuItem.Enabled = writtenPassed && !streetPassed;
        }
    }
}
