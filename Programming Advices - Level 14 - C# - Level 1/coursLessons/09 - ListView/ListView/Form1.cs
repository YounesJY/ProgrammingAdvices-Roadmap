using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace ListView
{
    public partial class ListViewForm : Form
    {
        enum enImage { girl = 0, boy = 1 };

        public ListViewForm()
        {
            InitializeComponent();
        }
        private void resetFields()
        {
            txtName.Clear();
            txtID.Clear();
            txtID.Focus();
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            ListViewItem listViewItem = new ListViewItem(txtID.Text.Trim());

            if (string.IsNullOrEmpty(txtID.Text) || string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("All fields Must Be Filled !", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

            listViewItem.ImageIndex = (Byte)(rbGenderMale.Checked ? enImage.boy : enImage.boy);
            listViewItem.SubItems.Add(txtName.Text.Trim());
            listView.Items.Add(listViewItem);

            resetFields();
        }
        private void btnRemoveItem_Click(object sender, EventArgs e)
        {
            if (listView.Items.Count != 0)
            {
                if (MessageBox.Show("Are you sure you want  to removed the selected itmes ? ", "Verification", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    foreach (var item in listView.SelectedItems)
                        listView.Items.Remove((ListViewItem)item);
                }
            }
            else
                MessageBox.Show("The list is empty, no items to remove !", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
        private void btnFillWithRandomItems_Click(object sender, EventArgs e)
        {
            for (int i = 1; i <= 10; i++)
            {
                ListViewItem item = new ListViewItem(i.ToString());

                item.ImageIndex = (Byte)(i % 2 == 0 ? enImage.girl : enImage.boy);
                item.SubItems.Add("Person " + i);

                listView.Items.Add(item);
            }
        }

        private void rbListViewDetails_CheckedChanged(object sender, EventArgs e)
        {
            listView.View = View.Details;
        }
        private void rbListViewList_CheckedChanged(object sender, EventArgs e)
        {
            listView.View = View.List;
        }
        private void rbListViewSmallIcons_CheckedChanged(object sender, EventArgs e)
        {
            listView.View = View.SmallIcon;
        }
        private void rbListViewLargeIcons_CheckedChanged(object sender, EventArgs e)
        {
            listView.View = View.LargeIcon;
        }
        private void rbListViewTile_CheckedChanged(object sender, EventArgs e)
        {
            listView.View = View.Tile;
        }

        private void textBox_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrEmpty(((TextBox)sender).Text) || String.IsNullOrWhiteSpace(((TextBox)sender).Text))
            {
                e.Cancel = true;
                ((TextBox)sender).Focus();
                errorProvider.SetError((Control)sender, "This field is requiered !");
            }
            else
            {
                e.Cancel = false;
                errorProvider.SetError((Control)sender, "This field is requiered !");
            }
        }
    }
}
