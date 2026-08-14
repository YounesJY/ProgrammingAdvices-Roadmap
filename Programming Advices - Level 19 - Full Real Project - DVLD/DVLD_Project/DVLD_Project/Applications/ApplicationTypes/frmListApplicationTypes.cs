using DVLD_Business;
using DVLD_Project.Users;
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

namespace DVLD_Project.Applications.ApplicationTypes
{
    public partial class frmListApplicationTypes : Form
    {
        public frmListApplicationTypes()
        {
            InitializeComponent();
        }
        private void frmListApplicationTypes_Load(object sender, EventArgs e)
        {
            resetFormToDefaultValues();
        }

        private void resetFormToDefaultValues()
        {
            dgvApplicationTypes.DataSource = ApplicationType.GetAllApplicationTypes();
            dgvApplicationTypes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvApplicationTypes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            lblNumberOfRecords.Text = dgvApplicationTypes.RowCount.ToString();
        }
        private void refreshFormData()
        {
            dgvApplicationTypes.DataSource = ApplicationType.GetAllApplicationTypes();
            lblNumberOfRecords.Text = dgvApplicationTypes.RowCount.ToString();
        }
        private void refreshHandler(object sender, int userID)
        {
            MessageBox.Show("User information updated and data refreshed.",
                "Success",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            refreshFormData();
        }



        private void editApplicationTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvApplicationTypes.RowCount == 0)
            {
                MessageBox.Show("No application type selected to edit.",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }

            int applicationTypeID = Convert.ToInt32(dgvApplicationTypes.CurrentRow.Cells[0].Value);
            frmEditApplicationTypes frmEditApplicationTypes = new frmEditApplicationTypes(applicationTypeID);

            try
            {
                if (frmEditApplicationTypes != null)
                    frmEditApplicationTypes.OnApplicationTypeUpdated += refreshHandler;
                frmEditApplicationTypes.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while editing application type: {ex.Message}",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
            finally
            {
                if (frmEditApplicationTypes != null)
                    frmEditApplicationTypes.OnApplicationTypeUpdated -= refreshHandler;
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
