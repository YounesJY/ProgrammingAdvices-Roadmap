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

namespace DVLD_Project.Applications.LocalDrivingLicense
{
    public partial class frmAddEditLocalDrivingLicenseApplication : Form
    {
        public enum enMode : byte { AddNew = 0, Update = 1 };
        private enMode _Mode = enMode.AddNew;
        private int _ApplicationID = ValidationConstants.INVALID_ID;
        private LocalDrivingLicenseApplication _LocalDrivingLicenseApplication = null;
        private const string DEFAULT_LICENSE_CLASS = "Class 3 - Ordinary Driving Licence";

        public frmAddEditLocalDrivingLicenseApplication()
        {
            InitializeComponent();
            this._Mode = enMode.AddNew;
        }
        public frmAddEditLocalDrivingLicenseApplication(int ApplicationID)
        {
            InitializeComponent();
            this._Mode = enMode.Update;
            this._ApplicationID = ApplicationID;

            ResetFormToDefaultValues();
            if (this._Mode == enMode.Update)
            {
                // i'll implement this later, but for now, let's just show a message box to indicate that the update mode is not yet implemented.
                MessageBox.Show("Update mode is not yet implemented.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void frmAddEditLocalDrivingLicenseApplication_Activated(object sender, EventArgs e)
        {
            ctrlPersonCardWithFilters.FilterFocus();
        }

        private void setFormLabels()
        {
            this.Text = (this._Mode == enMode.AddNew) ? "Add New Local Driving License Application" : "Update Local Driving License Application";
            this.lblTitle.Text = this.Text;
        }
        private void ResetFormToDefaultValues()
        {
            setFormLabels();
            _LocalDrivingLicenseApplication = new LocalDrivingLicenseApplication();

            btnNext.Enabled = true;
            btnSave.Enabled = false;
            tpApplicationInformations.Enabled = false;
            tcAddEditLocalDrivingLicense.SelectedTab = tcAddEditLocalDrivingLicense.TabPages["tpPersonalInformations"];

            lblLocalDrivingLicebseApplicationID.Text = "[???]";
            lblApplicationDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            cbLicenseClass.DataSource = LicenseClass.GetAllLicenseClasses();
            cbLicenseClass.DisplayMember = "ClassName";
            cbLicenseClass.SelectedIndex = LicenseClass.Find(DEFAULT_LICENSE_CLASS).LicenseClassID;
            lblFees.Text = ApplicationType.Find((int)DVLD_Business.Application.enApplicationType.NewDrivingLicense).ApplicationFees.ToString("C");
            lblCreatedByUser.Text = Global.currentLoggedInUser.UserName;
        }



        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
