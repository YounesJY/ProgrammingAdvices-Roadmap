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
            string username = null;
            string password = null;
            if (Global.GetStoredCredentials(ref username, ref password))
            {
                txtUserName.Text = username;
                txtPassword.Text = password;
                chkRememberMe.Checked = true;
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

            /*
                ===========================================================
                    Very Important Note on Application Lifecycle Management 
                =========================================================== 

                IMPORTANT: Using delegate/event pattern instead of passing LoginFrom reference to MainScreen
                
                Why NOT pass a reference?
                    If MainScreen had a reference to LoginFrom and called _frmLogin.Show() on sign out,
                the application would never close. The LoginForm would keep both forms alive.
                    The Solution: Use FormClosed event with delegate
                When user signs out, MainScreen.Close() is called → FormClosed event fires →
                The subscribed delegate automatically executes this.Show() to bring LoginForm back.
                This way LoginForm controls its own visibility without MainScreen needing a reference to it.
                Result: Clean separation of concerns + proper application lifecycle management 
            */
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
