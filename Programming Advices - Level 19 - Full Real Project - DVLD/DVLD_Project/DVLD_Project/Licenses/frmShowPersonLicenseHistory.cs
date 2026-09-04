using DVLD_Business;
using DVLD_Common;
using DVLD_Project.Licenses.InternationalLicenses.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project.Licenses
{
    public partial class frmShowPersonLicenseHistory : Form
    {
        public event Action<object, int> OnPersonCardDetailsUpdated
        {
            add { this.ctrlPersonCardWithFilters.OnPersonCardDetailsUpdated += value; }
            remove { this.ctrlPersonCardWithFilters.OnPersonCardDetailsUpdated -= value; }
        }

        private int _PersonID = ValidationConstants.INVALID_ID;
        private Driver _Driver = null;


        public frmShowPersonLicenseHistory()
        {
            InitializeComponent();
            this._PersonID = ValidationConstants.INVALID_ID;
        }
        public frmShowPersonLicenseHistory(int personID)
        {
            InitializeComponent();
            this._PersonID = personID;
        }
        private void frmShowPersonLicenseHistory_Load(object sender, EventArgs e)
        {
            if (this._PersonID != ValidationConstants.INVALID_ID)
                LoadData();
            else
            {
                ctrlPersonCardWithFilters.OnPersonSelected += LoadData;
                ctrlPersonCardWithFilters.Focus();
            }
        }


        private void LoadData()
        {
            if (this._PersonID <= 0)
            {
                MessageBox.Show($"Invalid PersonID = {this._PersonID}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            this._Driver = Driver.FindByPersonID(this._PersonID);
            if (this._Driver == null)
            {
                MessageBox.Show("Driver not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            ctrlPersonCardWithFilters.loadPersonDetailsToCard(this._PersonID);
            ctrlDriverLicenses.LoadDriverLicensesByDriverID(this._Driver.DriverID);
            ctrlPersonCardWithFilters.DisactiviteFilter();
        }
        private void LoadData(object sender, int personID)
        {
            this._PersonID = personID;
            LoadData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ctrlPersonCardWithFilters_OnPersonSelected(object sender, int personID)
        {
            MessageBox.Show("WHy.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            this._PersonID = personID;
            ctrlPersonCardWithFilters.DisactiviteFilter();
            ctrlDriverLicenses.LoadDriverLicensesByPersonID(this._PersonID);
        }
    }
}
