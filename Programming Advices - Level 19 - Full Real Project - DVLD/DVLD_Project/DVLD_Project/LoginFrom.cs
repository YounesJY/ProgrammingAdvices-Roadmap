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

namespace DVLD_Project
{
    public partial class LoginFrom : Form
    {
        public LoginFrom()
        {
            InitializeComponent();
        }
        private void LoginFrom_Load(object sender, EventArgs e)
        {
            var (username, password) = Global.GetStoredCredentials();
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                txtUserName.Text = username;
                txtPassword.Text = password;
            }
        }
        private void LoginFrom_Activated(object sender, EventArgs e)
        {
            this.txtUserName.Focus();
        }


        private void btnLogin_Click(object sender, EventArgs e)
        {
            User user = User.FindByUsernameAndPassword(txtUserName.Text, txtPassword.Text);
            if (user == null)
            {
                MessageBox.Show("Invalid username or password!", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (user != null && !user.IsActive)
            {
                MessageBox.Show("Your account is inactive. Please contact the administrator.", "Account Inactive", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            Global.currentLoggedInUser = user;
            if (chkRememberMe.Checked)
                Global.RememberLoggedInUser(txtUserName.Text, txtPassword.Text);
            else
                Global.ClearStoredCredentials();

            MainScreen mainScreen = new MainScreen();
            mainScreen.FormClosed += (s, args) => this.Show();

            this.Hide();
            mainScreen.Show();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
