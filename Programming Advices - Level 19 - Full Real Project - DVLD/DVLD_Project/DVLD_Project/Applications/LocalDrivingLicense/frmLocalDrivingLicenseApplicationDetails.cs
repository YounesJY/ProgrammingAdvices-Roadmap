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

namespace DVLD_Project.Applications.LocalDrivingLicense
{
    public partial class frmLocalDrivingLicenseApplicationDetails : Form
    {
        private int _applicationID = ValidationConstants.INVALID_ID;

        public event Action<object, int> OnApplicationCardDetailsUpdated
        {
            add { this.ctrlLocalDrivingApplicationDetails.OnApplicationCardDetailsUpdated += value; }
            remove { this.ctrlLocalDrivingApplicationDetails.OnApplicationCardDetailsUpdated -= value; }
        }

        private frmLocalDrivingLicenseApplicationDetails()
        {
            InitializeComponent();
        }
        public frmLocalDrivingLicenseApplicationDetails(int applicationID)
        {
            InitializeComponent();
            this._applicationID = applicationID;
        }
        private void frmLocalDrivingLicenseApplicationDetails_Load(object sender, EventArgs e)
        {
            this.ctrlLocalDrivingApplicationDetails.LoadApplicationDetailsByApplicationID(this._applicationID);
        }
        private void frmLocalDrivingLicenseApplicationDetails_Activated(object sender, EventArgs e)
        {
            this.OnApplicationCardDetailsUpdated += RefreshFormData;
        }
        private void frmLocalDrivingLicenseApplicationDetails_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.OnApplicationCardDetailsUpdated -= RefreshFormData;
        }


        private void RefreshFormData(object obj, int applicationID)
        {
            this.ctrlLocalDrivingApplicationDetails.LoadApplicationDetailsByApplicationID(applicationID);
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
