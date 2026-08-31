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
        private LicenseInfo _LicenseDetails = null;
        public LicenseInfo LicenseDetails
        {
            get { return this._LicenseDetails; }
        }


        public ctrlDriverLicenseDetails()
        {
            InitializeComponent();
        }
        public bool LoadDriverLicenseDetails(int LicenseID)
        {
            if (LicenseID <= 0)
            {
                MessageBox.Show($"Invalid LicenseID = {LicenseID}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            this._LicenseDetails = LicenseInfo.Find(LicenseID);
            if (this._LicenseDetails == null)
            {
                MessageBox.Show("Failed to load the license. Please try again.",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return false;
            }

            return FillDriverLicenseCardDetails();
        }


        private bool LoadPersonImage()
        {
            pbPersonImage.Image = (_LicenseDetails.DriverInfo.PersonInfo.Gender == Person.enGender.Male) ? Resources.Male_512 : Resources.Female_512;
            string ImagePath = _LicenseDetails.DriverInfo.PersonInfo.ProfilePhotoPath;

            if (!String.IsNullOrEmpty(ImagePath))
                if (File.Exists(ImagePath))
                    pbPersonImage.Load(ImagePath);
                else
                {
                    MessageBox.Show($"Could not find this image: = {ImagePath}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            return true;
        }
        private bool FillDriverLicenseCardDetails()
        {
            // NOTE: this a an example of high usability of composition 

            lblLicenseID.Text = _LicenseDetails.LicenseID.ToString();
            lblClass.Text = _LicenseDetails.LicenseClassInfo.ClassName;
            lblFullName.Text = _LicenseDetails.DriverInfo.PersonInfo.FullName;
            lblNationalNo.Text = _LicenseDetails.DriverInfo.PersonInfo.NationalNumber;
            lblGender.Text = (_LicenseDetails.DriverInfo.PersonInfo.Gender == Person.enGender.Male) ? "Male" : "Female";
            lblDateOfBirth.Text = Format.DateToShort(_LicenseDetails.DriverInfo.PersonInfo.DateOfBirth);

            lblDriverID.Text = _LicenseDetails.DriverID.ToString();
            lblIssueDate.Text = Format.DateToShort(_LicenseDetails.IssueDate);
            lblExpirationDate.Text = Format.DateToShort(_LicenseDetails.ExpirationDate);
            lblIssueReason.Text = _LicenseDetails.IssueReasonText;

            lblNotes.Text = String.IsNullOrEmpty(_LicenseDetails.Notes) ? "No Notes" : _LicenseDetails.Notes;
            lblIsActive.Text = _LicenseDetails.IsActive ? "Yes" : "No";
            lblIsDetained.Text = _LicenseDetails.IsDetained ? "Yes" : "No";

            return LoadPersonImage();
        }
    }
}
