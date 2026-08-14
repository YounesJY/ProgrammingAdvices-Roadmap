using DVLD_Business;
using DVLD_Common;
using System;
using System.Windows.Forms;
using static DVLD_Project.Applications.LocalDrivingLicense.frmListLocalDrivingLicenseApplications;

namespace DVLD_Project.Tests
{
    public partial class frmListTestAppointments : Form
    {

        private int _LocalDrivingApplicationID = ValidationConstants.INVALID_ID;
        private TestType.enTestType _TestType = TestType.enTestType.VisionTest;

        public frmListTestAppointments(int localDrivingApplicationID, TestType.enTestType testType)
        {
            InitializeComponent();
            this._LocalDrivingApplicationID = localDrivingApplicationID;
            this._TestType = testType;
        }

        private void frmListTestAppointments_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void setFormLabels()
        {
            switch (this._TestType)
            {
                case TestType.enTestType.VisionTest:
                    this.Text = "Vision Test Appointments";
                    break;
                case TestType.enTestType.WrittenTest:
                    this.Text = "Written Test Appointments";
                    break;
                case TestType.enTestType.StreetTest:
                    this.Text = "Street Test Appointments";
                    break;
            }

            this.lblTitle.Text = this.Text;
        }
        private void LoadTestAppointmentsData()
        {
            dgvTestAppointments.DataSource = TestAppointment.GetApplicationTestAppointmentsPerTestType(this._LocalDrivingApplicationID, this._TestType);
            dgvTestAppointments.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTestAppointments.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            lblNumberOfRecords.Text = dgvTestAppointments.RowCount.ToString();
        }
        private void LoadData()
        {
            setFormLabels();
            this.ctrlLocalDrivingApplicationDetails.LoadApplicationDetailsByLocalDrivingApplicationID(this._LocalDrivingApplicationID);
            LoadTestAppointmentsData();
        }



    }
}
