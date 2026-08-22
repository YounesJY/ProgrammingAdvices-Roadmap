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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
