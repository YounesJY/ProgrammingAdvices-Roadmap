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

namespace DVLD_Project.Users.Controls
{
    public partial class ctrlUserCard : UserControl
    {
        public event Action<object, int> OnUserCardDetailsUpdated;
        private User _user;
        public ctrlUserCard()
        {
            InitializeComponent();
        }
        public void LoadUserData(int userID)
        {
            _user = User.FindByUserID(userID);
            if (_user != null)
                fillPersonInfo();
            else
                MessageBox.Show("User not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void fillPersonInfo()
        {
            ctrlPersonCard.loadPersonDetailsToCard(_user.PersonID);
            lblUserID.Text = _user.UserID.ToString();
            lblUserName.Text = _user.UserName;
            lblIsActive.Text = _user.IsActive ? "Active" : "Inactive";

            ctrlPersonCard.OnPersonCardDetailsUpdated += refreshDataOnUpdate;
        }
        private void refreshDataOnUpdate(object sender, int PersonID)
        {
            this.LoadUserData(this._user.UserID);
            OnUserCardDetailsUpdated?.Invoke(this, this._user.UserID);
        }
    }
}
