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

        public enum enMode { AddNew, Update };
        public enum enCreationMode { FirstTimeSchedule, RetakeTestSchedule };

        private LocalDrivingLicenseApplication _LocalDrivingApplication = null;
        private ApplicationInfo _RetakeTestApplication = null;
        private TestType.enTestType _TestType = TestType.enTestType.VisionTest;
        private TestAppointment _TestAppointment = null;
        private bool IsRetakingTest = false;
        private enCreationMode _CreationMode = enCreationMode.FirstTimeSchedule;
        private enMode _Mode = enMode.AddNew;
        public TestType.enTestType TestTypeID
        {
            get { return this._TestType; }
            set
            {
                this._TestType = value;
                SetTestIcon();
            }
        }


        public ctrlSheduleTest()
        {
            InitializeComponent();
            ResetToDefaultValues();
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
        private bool IsValidApplication(int localDrivingApplicationID)
        {
            if (localDrivingApplicationID <= 0)
            {
                MessageBox.Show($"Invalid ApplicationID = {localDrivingApplicationID}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            this._LocalDrivingApplication = LocalDrivingLicenseApplication.FindByLocalDrivingApplicationID(localDrivingApplicationID);
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
            }

            return true;
        }
        private bool IsFailedInLastTest()
        {
            Test LastTakenTest = this._LocalDrivingApplication.GetLastTestPerTestType(this._TestType);

            return ((LastTakenTest == null) ? false : (LastTakenTest.TestResult == false));
            // return Test.FindLastTestPerPersonAndLicenseClass(this._LocalDrivingApplication.ApplicantPersonID, this._LocalDrivingApplication.LicenseClassID, this._TestType).TestResult == false;
        }
        public bool LoadTestDetails(int localDrivingApplicationID, TestType.enTestType testType)
        {
            if (!IsValidApplication(localDrivingApplicationID))
                return false;

            if (this._LocalDrivingApplication.PassedAllTests())
            {
                MessageBox.Show("All tests have already been passed for this application.\n\n" +
                                "The license can now be issued.",
                                "All Tests Passed",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                return false;
            }

            if (this._LocalDrivingApplication.IsThereAnActiveScheduledTest(this._TestType))
            {
                MessageBox.Show("There is already an active scheduled test for this application.",
                                "Active Test Exists",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return false;
            }

            if (this._LocalDrivingApplication.DoesPassTestType(this._TestType))
            {
                MessageBox.Show("This test has already been passed for this application.",
                                "Test Already Passed",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                return false;
            }

            if (IsFailedInLastTest())
            {
                this._RetakeTestApplication = new ApplicationInfo()
                {
                    ApplicantPersonID = this._LocalDrivingApplication.ApplicantPersonID,
                    ApplicationDate = DateTime.Now,
                    ApplicationTypeID = (int)ApplicationInfo.enApplicationType.RetakeTest,
                    PaidFees = ApplicationType.Find((int)ApplicationInfo.enApplicationType.RetakeTest).ApplicationFees,
                    CreatedByUserID = Global.currentLoggedInUser.UserID,
                };

                if (_RetakeTestApplication.Save())
                {
                    IsRetakingTest = true;
                }
                else
                {
                    MessageBox.Show("Failed to create retake test application. Please try again.",
                                    "Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                    return false;
                }
            }

            this._TestType = testType;
            FillNewTestDetails();
            return true;
        }
        public bool LoadTestDetails(int testAppointmentID)
        {
            if (testAppointmentID <= 0)
            {
                MessageBox.Show($"Invalid TestAppointmentID = {testAppointmentID}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            this._TestAppointment = TestAppointment.Find(testAppointmentID);
            if (this._TestAppointment == null)
            {
                MessageBox.Show($"No test appointment found with ID = {testAppointmentID}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!IsValidApplication(this._TestAppointment.LocalDrivingLicenseApplicationID))
                return false;

            if (this._LocalDrivingApplication.PassedAllTests())
            {
                MessageBox.Show("All tests have already been passed for this application.\n\n" +
                                "The license can now be issued.",
                                "All Tests Passed",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                return false;
            }

            this._RetakeTestApplication = ApplicationInfo.Find(this._TestAppointment.RetakeTestApplicationID);
            IsRetakingTest = (this._RetakeTestApplication != null);

            this._TestType = (TestType.enTestType)this._TestAppointment.TestTypeID;
            FillUpdatedTestDetails();
            return true;
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
            switch (this._TestType)
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
            float testFees = TestType.Find(this._TestType).TestTypeFees;
            float retakeTestApplicationFees = (IsRetakingTest) ? this._RetakeTestApplication.PaidFees : 0;

            lblLocalDrivingLicenseAppID.Text = this._LocalDrivingApplication.LocalDrivingLicenseApplicationID.ToString();
            lblDrivingClass.Text = this._LocalDrivingApplication.LicenseClassInfo.ClassName;
            lblFullName.Text = this._LocalDrivingApplication.ApplicantFullName.ToString();
            lblTrial.Text = this._LocalDrivingApplication.TotalTrialsPerTest(this._TestType).ToString();
            dtpTestDate.Value = DateTime.Now;
            lblFees.Text = TestType.Find(this._TestType).TestTypeFees.ToString();

            gbRetakeTestInfo.Enabled = IsRetakingTest;
            if (IsRetakingTest)
                lblRetakeTestAppID.Text = this._RetakeTestApplication.ApplicationID.ToString();

            lblRetakeAppFees.Text = retakeTestApplicationFees.ToString();
            lblTotalFees.Text = (retakeTestApplicationFees + testFees).ToString();
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
                lblUserMessage.Text = "Test appointment is already locked and cannot be modified.";
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
                    CreatedByUserID = Global.currentLoggedInUser.UserID,
                    RetakeTestApplicationID = (IsRetakingTest ? this._RetakeTestApplication.ApplicationID : ValidationConstants.INVALID_ID)
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
