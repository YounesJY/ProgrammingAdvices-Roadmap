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
        public event Action<object, int> OnTestAppointmentAddUpdate
        {
            add { this.ctrlSheduleTest.OnTestAppointmentAddUpdate += value; }
            remove { this.ctrlSheduleTest.OnTestAppointmentAddUpdate -= value; }
        }
        public enum enMode { AddNew, Update };


        private int _LocalDrivingApplicationID = ValidationConstants.INVALID_ID;
        private TestType.enTestType _TestType = TestType.enTestType.VisionTest;
        private int _TestAppointmentID = ValidationConstants.INVALID_ID;
        private enMode _Mode = enMode.AddNew;

        public frmScheduleTest(int localDrivingApplicationID, TestType.enTestType testType)
        {
            InitializeComponent();
            this._LocalDrivingApplicationID = localDrivingApplicationID;
            this._TestType = testType;
            this._Mode = enMode.AddNew;
        }
        public frmScheduleTest(int testAppointmentID)
        {
            InitializeComponent();
            this._TestAppointmentID = testAppointmentID;
            this._Mode = enMode.Update;
        }
        private void frmScheduleTest_Load(object sender, EventArgs e)
        {
            if (this._Mode == enMode.AddNew)
                ctrlSheduleTest.LoadTestDetails(this._LocalDrivingApplicationID, this._TestType);
            else
                ctrlSheduleTest.LoadTestDetails(this._TestAppointmentID);
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
