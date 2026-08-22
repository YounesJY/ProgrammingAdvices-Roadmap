using DVLD_Business;
using DVLD_Project.Properties;
using System;
using System.Windows.Forms;

namespace DVLD_Project.Tests
{
    public partial class ctrlSheduledTestDetails : UserControl
    {
        public LocalDrivingLicenseApplication LocalDrivingApplication { get; set; }
        private TestType.enTestType _TestType = TestType.enTestType.VisionTest;
        public TestAppointment _TestAppointment { get; set; }
        public String TestID
        {
            get { return this.lblTestID.Text; }
            set { this.lblTestID.Text = value; }
        }


        public ctrlSheduledTestDetails()
        {
            InitializeComponent();
            ResetToDefaultValues();
        }


        private void ResetToDefaultValues()
        {
            lblLocalDrivingLicenseAppID.Text = "N/A";
            lblDrivingClass.Text = "[??]";
            lblFullName.Text = "[??]";
            lblTrial.Text = "[??]";
            dtpTestDate.Enabled = false;
            dtpTestDate.Value = DateTime.Now;
            lblFees.Text = "[$$]";
        }
        private bool IsValidApplication(int localDrivingApplicationID)
        {
            if (localDrivingApplicationID <= 0)
            {
                MessageBox.Show($"Invalid ApplicationID = {localDrivingApplicationID}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            this.LocalDrivingApplication = LocalDrivingLicenseApplication.FindByLocalDrivingApplicationID(localDrivingApplicationID);
            if (this.LocalDrivingApplication == null)
            {
                MessageBox.Show($"No application with ApplicationID = {localDrivingApplicationID}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }
        public void LoadTestDetails(int testAppointmentID)
        {
            if (testAppointmentID <= 0)
            {
                MessageBox.Show($"Invalid TestAppointmentID = {testAppointmentID}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this._TestAppointment = TestAppointment.Find(testAppointmentID);
            if (this._TestAppointment == null)
            {
                MessageBox.Show($"No test appointment with testAppointmentID = {_TestAppointment}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if (!IsValidApplication(this._TestAppointment.LocalDrivingLicenseApplicationID))
                return;

            if (this.LocalDrivingApplication.PassedAllTests())
            {
                MessageBox.Show("All tests have already been passed for this application.\n\n" +
                                "The license can now be issued.",
                                "All Tests Passed",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }

            this._TestType = (TestType.enTestType)this._TestAppointment.TestTypeID;
            FillTestDetails();
        }

        private void SetTestLabel()
        {
            switch (_TestType)
            {
                case TestType.enTestType.VisionTest:
                    this.gbTestType.Text = "Vision Test";
                    break;
                case TestType.enTestType.WrittenTest:
                    this.gbTestType.Text = "Written Test";
                    break;
                case TestType.enTestType.StreetTest:
                    this.gbTestType.Text = "Street Test";
                    break;
            }
        }
        private void SetTestIcon()
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
        private void SetTestData()
        {
            lblLocalDrivingLicenseAppID.Text = this.LocalDrivingApplication.LocalDrivingLicenseApplicationID.ToString();
            lblDrivingClass.Text = this.LocalDrivingApplication.LicenseClassInfo.ClassName;
            lblFullName.Text = this.LocalDrivingApplication.ApplicantFullName.ToString();
            lblTrial.Text = this.LocalDrivingApplication.TotalTrialsPerTest(this._TestType).ToString();
            dtpTestDate.Value = this._TestAppointment.AppointmentDate;
            lblFees.Text = this._TestAppointment.PaidFees.ToString();
        }
        private void FillTestDetails()
        {
            SetTestIcon();
            SetTestLabel();
            SetTestData();
        }
    }
}
