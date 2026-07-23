using System;
using System.Windows.Forms;

namespace DVLD_Project.People
{
    public partial class frmPersonDetails : Form
    {
        public ctrlPersonCard PersonCard { get => ctrlPersonCard; }

        public frmPersonDetails(int PersonID)
        {
            InitializeComponent();
            ctrlPersonCard.loadPersonDetailsToCard(PersonID);
        }
        public frmPersonDetails(string nationalNumber)
        {
            InitializeComponent();
            ctrlPersonCard.loadPersonDetailsToCard(nationalNumber);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
