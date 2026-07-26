using DVLD_Business;
using DVLD_Project.Users.Controls;
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
    public partial class frmUserInfo : Form
    {
        public event Action<object, int> OnUserInfoUpdated;
        private int _userID;
        private frmUserInfo()
        {
            InitializeComponent();
        }

        public frmUserInfo(int userID)
        {
            InitializeComponent();
            this._userID = userID;
        }
        private void frmUserInfo_Load(object sender, EventArgs e)
        {
            userCard.LoadUserData(_userID);

            if (userCard != null)
                userCard.OnUserCardDetailsUpdated += userCard_OnUserCardDetailsUpdated;
        }

        private void userCard_OnUserCardDetailsUpdated(object sender, int userID)
        {
            OnUserInfoUpdated?.Invoke(this, userID);
        }
    }
}
