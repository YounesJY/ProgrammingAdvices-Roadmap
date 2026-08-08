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

namespace DVLD_Project.Applications.LocalDrivingLicense
{
    public partial class frmListLocalDrivingLicenseApplications : Form
    {
        public enum enLocalDrivingLicenseApplicationsFilter
        {
            None,
            ApplicationID,
            NationalNumber,
            FullName,
            Status
        }
        public enum enLocalDrivingLicenseApplicationsStatusFilter
        {
            New,
            Cancelled,
            Completed
        }


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
            dgvLocalDrivingLicenseApplications.DataSource = LocalDrivingLicenseApplication.GetAllLocalDrivingLicenseApplications();
            dgvLocalDrivingLicenseApplications.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLocalDrivingLicenseApplications.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            lblNumberOfRecords.Text = dgvLocalDrivingLicenseApplications.RowCount.ToString();
            cbFilterRows.SelectedIndex = (int)enLocalDrivingLicenseApplicationsFilter.None;
        }
        private void RefreshFormData()
        {
            dgvLocalDrivingLicenseApplications.DataSource = LocalDrivingLicenseApplication.GetAllLocalDrivingLicenseApplications();
            dgvLocalDrivingLicenseApplications.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.ColumnHeader);
            lblNumberOfRecords.Text = dgvLocalDrivingLicenseApplications.RowCount.ToString();
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

        private void FilterLocalDrivingApplications()
        {
            string filterColumn = cbFilterRows.SelectedItem.ToString().ToLower();
            string searchValue = mtbFilterSearch.Text.Trim();

            // If search is empty, show all
            if (string.IsNullOrEmpty(searchValue))
            {
                dgvLocalDrivingLicenseApplications.DataSource = LocalDrivingLicenseApplication.GetAllLocalDrivingLicenseApplications();
                lblNumberOfRecords.Text = dgvLocalDrivingLicenseApplications.RowCount.ToString();
                return;
            }

            /*
                Using DataView filtering instead of direct SQL queries allows us to:
                1. Filter already-loaded data without additional DB round-trips
                2. Maintain a consistent dataset in memory for the UI
                3. Provide real-time filtering as the user types without performance overhead
                4. Avoid SQL injection risks since we are not constructing raw SQL queries
            */
            DataTable dataTable = LocalDrivingLicenseApplication.GetAllLocalDrivingLicenseApplications();
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
                default: // NationalNumber & FullName
                    dataView.RowFilter = $"{filterColumn} LIKE '%{searchValue}%'";
                    break;
            }

            dgvLocalDrivingLicenseApplications.DataSource = dataView;
            lblNumberOfRecords.Text = dataView.Count.ToString();
        }
        private void FilterLocalDrivingApplicationsByStatus()
        {
            string searchValue = cbApplicationStatus.SelectedItem.ToString().ToLower().Trim();
            DataTable dataTable = LocalDrivingLicenseApplication.GetAllLocalDrivingLicenseApplications();
            DataView dataView = dataTable.DefaultView;

            dataView.RowFilter = $"Status = '{searchValue}'";

            dgvLocalDrivingLicenseApplications.DataSource = dataView;
            lblNumberOfRecords.Text = dataView.Count.ToString();
        }

        private void cbFilterRows_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cbFilterRows.SelectedItem.ToString().ToLower() == enLocalDrivingLicenseApplicationsFilter.None.ToString().ToLower())
            {
                dgvLocalDrivingLicenseApplications.DataSource = LocalDrivingLicenseApplication.GetAllLocalDrivingLicenseApplications();
                mtbFilterSearch.Visible = false;
                cbApplicationStatus.Visible = false;
                lblNumberOfRecords.Text = dgvLocalDrivingLicenseApplications.RowCount.ToString();
            }
            else if (cbFilterRows.SelectedItem.ToString().ToLower() == enLocalDrivingLicenseApplicationsFilter.Status.ToString().ToLower())
            {
                mtbFilterSearch.Visible = false;
                cbApplicationStatus.Visible = true;
                cbApplicationStatus.SelectedItem = enLocalDrivingLicenseApplicationsStatusFilter.New.ToString();
                FilterLocalDrivingApplicationsByStatus();
            }
            else
            {
                cbApplicationStatus.Visible = false;
                mtbFilterSearch.Visible = true;
                mtbFilterSearch.Clear();

                if (cbFilterRows.SelectedItem.ToString().ToLower() == enLocalDrivingLicenseApplicationsFilter.ApplicationID.ToString().ToLower())
                {
                    mtbFilterSearch.Mask = "00000000";
                    mtbFilterSearch.Select(0, 0);
                }
                else
                    mtbFilterSearch.Mask = string.Empty;

                mtbFilterSearch.Focus();
            }

        }
        private void cbApplicationStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterLocalDrivingApplicationsByStatus();
        }
        private void mtbFilterSearch_TextChanged(object sender, EventArgs e)
        {
            FilterLocalDrivingApplications();
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
