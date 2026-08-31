using DVLD.Classes;
using DVLD_Business;
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

namespace DVLD_Project.Licenses.InternationalLicenses.Controls
{
    public partial class ctrlDriverInternationalLicenseDetails : UserControl
    {
        private InternationalDrivingLicenseApplication _InternationalLicenseDetails = null;
        public InternationalDrivingLicenseApplication InternationalLicenseDetails
        {
            get { return this._InternationalLicenseDetails; }
        }


        public ctrlDriverInternationalLicenseDetails()
        {
            InitializeComponent();
        }
        public bool LoadDriverLicenseDetails(int InternationalLicenseID)
        {
            if (InternationalLicenseID <= 0)
            {
                MessageBox.Show($"Invalid InternationalLicenseID = {InternationalLicenseID}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            this._InternationalLicenseDetails = InternationalDrivingLicenseApplication.Find(InternationalLicenseID);
            if (this._InternationalLicenseDetails == null)
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
            pbPersonImage.Image = (_InternationalLicenseDetails.DriverInfo.PersonInfo.Gender == Person.enGender.Male) ? Resources.Male_512 : Resources.Female_512;
            string ImagePath = _InternationalLicenseDetails.DriverInfo.PersonInfo.ProfilePhotoPath;

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

            lblInternationalLicenseID.Text = this._InternationalLicenseDetails.InternationalLicenseID.ToString();
            lblApplicationID.Text = this._InternationalLicenseDetails.ApplicationID.ToString();
            lblIsActive.Text = this._InternationalLicenseDetails.IsActive ? "Yes" : "No";
            lblLocalLicenseID.Text = this._InternationalLicenseDetails.IssuedUsingLocalLicenseID.ToString();
            lblFullName.Text = this._InternationalLicenseDetails.DriverInfo.PersonInfo.FullName;
            lblNationalNo.Text = this._InternationalLicenseDetails.DriverInfo.PersonInfo.NationalNumber;
            lblGendor.Text = (this._InternationalLicenseDetails.DriverInfo.PersonInfo.Gender == Person.enGender.Male) ? Person.enGender.Male.ToString() : Person.enGender.Female.ToString();
            lblDateOfBirth.Text = Format.DateToShort(_InternationalLicenseDetails.DriverInfo.PersonInfo.DateOfBirth);

            lblDriverID.Text = _InternationalLicenseDetails.DriverID.ToString();
            lblIssueDate.Text = Format.DateToShort(_InternationalLicenseDetails.IssueDate);
            lblExpirationDate.Text = Format.DateToShort(_InternationalLicenseDetails.ExpirationDate);

            return LoadPersonImage();
        }
    }
}
