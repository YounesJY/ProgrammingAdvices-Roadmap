using DVLD_Business;
using DVLD_Project.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project.Tests
{
    public partial class ctrlSheduledTestDetails : UserControl
    {
        private LocalDrivingLicenseApplication _LocalDrivingApplication = null;
        private TestType.enTestType _TestType = TestType.enTestType.VisionTest;
        public TestAppointment TestAppointment { get; set; }
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

        private bool IsValidApplication(int localDrivingApplicationID)
        {
            if (localDrivingApplicationID <= 0)
            {
                MessageBox.Show($"Invalid ApplicationID = {localDrivingApplicationID}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            this._LocalDrivingApplication = LocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(localDrivingApplicationID);
            if (this._LocalDrivingApplication == null)
            {
                MessageBox.Show($"No application with ApplicationID = {localDrivingApplicationID}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
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
        public void LoadTestDetails(int testAppointmentID)
        {
            if (testAppointmentID <= 0)
            {
                MessageBox.Show($"Invalid TestAppointmentID = {testAppointmentID}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.TestAppointment = TestAppointment.Find(testAppointmentID);
            if (this.TestAppointment == null)
            {
                MessageBox.Show($"No test appointment with testAppointmentID = {TestAppointment}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if (!IsValidApplication(this.TestAppointment.LocalDrivingLicenseApplicationID))
                return;

            if (this._LocalDrivingApplication.PassedAllTests())
            {
                MessageBox.Show("All tests have already been passed for this application.\n\n" +
                                "The license can now be issued.",
                                "All Tests Passed",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }

            this._TestType = (TestType.enTestType)this.TestAppointment.TestTypeID;
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
            lblLocalDrivingLicenseAppID.Text = this._LocalDrivingApplication.LocalDrivingLicenseApplicationID.ToString();
            lblDrivingClass.Text = this._LocalDrivingApplication.LicenseClassInfo.ClassName;
            lblFullName.Text = this._LocalDrivingApplication.ApplicantFullName.ToString();
            lblTrial.Text = this._LocalDrivingApplication.TotalTrialsPerTest(this._TestType).ToString();
            dtpTestDate.Value = this.TestAppointment.AppointmentDate;
            lblFees.Text = this.TestAppointment.PaidFees.ToString();
        }
        private void FillTestDetails()
        {
            SetTestIcon();
            SetTestLabel();
            SetTestData();
        }
    }
}
