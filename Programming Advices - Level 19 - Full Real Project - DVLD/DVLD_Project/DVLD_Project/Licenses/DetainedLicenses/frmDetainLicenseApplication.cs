using DVLD.Classes;
using DVLD_Business;
using DVLD_Common;
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

namespace DVLD_Project.Licenses.DetainedLicenses
{
    public partial class frmDetainLicenseApplication : Form
    {
        public event Action<object, int> OnLicenseDetain;

        private int _LicenseID = ValidationConstants.INVALID_ID;
        private int _DetainLicenseID = ValidationConstants.INVALID_ID;


        public frmDetainLicenseApplication()
        {
            InitializeComponent();
            this._LicenseID = ValidationConstants.INVALID_ID;
        }
        public frmDetainLicenseApplication(int licenseID)
        {
            InitializeComponent();
            this._LicenseID = licenseID;
        }
        private void frmDetainLicenseApplication_Load(object sender, EventArgs e)
        {
            this.OnLicenseDetain += LicenseDetaintHandler;
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
        private void frmDetainLicenseApplication_Activated(object sender, EventArgs e)
        {
            if (this._LicenseID == ValidationConstants.INVALID_ID)
                ctrlDriverLicenseInfoWithFilter.FilterFocus();
        }
        private void frmDetainLicenseApplication_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.OnLicenseDetain -= LicenseDetaintHandler;
        }


        private void ResetformToDefaultValues()
        {
            btnDetain.Enabled = false;
            llShowLicenseHistory.Enabled = false;
            llShowLicenseInfo.Enabled = false;
        }
        private void LicenseDetaintHandler(object sender, int licenseID)
        {
            MessageBox.Show($"License has been detained and refreshed !", "License Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ctrlDriverLicenseInfoWithFilter.LoadDriverLicenseDetails(this._LicenseID);
        }

        private void ctrlDriverLicenseInfoWithFilter_OnLicenseSelected(object sender, int licenseID)
        {
            if (licenseID <= 0)
            {
                MessageBox.Show($"Invalid LicenseID = {licenseID}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnDetain.Enabled = false;
                return;
            }
            this._LicenseID = licenseID;

            lblDetainDate.Text = Format.DateToShort(DateTime.Now);
            lblLicenseID.Text = this._LicenseID.ToString();
            lblCreatedByUser.Text = Global.currentLoggedInUser.UserName;

            llShowLicenseHistory.Enabled = true;
            if (ctrlDriverLicenseInfoWithFilter.SelectedLicenseDetails.IsLicenseExpired())
            {
                MessageBox.Show($"Selected License is expired!",
                    "Not allowed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                btnDetain.Enabled = false;
                return;
            }

            if (!ctrlDriverLicenseInfoWithFilter.SelectedLicenseDetails.IsActive)
            {
                MessageBox.Show("Selected License is not active, choose an active license.", "Not allowed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                btnDetain.Enabled = false;
                return;
            }

            if (ctrlDriverLicenseInfoWithFilter.SelectedLicenseDetails.IsDetained)
            {
                MessageBox.Show("Selected License is already detained !", "Not allowed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                btnDetain.Enabled = false;
                return;
            }

            btnDetain.Enabled = true;
        }
        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (ctrlDriverLicenseInfoWithFilter.SelectedLicenseDetails == null)
            {
                MessageBox.Show($"Selected a license First !",
                    "Not allowed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                btnDetain.Enabled = false;
                return;
            }

            new frmShowPersonLicenseHistory(ctrlDriverLicenseInfoWithFilter.SelectedLicenseDetails.DriverInfo.PersonInfo.PersonID).ShowDialog();
        }
        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new frmShowLicenseDetails(this._LicenseID).ShowDialog();
        }
        private void btnDetain_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Please correct the errors before first !", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Are you sure you want to detain the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            int detainedLicenseID = ctrlDriverLicenseInfoWithFilter.SelectedLicenseDetails.Detain(Convert.ToSingle(txtFineFees.Text), Global.currentLoggedInUser.UserID);
            if (detainedLicenseID == ValidationConstants.INVALID_ID)
            {
                MessageBox.Show("Faild to detain the License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this._DetainLicenseID = detainedLicenseID;
            MessageBox.Show($"Licensed Detained Successfully with ID = {this._DetainLicenseID.ToString()}", "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);
            lblDetainID.Text = this._DetainLicenseID.ToString();

            ctrlDriverLicenseInfoWithFilter.DisactiviteFilter();
            txtFineFees.Enabled = false;
            btnDetain.Enabled = false;
            llShowLicenseInfo.Enabled = true;

            this.OnLicenseDetain?.Invoke(this, this._DetainLicenseID);
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtFineFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFineFees.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider.SetError(txtFineFees, "Fees cannot be empty!");
                return;
            }
            else
                errorProvider.SetError(txtFineFees, null);

            if (!Validation.IsNumber(txtFineFees.Text))
            {
                e.Cancel = true;
                errorProvider.SetError(txtFineFees, "Invalid Number !");
            }
            else
                errorProvider.SetError(txtFineFees, null);
        }
    }
}
