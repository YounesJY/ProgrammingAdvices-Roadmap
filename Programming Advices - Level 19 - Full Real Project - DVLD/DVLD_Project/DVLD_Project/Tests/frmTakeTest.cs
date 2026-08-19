using DVLD_Business;
using DVLD_Common;
using System;
using System.Windows.Forms;

namespace DVLD_Project.Tests
{
    public partial class frmTakeTest : Form
    {
        public event Action<object, int> OnTestAppointmentTaken;
        public event Action<object, int> OnTestPassed;

        private TestAppointment _TestAppointment
        {
            get { return ctrlSheduledTestDetails.TestAppointment; }
            set { ctrlSheduledTestDetails.TestAppointment = value; }
        }
        private String _TestID
        {
            get { return ctrlSheduledTestDetails.TestID; }
            set { ctrlSheduledTestDetails.TestID = value; }
        }
        private int _TestAppointmentID = ValidationConstants.INVALID_ID;

        public frmTakeTest(int testAppointmentID)
        {
            InitializeComponent();
            this._TestAppointmentID = testAppointmentID;
            this._TestAppointment = TestAppointment.Find(this._TestAppointmentID);
        }
        private void frmTakeTest_Load(object sender, EventArgs e)
        {
            ctrlSheduledTestDetails.LoadTestDetails(this._TestAppointmentID);
            if (this._TestAppointment.IsLocked)
                ShowLockedTest();
        }

        private void ShowLockedTest()
        {
            MessageBox.Show("This test appointment is already locked and cannot be modified.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            ctrlSheduledTestDetails.TestID = this._TestAppointment.TestID.ToString();
            ctrlSheduledTestDetails.Enabled = false;
            lblUserMessage.Visible = true;

            rbPass.Enabled = false;
            rbFail.Enabled = false;
            txtNotes.Enabled = false;
            btnSave.Enabled = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this._TestAppointmentID <= 0)
            {
                MessageBox.Show($"Invalid TestAppointmentID = {this._TestAppointmentID}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (this._TestAppointment == null)
            {
                MessageBox.Show($"No test appointment with testAppointmentID = {_TestAppointment}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            bool isPassedTest = (rbPass.Checked == true);

            Test test = new Test()
            {
                TestAppointmentID = this._TestAppointmentID,
                TestResult = isPassedTest,
                Notes = this.txtNotes.Text,
                CreatedByUserID = Global.currentLoggedInUser.UserID
            };
            this._TestAppointment.IsLocked = true;

            // this will auto set TestAppointment to locked by the SQL QUERY
            if (test.Save())
            {
                MessageBox.Show("Test has been taken and recorded successfully.",
                                "Success",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                OnTestAppointmentTaken?.Invoke(this, test.TestAppointmentID);
                if (test.TestResult == true)
                    OnTestPassed?.Invoke(this, test.TestID);
            }
            else
            {
                MessageBox.Show("Failed to record the test. Please try again.",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }
    }
}
