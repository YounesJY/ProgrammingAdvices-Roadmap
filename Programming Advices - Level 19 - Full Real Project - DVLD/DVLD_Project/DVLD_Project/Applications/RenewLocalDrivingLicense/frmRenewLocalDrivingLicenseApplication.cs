using DVLD.Classes;
using DVLD_Business;
using DVLD_Common;
using DVLD_Project.Licenses;
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

namespace DVLD_Project.Applications.RenewLocalDrivingLicense
{
    public partial class frmRenewLocalDrivingLicenseApplication : Form
    {
        public event Action<object, int> OnLicenseRenewal;

        private int _OldLicenseID = ValidationConstants.INVALID_ID;
        private int _NewLicenseID = ValidationConstants.INVALID_ID;


        public frmRenewLocalDrivingLicenseApplication()
        {
            InitializeComponent();
            this._OldLicenseID = ValidationConstants.INVALID_ID;
        }
        public frmRenewLocalDrivingLicenseApplication(int licenseID)
        {
            InitializeComponent();
            this._OldLicenseID = licenseID;
        }

        private void frmRenewLocalDrivingLicenseApplication_Load(object sender, EventArgs e)
        {
            this.OnLicenseRenewal += LicenseRenewalHandler;
            ResetformToDefaultValues();

            if (this._OldLicenseID != ValidationConstants.INVALID_ID)
            {
                if (this._OldLicenseID <= 0)
                {
                    MessageBox.Show($"Invalid LicenseID = {this._OldLicenseID}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }
                ctrlDriverLicenseInfoWithFilter.LoadDriverLicenseDetails(this._OldLicenseID);
                ctrlDriverLicenseInfoWithFilter.DisactiviteFilter();
            }
        }
        private void frmRenewLocalDrivingLicenseApplication_Activated(object sender, EventArgs e)
        {
            if (this._OldLicenseID == ValidationConstants.INVALID_ID)
                ctrlDriverLicenseInfoWithFilter.FilterFocus();
        }
        private void frmRenewLocalDrivingLicenseApplication_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.OnLicenseRenewal -= LicenseRenewalHandler;
        }


        private void ResetformToDefaultValues()
        {
            btnRenewLicense.Enabled = false;
            llShowLicenseHistory.Enabled = false;
            llShowLicenseInfo.Enabled = false;
        }
        private void LicenseRenewalHandler(object sender, int licenseID)
        {
            MessageBox.Show($"Old license has been disactivated and refreshed !", "License Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ctrlDriverLicenseInfoWithFilter.LoadDriverLicenseDetails(this._OldLicenseID);
        }

        private void ctrlDriverLicenseInfoWithFilter_OnLicenseSelected(object sender, int licenseID)
        {
            if (licenseID <= 0)
            {
                MessageBox.Show($"Invalid LicenseID = {licenseID}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnRenewLicense.Enabled = false;
                return;
            }
            this._OldLicenseID = licenseID;

            lblApplicationDate.Text = Format.DateToShort(DateTime.Now);
            lblApplicationFees.Text = ApplicationType.Find((int)ApplicationInfo.enApplicationType.RenewDrivingLicense).ApplicationFees.ToString();
            lblIssueDate.Text = lblApplicationDate.Text;
            lblLicenseFees.Text = ctrlDriverLicenseInfoWithFilter.SelectedLicenseDetails.LicenseClassInfo.ClassFees.ToString();
            txtNotes.Text = ctrlDriverLicenseInfoWithFilter.SelectedLicenseDetails.Notes;

            lblOldLicenseID.Text = this._OldLicenseID.ToString();
            int DefaultValidityLength = ctrlDriverLicenseInfoWithFilter.SelectedLicenseDetails.LicenseClassInfo.DefaultValidityLength;
            lblExpirationDate.Text = Format.DateToShort(DateTime.Now.AddYears(DefaultValidityLength));
            lblTotalFees.Text = (Convert.ToSingle(lblApplicationFees.Text) + Convert.ToSingle(lblLicenseFees.Text)).ToString();
            lblCreatedByUser.Text = Global.currentLoggedInUser.UserName;

            llShowLicenseHistory.Enabled = true;
            if (!ctrlDriverLicenseInfoWithFilter.SelectedLicenseDetails.IsLicenseExpired())
            {
                MessageBox.Show($"Selected License is not yet expired, it will expire on: {Format.DateToShort(ctrlDriverLicenseInfoWithFilter.SelectedLicenseDetails.ExpirationDate)}",
                    "Not allowed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                btnRenewLicense.Enabled = false;
                return;
            }

            if (!ctrlDriverLicenseInfoWithFilter.SelectedLicenseDetails.IsActive)
            {
                MessageBox.Show("Selected License is not active, choose an active license.", "Not allowed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                btnRenewLicense.Enabled = false;
                return;
            }

            btnRenewLicense.Enabled = true;
        }
        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (ctrlDriverLicenseInfoWithFilter.SelectedLicenseDetails == null)
            {
                MessageBox.Show($"Selected a license First !",
                    "Not allowed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                btnRenewLicense.Enabled = false;
                return;
            }

            new frmShowPersonLicenseHistory(ctrlDriverLicenseInfoWithFilter.SelectedLicenseDetails.DriverInfo.PersonInfo.PersonID).ShowDialog();
        }
        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new frmShowLicenseDetails(this._NewLicenseID).ShowDialog();
        }
        private void btnRenewLicense_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to Renew the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            LicenseInfo NewLicense = ctrlDriverLicenseInfoWithFilter.SelectedLicenseDetails.RenewLicense(txtNotes.Text.Trim(), Global.currentLoggedInUser.UserID);
            if (NewLicense == null)
            {
                MessageBox.Show("Faild to Renew the License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this._NewLicenseID = NewLicense.LicenseID;
            MessageBox.Show($"Licensed Renewed Successfully with ID = {this._NewLicenseID.ToString()}", "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);
            lblApplicationID.Text = NewLicense.ApplicationID.ToString();
            lblRenewedLicenseID.Text = this._NewLicenseID.ToString();

            ctrlDriverLicenseInfoWithFilter.DisactiviteFilter();
            btnRenewLicense.Enabled = false;
            llShowLicenseInfo.Enabled = true;

            this.OnLicenseRenewal?.Invoke(this, this._NewLicenseID);
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
