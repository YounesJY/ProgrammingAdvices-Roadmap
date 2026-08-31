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
        private int _LicenseID;


        public frmShowLicenseDetails(int LicenseID)
        {
            InitializeComponent();
            this._LicenseID = LicenseID;
        }
        private void frmShowLicenseDetails_Load(object sender, EventArgs e)
        {
            bool loadedSuccessfully = ctrlDriverLicenseDetails.LoadDriverLicenseDetails(this._LicenseID);

            if (!loadedSuccessfully)
                this.Close();
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
