using DVLD_Business;
using DVLD_Common;
using DVLD_Project.Licenses.InternationalLicenses;
using DVLD_Project.Licenses.LocalLicenses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DVLD_Project.Drivers.frmListDrivers;

namespace DVLD_Project.Licenses
{
    public partial class ctrlDriverLicenses : UserControl
    {
        private Driver _Driver = null;


        public ctrlDriverLicenses()
        {
            InitializeComponent();
        }
        public void LoadDriverLicensesByDriverID(int driverID)
        {
            if (driverID <= 0)
            {
                MessageBox.Show($"Invalid DriverID = {driverID}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this._Driver = Driver.FindByDriverID(driverID);
            if (this._Driver == null)
            {
                MessageBox.Show("Driver not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            LoadDriverLicenses();
        }
        public void LoadDriverLicensesByPersonID(int personID)
        {
            if (personID <= 0)
            {
                MessageBox.Show($"Invalid PersonID = {personID}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this._Driver = Driver.FindByPersonID(personID);
            if (this._Driver == null)
            {
                MessageBox.Show("Driver not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            LoadDriverLicenses();
        }

        private void LoadInternationalDrivingLicenses()
        {
            dgvInternationalLicensesHistory.DataSource = Driver.GetInternationalLicenses(this._Driver.DriverID);
            dgvInternationalLicensesHistory.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvInternationalLicensesHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            lblInternationalLicensesRecords.Text = dgvInternationalLicensesHistory.RowCount.ToString();
        }
        private void LoadLocalDrivingLicenses()
        {
            dgvLocalLicensesHistory.DataSource = Driver.GetLicenses(this._Driver.DriverID);
            dgvLocalLicensesHistory.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLocalLicensesHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            lblLocalLicensesRecords.Text = dgvLocalLicensesHistory.RowCount.ToString();
        }
        private void LoadDriverLicenses()
        {
            LoadLocalDrivingLicenses();
            LoadInternationalDrivingLicenses();
            tcDriverLicenses.SelectedTab = tpLocalDrivingLicenses;
        }

        private void tsmiShowInternationalLicenseDetails_Click(object sender, EventArgs e)
        {
            if (dgvInternationalLicensesHistory.RowCount == 0)
            {
                MessageBox.Show("No license selected to show details.",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }

            int InternationalLicenseID = Convert.ToInt32(dgvInternationalLicensesHistory.CurrentRow.Cells[0].Value);
            if (InternationalLicenseID <= 0)
            {
                MessageBox.Show($"Invalid LicenseID = {InternationalLicenseID}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            new frmShowInternationalLicenseDetails(InternationalLicenseID).ShowDialog();
        }
        private void tsmiShowLocalLicenseDetails_Click(object sender, EventArgs e)
        {
            if (dgvLocalLicensesHistory.RowCount == 0)
            {
                MessageBox.Show("No license selected to show details.",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }

            int LocalLicenseID = Convert.ToInt32(dgvLocalLicensesHistory.CurrentRow.Cells[0].Value);
            if (LocalLicenseID <= 0)
            {
                MessageBox.Show($"Invalid LicenseID = {LocalLicenseID}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            new frmShowLicenseDetails(LocalLicenseID).ShowDialog();
        }
        private void dgvInternationalLicensesHistory_DoubleClick(object sender, EventArgs e)
        {
            tsmiShowInternationalLicenseDetails_Click(sender, e);
        }
        private void dgvLocalLicensesHistory_DoubleClick(object sender, EventArgs e)
        {
            tsmiShowLocalLicenseDetails_Click(sender, e);
        }
    }
}
