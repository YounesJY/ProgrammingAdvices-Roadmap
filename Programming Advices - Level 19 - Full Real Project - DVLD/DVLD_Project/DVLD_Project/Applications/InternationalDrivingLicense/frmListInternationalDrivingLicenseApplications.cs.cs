using DVLD_Business;
using DVLD_Project.Licenses;
using DVLD_Project.Licenses.InternationalLicenses;
using DVLD_Project.Licenses.LocalLicenses;
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
using static DVLD_Project.Applications.LocalDrivingLicense.frmListLocalDrivingLicenseApplications;
using static DVLD_Project.Users.frmListUsers;

namespace DVLD_Project.Applications.InternationalDrivingLicense
{
    public partial class frmListInternationalDrivingLicenseApplications : Form
    {
        public enum enInternationalDrivingLicenseApplicationsFilter
        {
            None,
            ApplicationID,
            InternationalLicenseID,
            LocalLicenseID,
            DriverID,
            IsActive
        }
        public enum enInternationalDrivingLicenseApplicationsStatusFilter
        {
            No,
            Yes,
            All
        }

        public frmListInternationalDrivingLicenseApplications()
        {
            InitializeComponent();
        }
        private void frmListInternationalDrivingLicenseApplications_Load(object sender, EventArgs e)
        {
            ResetForm();
        }


        private void ResetForm()
        {
            dgvInternationalDrivingLicenseApplications.DataSource = InternationalDrivingLicenseApplication.GetAllInternationalLicenses();
            dgvInternationalDrivingLicenseApplications.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvInternationalDrivingLicenseApplications.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            lblNumberOfRecords.Text = dgvInternationalDrivingLicenseApplications.RowCount.ToString();
            cbFilterRows.SelectedIndex = (int)enInternationalDrivingLicenseApplicationsFilter.None;
        }
        private void RefreshFormData()
        {
            dgvInternationalDrivingLicenseApplications.DataSource = InternationalDrivingLicenseApplication.GetAllInternationalLicenses();
            lblNumberOfRecords.Text = dgvInternationalDrivingLicenseApplications.RowCount.ToString();
        }
        private void RefreshHandler(object sender, int ID)
        {
            MessageBox.Show("International driving license applications has been updated and data refreshed successfully.",
                "Information",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );

            RefreshFormData();
        }

        private void FilterInternationalDrivingApplications()
        {
            string filterColumn = cbFilterRows.SelectedItem.ToString().ToLower();
            string searchValue = mtbFilterSearch.Text.Trim();

            if (string.IsNullOrEmpty(searchValue))
            {
                RefreshFormData();
                return;
            }

            DataTable dataTable = InternationalDrivingLicenseApplication.GetAllInternationalLicenses();
            DataView dataView = dataTable.DefaultView;

            switch (filterColumn)
            {
                // '=' for [numeric values] and 'LIKE' for [strings]
                case "applicationid":
                    if (int.TryParse(searchValue, out int applicationID))
                        dataView.RowFilter = $"ApplicationID = {applicationID}";
                    else
                        dataView.RowFilter = "ApplicationID = -1";
                    break;
                case "internationallicenseid":
                    if (int.TryParse(searchValue, out int internationalLicenseID))
                        dataView.RowFilter = $"InternationalLicenseID = {internationalLicenseID}";
                    else
                        dataView.RowFilter = "InternationalLicenseID = -1";
                    break;
                case "locallicenseid":
                    if (int.TryParse(searchValue, out int localLicenseID))
                        dataView.RowFilter = $"IssuedUsingLocalLicenseID = {localLicenseID}";
                    else
                        dataView.RowFilter = "IssuedUsingLocalLicenseID = -1";
                    break;
                case "driverid":
                    if (int.TryParse(searchValue, out int driverID))
                        dataView.RowFilter = $"DriverID = {driverID}";
                    else
                        dataView.RowFilter = "DriverID = -1";
                    break;
            }

            dgvInternationalDrivingLicenseApplications.DataSource = dataView;
            lblNumberOfRecords.Text = dataView.Count.ToString();
        }
        private void FilterInternationalDrivingApplicationsByStatus()
        {
            string searchValue = cbApplicationStatus.SelectedItem.ToString().ToLower().Trim();
            DataTable dataTable = InternationalDrivingLicenseApplication.GetAllInternationalLicenses();
            DataView dataView = dataTable.DefaultView;

            if (searchValue == enInternationalDrivingLicenseApplicationsStatusFilter.Yes.ToString().ToLower() || searchValue == enInternationalDrivingLicenseApplicationsStatusFilter.No.ToString().ToLower())
                dataView.RowFilter = $@"IsActive = {(searchValue == enInternationalDrivingLicenseApplicationsStatusFilter.Yes.ToString().ToLower() ? (int)enInternationalDrivingLicenseApplicationsStatusFilter.Yes : (int)enInternationalDrivingLicenseApplicationsStatusFilter.No)}";


            dgvInternationalDrivingLicenseApplications.DataSource = dataView;
            lblNumberOfRecords.Text = dataView.Count.ToString();
        }

        private void cbFilterRows_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterRows.SelectedItem.ToString().ToLower() == enInternationalDrivingLicenseApplicationsFilter.None.ToString().ToLower())
            {
                RefreshFormData();
                mtbFilterSearch.Visible = false;
                cbApplicationStatus.Visible = false;
            }
            else if (cbFilterRows.SelectedItem.ToString().ToLower() == enInternationalDrivingLicenseApplicationsFilter.IsActive.ToString().ToLower())
            {
                mtbFilterSearch.Visible = false;
                cbApplicationStatus.Visible = true;
                cbApplicationStatus.SelectedItem = enInternationalDrivingLicenseApplicationsStatusFilter.All.ToString();
            }
            else
            {
                cbApplicationStatus.Visible = false;
                mtbFilterSearch.Visible = true;

                mtbFilterSearch.Mask = "00000000";
                mtbFilterSearch.Select(0, 0);
                mtbFilterSearch.Clear();

                mtbFilterSearch.Focus();
            }
        }
        private void cbApplicationStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterInternationalDrivingApplicationsByStatus();
        }
        private void mtbFilterSearch_TextChanged(object sender, EventArgs e)
        {
            FilterInternationalDrivingApplications();
        }

        private void tsmiShowPersonDetails_Click(object sender, EventArgs e)
        {
            if (dgvInternationalDrivingLicenseApplications.RowCount == 0)
            {
                MessageBox.Show("No application selected to show details.",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }

            int driverID = Convert.ToInt32(dgvInternationalDrivingLicenseApplications.CurrentRow.Cells[2].Value);
            new frmPersonDetails((Driver.FindByDriverID(driverID).PersonID)).ShowDialog();
        }
        private void tsmiShowLocalLicenseDetails_Click(object sender, EventArgs e)
        {
            if (dgvInternationalDrivingLicenseApplications.RowCount == 0)
            {
                MessageBox.Show("No application selected to show details.",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }

            int licenseID = Convert.ToInt32(dgvInternationalDrivingLicenseApplications.CurrentRow.Cells[3].Value);
            new frmShowLicenseDetails(licenseID).ShowDialog();
        }
        private void tsmiShowInternationalLicenseDetails_Click(object sender, EventArgs e)
        {
            if (dgvInternationalDrivingLicenseApplications.RowCount == 0)
            {
                MessageBox.Show("No application selected to show details.",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }

            int internationalLicenseID = Convert.ToInt32(dgvInternationalDrivingLicenseApplications.CurrentRow.Cells[0].Value);
            new frmShowInternationalLicenseDetails(internationalLicenseID).ShowDialog();
        }
        private void tsmiShowPersonLicenseHistory_Click(object sender, EventArgs e)
        {
            if (dgvInternationalDrivingLicenseApplications.RowCount == 0)
            {
                MessageBox.Show("No application selected to show details.",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }

            int driverID = Convert.ToInt32(dgvInternationalDrivingLicenseApplications.CurrentRow.Cells[2].Value);
            new frmShowPersonLicenseHistory(Driver.FindByDriverID(driverID).PersonID).ShowDialog();
        }

        private void dgvInternationalDrivingLicenseApplications_DoubleClick(object sender, EventArgs e)
        {
            tsmiShowInternationalLicenseDetails_Click(sender, e);
        }

        private void pbAddNewLocalDrivingLicenseApplication_Click(object sender, EventArgs e)
        {
            frmNewInternationalLicenseApplication frmNewInternationalLicenseApplication = new frmNewInternationalLicenseApplication();

            // [This teaches how to handle events for inner controls via Event Exposure pattern]
            try
            {
                if (frmNewInternationalLicenseApplication != null)
                    frmNewInternationalLicenseApplication.OnInternationalLicenseIssuance += RefreshHandler;
                frmNewInternationalLicenseApplication.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while showing application details: {ex.Message}",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
            finally
            {
                if (frmNewInternationalLicenseApplication != null)
                    frmNewInternationalLicenseApplication.OnInternationalLicenseIssuance -= RefreshHandler;
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
