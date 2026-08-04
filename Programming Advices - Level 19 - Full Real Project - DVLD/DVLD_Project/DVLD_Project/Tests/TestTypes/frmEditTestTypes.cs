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

namespace DVLD_Project.Tests.TestTypes
{
    public partial class frmEditTestTypes : Form
    {
        public Action<object, int> OnTestTypeUpdated;

        private TestType.enTestType _testTypeID;
        private TestType _testType;

        public frmEditTestTypes(TestType.enTestType testType)
        {
            InitializeComponent();
            this._testTypeID = testType;
        }
        private void frmEditTestTypes_Load(object sender, EventArgs e)
        {
            this._testType = TestType.Find(this._testTypeID);
            if (_testType == null)
            {
                MessageBox.Show("Test type not found.",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                this.Close();
                return;
            }

            fillFormWithTestTypeDetails();
        }
        private void fillFormWithTestTypeDetails()
        {
            lblTestTypeID.Text = this._testType.TestTypeID.ToString();
            txtTitle.Text = this._testType.TestTypeTitle;
            txtDescription.Text = this._testType.TestTypeDescription;
            txtFees.Text = this._testType.TestTypeFees.ToString("0.00");
        }

        private void txtTitle_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                errorProvider.SetError(txtTitle, "Title is required.");
                txtTitle.Focus();
                e.Cancel = true;
            }
            else
            {
                e.Cancel = false;
                errorProvider.SetError(txtTitle, string.Empty);
            }
        }
        private void txtDescription_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                errorProvider.SetError(txtDescription, "Description is required.");
                txtDescription.Focus();
                e.Cancel = true;
            }
            else
            {
                e.Cancel = false;
                errorProvider.SetError(txtDescription, string.Empty);
            }
        }
        private void txtFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFees.Text))
            {
                errorProvider.SetError(txtFees, "Fees are required.");
                txtFees.Focus();
                e.Cancel = true;
            }
            /*
                else if(!Validation.IsNumber(txtFees.Text))
                {
                    errorProvider.SetError(txtFees, "Please enter a valid number.");
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
                errorProvider.SetError(txtFees, string.Empty);
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

            if (this._testType.TestTypeTitle.Equals(txtTitle.Text.Trim()) && this._testType.TestTypeDescription.Equals(txtDescription.Text.Trim()) && this._testType.TestTypeFees == float.Parse(txtFees.Text.Trim()))
            {
                MessageBox.Show("No changes detected.",
                                "Information",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                return;
            }


            this._testType.TestTypeTitle = txtTitle.Text.Trim();
            this._testType.TestTypeDescription = txtDescription.Text.Trim();
            this._testType.TestTypeFees = float.Parse(txtFees.Text.Trim());

            if (this._testType.Save())
            {
                MessageBox.Show("Test type updated successfully.",
                                "Success",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                OnTestTypeUpdated?.Invoke(this, (int)this._testType.TestTypeID);
            }
            else
            {
                MessageBox.Show("Failed to update test type.",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
