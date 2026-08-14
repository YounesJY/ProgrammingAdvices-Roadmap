using DVLD.Classes;
using DVLD_Business;
using DVLD_Common;
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

            this._LocalDrivingLicenseApplication = LocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(localDrivingApplicationID);
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
        private void FillLocalDrivingApplicationDetails()
        {
            /*
             *  This is still commented due to non implemented forms of Licenses, Drivers...
             * 
                //incase there is license enable the show link.
                llShowLicenceInfo.Enabled = this._localDrivingLicenseApplication.GetActiveLicenseID() != ValidationConstants.INVALID_ID;
            */

            lblLocalDrivingLicenseApplicationID.Text = this._LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
            lblAppliedFor.Text = this._LocalDrivingLicenseApplication.LicenseClassInfo.ClassName;
            lblPassedTests.Text = this._LocalDrivingLicenseApplication.GetPassedTestCount().ToString() + "/3";
            ctrlApplicationDetails.LoadApplicationDetailsToCard(this._LocalDrivingLicenseApplication.ApplicationID);
        }
        private void ResetLocalDrivingApplicationDetails()
        {
            this._LocalDrivingLicenseApplication = null;
            ctrlApplicationDetails.ResetApplicationDetails();
            lblLocalDrivingLicenseApplicationID.Text = "[????]";
            lblAppliedFor.Text = "[????]";
        }
    }
}
