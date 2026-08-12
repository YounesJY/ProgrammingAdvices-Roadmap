using System;
using System.Windows.Forms;

namespace DVLD_Project.People
{
    public partial class frmPersonDetails : Form
    {
        public event Action<object, int> OnPersonCardDetailsUpdated
        {
            add { this.ctrlPersonCard.OnPersonCardDetailsUpdated += value; }
            remove { this.ctrlPersonCard.OnPersonCardDetailsUpdated -= value; }
        }

        public frmPersonDetails(int PersonID)
        {
            InitializeComponent();
            ctrlPersonCard.LoadPersonDetailsToCard(PersonID);
        }
        public frmPersonDetails(string nationalNumber)
        {
            InitializeComponent();
            ctrlPersonCard.LoadPersonDetailsToCard(nationalNumber);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
