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
        private LocalDrivingLicenseApplication _LocalDrivingApplication = null;
        private TestType.enTestType _TestType = TestType.enTestType.VisionTest;

        public ctrlSheduleTest()
        {
            InitializeComponent();
            _ResetToDefaultValues();
        }

        private void _ResetToDefaultValues()
        {
            lblLocalDrivingLicenseAppID.Text = "[??]";
            lblDrivingClass.Text = "[??]";
            lblFullName.Text = "[??]";
            lblTrial.Text = "[??]";
            dtpTestDate.Value = DateTime.Now;
            dtpTestDate.MinDate = DateTime.Now;
            lblFees.Text = "[$$]";
            gbRetakeTestInfo.Enabled = false;
        }

        public void LoadTestDetails(int localDrivingApplicationID, TestType.enTestType testType)
        {
            if (localDrivingApplicationID <= 0)
            {
                MessageBox.Show($"Invalid ApplicationID = {localDrivingApplicationID}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this._LocalDrivingApplication = LocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(localDrivingApplicationID);
            if (this._LocalDrivingApplication == null)
            {
                MessageBox.Show($"No application with ApplicationID = {localDrivingApplicationID}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this._TestType = testType;
            setTestIcon();
            setTestLabel();
            _FillTestDetails();
        }

        private void setTestLabel()
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
        private void _FillTestDetails()
        {
            lblLocalDrivingLicenseAppID.Text = this._LocalDrivingApplication.LocalDrivingLicenseApplicationID.ToString();
            lblDrivingClass.Text = this._LocalDrivingApplication.LicenseClassInfo.ClassName;
            lblFullName.Text = this._LocalDrivingApplication.ApplicantFullName.ToString();
            lblTrial.Text = this._LocalDrivingApplication.TotalTrialsPerTest(this._TestType).ToString();
            dtpTestDate.MinDate = DateTime.Now;
            dtpTestDate.Value = DateTime.Now;
            lblFees.Text = TestType.Find(this._TestType).TestTypeFees.ToString();
            gbRetakeTestInfo.Enabled = false;
        }
    }
}
