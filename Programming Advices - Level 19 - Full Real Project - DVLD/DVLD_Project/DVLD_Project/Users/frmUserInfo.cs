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
        // [Inner Event] Event to notify when person card details are updated
        public event Action<object, int> OnPersonCardDetailsUpdated
        {
            add { ctrlUserCard.OnUserCardDetailsUpdated += value; }
            remove { ctrlUserCard.OnUserCardDetailsUpdated -= value; }
        }
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
            ctrlUserCard.loadUserDataToCard(_userID);
        }
    }
}
