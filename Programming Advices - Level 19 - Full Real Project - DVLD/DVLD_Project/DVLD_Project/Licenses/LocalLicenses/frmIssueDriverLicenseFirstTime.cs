using DVLD_Business;
using DVLD_Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD_Project.Licenses.LocalLicenses
{
    public partial class frmIssueDriverLicenseFirstTime : Form
    {
        public event Action<object, int> OnApplicationCardDetailsUpdated
        {
            add { this.ctrlLocalDrivingApplicationDetails.OnApplicationCardDetailsUpdated += value; }
            remove { this.ctrlLocalDrivingApplicationDetails.OnApplicationCardDetailsUpdated -= value; }
        }
        public event Action<object, int> OnLicenseIssuanceForFirstTime;
        private int _LocalDrivingApplicationID = ValidationConstants.INVALID_ID;
        private LocalDrivingLicenseApplication _LocalDrivingLicenseApplication;

        public frmIssueDriverLicenseFirstTime(int localDrivingApplicationID)
        {
            InitializeComponent();
            this._LocalDrivingApplicationID = localDrivingApplicationID;
        }
        private void frmIssueDriverLicenseFirstTime_Load(object sender, EventArgs e)
        {
            this.OnApplicationCardDetailsUpdated += RefreshApplicationDetailsOnUpdate;

            if (this._LocalDrivingApplicationID <= 0)
            {
                MessageBox.Show($"Invalid ApplicationID = {this._LocalDrivingApplicationID}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            this.ctrlLocalDrivingApplicationDetails.LoadApplicationDetailsByLocalDrivingApplicationID(this._LocalDrivingApplicationID);

            this._LocalDrivingLicenseApplication = LocalDrivingLicenseApplication.FindByLocalDrivingApplicationID(this._LocalDrivingApplicationID);
            if (this._LocalDrivingLicenseApplication == null)
            {
                MessageBox.Show($"No application with ApplicationID = {this._LocalDrivingApplicationID}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            if (!this._LocalDrivingLicenseApplication.PassedAllTests())
            {
                MessageBox.Show($"Applicant person should pass all test first !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            if (this._LocalDrivingLicenseApplication.GetActiveLicenseID() != ValidationConstants.INVALID_ID)
            {
                MessageBox.Show($"Applicant person already has an active licence !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
        }
        private void frmIssueDriverLicenseFirstTime_FormClosing(object sender, FormClosingEventArgs e)
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
        private void btnIssueLicense_Click(object sender, EventArgs e)
        {
            int licenseID = ValidationConstants.INVALID_ID;

            licenseID = this._LocalDrivingLicenseApplication.IssueLicenseForTheFirtTime(this.txtNotes.Text.Trim(), Global.currentLoggedInUser.UserID);
            if (licenseID == ValidationConstants.INVALID_ID)
            {
                MessageBox.Show("Failed to issue the license. Please try again.",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show($"License issued successfully.\n\nLicense ID: {licenseID}",
                            "Success",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

            this.OnLicenseIssuanceForFirstTime?.Invoke(this, licenseID);
            this.ctrlLocalDrivingApplicationDetails.LoadApplicationDetailsByLocalDrivingApplicationID(this._LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID);
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
