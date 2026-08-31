using DVLD.Classes;
using DVLD_Business;
using DVLD_Common;
using DVLD_Project.Licenses.LocalLicenses;
using DVLD_Project.People;
using System;
using System.Windows.Forms;

namespace DVLD_Project.Applications.LocalDrivingLicense.Controls
{
    public partial class ctrlLocalDrivingApplicationDetails : UserControl
    {
        // [Event Exposure]
        public event Action<object, int> OnApplicationCardDetailsUpdated
        {
            add { this.ctrlApplicationDetails.OnApplicationCardDetailsUpdated += value; }
            remove { this.ctrlApplicationDetails.OnApplicationCardDetailsUpdated -= value; }
        }
        private LocalDrivingLicenseApplication _LocalDrivingLicenseApplication = null;


        public ctrlLocalDrivingApplicationDetails()
        {
            InitializeComponent();
        }


        public void LoadApplicationDetailsByLocalDrivingApplicationID(int localDrivingApplicationID)
        {
            if (localDrivingApplicationID <= 0)
            {
                ResetLocalDrivingApplicationDetails();
                MessageBox.Show($"Invalid ApplicationID = {localDrivingApplicationID}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this._LocalDrivingLicenseApplication = LocalDrivingLicenseApplication.FindByLocalDrivingApplicationID(localDrivingApplicationID);
            if (this._LocalDrivingLicenseApplication == null)
            {
                ResetLocalDrivingApplicationDetails();
                MessageBox.Show($"No application with ApplicationID = {localDrivingApplicationID}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            FillLocalDrivingApplicationDetails();
        }
        public void LoadApplicationDetailsByApplicationID(int applicationID)
        {
            if (applicationID <= 0)
            {
                ResetLocalDrivingApplicationDetails();
                MessageBox.Show($"Invalid ApplicationID = {applicationID}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this._LocalDrivingLicenseApplication = LocalDrivingLicenseApplication.FindByApplicationID(applicationID);
            if (this._LocalDrivingLicenseApplication == null)
            {
                ResetLocalDrivingApplicationDetails();
                MessageBox.Show($"No application with ApplicationID = {applicationID}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            FillLocalDrivingApplicationDetails();
        }
        private void ResetLocalDrivingApplicationDetails()
        {
            this._LocalDrivingLicenseApplication = null;
            ctrlApplicationDetails.ResetApplicationDetails();
            lblLocalDrivingLicenseApplicationID.Text = "[????]";
            lblAppliedFor.Text = "[????]";
        }
        private void FillLocalDrivingApplicationDetails()
        {
            //incase there is license enable the show link.
            //llShowLicenceInfo.Enabled = (this._LocalDrivingLicenseApplication.GetActiveLicenseID() != ValidationConstants.INVALID_ID);
            llShowLicenceInfo.Enabled = this._LocalDrivingLicenseApplication.IsLicenseIssued();

            lblLocalDrivingLicenseApplicationID.Text = this._LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
            lblAppliedFor.Text = this._LocalDrivingLicenseApplication.LicenseClassInfo.ClassName;
            lblPassedTests.Text = this._LocalDrivingLicenseApplication.GetPassedTestCount().ToString() + "/3";
            ctrlApplicationDetails.LoadApplicationDetailsToCard(this._LocalDrivingLicenseApplication.ApplicationID);
        }

        private void llShowLicenceInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int licenseID = LicenseInfo.GetActiveLicenseIDByPersonID(this._LocalDrivingLicenseApplication.ApplicantPersonID, this._LocalDrivingLicenseApplication.LicenseClassID);
            new frmShowLicenseDetails(licenseID).ShowDialog();
        }
    }
}
