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

namespace DVLD_Project.People
{
    public partial class frmManagerPeople : Form
    {
        public frmManagerPeople()
        {
            InitializeComponent();
        }

        private void ManagerPeople_Load(object sender, EventArgs e)
        {
            peopleDataGridView.DataSource = Person.GetPeople();
            peopleDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {

            // Check if a row is selected
            if (peopleDataGridView.SelectedRows.Count > 0)
            {
                // Get the selected row
                DataGridViewRow selectedRow = peopleDataGridView.SelectedRows[0];

                // Get PersonID from the row (assuming PersonID is in column index 0 or use column name)
                int PersonID = Convert.ToInt32(selectedRow.Cells["PersonID"].Value);

                // Open the details form
                new frmPersonDetails( PersonID).ShowDialog();
            }

        }

        private void peopleDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            showDetailsToolStripMenuItem_Click(sender, e);
        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AddNewPerson().ShowDialog();
        }
    }
}
