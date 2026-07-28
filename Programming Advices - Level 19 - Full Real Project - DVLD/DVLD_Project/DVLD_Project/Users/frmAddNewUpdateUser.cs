using DVLD_Business;
using DVLD_Project.People;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace DVLD_Project.Users
{
    public partial class frmAddNewUpdateUser : Form
    {
        public event Action<object, int> OnUserAddedOrUpdated;
        enum enMode { AddNew, Update }
        private enMode _mode;
        private User _user;
        private int _userID;

        public frmAddNewUpdateUser()
        {
            InitializeComponent();
            _mode = enMode.AddNew;
        }
        public frmAddNewUpdateUser(int userID)
        {
            InitializeComponent();
            _mode = enMode.Update;
            _userID = userID;
        }
        private void frmAddNewUpdateUser_Load(object sender, EventArgs e)
        {
            ResetDefualtValues();
            if (this._mode == enMode.Update)
                LoadUserData(this._userID);
        }

        private void LoadUserData(int userID)
        {
            _user = User.Find(userID);
            if (_user == null)
            {
                MessageBox.Show("User not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            ctrlPersonCardWithFilters.loadPersonDetailsToCard(_user.PersonID);

            btnNext.Enabled = false;
            btnSave.Enabled = true;
            tpUserLoginInfo.Enabled = true;
            tcAddUpdateUser.SelectedTab = tcAddUpdateUser.TabPages["tpPersonalInformations"];
            
            lblUserID.Text = _user.UserID.ToString();
            txtUserName.Text = _user.UserName;
            txtPassword.Text = _user.Password;
            txtConfirmPassword.Text = _user.Password;
            chkIsActive.Checked = _user.IsActive;
        }
        private void setFormLabels()
        {
            this.Text = this.lblTitle.Text = (this._mode == enMode.AddNew) ? "Add New User" : "Update User";
        }
        private void ResetDefualtValues()
        {
            setFormLabels();

            _user = new User();
            ctrlPersonCardWithFilters.FilterFocus();

            btnNext.Enabled = true;
            btnSave.Enabled = false;
            tpUserLoginInfo.Enabled = false;
            tcAddUpdateUser.SelectedTab = tcAddUpdateUser.TabPages["tpPersonalInformations"];

            txtUserName.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtConfirmPassword.Text = string.Empty;
            chkIsActive.Checked = true;
        }


        private void txtUserName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUserName.Text))
            {
                errorProvider.SetError(txtUserName, "Username is required.");
                txtUserName.Focus();
                e.Cancel = true;
            }
            else if (_mode == enMode.AddNew && User.IsUserExistForPersonID(ctrlPersonCardWithFilters.SelectedPerson.PersonID))
            {
                errorProvider.SetError(txtUserName, "Username already exists.");
                txtUserName.Focus();
                e.Cancel = true;
            }
            else if (_mode == enMode.Update && User.IsUserExistForPersonID(ctrlPersonCardWithFilters.SelectedPerson.PersonID) && txtUserName.Text != _user.UserName)
            {
                errorProvider.SetError(txtUserName, "Username already exists.");
                txtUserName.Focus();
                e.Cancel = true;
            }
            else
                errorProvider.SetError(txtUserName, string.Empty);
        }
        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                errorProvider.SetError(txtPassword, "Password is required.");
                e.Cancel = true;
                txtPassword.Focus();
            }
            else
                errorProvider.SetError(txtPassword, string.Empty);
        }
        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                errorProvider.SetError(txtConfirmPassword, "Passwords do not match.");
                txtConfirmPassword.Focus();
                e.Cancel = true;
            }
            else
                errorProvider.SetError(txtConfirmPassword, string.Empty);
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
