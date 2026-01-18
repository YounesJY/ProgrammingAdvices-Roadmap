using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ContactsBusinessLayer;

namespace Contacts
{
    public partial class frmAddEditContact : Form
    {
        struct CountryItem
        {
            public string Text;
            public int Value;
            public CountryItem(string Text, int Value)
            {
                this.Text = Text;
                this.Value = Value;
            }
        }
        public enum enMode { AddNew = -1, Update = 0 };


        int _ContactID;
        clsContact _Contact;
        private enMode _Mode;


        public frmAddEditContact(int ContactID)
        {
            InitializeComponent();

            _ContactID = ContactID;
            _Mode = ((_ContactID == -1) ? enMode.AddNew : enMode.Update);
        }
        private void frmContact_Load(object sender, EventArgs e)
        {
            _LoadData();
        }


        private void _LoadData()
        {
            _FillCountriesInComoboBox();
            cbCountry.SelectedIndex = 0;

            // In this case [Add new contact], we we don't fill the other fields because it's a new contact
            // and it doesn't have any values yet, so we stop LOADING data here,
            if (_Mode == enMode.AddNew)
            {
                lblMode.Text = "Add New Contact";
                _Contact = new clsContact();
                return;
            }

            // Otherwise, we look for the intended contact, and we continue filling the other fields
            _Contact = clsContact.Find(_ContactID);

            // [In case of conccurency access], so this is a double check (not reallt neccessaty for our case) ?!
            if (_Contact == null)
            {
                MessageBox.Show("This form will be closed because No Contact with ID = " + _ContactID);
                this.Close();
                return;
            }

            lblMode.Text = $"Edit Contact ID = {_ContactID}";
            lblContactID.Text = _ContactID.ToString();
            txtFirstName.Text = _Contact.FirstName;
            txtLastName.Text = _Contact.LastName;
            txtEmail.Text = _Contact.Email;
            txtPhone.Text = _Contact.Phone;
            // this will select the country in the combobox.
            cbCountry.SelectedIndex = cbCountry.FindString(clsCountry.Find(_Contact.CountryID).CountryName);
            txtAddress.Text = _Contact.Address;
            dtpDateOfBirth.Value = _Contact.DateOfBirth;

            if (!String.IsNullOrEmpty(_Contact.ImagePath))
                pictureBox1.Load(_Contact.ImagePath);

            llblRemoveImage.Visible = !String.IsNullOrEmpty(_Contact.ImagePath);
        }
        private void _FillCountriesInComoboBox()
        {
            DataTable dtCountries = clsCountry.GetAllCountries();

            foreach (DataRow row in dtCountries.Rows)
                cbCountry.Items.Add(row["CountryName"]);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _Contact.FirstName = txtFirstName.Text;
            _Contact.LastName = txtLastName.Text;
            _Contact.Email = txtEmail.Text;
            _Contact.Phone = txtPhone.Text;
            _Contact.Address = txtAddress.Text;
            _Contact.DateOfBirth = dtpDateOfBirth.Value;
            _Contact.CountryID = clsCountry.Find(cbCountry.Text).ID;

            if (!String.IsNullOrEmpty(pictureBox1.ImageLocation))
                _Contact.ImagePath = pictureBox1.ImageLocation;
            else
                _Contact.ImagePath = string.Empty;

            if (_Contact.Save())
                MessageBox.Show("Data Saved Successfully.");
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.");

            _Mode = enMode.Update;
            lblMode.Text = $"Edit Contact ID = {_Contact.ID}";
            lblContactID.Text = _Contact.ID.ToString();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void llOpenFileDialog_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Process the selected file
                string selectedFilePath = openFileDialog1.FileName;
                //MessageBox.Show("Selected Image is:" + selectedFilePath);

                pictureBox1.Load(selectedFilePath);
                // ...
            }
        }
        private void llRemoveImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pictureBox1.ImageLocation = string.Empty;
            llblRemoveImage.Visible = false;
        }
    }
}
