using DVLD_Business;
using System;
using System.ComponentModel;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DVLD_Project.Users
{

    public partial class frmAddNewUpdateUser : Form
    {
        public event Action<object, int> OnUserAddedOrUpdated;
        enum enFormMode { AddNew, Update }
        private enFormMode formMode;
        private User _user;

        public frmAddNewUpdateUser()
        {
            InitializeComponent();
            formMode = enFormMode.AddNew;
        }
        public frmAddNewUpdateUser(int userID)
        {
            InitializeComponent();
            formMode = enFormMode.Update;
            LoadUserData(userID);
        }

        private void frmAddNewUpdateUser_Load(object sender, EventArgs e)
        {
            if (formMode == enFormMode.AddNew)
            {
                btnNext.Enabled = true;
                btnSave.Enabled = false;
                tpUserLoginInfo.Enabled = false;
                tcAddUpdateUser.SelectedTab = tcAddUpdateUser.TabPages["tpPersonalInformations"];
            }
            if (formMode == enFormMode.Update)
            {
                txtUserName.Text = _user.UserName;
                txtPassword.Text = _user.Password;
                txtConfirmPassword.Text = _user.Password;
                chkIsActive.Checked = _user.IsActive;

                btnNext.Enabled = false;
                btnSave.Enabled = true;
                tpUserLoginInfo.Enabled = true;
                tcAddUpdateUser.SelectedTab = tcAddUpdateUser.TabPages["tpPersonalInformations"];
            }
        }
        private void LoadUserData(int userID)
        {
            _user = User.Find(userID);
            if (_user != null)
                ctrlPersonCardWithFilters.loadPersonDetailsToCard(_user.PersonID);
            else
            {
                MessageBox.Show("User not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }


        private void txtUserName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUserName.Text))
            {
                errorProvider.SetError(txtUserName, "Username is required.");
                txtUserName.Focus();
            }
            else if (formMode == enFormMode.AddNew && User.IsUserExistForPersonID(ctrlPersonCardWithFilters.SelectedPerson.PersonID))
            {
                errorProvider.SetError(txtUserName, "Username already exists.");
                txtUserName.Focus();
            }
            else if (formMode == enFormMode.Update && User.IsUserExistForPersonID(ctrlPersonCardWithFilters.SelectedPerson.PersonID) && txtUserName.Text != _user.UserName)
            {
                errorProvider.SetError(txtUserName, "Username already exists.");
                txtUserName.Focus();
            }
            else
            {
                errorProvider.SetError(txtUserName, string.Empty);
            }
        }
        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                errorProvider.SetError(txtPassword, "Password is required.");
                txtPassword.Focus();
            }
            else
            {
                errorProvider.SetError(txtPassword, string.Empty);
            }
        }
        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                errorProvider.SetError(txtConfirmPassword, "Passwords do not match.");
                txtConfirmPassword.Focus();
            }
            else
            {
                errorProvider.SetError(txtConfirmPassword, string.Empty);
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (this.ctrlPersonCardWithFilters.SelectedPerson == null)
            {
                MessageBox.Show("Please select a person first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (User.IsUserExistForPersonID(ctrlPersonCardWithFilters.SelectedPerson.PersonID))
            {
                MessageBox.Show("User already exists for this person.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            btnSave.Enabled = true;
            tpUserLoginInfo.Enabled = true;
            tcAddUpdateUser.SelectedTab = tcAddUpdateUser.TabPages["tpUserLoginInfo"];
            this.tcAddUpdateUser.SelectedTab = this.tpUserLoginInfo;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.ctrlPersonCardWithFilters.SelectedPerson == null)
            {
                MessageBox.Show("Please select a person first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the error", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this._user = User.create(
                ctrlPersonCardWithFilters.SelectedPerson.PersonID,
                txtUserName.Text,
                txtPassword.Text,
                chkIsActive.Checked
            );

            if (this._user.Save())
            {
                MessageBox.Show("User saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                OnUserAddedOrUpdated?.Invoke(this, this._user.UserID);
                this.Close();
            }
            else
            {
                MessageBox.Show("Failed to save user.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
