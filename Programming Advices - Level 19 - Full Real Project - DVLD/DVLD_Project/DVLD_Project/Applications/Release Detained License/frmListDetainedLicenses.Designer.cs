namespace DVLD_Project.Applications.Release_Detained_License
{
    partial class frmListDetainedLicenses
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnReleaseDetainedLicense = new System.Windows.Forms.Button();
            this.btnDetainLicense = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.tsmiShowPersonLicenseHistory = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiReleaseDetainedLicense = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiShowLicenseDetails = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsDetainedApplications = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiShowPersonDetails = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.lblFilter = new System.Windows.Forms.Label();
            this.lblNumberOfRecords = new System.Windows.Forms.Label();
            this.lblNumberOfRecordsLabel = new System.Windows.Forms.Label();
            this.dgvDetainedLicenses = new System.Windows.Forms.DataGridView();
            this.cbApplicationStatus = new System.Windows.Forms.ComboBox();
            this.mtbFilterSearch = new System.Windows.Forms.MaskedTextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.cbFilterRows = new System.Windows.Forms.ComboBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.cmsDetainedApplications.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetainedLicenses)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnReleaseDetainedLicense
            // 
            this.btnReleaseDetainedLicense.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReleaseDetainedLicense.Image = global::DVLD_Project.Properties.Resources.Release_Detained_License_32;
            this.btnReleaseDetainedLicense.Location = new System.Drawing.Point(794, 174);
            this.btnReleaseDetainedLicense.Name = "btnReleaseDetainedLicense";
            this.btnReleaseDetainedLicense.Size = new System.Drawing.Size(43, 42);
            this.btnReleaseDetainedLicense.TabIndex = 162;
            this.btnReleaseDetainedLicense.UseVisualStyleBackColor = true;
            this.btnReleaseDetainedLicense.Click += new System.EventHandler(this.btnReleaseDetainedLicense_Click);
            // 
            // btnDetainLicense
            // 
            this.btnDetainLicense.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDetainLicense.Image = global::DVLD_Project.Properties.Resources.Detain_32;
            this.btnDetainLicense.Location = new System.Drawing.Point(855, 174);
            this.btnDetainLicense.Name = "btnDetainLicense";
            this.btnDetainLicense.Size = new System.Drawing.Size(43, 42);
            this.btnDetainLicense.TabIndex = 161;
            this.btnDetainLicense.UseVisualStyleBackColor = true;
            this.btnDetainLicense.Click += new System.EventHandler(this.btnDetainLicense_Click);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClose.Image = global::DVLD_Project.Properties.Resources.Close_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(772, 423);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(126, 37);
            this.btnClose.TabIndex = 173;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // tsmiShowPersonLicenseHistory
            // 
            this.tsmiShowPersonLicenseHistory.Image = global::DVLD_Project.Properties.Resources.PersonLicenseHistory_32;
            this.tsmiShowPersonLicenseHistory.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiShowPersonLicenseHistory.Name = "tsmiShowPersonLicenseHistory";
            this.tsmiShowPersonLicenseHistory.Size = new System.Drawing.Size(241, 38);
            this.tsmiShowPersonLicenseHistory.Text = "Show Person License History";
            this.tsmiShowPersonLicenseHistory.Click += new System.EventHandler(this.tsmiShowPersonLicenseHistory_Click);
            // 
            // tsmiReleaseDetainedLicense
            // 
            this.tsmiReleaseDetainedLicense.Image = global::DVLD_Project.Properties.Resources.Release_Detained_License_32;
            this.tsmiReleaseDetainedLicense.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiReleaseDetainedLicense.Name = "tsmiReleaseDetainedLicense";
            this.tsmiReleaseDetainedLicense.Size = new System.Drawing.Size(241, 38);
            this.tsmiReleaseDetainedLicense.Text = "Release Detained License";
            this.tsmiReleaseDetainedLicense.Click += new System.EventHandler(this.tsmiReleaseDetainedLicense_Click);
            // 
            // tsmiShowLicenseDetails
            // 
            this.tsmiShowLicenseDetails.Image = global::DVLD_Project.Properties.Resources.License_View_32;
            this.tsmiShowLicenseDetails.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiShowLicenseDetails.Name = "tsmiShowLicenseDetails";
            this.tsmiShowLicenseDetails.Size = new System.Drawing.Size(241, 38);
            this.tsmiShowLicenseDetails.Text = "Show &Local License";
            this.tsmiShowLicenseDetails.Click += new System.EventHandler(this.tsmiShowLicenseDetails_Click);
            // 
            // cmsDetainedApplications
            // 
            this.cmsDetainedApplications.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiShowPersonDetails,
            this.toolStripSeparator3,
            this.tsmiShowLicenseDetails,
            this.tsmiShowPersonLicenseHistory,
            this.toolStripSeparator6,
            this.tsmiReleaseDetainedLicense});
            this.cmsDetainedApplications.Name = "contextMenuStrip1";
            this.cmsDetainedApplications.Size = new System.Drawing.Size(242, 190);
            // 
            // tsmiShowPersonDetails
            // 
            this.tsmiShowPersonDetails.Image = global::DVLD_Project.Properties.Resources.PersonDetails_32;
            this.tsmiShowPersonDetails.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiShowPersonDetails.Name = "tsmiShowPersonDetails";
            this.tsmiShowPersonDetails.Size = new System.Drawing.Size(241, 38);
            this.tsmiShowPersonDetails.Text = "&Show Person Details";
            this.tsmiShowPersonDetails.Click += new System.EventHandler(this.tsmiShowPersonDetails_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(238, 6);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(238, 6);
            // 
            // lblFilter
            // 
            this.lblFilter.AutoSize = true;
            this.lblFilter.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFilter.Location = new System.Drawing.Point(5, 189);
            this.lblFilter.Name = "lblFilter";
            this.lblFilter.Size = new System.Drawing.Size(68, 19);
            this.lblFilter.TabIndex = 169;
            this.lblFilter.Text = "Filter By";
            // 
            // lblNumberOfRecords
            // 
            this.lblNumberOfRecords.AutoSize = true;
            this.lblNumberOfRecords.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumberOfRecords.Location = new System.Drawing.Point(93, 441);
            this.lblNumberOfRecords.Name = "lblNumberOfRecords";
            this.lblNumberOfRecords.Size = new System.Drawing.Size(16, 19);
            this.lblNumberOfRecords.TabIndex = 168;
            this.lblNumberOfRecords.Text = "?";
            // 
            // lblNumberOfRecordsLabel
            // 
            this.lblNumberOfRecordsLabel.AutoSize = true;
            this.lblNumberOfRecordsLabel.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumberOfRecordsLabel.Location = new System.Drawing.Point(5, 441);
            this.lblNumberOfRecordsLabel.Name = "lblNumberOfRecordsLabel";
            this.lblNumberOfRecordsLabel.Size = new System.Drawing.Size(86, 19);
            this.lblNumberOfRecordsLabel.TabIndex = 167;
            this.lblNumberOfRecordsLabel.Text = "# Records: ";
            // 
            // dgvDetainedLicenses
            // 
            this.dgvDetainedLicenses.AllowUserToAddRows = false;
            this.dgvDetainedLicenses.AllowUserToDeleteRows = false;
            this.dgvDetainedLicenses.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetainedLicenses.ContextMenuStrip = this.cmsDetainedApplications;
            this.dgvDetainedLicenses.Location = new System.Drawing.Point(5, 222);
            this.dgvDetainedLicenses.Name = "dgvDetainedLicenses";
            this.dgvDetainedLicenses.Size = new System.Drawing.Size(893, 198);
            this.dgvDetainedLicenses.TabIndex = 165;
            // 
            // cbApplicationStatus
            // 
            this.cbApplicationStatus.AutoCompleteCustomSource.AddRange(new string[] {
            "All",
            "Active",
            "Inactive"});
            this.cbApplicationStatus.FormattingEnabled = true;
            this.cbApplicationStatus.Items.AddRange(new object[] {
            "All",
            "Yes",
            "No"});
            this.cbApplicationStatus.Location = new System.Drawing.Point(258, 187);
            this.cbApplicationStatus.Name = "cbApplicationStatus";
            this.cbApplicationStatus.Size = new System.Drawing.Size(121, 21);
            this.cbApplicationStatus.TabIndex = 174;
            this.cbApplicationStatus.SelectedIndexChanged += new System.EventHandler(this.cbApplicationStatus_SelectedIndexChanged);
            // 
            // mtbFilterSearch
            // 
            this.mtbFilterSearch.Location = new System.Drawing.Point(258, 188);
            this.mtbFilterSearch.Name = "mtbFilterSearch";
            this.mtbFilterSearch.Size = new System.Drawing.Size(121, 20);
            this.mtbFilterSearch.TabIndex = 171;
            this.mtbFilterSearch.TextChanged += new System.EventHandler(this.mtbFilterSearch_TextChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DVLD_Project.Properties.Resources.Detain_512;
            this.pictureBox1.Location = new System.Drawing.Point(403, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(150, 134);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 164;
            this.pictureBox1.TabStop = false;
            // 
            // cbFilterRows
            // 
            this.cbFilterRows.FormattingEnabled = true;
            this.cbFilterRows.Items.AddRange(new object[] {
            "None",
            "DetainID",
            "FullName",
            "NationalNumber",
            "IsReleased",
            "ReleaseApplicationID"});
            this.cbFilterRows.Location = new System.Drawing.Point(113, 187);
            this.cbFilterRows.Name = "cbFilterRows";
            this.cbFilterRows.Size = new System.Drawing.Size(121, 21);
            this.cbFilterRows.TabIndex = 170;
            this.cbFilterRows.SelectedIndexChanged += new System.EventHandler(this.cbFilterRows_SelectedIndexChanged);
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblTitle.Location = new System.Drawing.Point(167, 140);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(568, 39);
            this.lblTitle.TabIndex = 175;
            this.lblTitle.Text = "List Detained Licenses";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmListDetainedLicenses
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(902, 465);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblFilter);
            this.Controls.Add(this.lblNumberOfRecords);
            this.Controls.Add(this.lblNumberOfRecordsLabel);
            this.Controls.Add(this.dgvDetainedLicenses);
            this.Controls.Add(this.mtbFilterSearch);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.cbFilterRows);
            this.Controls.Add(this.btnReleaseDetainedLicense);
            this.Controls.Add(this.btnDetainLicense);
            this.Controls.Add(this.cbApplicationStatus);
            this.Name = "frmListDetainedLicenses";
            this.Text = "frmListDetainedLicenses";
            this.Load += new System.EventHandler(this.frmListDetainedLicenses_Load);
            this.cmsDetainedApplications.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetainedLicenses)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnReleaseDetainedLicense;
        private System.Windows.Forms.Button btnDetainLicense;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ToolStripMenuItem tsmiShowPersonLicenseHistory;
        private System.Windows.Forms.ToolStripMenuItem tsmiReleaseDetainedLicense;
        private System.Windows.Forms.ToolStripMenuItem tsmiShowLicenseDetails;
        private System.Windows.Forms.ContextMenuStrip cmsDetainedApplications;
        private System.Windows.Forms.ToolStripMenuItem tsmiShowPersonDetails;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.Label lblFilter;
        private System.Windows.Forms.Label lblNumberOfRecords;
        private System.Windows.Forms.Label lblNumberOfRecordsLabel;
        private System.Windows.Forms.DataGridView dgvDetainedLicenses;
        private System.Windows.Forms.ComboBox cbApplicationStatus;
        private System.Windows.Forms.MaskedTextBox mtbFilterSearch;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ComboBox cbFilterRows;
        private System.Windows.Forms.Label lblTitle;
    }
}