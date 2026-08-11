using DVLD.Classes;
using DVLD_Business;
using DVLD_Common;
using System;
using System.Windows.Forms;

namespace DVLD_Project.Applications.LocalDrivingLicense.Controls
{
    public partial class ctrlLocalDrivingApplicationDetails : UserControl
    {
        public event Action<object, int> OnApplicationCardDetailsUpdated
        {
            add { this.ctrlApplicationDetails.OnApplicationCardDetailsUpdated += value; }
            remove { this.ctrlApplicationDetails.OnApplicationCardDetailsUpdated -= value; }
        }
        private LocalDrivingLicenseApplication _localDrivingLicenseApplication = null;


        public ctrlLocalDrivingApplicationDetails()
        {
            InitializeComponent();
        }


        public void LoadLocalDrivingApplicationDetailsToCard(int localDrivingApplicationID)
        {
            if (localDrivingApplicationID <= 0)
            {
                ResetLocalDrivingApplicationDetails();
                MessageBox.Show($"Invalid ApplicationID = {localDrivingApplicationID}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this._localDrivingLicenseApplication = LocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(localDrivingApplicationID);
            if (this._localDrivingLicenseApplication == null)
            {
                ResetLocalDrivingApplicationDetails();
                MessageBox.Show($"No application with ApplicationID = {localDrivingApplicationID}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            lblLocalDrivingLicenseApplicationID.Text = this._localDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
            lblAppliedFor.Text = this._localDrivingLicenseApplication.LicenseClassInfo.ClassName;
            lblPassedTests.Text = this._localDrivingLicenseApplication.GetPassedTestCount().ToString() + "/3";
            ctrlApplicationDetails.LoadApplicationDetailsToCard(this._localDrivingLicenseApplication.ApplicationID);
        }
        private void ResetLocalDrivingApplicationDetails()
        {
            this._localDrivingLicenseApplication = null;
            ctrlApplicationDetails.ResetApplicationDetails();
            lblLocalDrivingLicenseApplicationID.Text = "[????]";
            lblAppliedFor.Text = "[????]";
        }
    }
}
