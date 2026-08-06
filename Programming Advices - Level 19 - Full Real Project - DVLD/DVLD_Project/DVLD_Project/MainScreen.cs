using DVLD_Business;
using DVLD_Project.People;
using DVLD_Project.Users;
using System;
using System.Windows.Forms;

namespace DVLD_Project
{
    public partial class MainScreen : Form
    {
        public MainScreen()
        {
            InitializeComponent();
        }

        private void peopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmListPeople().ShowDialog();
        }
        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmListUsers().ShowDialog();
        }


        private void currentUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmUserInfo(Global.currentLoggedInUser.UserID).ShowDialog();
        }
        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmChangeUserPassword(Global.currentLoggedInUser.UserID).ShowDialog();
        }
        private void singOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Global.currentLoggedInUser = null;
            this.Close();
        }

        private void manageApplicationTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Applications.ApplicationTypes.frmListApplicationTypes().ShowDialog();
        }

        private void manageTestTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Tests.TestTypes.frmListTestTypes().ShowDialog();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Applications.LocalDrivingLicense.frmAddEditLocalDrivingLicenseApplication().ShowDialog();
        }
    }
}
