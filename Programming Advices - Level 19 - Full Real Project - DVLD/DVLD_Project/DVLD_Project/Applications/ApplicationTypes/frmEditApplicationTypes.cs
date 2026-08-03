using DVLD.Classes;
using DVLD_Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project.Applications.ApplicationTypes
{
    public partial class frmEditApplicationTypes : Form
    {
        // [From Own Event]
        internal Action<object, int> OnApplicationTypeUpdated;

        private int _applicationTypeID;
        private ApplicationType _applicationType;

        public frmEditApplicationTypes(int applicationTypeID)
        {
            InitializeComponent();
            this._applicationTypeID = applicationTypeID;
        }
        private void frmEditApplicationTypes_Load(object sender, EventArgs e)
        {
            this._applicationType = ApplicationType.Find(this._applicationTypeID);
            if (_applicationType == null)
            {
                MessageBox.Show("Application type not found.",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                Close();
                return;
            }
            fillFormWithApplicationTypeDetails();
        }

        private void fillFormWithApplicationTypeDetails()
        {
            lblApplicationTypeID.Text = this._applicationType.ApplicationTypeID.ToString();
            txtTitle.Text = this._applicationType.ApplicationTypeTitle;
            txtFees.Text = this._applicationType.ApplicationFees.ToString("0.00");
        }
        private void txtTitle_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                errorProvider.SetError(txtTitle, "Application type title is required.");
                txtTitle.Focus();
                e.Cancel = true;
            }
            else
            {
                e.Cancel = false;
                errorProvider.SetError(txtTitle, null);
            }
        }
        private void txtFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFees.Text))
            {
                errorProvider.SetError(txtFees, "Application fees are required.");
                txtFees.Focus();
                e.Cancel = true;
            }
            /*
                else if (!Validation.IsNumber(txtFees.Text))
                {
                    errorProvider.SetError(txtFees, "Please enter a valid number for fees.");
                    txtFees.Focus();
                    e.Cancel = true;
                }
            */
            else if (!float.TryParse(txtFees.Text, out float fees) || fees < 0)
            {
                errorProvider.SetError(txtFees, "Please enter a valid non-negative fee.");
                txtFees.Focus();
                e.Cancel = true;
            }
            else
            {
                e.Cancel = false;
                errorProvider.SetError(txtFees, null);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren())
            {
                MessageBox.Show("Please correct the errors before saving.",
                                "Validation Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            if (this._applicationType.ApplicationTypeTitle == this.txtTitle.Text.Trim() && this._applicationType.ApplicationFees == float.Parse(this.txtFees.Text.Trim()))
            {
                MessageBox.Show("No changes detected. Please modify the fields before saving.",
                                "No Changes",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                return;
            }

            this._applicationType.ApplicationTypeTitle = txtTitle.Text.Trim();
            this._applicationType.ApplicationFees = float.Parse(txtFees.Text.Trim());

            if (this._applicationType.Save())
            {
                MessageBox.Show("Application type updated successfully.",
                                "Success",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                OnApplicationTypeUpdated?.Invoke(this, this._applicationTypeID);
            }
            else
            {
                MessageBox.Show("Failed to update application type. Please try again.",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
