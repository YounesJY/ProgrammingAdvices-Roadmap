using DVLD_Business;
using DVLD_Project.Licenses;
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
using static DVLD_Project.Users.frmListUsers;

namespace DVLD_Project.Drivers
{
    public partial class frmListDrivers : Form
    {
        public enum enDriversFilter
        {
            None,
            DriverID,
            PersonID,
            FullName,
            NationalNumber
        }


        public frmListDrivers()
        {
            InitializeComponent();
        }
        private void frmListDrivers_Load(object sender, EventArgs e)
        {
            LoadDriversData();
        }


        private void LoadDriversData()
        {
            dgvDrivers.DataSource = Driver.GetAllDrivers();
            dgvDrivers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDrivers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            cbFilterRows.SelectedIndex = (int)enDriversFilter.None;
            lblNumberOfRecords.Text = dgvDrivers.RowCount.ToString();
        }
        private void RefreshDriversData()
        {
            dgvDrivers.DataSource = Driver.GetAllDrivers();
            lblNumberOfRecords.Text = dgvDrivers.RowCount.ToString();
        }
        private void RefreshHandler(object sender, int ID)
        {
            MessageBox.Show("Drivers has been updated and data refreshed successfully.",
                "Information",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );

            RefreshDriversData();
        }
        private void FilterDrivers()
        {
            string filterColumn = cbFilterRows.SelectedItem.ToString().ToLower();
            string searchValue = mtbFilterSearch.Text.Trim();


            if (string.IsNullOrEmpty(searchValue))
            {
                RefreshDriversData();
                return;
            }

            /*
                Using DataView filtering instead of direct SQL queries allows us to:
                1. Filter already-loaded data without additional DB round-trips
                2. Maintain a consistent dataset in memory for the UI
                3. Provide real-time filtering as the user types without performance overhead
                4. Avoid SQL injection risks since we are not constructing raw SQL queries
            */
            DataTable dataTable = Driver.GetAllDrivers();
            DataView dataView = dataTable.DefaultView;

            switch (filterColumn)
            {
                // '=' for [numeric values] and 'LIKE' for [strings]
                case "driverid":
                    if (int.TryParse(searchValue, out int driverID))
                        dataView.RowFilter = $"DriverID = {driverID}";
                    else
                        dataView.RowFilter = "DriverID = -1";
                    break;

                case "personid":
                    if (int.TryParse(searchValue, out int personId))
                        dataView.RowFilter = $"PersonID = {personId}";
                    else
                        dataView.RowFilter = "PersonID = -1";
                    break;

                default:
                    dataView.RowFilter = $"{filterColumn} LIKE '%{searchValue}%'";
                    break;
            }

            dgvDrivers.DataSource = dataView;
            lblNumberOfRecords.Text = dataView.Count.ToString();
        }
        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterRows.SelectedItem.ToString().ToLower() == enDriversFilter.None.ToString().ToLower())
            {
                RefreshDriversData();
                mtbFilterSearch.Visible = false;
            }
            else
            {
                mtbFilterSearch.Visible = true;
                mtbFilterSearch.Clear();

                if (cbFilterRows.SelectedItem.ToString().ToLower() == enDriversFilter.DriverID.ToString().ToLower() || cbFilterRows.SelectedItem.ToString().ToLower() == enDriversFilter.PersonID.ToString().ToLower())
                {
                    mtbFilterSearch.Mask = "00000000";
                    mtbFilterSearch.Select(0, 0);
                }
                else
                    mtbFilterSearch.Mask = string.Empty;

                mtbFilterSearch.Focus();
            }
        }
        private void mtbFilterSearch_TextChanged(object sender, EventArgs e)
        {
            FilterDrivers();
        }


        private void tsmiShowPersonDetailsTool_Click(object sender, EventArgs e)
        {
            if (dgvDrivers.RowCount == 0)
            {
                MessageBox.Show("No driver selected to show details.",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }


            int PersonID = Convert.ToInt32(dgvDrivers.CurrentRow.Cells[1].Value);
            frmPersonDetails frmPersonDetails = new frmPersonDetails(PersonID);

            try
            {
                if (frmPersonDetails != null)
                    frmPersonDetails.OnPersonCardDetailsUpdated += RefreshHandler;
                frmPersonDetails.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while showing person details: {ex.Message}",
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            }
            finally
            {
                if (frmPersonDetails != null)
                    frmPersonDetails.OnPersonCardDetailsUpdated -= RefreshHandler;
            }
        }
        private void tsmiShowPersonLicenseHistory_Click(object sender, EventArgs e)
        {
            if (dgvDrivers.RowCount == 0)
            {
                MessageBox.Show("No driver selected to show details.",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }


            int PersonID = Convert.ToInt32(dgvDrivers.CurrentRow.Cells[1].Value);
            frmShowPersonLicenseHistory frmShowPersonLicenseHistory = new frmShowPersonLicenseHistory(PersonID);

            try
            {
                if (frmShowPersonLicenseHistory != null)
                    frmShowPersonLicenseHistory.OnPersonCardDetailsUpdated += RefreshHandler;
                frmShowPersonLicenseHistory.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while showing licenses history: {ex.Message}",
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            }
            finally
            {
                if (frmShowPersonLicenseHistory != null)
                    frmShowPersonLicenseHistory.OnPersonCardDetailsUpdated -= RefreshHandler;
            }
        }
        private void dgvDrivers_DoubleClick(object sender, EventArgs e)
        {
            tsmiShowPersonDetailsTool_Click(sender, e);
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
