using DVLD_Business;
using DVLD_Project.People;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project.Users
{
    public partial class frmChangeUserPassword : Form
    {
        public event Action<object, int> OnUserPasswordChanged;

        private int _userID = -1;
        private User _user = null;

        private frmChangeUserPassword()
        {
            InitializeComponent();
            this._userID = -1;
        }
        public frmChangeUserPassword(int userID)
        {
            InitializeComponent();
            _userID = userID;
        }

        private void frmChangeUserPassword_Load(object sender, EventArgs e)
        {
            resetDefualtValues();
            fillFromWithUserData();
        }


        private void fillFromWithUserData()
        {
            _user = User.Find(this._userID);
            if (_user == null)
            {
                MessageBox.Show("User not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            ctrlUserCard.LoadUserData(this._userID);
        }
        private void resetDefualtValues()
        {
            txtCurrentPassword.Text = string.Empty;
            txtNewPassword.Text = string.Empty;
            txtConfirmPassword.Text = string.Empty;
        }

        private void txtCurrentPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCurrentPassword.Text))
            {
                e.Cancel = true;
                errorProvider.SetError(txtCurrentPassword, "Current password is required.");
            }
            else if (txtCurrentPassword.Text != _user.Password)
            {
                e.Cancel = true;
                errorProvider.SetError(txtCurrentPassword, "Current password is incorrect.");
            }
            else
                errorProvider.SetError(txtCurrentPassword, string.Empty);
        }
        private void txtNewPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNewPassword.Text))
            {
                e.Cancel = true;
                errorProvider.SetError(txtNewPassword, "New password is required.");
            }
            else
                errorProvider.SetError(txtNewPassword, string.Empty);
        }
        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtConfirmPassword.Text))
            {
                e.Cancel = true;
                errorProvider.SetError(txtConfirmPassword, "Please confirm the new password.");
            }
            else if (txtNewPassword.Text != txtConfirmPassword.Text)
            {
                e.Cancel = true;
                errorProvider.SetError(txtConfirmPassword, "Passwords do not match.");
            }
            else
                errorProvider.SetError(txtConfirmPassword, string.Empty);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren())
            {
                MessageBox.Show("Please correct the errors before saving.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
                }

            if (_user.ChangePassword(txtNewPassword.Text))
            {
                MessageBox.Show("Password changed successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.resetDefualtValues();
                OnUserPasswordChanged?.Invoke(this, _user.UserID);
                this.Close();
            }
            else
            {
                MessageBox.Show("Failed to change password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
