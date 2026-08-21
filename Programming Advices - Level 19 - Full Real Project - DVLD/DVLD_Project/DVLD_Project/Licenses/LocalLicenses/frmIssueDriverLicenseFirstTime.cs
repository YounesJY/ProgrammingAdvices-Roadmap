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

namespace DVLD_Project.Licenses.LocalLicenses
{
    public partial class frmIssueDriverLicenseFirstTime : Form
    {
        public event Action<object, int> OnApplicationCardDetailsUpdated
        {
            add { this.ctrlLocalDrivingApplicationDetails.OnApplicationCardDetailsUpdated += value; }
            remove { this.ctrlLocalDrivingApplicationDetails.OnApplicationCardDetailsUpdated -= value; }
        }

        private int _LocalDrivingApplicationID = ValidationConstants.INVALID_ID;
        private TestType.enTestType _TestType = TestType.enTestType.VisionTest;

        public frmIssueDriverLicenseFirstTime(int localDrivingApplicationID, TestType.enTestType testType)
        {
            InitializeComponent();
            this._LocalDrivingApplicationID = localDrivingApplicationID;
            this._TestType = testType;
        }
        private void frmIssueDriverLicenseFirstTime_Load(object sender, EventArgs e)
        {
            this.OnApplicationCardDetailsUpdated += RefreshApplicationDetailsOnUpdate;
            this.ctrlLocalDrivingApplicationDetails.LoadApplicationDetailsByLocalDrivingApplicationID(this._LocalDrivingApplicationID);
        }
        private void frmIssueDriverLicenseFirstTime_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.OnApplicationCardDetailsUpdated -= RefreshApplicationDetailsOnUpdate;
        }


        private void RefreshApplicationDetailsOnUpdate(object sender, int applicationID)
        {
            MessageBox.Show("Applications data has been updated and data refreshed successfully.",
                "Information",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );

            this.ctrlLocalDrivingApplicationDetails.LoadApplicationDetailsByApplicationID(applicationID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
