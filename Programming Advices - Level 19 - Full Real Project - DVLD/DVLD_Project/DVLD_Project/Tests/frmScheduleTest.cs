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
using static DVLD_Business.TestType;

namespace DVLD_Project.Tests
{
    public partial class frmScheduleTest : Form
    {
        private int _LocalDrivingApplicationID = ValidationConstants.INVALID_ID;
        private TestType.enTestType _TestType = TestType.enTestType.VisionTest;

        public frmScheduleTest(int localDrivingApplicationID, TestType.enTestType testType)
        {
            InitializeComponent();
            this._LocalDrivingApplicationID = localDrivingApplicationID;
            this._TestType = testType;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmScheduleTest_Load(object sender, EventArgs e)
        {
            ctrlSheduleTest.LoadTestDetails(this._LocalDrivingApplicationID, this._TestType);
        }
    }
}
