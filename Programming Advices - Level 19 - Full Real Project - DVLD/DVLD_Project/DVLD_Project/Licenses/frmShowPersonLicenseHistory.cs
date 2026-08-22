using DVLD_Business;
using DVLD_Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project.Licenses
{
    public partial class frmShowPersonLicenseHistory : Form
    {
        public event Action<object, int> OnPersonCardDetailsUpdated
        {
            add { this.ctrlPersonCardWithFilters.OnPersonCardDetailsUpdated += value; }
            remove { this.ctrlPersonCardWithFilters.OnPersonCardDetailsUpdated -= value; }
        }

        private int _LocalDrivingApplicationID = ValidationConstants.INVALID_ID;
        private LocalDrivingLicenseApplication _LocalDrivingLicenseApplication;
        private Driver _Driver = null;


        public frmShowPersonLicenseHistory(int localDrivingApplicationID)
        {
            InitializeComponent();
            this._LocalDrivingApplicationID = localDrivingApplicationID;
        }
        private void frmShowPersonLicenseHistory_Load(object sender, EventArgs e)
        {
            if (this._LocalDrivingApplicationID <= 0)
            {
                MessageBox.Show($"Invalid ApplicationID = {_LocalDrivingApplicationID}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this._LocalDrivingLicenseApplication = LocalDrivingLicenseApplication.FindByLocalDrivingApplicationID(this._LocalDrivingApplicationID);
            if (this._LocalDrivingLicenseApplication == null)
            {
                MessageBox.Show("Application not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this._Driver = Driver.FindByPersonID(_LocalDrivingLicenseApplication.ApplicantPersonID);
            if (this._Driver == null)
            {
                MessageBox.Show("Driver not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            LoadData();
        }


        private void LoadData()
        {
            ctrlPersonCardWithFilters.loadPersonDetailsToCard(this._LocalDrivingLicenseApplication.ApplicantPersonID);
            ctrlDriverLicenses.LoadDriverLicensesByDriverID(this._Driver.DriverID);

            ctrlPersonCardWithFilters.SwitchFilterState();
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
