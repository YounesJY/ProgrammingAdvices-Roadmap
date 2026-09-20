using System;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        private int _PersonID;

        /*
            The most professional & recommended way is to use Ctor (since From Is-A Class) to pass data between objects or via a setter.
        other same behavior can be achieved via less recommended ways such global/static vars ... ?
        */

        public Form2(int PersonID)
        {
            InitializeComponent();
            _PersonID = PersonID;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            lblPersonID.Text = _PersonID.ToString();
        }
    }
}
