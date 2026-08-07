using DVLD_Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DVLD_Project.Users.frmListUsers;

namespace DVLD_Project.Applications.LocalDrivingLicense
{
    public partial class frmListLocalDrivingLicenseApplications : Form
    {
        public frmListLocalDrivingLicenseApplications()
        {
            InitializeComponent();
        }

        private void frmListLocalDrivingLicenseApplications_Load(object sender, EventArgs e)
        {
            ResetForm();
        }


        private void ResetForm()
        {
            dvgLocalDrivingLicenseApplications.DataSource = LocalDrivingLicenseApplication.GetAllLocalDrivingLicenseApplications();
            dvgLocalDrivingLicenseApplications.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dvgLocalDrivingLicenseApplications.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            lblNumberOfRecords.Text = dvgLocalDrivingLicenseApplications.RowCount.ToString();
            cbFilterRows.SelectedIndex = (int)enUsersFilter.None;
        }
        private void RefreshFormData()
        {
            dvgLocalDrivingLicenseApplications.DataSource = LocalDrivingLicenseApplication.GetAllLocalDrivingLicenseApplications();
            dvgLocalDrivingLicenseApplications.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.ColumnHeader);
            lblNumberOfRecords.Text = dvgLocalDrivingLicenseApplications.RowCount.ToString();
        }
        private void RefreshHandler(object sender, int userID)
        {
            MessageBox.Show("A new local driving license application has been created successfully.",
                "Information",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );

            RefreshFormData();
        }


        private void pbAddNewLocalDrivingLicenseApplication_Click(object sender, EventArgs e)
        {
            frmAddEditLocalDrivingLicenseApplication frmAddEditLocalDrivingLicenseApplication = new frmAddEditLocalDrivingLicenseApplication();

            try
            {
                if (frmAddEditLocalDrivingLicenseApplication != null)
                    frmAddEditLocalDrivingLicenseApplication.OnNewLocalDrivingLicenseApplicationCreated += RefreshHandler;
                frmAddEditLocalDrivingLicenseApplication.ShowDialog();
            }
            catch (Exception)
            {
                MessageBox.Show("An error occurred while trying to add a new local driving license application.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                throw;
            }
            finally
            {
                if (frmAddEditLocalDrivingLicenseApplication != null)
                    frmAddEditLocalDrivingLicenseApplication.OnNewLocalDrivingLicenseApplicationCreated -= RefreshHandler;
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
