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
    }
}
