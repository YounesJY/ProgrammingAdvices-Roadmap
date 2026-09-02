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
using static System.Net.Mime.MediaTypeNames;

namespace DVLD_Project.Applications.Release_Detained_License
{
    public partial class frmReleaseDetainedLicenseApplication : Form
    {
        public event Action<object, int> OnLicenseRelease;

        private int _LicenseID = ValidationConstants.INVALID_ID;


        public frmReleaseDetainedLicenseApplication()
        {
            InitializeComponent();
            this._LicenseID = ValidationConstants.INVALID_ID;
        }
        public frmReleaseDetainedLicenseApplication(int licenseID)
        {
            InitializeComponent();
            this._LicenseID = licenseID;
        }
        private void frmReleaseDetainedLicenseApplication_Load(object sender, EventArgs e)
        {
            this.OnLicenseRelease += LicenseReleaseHandler;
            ResetformToDefaultValues();

            if (this._LicenseID != ValidationConstants.INVALID_ID)
            {
                if (this._LicenseID <= 0)
                {
                    MessageBox.Show($"Invalid LicenseID = {this._LicenseID}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }
                ctrlDriverLicenseInfoWithFilter.LoadDriverLicenseDetails(this._LicenseID);
                ctrlDriverLicenseInfoWithFilter.DisactiviteFilter();
            }
        }
        private void frmReleaseDetainedLicenseApplication_Activated(object sender, EventArgs e)
        {
            if (this._LicenseID == ValidationConstants.INVALID_ID)
                ctrlDriverLicenseInfoWithFilter.FilterFocus();
        }
        private void frmReleaseDetainedLicenseApplication_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.OnLicenseRelease -= LicenseReleaseHandler;
        }


        private void ResetformToDefaultValues()
        {
            btnRelease.Enabled = false;
            llShowLicenseHistory.Enabled = false;
            llShowLicenseInfo.Enabled = false;
        }
        private void LicenseReleaseHandler(object sender, int licenseID)
        {
            MessageBox.Show($"License has been released and refreshed !", "License Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ctrlDriverLicenseInfoWithFilter.LoadDriverLicenseDetails(this._LicenseID);
        }

        private void ctrlDriverLicenseInfoWithFilter_OnLicenseSelected(object sender, int licenseID)
        {
            if (licenseID <= 0)
            {
                MessageBox.Show($"Invalid LicenseID = {licenseID}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnRelease.Enabled = false;
                return;
            }
            this._LicenseID = licenseID;
            llShowLicenseHistory.Enabled = true;

            if (!ctrlDriverLicenseInfoWithFilter.SelectedLicenseDetails.IsDetained)
            {
                MessageBox.Show("Selected License is not detained !", "Not allowed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                btnRelease.Enabled = false;
                return;
            }

            lblDetainID.Text = ctrlDriverLicenseInfoWithFilter.SelectedLicenseDetails.DetainedInfo.DetainID.ToString();
            lblDetainDate.Text = Format.DateToShort(ctrlDriverLicenseInfoWithFilter.SelectedLicenseDetails.DetainedInfo.DetainDate);
            lblLicenseID.Text = ctrlDriverLicenseInfoWithFilter.SelectedLicenseDetails.LicenseID.ToString();
            lblApplicationFees.Text = ApplicationType.Find((int)ApplicationInfo.enApplicationType.ReleaseDetainedDrivingLicense).ApplicationFees.ToString();
            lblFineFees.Text = ctrlDriverLicenseInfoWithFilter.SelectedLicenseDetails.DetainedInfo.FineFees.ToString();
            lblTotalFees.Text = (Convert.ToSingle(lblApplicationFees.Text) + Convert.ToSingle(lblFineFees.Text)).ToString();
            lblCreatedByUser.Text = ctrlDriverLicenseInfoWithFilter.SelectedLicenseDetails.DetainedInfo.CreatedByUserInfo.UserName;


            btnRelease.Enabled = true;
        }
        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (ctrlDriverLicenseInfoWithFilter.SelectedLicenseDetails == null)
            {
                MessageBox.Show($"Selected a license First !",
                    "Not allowed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                btnRelease.Enabled = false;
                return;
            }

            new frmShowPersonLicenseHistory(ctrlDriverLicenseInfoWithFilter.SelectedLicenseDetails.DriverInfo.PersonInfo.PersonID).ShowDialog();
        }
        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new frmShowLicenseDetails(this._LicenseID).ShowDialog();
        }
        private void btnRelease_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to release the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            int releaseApplicationID = ValidationConstants.INVALID_ID;
            bool isReleased = ctrlDriverLicenseInfoWithFilter.SelectedLicenseDetails.ReleaseDetainedLicense(Global.currentLoggedInUser.UserID, ref releaseApplicationID); ;

            if (!isReleased)
            {
                MessageBox.Show("Faild to to release the Detained License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            MessageBox.Show($"Licensed Released Successfully with ID = {releaseApplicationID.ToString()}", "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //You can do this but it's dependant on the control and not a standalone process
            //lblApplicationID.Text = ctrlDriverLicenseInfoWithFilter.SelectedLicenseDetails.DetainedInfo.ReleaseApplicationID;
            lblApplicationID.Text = releaseApplicationID.ToString();
            ctrlDriverLicenseInfoWithFilter.DisactiviteFilter();
            btnRelease.Enabled = false;
            llShowLicenseInfo.Enabled = true;

            this.OnLicenseRelease?.Invoke(this, this._LicenseID);
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
