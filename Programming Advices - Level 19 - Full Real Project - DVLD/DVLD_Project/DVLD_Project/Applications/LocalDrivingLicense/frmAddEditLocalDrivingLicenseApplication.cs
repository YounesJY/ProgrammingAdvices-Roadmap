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
        // [Form own Event] Event to notify when a user is added or updated
        public event Action<object, int> OnNewApplicationCreated;
        public event Action<object, int> OnNewLocalDrivingLicenseApplicationCreated;
        public event Action<object, int> OnApplicationUpdate;

        public event Action<object, int> OnPersonDetailsUpdated
        {
            add { ctrlPersonCardWithFilters.OnPersonCardDetailsUpdated += value; }
            remove { ctrlPersonCardWithFilters.OnPersonCardDetailsUpdated -= value; }
        }

        public enum enMode : byte { AddNew = 0, Update = 1 };
        private enMode _Mode = enMode.AddNew;
        private int _LocalDrivingApplicationID = ValidationConstants.INVALID_ID;
        private LocalDrivingLicenseApplication _LocalDrivingLicenseApplication = null;


        public frmAddEditLocalDrivingLicenseApplication()
        {
            InitializeComponent();
            this._Mode = enMode.AddNew;
        }
        public frmAddEditLocalDrivingLicenseApplication(int LocalDrivingApplicationID)
        {
            InitializeComponent();
            this._Mode = enMode.Update;
            this._LocalDrivingApplicationID = LocalDrivingApplicationID;
        }
        private void frmAddEditLocalDrivingLicenseApplication_Load(object sender, EventArgs e)
        {
            OnPersonDetailsUpdated += HandlePersonDetailsUpdated;
            ResetFormToDefaultValues();

            if (this._Mode == enMode.Update)
                FillFormWithApplicationDetails();
        }
        private void frmAddEditLocalDrivingLicenseApplication_Activated(object sender, EventArgs e)
        {
            ctrlPersonCardWithFilters.FilterFocus();
        }
        private void frmAddEditLocalDrivingLicenseApplication_FormClosing(object sender, FormClosingEventArgs e)
        {
            OnPersonDetailsUpdated -= HandlePersonDetailsUpdated;
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

            lblApplicationID.Text = "[???]";
            lblApplicationDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            cbLicenseClass.DataSource = LicenseClass.GetAllLicenseClasses();
            cbLicenseClass.DisplayMember = "ClassName";
            cbLicenseClass.SelectedIndex = (int)LicenseClass.enLicenseClass.OrdinaryDrivingLicense;
            lblFees.Text = ApplicationType.Find((int)ApplicationInfo.enApplicationType.NewDrivingLicense).ApplicationFees.ToString("C");
            lblCreatedByUser.Text = Global.currentLoggedInUser.UserName;
        }
        private void FillFormWithApplicationDetails()
        {
            _LocalDrivingLicenseApplication = LocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(this._LocalDrivingApplicationID);

            if (_LocalDrivingLicenseApplication == null)
            {
                MessageBox.Show("Application not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            btnNext.Enabled = true;
            btnSave.Enabled = true;
            tpApplicationInformations.Enabled = true;
            tcAddEditLocalDrivingLicense.SelectedTab = tcAddEditLocalDrivingLicense.TabPages["tpApplicationInformations"];
            ctrlPersonCardWithFilters.loadPersonDetailsToCard(_LocalDrivingLicenseApplication.ApplicantPersonID);


            lblApplicationID.Text = _LocalDrivingLicenseApplication.ApplicationID.ToString();
            lblApplicationDate.Text = _LocalDrivingLicenseApplication.ApplicationDate.ToString("dd/MM/yyyy");
            cbLicenseClass.DataSource = LicenseClass.GetAllLicenseClasses();
            cbLicenseClass.DisplayMember = "ClassName";
            cbLicenseClass.SelectedIndex = _LocalDrivingLicenseApplication.LicenseClassID - 1;
            lblFees.Text = _LocalDrivingLicenseApplication.PaidFees.ToString("C");
            lblCreatedByUser.Text = _LocalDrivingLicenseApplication.CreatedByUserInfo.UserName;
        }
        private void HandlePersonDetailsUpdated(object arg1, int arg2)
        {
            if (this._Mode == enMode.Update && this._LocalDrivingLicenseApplication != null)
                FillFormWithApplicationDetails();
        }


        private void btnNext_Click(object sender, EventArgs e)
        {
            if (this.ctrlPersonCardWithFilters.SelectedPerson == null)
            {
                MessageBox.Show("Please select a person first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


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

            if (this._Mode == enMode.AddNew)
            {
                int activeApplicationID = ApplicationInfo.GetActiveApplicationID(this.ctrlPersonCardWithFilters.SelectedPerson.PersonID, ApplicationInfo.enApplicationType.NewDrivingLicense);
                if (activeApplicationID != ValidationConstants.INVALID_ID)
                {
                    MessageBox.Show($"The selected person already has an active application for a [New Driving License] with ID [{activeApplicationID}].", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int activeLicenseID = LicenseInfo.GetActiveLicenseIDByPersonID(this.ctrlPersonCardWithFilters.SelectedPerson.PersonID, LicenseClass.Find(cbLicenseClass.Text).LicenseClassID);
                if (activeLicenseID != ValidationConstants.INVALID_ID)
                {
                    MessageBox.Show($"The selected person already has an active license for a [{cbLicenseClass.Text}] with ID [{activeLicenseID}].", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                LocalDrivingLicenseApplication localDrivingLicenseApplication = new LocalDrivingLicenseApplication
                {
                    // setting ApplicationInfo properties
                    ApplicantPersonID = this.ctrlPersonCardWithFilters.SelectedPerson.PersonID,
                    ApplicationDate = DateTime.Now,
                    ApplicationTypeID = (int)ApplicationInfo.enApplicationType.NewDrivingLicense,
                    LastStatusDate = DateTime.Now,
                    PaidFees = float.Parse(lblFees.Text, System.Globalization.NumberStyles.Currency),
                    CreatedByUserID = Global.currentLoggedInUser.UserID,

                    // setting LocalDrivingLicenseApplication properties
                    LicenseClassID = LicenseClass.Find(cbLicenseClass.Text).LicenseClassID
                };
                /* 
                    [Class initializer] used instead of constructor to set properties directly
                WHY ? Because the constructor of LocalDrivingLicenseApplication is private, so we can't use it directly. Instead, we can use the class initializer to set the properties directly.
                    It's more readable and maintainable than using a normal setter method or constructor with parameters, especially when there are many properties to set.
                It also allows us to set only the properties we want to set, without having to provide values for all properties in a constructor.
                */

                if (localDrivingLicenseApplication.Save())
                {
                    MessageBox.Show("Local Driving License Application saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblApplicationID.Text = localDrivingLicenseApplication.ApplicationID.ToString();

                    OnNewApplicationCreated?.Invoke(this, localDrivingLicenseApplication.ApplicationID);
                    OnNewLocalDrivingLicenseApplicationCreated?.Invoke(this, localDrivingLicenseApplication.LocalDrivingLicenseApplicationID);
                    this._Mode = enMode.Update;
                }
                else
                    MessageBox.Show("Failed to save Local Driving License Application.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                /*
                    Currently, the update mode is tend only to update the category of the requested license, and not the person associated with the application
                changing the applicant person itself seems to be compicated/will compilcate the logic here,
                it's just better to delete the application and re create it in this case

                I'll try to find a better solution if i can
                */


                if (this._LocalDrivingLicenseApplication.LicenseClassInfo.ClassName.Equals(cbLicenseClass.SelectedText))
                {
                    MessageBox.Show(
                        "No changes detected.",
                        "Information",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    return;
                }

                this._LocalDrivingLicenseApplication.LicenseClassID = LicenseClass.Find(cbLicenseClass.Text).LicenseClassID;
                if (this._LocalDrivingLicenseApplication.Save())
                {
                    MessageBox.Show("Local Driving License Application updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    OnApplicationUpdate?.Invoke(this, this._LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID);
                }
            }

        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
