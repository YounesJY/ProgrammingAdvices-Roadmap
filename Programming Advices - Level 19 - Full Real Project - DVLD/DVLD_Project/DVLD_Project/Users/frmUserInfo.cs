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
            ctrlUserCard.LoadUserData(_userID);

            if (ctrlUserCard != null)
                ctrlUserCard.OnUserCardDetailsUpdated += ctrlUserCard_OnUserCardDetailsUpdated;
        }

        private void ctrlUserCard_OnUserCardDetailsUpdated(object sender, int userID)
        {
            OnUserInfoUpdated?.Invoke(this, userID);
        }
    }
}
