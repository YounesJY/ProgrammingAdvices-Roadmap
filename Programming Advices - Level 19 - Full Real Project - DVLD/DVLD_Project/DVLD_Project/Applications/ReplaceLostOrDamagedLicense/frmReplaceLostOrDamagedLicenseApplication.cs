using DVLD.Classes;
using DVLD_Business;
using DVLD_Common;
using DVLD_Project.Licenses;
using DVLD_Project.Licenses.LocalLicenses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DVLD_Business.LicenseInfo;

namespace DVLD_Project.Applications.ReplaceLostOrDamagedLicense
{
    public partial class frmReplaceLostOrDamagedLicenseApplication : Form
    {
        public event Action<object, int> OnLicenseReplacement;

        private int _OldLicenseID = ValidationConstants.INVALID_ID;
        private int _NewLicenseID = ValidationConstants.INVALID_ID;


        public frmReplaceLostOrDamagedLicenseApplication()
        {
            InitializeComponent();
            this._OldLicenseID = ValidationConstants.INVALID_ID;
        }
        public frmReplaceLostOrDamagedLicenseApplication(int licenseID)
        {
            InitializeComponent();
            this._OldLicenseID = licenseID;
        }
        private void frmReplaceLostOrDamagedLicenseApplication_Load(object sender, EventArgs e)
        {
            this.OnLicenseReplacement += LicenseRenewalHandler;
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
        private void frmReplaceLostOrDamagedLicenseApplication_Activated(object sender, EventArgs e)
        {
            if (this._OldLicenseID == ValidationConstants.INVALID_ID)
                ctrlDriverLicenseInfoWithFilter.FilterFocus();
        }
        private void frmReplaceLostOrDamagedLicenseApplication_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.OnLicenseReplacement -= LicenseRenewalHandler;
        }


        private void ResetformToDefaultValues()
        {
            rbDamagedLicense.Checked = true;
            btnIssueReplacement.Enabled = false;
            llShowLicenseHistory.Enabled = false;
            llShowLicenseInfo.Enabled = false;
        }
        private void LicenseRenewalHandler(object sender, int licenseID)
        {
            MessageBox.Show($"Old license has been disactivated and refreshed !", "License Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ctrlDriverLicenseInfoWithFilter.LoadDriverLicenseDetails(this._OldLicenseID);
        }

        private ApplicationInfo.enApplicationType GetApplicationType()
        {
            return (rbDamagedLicense.Checked) ? ApplicationInfo.enApplicationType.ReplaceDamagedDrivingLicense : ApplicationInfo.enApplicationType.ReplaceLostDrivingLicense;
        }
        private enIssueReason GetIssueReason()
        {
            return (rbDamagedLicense.Checked) ? enIssueReason.DamagedReplacement : enIssueReason.LostReplacement;
        }

        private void ctrlDriverLicenseInfoWithFilter_OnLicenseSelected(object sernder, int licenseID)
        {
            if (licenseID <= 0)
            {
                MessageBox.Show($"Invalid LicenseID = {licenseID}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnIssueReplacement.Enabled = false;
                return;
            }
            this._OldLicenseID = licenseID;

            lblApplicationDate.Text = Format.DateToShort(DateTime.Now);
            lblApplicationFees.Text = ApplicationType.Find((int)ApplicationInfo.enApplicationType.RenewDrivingLicense).ApplicationFees.ToString();
            lblOldLicenseID.Text = this._OldLicenseID.ToString();
            lblCreatedByUser.Text = Global.currentLoggedInUser.UserName;

            llShowLicenseHistory.Enabled = true;
            if (!ctrlDriverLicenseInfoWithFilter.SelectedLicenseDetails.IsActive)
            {
                MessageBox.Show("Selected License is not active, choose an active license.", "Not allowed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                btnIssueReplacement.Enabled = false;
                return;
            }

            btnIssueReplacement.Enabled = true;
        }
        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (ctrlDriverLicenseInfoWithFilter.SelectedLicenseDetails == null)
            {
                MessageBox.Show($"Selected a license First !",
                    "Not allowed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                btnIssueReplacement.Enabled = false;
                return;
            }

            new frmShowPersonLicenseHistory(ctrlDriverLicenseInfoWithFilter.SelectedLicenseDetails.DriverInfo.PersonInfo.PersonID).ShowDialog();
        }
        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new frmShowLicenseDetails(this._NewLicenseID).ShowDialog();
        }
        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            this.lblTitle.Text = ((RadioButton)sender).Tag.ToString();
            this.Text = lblTitle.Text;
            lblApplicationFees.Text = ApplicationType.Find((int)GetApplicationType()).ApplicationFees.ToString();
        }

        private void btnIssueReplacement_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to repalce the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            LicenseInfo NewLicense = ctrlDriverLicenseInfoWithFilter.SelectedLicenseDetails.Repalce(GetIssueReason(), Global.currentLoggedInUser.UserID);
            if (NewLicense == null)
            {
                MessageBox.Show("Faild to Replace the License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this._NewLicenseID = NewLicense.LicenseID;
            MessageBox.Show($"Licensed Replaced Successfully with ID = {this._NewLicenseID.ToString()}", "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);
            lblApplicationID.Text = NewLicense.ApplicationID.ToString();
            lblReplacedLicenseID.Text = this._NewLicenseID.ToString();

            ctrlDriverLicenseInfoWithFilter.DisactiviteFilter();
            btnIssueReplacement.Enabled = false;
            llShowLicenseInfo.Enabled = true;

            this.OnLicenseReplacement?.Invoke(this, this._NewLicenseID);
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
