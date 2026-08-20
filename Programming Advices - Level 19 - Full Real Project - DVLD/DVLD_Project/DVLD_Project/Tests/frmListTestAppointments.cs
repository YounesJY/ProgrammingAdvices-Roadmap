using DVLD_Business;
using DVLD_Common;
using DVLD_Project.Properties;
using System;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using static DVLD_Project.Applications.LocalDrivingLicense.frmListLocalDrivingLicenseApplications;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD_Project.Tests
{
    public partial class frmListTestAppointments : Form
    {
        public event Action<object, int> OnApplicationCardDetailsUpdated
        {
            add { this.ctrlLocalDrivingApplicationDetails.OnApplicationCardDetailsUpdated += value; }
            remove { this.ctrlLocalDrivingApplicationDetails.OnApplicationCardDetailsUpdated -= value; }
        }
        public event Action<object, int> OnTestAppointmentTaken;
        public event Action<object, int> OnTestPassed;


        private int _LocalDrivingApplicationID = ValidationConstants.INVALID_ID;
        private TestType.enTestType _TestType = TestType.enTestType.VisionTest;


        public frmListTestAppointments(int localDrivingApplicationID, TestType.enTestType testType)
        {
            InitializeComponent();
            this._LocalDrivingApplicationID = localDrivingApplicationID;
            this._TestType = testType;
        }
        private void frmListTestAppointments_Load(object sender, EventArgs e)
        {
            this.OnApplicationCardDetailsUpdated += RefreshApplicationDetailsOnUpdate;
            LoadData();
        }
        private void frmListTestAppointments_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.OnApplicationCardDetailsUpdated -= RefreshApplicationDetailsOnUpdate;
        }


        private void RefreshApplicationDetailsOnUpdate(object sender, int applicationID)
        {
            MessageBox.Show("Applications data has been updated and data refreshed successfully.",
                "Information",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );

            this.ctrlLocalDrivingApplicationDetails.LoadApplicationDetailsByApplicationID(applicationID);
        }
        private void RefreshApplicationDetailsOnTestPassed(object sender, int testID)
        {
            this.OnTestPassed?.Invoke(this, testID);
        }
        private void RefreshApplicationDetailsOnAllTestPassed(object sender, int localDrivingApplicationID)
        {
            this.ctrlLocalDrivingApplicationDetails.LoadApplicationDetailsByLocalDrivingApplicationID(localDrivingApplicationID);
        }

        private void setFormLabels()
        {
            switch (this._TestType)
            {
                case TestType.enTestType.VisionTest:
                    this.Text = "Vision Test Appointments";
                    break;
                case TestType.enTestType.WrittenTest:
                    this.Text = "Written Test Appointments";
                    break;
                case TestType.enTestType.StreetTest:
                    this.Text = "Street Test Appointments";
                    break;
            }

            this.lblTitle.Text = this.Text;
        }
        private void setTestIcon()
        {
            switch (_TestType)
            {
                case TestType.enTestType.VisionTest:
                    this.pbTestTypeImage.Image = Resources.Vision_512;
                    break;
                case TestType.enTestType.WrittenTest:
                    this.pbTestTypeImage.Image = Resources.Written_Test_512;
                    break;
                case TestType.enTestType.StreetTest:
                    this.pbTestTypeImage.Image = Resources.driving_test_512;
                    break;
            }
        }
        private void LoadTestAppointmentsData()
        {
            dgvTestAppointments.DataSource = TestAppointment.GetApplicationTestAppointmentsPerTestType(this._LocalDrivingApplicationID, this._TestType);
            dgvTestAppointments.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTestAppointments.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            lblNumberOfRecords.Text = dgvTestAppointments.RowCount.ToString();
        }
        private void LoadData()
        {
            setFormLabels();
            setTestIcon();
            this.ctrlLocalDrivingApplicationDetails.LoadApplicationDetailsByLocalDrivingApplicationID(this._LocalDrivingApplicationID);
            LoadTestAppointmentsData();
        }

        private void RefreshTestAppoitments(object sender, int testAppointmentID)
        {
            dgvTestAppointments.DataSource = TestAppointment.GetApplicationTestAppointmentsPerTestType(this._LocalDrivingApplicationID, this._TestType);
            lblNumberOfRecords.Text = dgvTestAppointments.RowCount.ToString();
        }
        private void btnAddNewAppointment_Click(object sender, EventArgs e)
        {
            if (LocalDrivingLicenseApplication.IsThereAnActiveScheduledTest(this._LocalDrivingApplicationID, this._TestType))
            {
                MessageBox.Show("There is already an active scheduled test for this application.",
                                "Active Test Exists",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            if (LocalDrivingLicenseApplication.GetPassedTestCount(this._LocalDrivingApplicationID) == 3)
            {
                MessageBox.Show("All three tests (Vision, Written, and Street) have already been passed.\n\n" +
                                "The license is ready to be issued for this application.",
                                "All Tests Passed",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                return;
            }

            if (LocalDrivingLicenseApplication.DoesPassTestType(this._LocalDrivingApplicationID, this._TestType))
            {
                MessageBox.Show("This test has already been passed for this application.",
                                "Test Already Passed",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                return;
            }


            frmScheduleTest frmScheduleTest = new frmScheduleTest(_LocalDrivingApplicationID, _TestType);
            try
            {
                if (frmScheduleTest != null)
                    frmScheduleTest.OnTestAppointmentAddUpdate += RefreshTestAppoitments;
                frmScheduleTest.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while sheduling the test: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                if (frmScheduleTest != null)
                    frmScheduleTest.OnTestAppointmentAddUpdate -= RefreshTestAppoitments;
            }
        }
        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvTestAppointments.RowCount == 0)
            {
                MessageBox.Show("No test appointment selected to edit.",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }

            int testAppointmentID = Convert.ToInt32(dgvTestAppointments.CurrentRow.Cells[0].Value);
            frmScheduleTest frmScheduleTest = new frmScheduleTest(testAppointmentID);

            try
            {
                if (frmScheduleTest != null)
                    frmScheduleTest.OnTestAppointmentAddUpdate += RefreshTestAppoitments;
                frmScheduleTest.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while editing the test appointement: {ex.Message}",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
            finally
            {
                if (frmScheduleTest != null)
                    frmScheduleTest.OnTestAppointmentAddUpdate -= RefreshTestAppoitments;
            }
        }
        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvTestAppointments.RowCount == 0)
            {
                MessageBox.Show("No test appointment selected to edit.",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }

            int testAppointmentID = Convert.ToInt32(dgvTestAppointments.CurrentRow.Cells[0].Value);
            frmTakeTest frmTakeTest = new frmTakeTest(testAppointmentID);

            try
            {
                if (frmTakeTest != null)
                {
                    frmTakeTest.OnTestAppointmentTaken += RefreshTestAppoitments;
                    frmTakeTest.OnTestPassed += RefreshApplicationDetailsOnTestPassed;
                    frmTakeTest.OnAllTestPassed += RefreshApplicationDetailsOnAllTestPassed;
                }
                frmTakeTest.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while taking the test appointement: {ex.Message}",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
            finally
            {
                if (frmTakeTest != null)
                {
                    frmTakeTest.OnTestAppointmentTaken -= RefreshTestAppoitments;
                    frmTakeTest.OnTestPassed -= RefreshApplicationDetailsOnTestPassed;
                    frmTakeTest.OnAllTestPassed -= RefreshApplicationDetailsOnAllTestPassed;
                }
            }
        }
    }
}
