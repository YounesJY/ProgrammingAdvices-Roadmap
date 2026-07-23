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
    public partial class frmFindPerson : Form
    {
        public event Action<object, int> OnPersonSelected;

        public frmFindPerson()
        {
            InitializeComponent();
        }

        private void frmFindPerson_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.OnPersonSelected?.Invoke(this, ctrlPersonCardWithFilters.PersonId);
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
