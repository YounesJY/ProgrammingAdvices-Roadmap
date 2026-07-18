using System;
using System.Windows.Forms;

namespace DVLD_Project.People
{
    public partial class frmPersonDetails : Form
    {
        public frmPersonDetails()
        {
            InitializeComponent();
        }
        public frmPersonDetails(int PersonID)
        {
            InitializeComponent();
            ctrlPersonCard.loadPersonDetailsToCard(PersonID);
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
