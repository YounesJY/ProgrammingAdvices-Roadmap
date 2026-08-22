using DVLD.Classes;
using DVLD_Business;
using DVLD_Common;
using DVLD_Project.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project.Licenses.Controls
{
    public partial class ctrlDriverLicenseDetails : UserControl
    {
        private LocalDrivingLicenseApplication _LocalDrivingLicenseApplication;
        private int _LicenseID = ValidationConstants.INVALID_ID;
        private LicenseInfo _LicenseDetails = null;

        public ctrlDriverLicenseDetails()
        {
            InitializeComponent();
        }
        public void LoadDriverLicenseDetails(int localDrivingApplicationID)
        {
            if (localDrivingApplicationID <= 0)
            {
                MessageBox.Show($"Invalid ApplicationID = {localDrivingApplicationID}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this._LocalDrivingLicenseApplication = LocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(localDrivingApplicationID);
            if (this._LocalDrivingLicenseApplication == null)
            {
                MessageBox.Show("Application not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this._LicenseID = LicenseInfo.GetActiveLicenseIDByPersonID(this._LocalDrivingLicenseApplication.ApplicantPersonID, this._LocalDrivingLicenseApplication.LicenseClassID);
            if (this._LicenseID <= 0)
            {
                MessageBox.Show($"No license with LicenseID = {this._LicenseID}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this._LicenseDetails = LicenseInfo.Find(this._LicenseID);
            if (this._LicenseDetails == null)
            {
                MessageBox.Show("Failed to load the license. Please try again.",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }


            FillDriverLicenseCardDetails();
        }

        private void LoadPersonImage()
        {
            pbPersonImage.Image = (_LicenseDetails.DriverInfo.PersonInfo.Gender == Person.enGender.Male) ? Resources.Male_512 : Resources.Female_512;
            string ImagePath = _LicenseDetails.DriverInfo.PersonInfo.ProfilePhotoPath;

            if (!String.IsNullOrEmpty(ImagePath))
                if (File.Exists(ImagePath))
                    pbPersonImage.Load(ImagePath);
                else
                    MessageBox.Show($"Could not find this image: = {ImagePath}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
        private void FillDriverLicenseCardDetails()
        {
            lblLicenseID.Text = _LicenseDetails.LicenseID.ToString();
            lblClass.Text = _LicenseDetails.LicenseClassInfo.ClassName;
            lblFullName.Text = _LicenseDetails.DriverInfo.PersonInfo.FullName;
            lblNationalNo.Text = _LicenseDetails.DriverInfo.PersonInfo.NationalNumber;
            lblGender.Text = (_LicenseDetails.DriverInfo.PersonInfo.Gender == Person.enGender.Male) ? "Male" : "Female";
            lblDateOfBirth.Text = clsFormat.DateToShort(_LicenseDetails.DriverInfo.PersonInfo.DateOfBirth);

            lblDriverID.Text = _LicenseDetails.DriverID.ToString();
            lblIssueDate.Text = clsFormat.DateToShort(_LicenseDetails.IssueDate);
            lblExpirationDate.Text = clsFormat.DateToShort(_LicenseDetails.ExpirationDate);
            lblIssueReason.Text = _LicenseDetails.IssueReasonText;

            lblNotes.Text = String.IsNullOrEmpty(_LicenseDetails.Notes) ? "No Notes" : _LicenseDetails.Notes;
            lblIsActive.Text = _LicenseDetails.IsActive ? "Yes" : "No";
            lblIsDetained.Text = _LicenseDetails.IsDetained ? "Yes" : "No";

            LoadPersonImage();
        }
    }
}
