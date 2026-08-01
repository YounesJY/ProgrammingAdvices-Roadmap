using DVLD_Business;
using DVLD_Project.People;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace DVLD_Project.Users
{
    public partial class frmAddNewUpdateUser : Form
    {
        // [Form own Event] Event to notify when a user is added or updated
        public event Action<object, int> OnUserAddedOrUpdated;
        // [Inner Event] Event to notify when person details are updated
        public event Action<object, int> OnPersonDetailsUpdated
        {
            add { ctrlPersonCardWithFilters.OnPersonCardDetailsUpdated += value; }
            remove { ctrlPersonCardWithFilters.OnPersonCardDetailsUpdated -= value; }
        }
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
            this._mode = enMode.Update;
            this._userID = userID;
        }

        private void frmAddNewUpdateUser_Load(object sender, EventArgs e)
        {
            resetDefualtValues();
            // Subscribe to person card details updated event
            OnPersonDetailsUpdated += HandlePersonDetailsUpdated;
            if (this._mode == enMode.Update)
                fillFromWithUserData(this._userID);
        }


        private void frmAddNewUpdateUser_Activated(object sender, EventArgs e)
        {
            ctrlPersonCardWithFilters.FilterFocus();
        }
        private void frmAddNewUpdateUser_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Cleanup: Unsubscribe from event when form closes to prevent memory leaks
            OnPersonDetailsUpdated -= HandlePersonDetailsUpdated;
        }


        private void HandlePersonDetailsUpdated(object arg1, int arg2)
        {
            if (this._mode == enMode.Update && this._user != null)
                fillFromWithUserData(this._user.UserID);
        }
        private void fillFromWithUserData(int userID)
        {
            _user = User.FindByUserID(userID);
            if (_user == null)
            {
                MessageBox.Show("User not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            ctrlPersonCardWithFilters.loadPersonDetailsToCard(_user.PersonID);
            /*
                This will prevent rebinding a user to a different Person*
                If you want to allow changing the associated person, you can remove this line and handle it accordingly (all cases are handled in the btnSave())
            */
            ctrlPersonCardWithFilters.FilterGroupBox.Enabled = false;

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
            this.Text = (this._mode == enMode.AddNew) ? "Add New User" : "Update User";
            this.lblTitle.Text = this.Text;
        }
        private void resetDefualtValues()
        {
            setFormLabels();
            _user = new User();

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
            else if (_mode == enMode.AddNew && User.IsUserExist(txtUserName.Text))
            {
                errorProvider.SetError(txtUserName, "Username already exists.");
                txtUserName.Focus();
                e.Cancel = true;
            }
            else if (_mode == enMode.Update && User.IsUserExist(txtUserName.Text) && this._user.UserName != txtUserName.Text)
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
                txtPassword.Focus();
                e.Cancel = true;
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

            if (this._mode == enMode.AddNew && User.IsUserExistForPersonID(ctrlPersonCardWithFilters.SelectedPerson.PersonID))
            {
                MessageBox.Show("User already exists for this person.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            /*
                this case is where we are updating an existing user and trying to change the associated person 
                the line  ctrlPersonCardWithFilters.FilterGroupBox.Enabled = false; in the fillFromWithUserData() method prevents this,
                but if you want to allow changing the associated person, you can remove that line and handle it here
             */

            if (this._mode == enMode.Update && this._user.PersonID != ctrlPersonCardWithFilters.SelectedPerson.PersonID && User.IsUserExistForPersonID(ctrlPersonCardWithFilters.SelectedPerson.PersonID))
            {
                MessageBox.Show("User already exists for this person.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                this._mode = enMode.Update;
                setFormLabels();
                fillFromWithUserData(this._user.UserID);

                OnUserAddedOrUpdated?.Invoke(this, this._user.UserID);
            }
            else
                MessageBox.Show("Failed to save user.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
