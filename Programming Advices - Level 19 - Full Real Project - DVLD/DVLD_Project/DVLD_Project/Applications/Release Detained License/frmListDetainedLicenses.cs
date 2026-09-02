using DVLD_Business;
using DVLD_Project.Licenses;
using DVLD_Project.Licenses.DetainedLicenses;
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
using static DVLD_Project.Applications.InternationalDrivingLicense.frmListInternationalDrivingLicenseApplications;

namespace DVLD_Project.Applications.Release_Detained_License
{
    public partial class frmListDetainedLicenses : Form
    {
        public enum enDetainedLicenseFilter
        {
            None,
            DetainID,
            FullName,
            NationalNumber,
            IsReleased,
            ReleaseApplicationID
        }
        public enum enDetainedLicenseStatusFilter
        {
            No,
            Yes,
            All
        }

        public frmListDetainedLicenses()
        {
            InitializeComponent();
        }
        private void frmListDetainedLicenses_Load(object sender, EventArgs e)
        {
            ResetForm();
        }

        private void ResetForm()
        {
            dgvDetainedLicenses.DataSource = DetainedLicense.GetAllDetainedLicenses();
            dgvDetainedLicenses.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDetainedLicenses.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            lblNumberOfRecords.Text = dgvDetainedLicenses.RowCount.ToString();
            cbFilterRows.SelectedIndex = (int)enDetainedLicenseFilter.None;
        }
        private void RefreshFormData()
        {
            dgvDetainedLicenses.DataSource = DetainedLicense.GetAllDetainedLicenses();
            lblNumberOfRecords.Text = dgvDetainedLicenses.RowCount.ToString();
        }
        private void RefreshHandler(object sender, int ID)
        {
            MessageBox.Show("Detained licenses has been updated and data refreshed successfully.",
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

            DataTable dataTable = DetainedLicense.GetAllDetainedLicenses();
            DataView dataView = dataTable.DefaultView;

            switch (filterColumn)
            {
                // '=' for [numeric values] and 'LIKE' for [strings]
                case "detainid":
                    if (int.TryParse(searchValue, out int DetainID))
                        dataView.RowFilter = $"DetainID = {DetainID}";
                    else
                        dataView.RowFilter = "DetainID = -1";
                    break;
                case "releaseapplicationid":
                    if (int.TryParse(searchValue, out int ReleaseApplicationID))
                        dataView.RowFilter = $"ReleaseApplicationID = {ReleaseApplicationID}";
                    else
                        dataView.RowFilter = "ReleaseApplicationID = -1";
                    break;
                default:
                    dataView.RowFilter = $"{filterColumn} LIKE '%{searchValue}%'";
                    break;
            }

            dgvDetainedLicenses.DataSource = dataView;
            lblNumberOfRecords.Text = dataView.Count.ToString();
        }
        private void FilterInternationalDrivingApplicationsByStatus()
        {
            string searchValue = cbApplicationStatus.Text.ToLower().Trim();
            DataTable dataTable = DetainedLicense.GetAllDetainedLicenses();
            DataView dataView = dataTable.DefaultView;

            if (searchValue == enDetainedLicenseStatusFilter.Yes.ToString().ToLower() || searchValue == enDetainedLicenseStatusFilter.No.ToString().ToLower())
                dataView.RowFilter = $@"IsReleased = {(searchValue == enDetainedLicenseStatusFilter.Yes.ToString().ToLower() ? (int)enDetainedLicenseStatusFilter.Yes : (int)enDetainedLicenseStatusFilter.No)}";


            dgvDetainedLicenses.DataSource = dataView;
            lblNumberOfRecords.Text = dataView.Count.ToString();
        }


        private void cbFilterRows_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterRows.SelectedItem.ToString().ToLower() == enDetainedLicenseFilter.None.ToString().ToLower())
            {
                RefreshFormData();
                mtbFilterSearch.Visible = false;
                cbApplicationStatus.Visible = false;
            }
            else if (cbFilterRows.SelectedItem.ToString().ToLower() == enDetainedLicenseFilter.IsReleased.ToString().ToLower())
            {
                mtbFilterSearch.Visible = false;
                cbApplicationStatus.Visible = true;
                cbApplicationStatus.SelectedItem = enDetainedLicenseStatusFilter.All.ToString();
            }
            else
            {
                cbApplicationStatus.Visible = false;
                mtbFilterSearch.Visible = true;
                mtbFilterSearch.Clear();

                if (cbFilterRows.SelectedItem.ToString().ToLower() == enDetainedLicenseFilter.DetainID.ToString().ToLower() ||
                    cbFilterRows.SelectedItem.ToString().ToLower() == enDetainedLicenseFilter.ReleaseApplicationID.ToString().ToLower())
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
            FilterInternationalDrivingApplicationsByStatus();
        }
        private void mtbFilterSearch_TextChanged(object sender, EventArgs e)
        {
            FilterInternationalDrivingApplications();
        }

        private void tsmiShowPersonDetails_Click(object sender, EventArgs e)
        {
            if (dgvDetainedLicenses.RowCount == 0)
            {
                MessageBox.Show("No application selected to show details.",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }

            string nationalNumber = dgvDetainedLicenses.CurrentRow.Cells[6].Value.ToString();
            Person person = Person.Find(nationalNumber);
            new frmPersonDetails(person.PersonID).ShowDialog();
        }
        private void tsmiShowLicenseDetails_Click(object sender, EventArgs e)
        {
            if (dgvDetainedLicenses.RowCount == 0)
            {
                MessageBox.Show("No application selected to show details.",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }

            int licenseID = Convert.ToInt32(dgvDetainedLicenses.CurrentRow.Cells[1].Value);
            new frmShowLicenseDetails(licenseID).ShowDialog();
        }
        private void tsmiShowPersonLicenseHistory_Click(object sender, EventArgs e)
        {
            if (dgvDetainedLicenses.RowCount == 0)
            {
                MessageBox.Show("No application selected to show details.",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }

            string nationalNumber = dgvDetainedLicenses.CurrentRow.Cells[6].Value.ToString();
            Person person = Person.Find(nationalNumber);
            new frmShowPersonLicenseHistory(person.PersonID).ShowDialog(this);
        }
        private void tsmiReleaseDetainedLicense_Click(object sender, EventArgs e)
        {
            if (dgvDetainedLicenses.RowCount == 0)
            {
                MessageBox.Show("No application selected to show details.",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }

            int licenseID = Convert.ToInt32(dgvDetainedLicenses.CurrentRow.Cells[1].Value);
            frmReleaseDetainedLicenseApplication releaseDetainedLicenseApplication = new frmReleaseDetainedLicenseApplication(licenseID);
            try
            {
                if (releaseDetainedLicenseApplication != null)
                    releaseDetainedLicenseApplication.OnLicenseRelease += RefreshHandler;
                releaseDetainedLicenseApplication.ShowDialog();
            }
            catch (Exception)
            {
                MessageBox.Show("An error occured during release process !",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }
            finally
            {
                if (releaseDetainedLicenseApplication != null)
                    releaseDetainedLicenseApplication.OnLicenseRelease -= RefreshHandler;
            }
        }

        private void btnReleaseDetainedLicense_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicenseApplication releaseDetainedLicenseApplication = new frmReleaseDetainedLicenseApplication();
            try
            {
                if (releaseDetainedLicenseApplication != null)
                    releaseDetainedLicenseApplication.OnLicenseRelease += RefreshHandler;
                releaseDetainedLicenseApplication.ShowDialog();
            }
            catch (Exception)
            {
                MessageBox.Show("An error occured during release process !",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }
            finally
            {
                if (releaseDetainedLicenseApplication != null)
                    releaseDetainedLicenseApplication.OnLicenseRelease -= RefreshHandler;
            }
        }
        private void btnDetainLicense_Click(object sender, EventArgs e)
        {
            frmDetainLicenseApplication frmDetainLicenseApplication = new frmDetainLicenseApplication();
            try
            {
                if (frmDetainLicenseApplication != null)
                    frmDetainLicenseApplication.OnLicenseDetain += RefreshHandler;
                frmDetainLicenseApplication.ShowDialog();
            }
            catch (Exception)
            {
                MessageBox.Show("An error occured during release process !",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }
            finally
            {
                if (frmDetainLicenseApplication != null)
                    frmDetainLicenseApplication.OnLicenseDetain -= RefreshHandler;
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
