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
        public event Action<object, int> OnPersonCardDetailsUpdated
        {
            add { ctrlUserCard.OnUserCardDetailsUpdated += value; }
            remove { ctrlUserCard.OnUserCardDetailsUpdated -= value; }
        }

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
            _user = User.FindByUserID(this._userID);
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
        /*
            ===============================
            ===== VERY IMPORTANT NOTE =====
            ===============================
        */
        /*
            [WHY both    field.Focus()  And e.Cancel = true]
            In the context of the Validating event in Windows Forms, 
            both field.Focus() and e.Cancel = true are used together to ensure proper validation behavior. Here's why both are necessary:
            1. field.Focus(): This method sets the input focus to the specific control (field) that is being validated. 
               It ensures that if validation fails, the user is immediately directed back to the control that needs correction. 
               This improves user experience by guiding them to the exact location where they need to make changes.
            2. e.Cancel = true: This property is part of the CancelEventArgs passed to the Validating event handler.
                Setting e.Cancel to true indicates that the validation has failed and prevents the control from losing focus. 
                It effectively cancels the event, keeping the user on the current control until they provide valid input. 
                Without this, the user could move to another control even if the current input is invalid, which could lead to confusion or errors.
            In summary, field.Focus() directs the user to the control that needs attention, while e.Cancel = true prevents them from leaving that control until they correct the input.
        */
        private void txtCurrentPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCurrentPassword.Text))
            {
                errorProvider.SetError(txtCurrentPassword, "Current password is required.");
                txtCurrentPassword.Focus();
                e.Cancel = true;
            }
            else if (txtCurrentPassword.Text != _user.Password)
            {
                errorProvider.SetError(txtCurrentPassword, "Current password is incorrect.");
                txtCurrentPassword.Focus();
                e.Cancel = true;
            }
            else
                errorProvider.SetError(txtCurrentPassword, string.Empty);
        }
        private void txtNewPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNewPassword.Text))
            {
                errorProvider.SetError(txtNewPassword, "New password is required.");
                txtNewPassword.Focus();
                e.Cancel = true;
            }
            else
                errorProvider.SetError(txtNewPassword, string.Empty);
        }
        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtConfirmPassword.Text))
            {
                errorProvider.SetError(txtConfirmPassword, "Please confirm the new password.");
                txtConfirmPassword.Focus();
                e.Cancel = true;
            }
            else if (txtNewPassword.Text != txtConfirmPassword.Text)
            {
                errorProvider.SetError(txtConfirmPassword, "Passwords do not match.");
                txtConfirmPassword.Focus();
                e.Cancel = true;
            }
            else
                errorProvider.SetError(txtConfirmPassword, string.Empty);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
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
