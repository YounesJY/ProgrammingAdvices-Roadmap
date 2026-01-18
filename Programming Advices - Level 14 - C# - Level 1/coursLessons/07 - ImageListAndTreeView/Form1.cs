using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageListAndTreeView
{
    public partial class Form : System.Windows.Forms.Form
    {
        public Form()
        {
            InitializeComponent();
        }


        private void treeView_DoubleClick(object sender, EventArgs e)
        {
            treeView.SelectedNode.Text = treeView.SelectedNode.Text.ToUpper();
            treeView.SelectedNode.ImageIndex = treeView.SelectedNode.SelectedImageIndex = (treeView.SelectedNode.SelectedImageIndex == 0) ? 1 : 0;



        }
    }
}
