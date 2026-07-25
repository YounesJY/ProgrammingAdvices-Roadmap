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

namespace DVLD_Project.Users
{
    public enum enUsersFilter
    {
        None,
        UserID,
        PersonID,
        Fullname,
        Username,
        isActive
    }
    public enum isActiveFilter
    {
        All,
        Active,
        Inactive
    }

    public partial class frmListUsers : Form
    {
        public frmListUsers()
        {
            InitializeComponent();
        }

        private void ManagerPeople_Load(object sender, EventArgs e)
        {
            resetForm();
        }
        private void resetForm()
        {
            peopleDataGridView.DataSource = User.GetAllUsers();

            peopleDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            lblNumberOfRecords.Text = peopleDataGridView.RowCount.ToString();
            cbFilterRows.SelectedIndex = 0;
        }

    }
}
