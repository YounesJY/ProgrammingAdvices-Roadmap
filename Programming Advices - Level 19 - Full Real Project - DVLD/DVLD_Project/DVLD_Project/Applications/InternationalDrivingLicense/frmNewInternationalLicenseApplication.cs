using DVLD.Classes;
using DVLD_Business;
using DVLD_Common;
using DVLD_Project.Licenses;
using DVLD_Project.Licenses.InternationalLicenses;
using DVLD_Project.Licenses.LocalLicenses;
using DVLD_Project.Licenses.LocalLicenses.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project.Applications.InternationalDrivingLicense
{
    public partial class frmNewInternationalLicenseApplication : Form
    {
        public event Action<object, int> OnInternationalLicenseIssuance;

        private int _LocalLicenseID = ValidationConstants.INVALID_ID;
        private int _InternationalLicenseID = ValidationConstants.INVALID_ID;


        public frmNewInternationalLicenseApplication()
        {
            InitializeComponent();
            this._LocalLicenseID = ValidationConstants.INVALID_ID;
        }
        public frmNewInternationalLicenseApplication(int LocalLicenseID)
        {
            InitializeComponent();
            this._LocalLicenseID = LocalLicenseID;
        }
        private void frmNewInternationalLicenseApplication_Load(object sender, EventArgs e)
        {
            this.OnInternationalLicenseIssuance += InternationalLicenseIssuanceHandler;
            ResetformToDefaultValues();

            if (this._LocalLicenseID != ValidationConstants.INVALID_ID)
            {
                if (this._LocalLicenseID <= 0)
                {
                    MessageBox.Show($"Invalid LicenseID = {this._LocalLicenseID}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }
                ctrlDriverLicenseInfoWithFilter.LoadDriverLicenseDetails(this._LocalLicenseID);
                ctrlDriverLicenseInfoWithFilter.DisactiviteFilter();
            }
        }
        private void frmNewInternationalLicenseApplication_Activated(object sender, EventArgs e)
        {
            if (this._LocalLicenseID == ValidationConstants.INVALID_ID)
                ctrlDriverLicenseInfoWithFilter.FilterFocus();
        }
        private void frmNewInternationalLicenseApplication_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.OnInternationalLicenseIssuance -= InternationalLicenseIssuanceHandler;
        }


        private void ResetformToDefaultValues()
        {
            btnIssueInternationalLicense.Enabled = false;
            llShowLicenseHistory.Enabled = false;
            llShowLicenseInfo.Enabled = false;
        }
        private void InternationalLicenseIssuanceHandler(object sender, int licenseID)
        {
            MessageBox.Show($"International license {this._InternationalLicenseID} has been linked with local license {this._LocalLicenseID}!", "License Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ctrlDriverLicenseInfoWithFilter_OnLicenseSelected(object sender, int licenseID)
        {
            if (licenseID <= 0)
            {
                MessageBox.Show($"Invalid LicenseID = {licenseID}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnIssueInternationalLicense.Enabled = false;
                llShowLicenseHistory.Enabled = false;
                return;
            }
            this._LocalLicenseID = licenseID;
            llShowLicenseHistory.Enabled = true;

            lblApplicationDate.Text = Format.DateToShort(DateTime.Now);
            lblIssueDate.Text = lblApplicationDate.Text;
            lblFees.Text = ApplicationType.Find((int)ApplicationInfo.enApplicationType.NewInternationalLicense).ApplicationFees.ToString();
            lblLocalLicenseID.Text = this._LocalLicenseID.ToString();
            lblExpirationDate.Text = Format.DateToShort(DateTime.Now.AddYears(1));
            lblCreatedByUser.Text = Global.currentLoggedInUser.UserName;

            if (ctrlDriverLicenseInfoWithFilter.SelectedLicenseDetails.LicenseClassID != (int)LicenseClass.enLicenseClass.OrdinaryDrivingLicense)
            {
                MessageBox.Show($"you can only issue international license for Ordinary Driving Licenses !",
                "Not allowed",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
                btnIssueInternationalLicense.Enabled = false;
                return;
            }

            if (!ctrlDriverLicenseInfoWithFilter.SelectedLicenseDetails.IsActive)
            {
                MessageBox.Show("Selected License is not active, choose an active license.", "Not allowed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                btnIssueInternationalLicense.Enabled = false;
                return;
            }

            if (ctrlDriverLicenseInfoWithFilter.SelectedLicenseDetails.IsLicenseExpired())
            {
                MessageBox.Show($"Selected License is expired and can't issue an international license: {Format.DateToShort(ctrlDriverLicenseInfoWithFilter.SelectedLicenseDetails.ExpirationDate)}",
                    "Not allowed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                btnIssueInternationalLicense.Enabled = false;
                llShowLicenseHistory.Enabled = false;

                return;
            }

            int internationalLicenseID = InternationalDrivingLicenseApplication.GetActiveInternationalLicenseIDByDriverID(ctrlDriverLicenseInfoWithFilter.SelectedLicenseDetails.DriverID);
            if (internationalLicenseID != ValidationConstants.INVALID_ID)
            {
                MessageBox.Show($"Driver already has an active international license with ID = {internationalLicenseID} and can't issue a new one until expired !", "Not allowed",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
                btnIssueInternationalLicense.Enabled = false;
                return;
            }

            btnIssueInternationalLicense.Enabled = true;
        }
        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new frmShowPersonLicenseHistory(ctrlDriverLicenseInfoWithFilter.SelectedLicenseDetails.DriverInfo.PersonInfo.PersonID).ShowDialog();
        }
        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new frmShowInternationalLicenseDetails(this._InternationalLicenseID).ShowDialog();
        }
        private void btnIssueLicense_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to issue the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            InternationalDrivingLicenseApplication internationalLicense = new InternationalDrivingLicenseApplication()
            {
                ApplicantPersonID = ctrlDriverLicenseInfoWithFilter.SelectedLicenseDetails.DriverInfo.PersonID,
                ApplicationDate = DateTime.Now,
                ApplicationStatusID = ApplicationInfo.enApplicationStatus.Completed,
                LastStatusDate = DateTime.Now,
                PaidFees = ApplicationType.Find((int)ApplicationInfo.enApplicationType.NewInternationalLicense).ApplicationFees,

                DriverID = ctrlDriverLicenseInfoWithFilter.SelectedLicenseDetails.DriverID,
                IssuedUsingLocalLicenseID = ctrlDriverLicenseInfoWithFilter.SelectedLicenseDetails.LicenseID,
                IssueDate = DateTime.Now,
                ExpirationDate = DateTime.Now.AddYears(1), // this value can be dyncamic using a database settings table
                IsActive = true,
                CreatedByUserID = Global.currentLoggedInUser.UserID
            };
            if (!internationalLicense.Save())
            {
                MessageBox.Show("Faild to Renew the License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this._InternationalLicenseID = internationalLicense.InternationalLicenseID;

            MessageBox.Show($"International Licensed issued Successfully with ID = {this._InternationalLicenseID.ToString()}", "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);
            lblApplicationID.Text = internationalLicense.ApplicationID.ToString();
            lblInternationalLicenseID.Text = this._InternationalLicenseID.ToString();

            ctrlDriverLicenseInfoWithFilter.DisactiviteFilter();
            btnIssueInternationalLicense.Enabled = false;
            llShowLicenseInfo.Enabled = true;

            this.OnInternationalLicenseIssuance?.Invoke(this, this._LocalLicenseID);
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
