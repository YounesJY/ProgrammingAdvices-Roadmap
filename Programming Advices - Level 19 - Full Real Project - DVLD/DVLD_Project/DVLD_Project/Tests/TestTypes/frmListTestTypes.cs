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

namespace DVLD_Project.Tests.TestTypes
{
    public partial class frmListTestTypes : Form
    {
        public frmListTestTypes()
        {
            InitializeComponent();
        }

        private void frmListTestTypes_Load(object sender, EventArgs e)
        {
            resetFormToDefaultValues();
        }
        private void resetFormToDefaultValues()
        {

            dgvTestTypes.DataSource = TestType.GetAllTestTypes();
            dgvTestTypes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTestTypes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            lblNumberOfRecords.Text = dgvTestTypes.RowCount.ToString();
        }
        private void refreshFormData()
        {
            dgvTestTypes.DataSource = TestType.GetAllTestTypes();
            lblNumberOfRecords.Text = dgvTestTypes.RowCount.ToString();
        }
        private void refreshHandler(object sender, int userID)
        {
            MessageBox.Show("User information updated and data refreshed.",
                "Success",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            refreshFormData();
        }

        private void editTestTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvTestTypes.RowCount == 0)
            {
                MessageBox.Show("No test type selected to edit.",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }

            int testTypeID = Convert.ToInt32(dgvTestTypes.CurrentRow.Cells["TestTypeID"].Value);
            frmEditTestTypes frmEditTestTypes = new frmEditTestTypes((TestType.enTestType)testTypeID);

            try
            {
                if (frmEditTestTypes != null)
                    frmEditTestTypes.OnTestTypeUpdated += refreshHandler;
                frmEditTestTypes.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while editing test type: {ex.Message}",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
            finally
            {
                if (frmEditTestTypes != null)
                    frmEditTestTypes.OnTestTypeUpdated -= refreshHandler;
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
