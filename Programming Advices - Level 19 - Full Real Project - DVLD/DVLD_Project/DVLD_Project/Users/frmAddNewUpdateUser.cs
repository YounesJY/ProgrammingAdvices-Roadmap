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

namespace DVLD_Project.Users
{

    public partial class frmAddNewUpdateUser : Form
    {   
        public event Action<object, int> OnUserAddedOrUpdated;

        public frmAddNewUpdateUser()
        {
            InitializeComponent();
        }

        public frmAddNewUpdateUser(int userID)
        {
            InitializeComponent();
            LoadUserData(userID);
        }

        

        private void LoadUserData(int userID)
        {
            // Load user data from the database based on the provided userID
            // and populate the form fields accordingly.
            // Example:
            User user = User.Find(userID);
            if (user != null)
            {
             ctrlPersonCardWithFilters = new People.ctrlPersonCardWithFilters(user.PersonID);
            }
            else
            {
                MessageBox.Show("User not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

    }
}
