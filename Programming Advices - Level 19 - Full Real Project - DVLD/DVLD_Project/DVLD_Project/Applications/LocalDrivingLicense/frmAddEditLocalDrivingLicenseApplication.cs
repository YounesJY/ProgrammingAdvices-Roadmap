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
        private const string DEFAULT_LICENSE_CLASS = "Class 3 - Ordinary Driving License";

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
        }
        private void frmAddEditLocalDrivingLicenseApplication_Load(object sender, EventArgs e)
        {
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
            cbLicenseClass.SelectedIndex = (int)LicenseClass.enLicenseClass.OrdinaryDrivingLicense;
            lblFees.Text = ApplicationType.Find((int)DVLD_Business.ApplicationInfo.enApplicationType.NewDrivingLicense).ApplicationFees.ToString("C");
            lblCreatedByUser.Text = Global.currentLoggedInUser.UserName;
        }



        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (this.ctrlPersonCardWithFilters.SelectedPerson == null)
            {
                MessageBox.Show("Please select a person first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //if (User.IsUserExistForPersonID(ctrlPersonCardWithFilters.SelectedPerson.PersonID))
            //{
            //    MessageBox.Show("User already exists for this person.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}


            btnSave.Enabled = true;
            tpApplicationInformations.Enabled = true;
            tcAddEditLocalDrivingLicense.SelectedTab = this.tpApplicationInformations;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.ctrlPersonCardWithFilters.SelectedPerson == null)
            {
                MessageBox.Show("Please select a person first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (ApplicationInfo.DoesPersonHaveActiveApplication(this.ctrlPersonCardWithFilters.SelectedPerson.PersonID, (int)ApplicationInfo.enApplicationType.NewDrivingLicense))
            {
                MessageBox.Show("The selected person already has an active application for a New Driving License.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (LocalDrivingLicenseApplication.)
            {
                
            }
        }
    }
}
