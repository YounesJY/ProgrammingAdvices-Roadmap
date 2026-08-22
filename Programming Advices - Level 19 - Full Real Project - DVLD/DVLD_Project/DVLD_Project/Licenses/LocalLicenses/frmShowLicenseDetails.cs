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

namespace DVLD_Project.Licenses.LocalLicenses
{
    public partial class frmShowLicenseDetails : Form
    {
        private int _LocalDrivingApplicationID;


        public frmShowLicenseDetails(int localDrivingApplicationID)
        {
            InitializeComponent();
            this._LocalDrivingApplicationID = localDrivingApplicationID;
        }
        private void frmShowLicenseDetails_Load(object sender, EventArgs e)
        {
            ctrlDriverLicenseDetails.LoadDriverLicenseDetails(this._LocalDrivingApplicationID);
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
