using DVLD_Business;
using DVLD_Common;
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

namespace DVLD_Project.Tests.Controls
{
    public partial class ctrlSheduleTest : UserControl
    {
        public event Action<object, int> OnTestAppointmentAddUpdate;

        private LocalDrivingLicenseApplication _LocalDrivingApplication = null;
        private TestType.enTestType _TestType = TestType.enTestType.VisionTest;
        private TestAppointment _TestAppointment = null;

        public ctrlSheduleTest()
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

            if (this._LocalDrivingApplication.PassedAllTests())
            {
                MessageBox.Show("All tests have already been passed for this application.\n\n" +
                                "The license can now be issued.",
                                "All Tests Passed",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                return false;
            }

            return true;
        }
        private void ResetToDefaultValues()
        {
            lblUserMessage.Visible = false;

            lblLocalDrivingLicenseAppID.Text = "N/A";
            lblDrivingClass.Text = "[??]";
            lblFullName.Text = "[??]";
            lblTrial.Text = "[??]";
            dtpTestDate.Enabled = false;
            dtpTestDate.Value = DateTime.Now;
            lblFees.Text = "[$$]";

            gbRetakeTestInfo.Enabled = false;
            lblRetakeAppFees.Text = "0";
            lblTotalFees.Text = "0";
            lblRetakeTestAppID.Text = "N/A";

            btnSave.Enabled = false;
        }
        public void LoadTestDetails(int localDrivingApplicationID, TestType.enTestType testType)
        {
            if (!IsValidApplication(localDrivingApplicationID))
                return;

            FillNewTestDetails();
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

            this._TestType = (TestType.enTestType)this._TestAppointment.TestTypeID;
            FillUpdatedTestDetails();
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
            Test lastPassedTest = this._LocalDrivingApplication.GetLastTestPerTestType(this._TestType);
            bool isFailedInLastTest = (lastPassedTest == null) ? false : lastPassedTest.TestResult;
            bool isRetakingTest = (isFailedInLastTest == true);
            float testFees = TestType.Find(this._TestType).TestTypeFees;
            float retakeApplicationFees = (isRetakingTest) ? ApplicationType.Find((int)ApplicationInfo.enApplicationType.RetakeTest).ApplicationFees : 0;


            lblLocalDrivingLicenseAppID.Text = this._LocalDrivingApplication.LocalDrivingLicenseApplicationID.ToString();
            lblDrivingClass.Text = this._LocalDrivingApplication.LicenseClassInfo.ClassName;
            lblFullName.Text = this._LocalDrivingApplication.ApplicantFullName.ToString();
            lblTrial.Text = this._LocalDrivingApplication.TotalTrialsPerTest(this._TestType).ToString();
            dtpTestDate.Value = DateTime.Now;
            lblFees.Text = testFees.ToString();

            gbRetakeTestInfo.Enabled = false;
            lblRetakeAppFees.Text = retakeApplicationFees.ToString();
            lblTotalFees.Text = (retakeApplicationFees + testFees).ToString();
        }
        private void FillNewTestDetails()
        {
            SetTestIcon();
            SetTestLabel();
            SetTestData();

            if (!this._LocalDrivingApplication.DoesPassPreviousTest(this._TestType))
            {
                lblUserMessage.Visible = true;
                lblUserMessage.Text = $"Cannot Sechule, {(this._TestType - 1).ToString()} Test Should be Passed First.";
                return;
            }

            dtpTestDate.MinDate = DateTime.Now;
            dtpTestDate.Enabled = true;
            btnSave.Enabled = true;
        }
        private void FillUpdatedTestDetails()
        {
            SetTestIcon();
            SetTestLabel();
            SetTestData();

            dtpTestDate.Value = this._TestAppointment.AppointmentDate;

            if (this._TestAppointment.IsLocked)
            {
                lblUserMessage.Visible = true;
                lblUserMessage.Text = "This test appointment is already locked and cannot be modified.";
                return;
            }

            dtpTestDate.MinDate = DateTime.Now;
            dtpTestDate.Enabled = true;
            btnSave.Enabled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this._TestAppointment == null)
            {
                TestAppointment testAppointment = new TestAppointment()
                {
                    TestTypeID = (int)this._TestType,
                    LocalDrivingLicenseApplicationID = this._LocalDrivingApplication.LocalDrivingLicenseApplicationID,
                    AppointmentDate = this.dtpTestDate.Value,
                    PaidFees = float.Parse(this.lblTotalFees.Text),
                    CreatedByUserID = Global.currentLoggedInUser.UserID
                };

                if (testAppointment.Save())
                {
                    MessageBox.Show("Test appointment has been scheduled successfully.",
                                    "Success",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);

                    this._TestAppointment = testAppointment;
                    OnTestAppointmentAddUpdate?.Invoke(this, this._TestAppointment.TestAppointmentID);
                }
                else
                {
                    MessageBox.Show("Failed to schedule the test appointment. Please try again.",
                                    "Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }
            }
            else
            {
                if (this._TestAppointment.AppointmentDate == dtpTestDate.Value)
                {
                    MessageBox.Show("No changes detected. The appointment date is the same.",
                                    "No Changes",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                    return;
                }

                this._TestAppointment.AppointmentDate = dtpTestDate.Value;
                if (this._TestAppointment.Save())
                {
                    MessageBox.Show("Test appointment has been updated successfully.",
                                    "Success",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);

                    OnTestAppointmentAddUpdate?.Invoke(this, this._TestAppointment.TestAppointmentID);
                }
                else
                {
                    MessageBox.Show("Failed to update the test appointment. Please try again.",
                                    "Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }
            }
        }
    }
}
