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

namespace DVLD_Project.Licenses.LocalLicenses.Controls
{
    public partial class ctrlDriverLicenseDetailsWithFilter : UserControl
    {
        public event Action<object, int> OnLicenseSelected;

        private int _LicenseID = ValidationConstants.INVALID_ID;
        private LicenseInfo _LicenseDetails = null;
        public LicenseInfo SelectedLicenseDetails { get { return this._LicenseDetails; } }


        public ctrlDriverLicenseDetailsWithFilter()
        {
            InitializeComponent();
        }
        public bool LoadDriverLicenseDetails(int LicenseID)
        {
            this._LicenseID = LicenseID;
            if (!isValidLicense())
                return false;

            this.ctrlDriverLicenseDetails.LoadDriverLicenseDetails(this._LicenseID);
            this.DisactiviteFilter();
            return true;
        }


        private bool isValidLicense()
        {
            if (this._LicenseID <= 0)
            {
                MessageBox.Show($"Invalid LicenseID = {this._LicenseID}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            this._LicenseDetails = LicenseInfo.Find(this._LicenseID);
            if (this._LicenseDetails == null)
            {
                MessageBox.Show($"Failed to load the license with ID = {this._LicenseID}. Please try again.",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return false;
            }

            return true;
        }
        private void txtLicenseID_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtLicenseID.Text.Trim()))
            {
                errorProvider.SetError(txtLicenseID, "This field is required!");
                txtLicenseID.Focus();
                e.Cancel = true;
            }
            else
            {
                errorProvider.SetError(txtLicenseID, null);
                e.Cancel = false;
            }
        }
        private void txtLicenseID_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

            if (e.KeyChar == (char)13)
                btnFind.PerformClick();
        }
        private void btnFind_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtLicenseID.Focus();
                return;
            }

            this._LicenseID = int.Parse(this.txtLicenseID.Text);
            if (isValidLicense())
            {
                this.ctrlDriverLicenseDetails.LoadDriverLicenseDetails(this._LicenseID);
                OnLicenseSelected?.Invoke(this, this._LicenseID);
            }
        }

        public void ActivateFilter()
        {
            gbFilter.Enabled = true;
        }
        public void DisactiviteFilter()
        {
            gbFilter.Enabled = false;
        }
        public void FilterFocus()
        {
            txtLicenseID.Focus();
        }
    }
}
