using DVLD_Project.Licenses.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project.Licenses.InternationalLicenses
{
    public partial class frmShowInternationalLicenseDetails : Form
    {
        private int _InternationalLicenseID;


        public frmShowInternationalLicenseDetails(int internationalLicenseID)
        {
            InitializeComponent();
            _InternationalLicenseID = internationalLicenseID;
        }
        private void frmShowInternationalLicenseDetails_Load(object sender, EventArgs e)
        {
            bool loadedSuccessfully = ctrlDriverInternationalLicenseDetails.LoadDriverLicenseDetails(this._InternationalLicenseID);

            if (!loadedSuccessfully)
                this.Close();
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
