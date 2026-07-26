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
        enum enFormMode { Add, Update }

        private enFormMode formMode;
        public frmAddNewUpdateUser()
        {
            InitializeComponent();
            formMode = enFormMode.Add;
        }

        public frmAddNewUpdateUser(int userID)
        {
            InitializeComponent();
            formMode = enFormMode.Update;
            LoadUserData(userID);
        }

        private void LoadUserData(int userID)
        {
            User user = User.Find(userID);
            if (user != null)
                ctrlPersonCardWithFilters.loadPersonDetailsToCard(user.PersonID);
            else
            {
                MessageBox.Show("User not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }
    }
}
