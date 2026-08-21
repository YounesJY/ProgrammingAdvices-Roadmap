using DVLD.Classes;
using DVLD_Business;
using DVLD_Common;
using DVLD_Project.Applications.LocalDrivingLicense;
using DVLD_Project.People;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD_Project.Applications.Controls
{
    public partial class ctrlApplicationDetails : UserControl
    {
        public event Action<object, int> OnApplicationCardDetailsUpdated;
        private ApplicationInfo _applicationInfo = null;

        public ctrlApplicationDetails()
        {
            InitializeComponent();
        }
        public void LoadApplicationDetailsToCard(int applicationID)
        {
            if (applicationID <= 0)
            {
                ResetApplicationDetails();
                MessageBox.Show($"Invalid ApplicationID = {applicationID}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this._applicationInfo = ApplicationInfo.Find(applicationID);
            if (this._applicationInfo == null)
            {
                ResetApplicationDetails();
                MessageBox.Show($"No application with ApplicationID = {applicationID}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            FillApplicationDetails();
        }


        public void ResetApplicationDetails()
        {
            lblApplicationID.Text = ValidationConstants.INVALID_ID.ToString();

            lblApplicationID.Text = "[????]";
            lblStatus.Text = "[????]";
            lblType.Text = "[????]";
            lblFees.Text = "[????]";
            lblApplicant.Text = "[????]";
            lblDate.Text = "[????]";
            lblStatusDate.Text = "[????]";
            lblCreatedByUser.Text = "[????]";

        }
        private void FillApplicationDetails()
        {
            lblApplicationID.Text = _applicationInfo.ApplicationID.ToString();
            lblStatus.Text = _applicationInfo.StatusText;
            lblType.Text = _applicationInfo.ApplicationTypeInfo.ApplicationTypeTitle;
            lblFees.Text = _applicationInfo.PaidFees.ToString();
            lblApplicant.Text = _applicationInfo.ApplicantFullName;
            lblDate.Text = clsFormat.DateToShort(_applicationInfo.ApplicationDate);
            lblStatusDate.Text = clsFormat.DateToShort(_applicationInfo.LastStatusDate);
            lblCreatedByUser.Text = _applicationInfo.CreatedByUserInfo.UserName;
        }
        private void llViewPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmPersonDetails form = new frmPersonDetails(this._applicationInfo.ApplicantPersonID);

            try
            {
                if (form != null)
                    form.OnPersonCardDetailsUpdated += RefreshDataOnApplicationUpdate;

                form.ShowDialog();
            }
            finally
            {
                if (form != null)
                    form.OnPersonCardDetailsUpdated -= RefreshDataOnApplicationUpdate;
            }
        }

        private void RefreshDataOnApplicationUpdate(object sender, int personID)
        {
            OnApplicationCardDetailsUpdated?.Invoke(this, this._applicationInfo.ApplicationID);
        }
    }
}
