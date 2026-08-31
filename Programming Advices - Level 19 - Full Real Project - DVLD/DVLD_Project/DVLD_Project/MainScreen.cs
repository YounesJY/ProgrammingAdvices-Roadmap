using DVLD_Business;
using DVLD_Project.Applications.InternationalDrivingLicense;
using DVLD_Project.Applications.LocalDrivingLicense;
using DVLD_Project.Applications.RenewLocalDrivingLicense;
using DVLD_Project.Applications.ReplaceLostOrDamagedLicense;
using DVLD_Project.Drivers;
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

        private void localDrivingLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmListLocalDrivingLicenseApplications().ShowDialog();
        }
        private void localLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmAddEditLocalDrivingLicenseApplication().ShowDialog();
        }

        private void internationalLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmNewInternationalLicenseApplication().ShowDialog();
        }
        private void internationalLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmListInternationalDrivingLicenseApplications().ShowDialog();
        }

        private void manageApplicationTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Applications.ApplicationTypes.frmListApplicationTypes().ShowDialog();
        }
        private void driversToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmListDrivers().ShowDialog();
        }
        private void retakeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmListLocalDrivingLicenseApplications().ShowDialog();
        }
        private void renewDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmRenewLocalDrivingLicenseApplication().ShowDialog();
        }
        private void replacementForLostOrDamagedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmReplaceLostOrDamagedLicenseApplication().ShowDialog();
        }

        private void manageTestTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Tests.TestTypes.frmListTestTypes().ShowDialog();
        }
    }
}
