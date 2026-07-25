namespace DVLD_Project.Users
{
    partial class frmListUsers
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
            this.mtbFilterSearch = new System.Windows.Forms.MaskedTextBox();
            this.lblFilter = new System.Windows.Forms.Label();
            this.lblNumberOfRecords = new System.Windows.Forms.Label();
            this.lblNumberOfRecordsLabel = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.makeAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cbFilterRows = new System.Windows.Forms.ComboBox();
            this.sendEmailToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addNewPersonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.showDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PeopleContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.usersDataGridView = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.pbAddPerson = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.cbUserActiveStatus = new System.Windows.Forms.ComboBox();
            this.PeopleContextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.usersDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbAddPerson)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // mtbFilterSearch
            // 
            this.mtbFilterSearch.Location = new System.Drawing.Point(263, 207);
            this.mtbFilterSearch.Name = "mtbFilterSearch";
            this.mtbFilterSearch.Size = new System.Drawing.Size(121, 20);
            this.mtbFilterSearch.TabIndex = 52;
            this.mtbFilterSearch.TextChanged += new System.EventHandler(this.mtbFilterSearch_TextChanged);
            // 
            // lblFilter
            // 
            this.lblFilter.AutoSize = true;
            this.lblFilter.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFilter.Location = new System.Drawing.Point(-1, 208);
            this.lblFilter.Name = "lblFilter";
            this.lblFilter.Size = new System.Drawing.Size(68, 19);
            this.lblFilter.TabIndex = 50;
            this.lblFilter.Text = "Filter By";
            // 
            // lblNumberOfRecords
            // 
            this.lblNumberOfRecords.AutoSize = true;
            this.lblNumberOfRecords.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumberOfRecords.Location = new System.Drawing.Point(91, 439);
            this.lblNumberOfRecords.Name = "lblNumberOfRecords";
            this.lblNumberOfRecords.Size = new System.Drawing.Size(16, 19);
            this.lblNumberOfRecords.TabIndex = 49;
            this.lblNumberOfRecords.Text = "?";
            // 
            // lblNumberOfRecordsLabel
            // 
            this.lblNumberOfRecordsLabel.AutoSize = true;
            this.lblNumberOfRecordsLabel.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumberOfRecordsLabel.Location = new System.Drawing.Point(-1, 439);
            this.lblNumberOfRecordsLabel.Name = "lblNumberOfRecordsLabel";
            this.lblNumberOfRecordsLabel.Size = new System.Drawing.Size(86, 19);
            this.lblNumberOfRecordsLabel.TabIndex = 48;
            this.lblNumberOfRecordsLabel.Text = "# Records: ";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(636, 435);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 47;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // makeAToolStripMenuItem
            // 
            this.makeAToolStripMenuItem.Name = "makeAToolStripMenuItem";
            this.makeAToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.makeAToolStripMenuItem.Text = "Make A  Call";
            // 
            // cbFilterRows
            // 
            this.cbFilterRows.FormattingEnabled = true;
            this.cbFilterRows.Items.AddRange(new object[] {
            "None",
            "UserID",
            "PersonID",
            "Fullname",
            "isActive"});
            this.cbFilterRows.Location = new System.Drawing.Point(95, 207);
            this.cbFilterRows.Name = "cbFilterRows";
            this.cbFilterRows.Size = new System.Drawing.Size(121, 21);
            this.cbFilterRows.TabIndex = 51;
            this.cbFilterRows.SelectedIndexChanged += new System.EventHandler(this.cbFilterRows_SelectedIndexChanged);
            // 
            // sendEmailToolStripMenuItem
            // 
            this.sendEmailToolStripMenuItem.Name = "sendEmailToolStripMenuItem";
            this.sendEmailToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.sendEmailToolStripMenuItem.Text = "Send Email";
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // addNewPersonToolStripMenuItem
            // 
            this.addNewPersonToolStripMenuItem.Name = "addNewPersonToolStripMenuItem";
            this.addNewPersonToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.addNewPersonToolStripMenuItem.Text = "Add New Person";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(159, 6);
            // 
            // showDetailsToolStripMenuItem
            // 
            this.showDetailsToolStripMenuItem.Name = "showDetailsToolStripMenuItem";
            this.showDetailsToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.showDetailsToolStripMenuItem.Text = "Show Details";
            // 
            // PeopleContextMenuStrip
            // 
            this.PeopleContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showDetailsToolStripMenuItem,
            this.toolStripMenuItem1,
            this.addNewPersonToolStripMenuItem,
            this.editToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.toolStripMenuItem2,
            this.sendEmailToolStripMenuItem,
            this.makeAToolStripMenuItem});
            this.PeopleContextMenuStrip.Name = "PeopleContextMenuStrip";
            this.PeopleContextMenuStrip.Size = new System.Drawing.Size(163, 148);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(159, 6);
            // 
            // usersDataGridView
            // 
            this.usersDataGridView.AllowUserToAddRows = false;
            this.usersDataGridView.AllowUserToDeleteRows = false;
            this.usersDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.usersDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.usersDataGridView.ContextMenuStrip = this.PeopleContextMenuStrip;
            this.usersDataGridView.Location = new System.Drawing.Point(3, 234);
            this.usersDataGridView.Name = "usersDataGridView";
            this.usersDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.usersDataGridView.Size = new System.Drawing.Size(708, 198);
            this.usersDataGridView.TabIndex = 45;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("JetBrainsMonoNL NF", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(311, 153);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(207, 36);
            this.label1.TabIndex = 43;
            this.label1.Text = "Manage Users";
            // 
            // pbAddPerson
            // 
            this.pbAddPerson.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbAddPerson.Image = global::DVLD_Project.Properties.Resources.addnewUser;
            this.pbAddPerson.Location = new System.Drawing.Point(668, 186);
            this.pbAddPerson.Name = "pbAddPerson";
            this.pbAddPerson.Size = new System.Drawing.Size(43, 42);
            this.pbAddPerson.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbAddPerson.TabIndex = 46;
            this.pbAddPerson.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DVLD_Project.Properties.Resources.follow_me_social_business_theme_design;
            this.pictureBox1.Location = new System.Drawing.Point(346, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(151, 147);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 44;
            this.pictureBox1.TabStop = false;
            // 
            // cbUserActiveStatus
            // 
            this.cbUserActiveStatus.AutoCompleteCustomSource.AddRange(new string[] {
            "All",
            "Active",
            "Inactive"});
            this.cbUserActiveStatus.FormattingEnabled = true;
            this.cbUserActiveStatus.Items.AddRange(new object[] {
            "All",
            "Yes",
            "No"});
            this.cbUserActiveStatus.Location = new System.Drawing.Point(263, 206);
            this.cbUserActiveStatus.Name = "cbUserActiveStatus";
            this.cbUserActiveStatus.Size = new System.Drawing.Size(121, 21);
            this.cbUserActiveStatus.TabIndex = 53;
            // 
            // frmListUsers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(716, 464);
            this.Controls.Add(this.lblFilter);
            this.Controls.Add(this.lblNumberOfRecords);
            this.Controls.Add(this.lblNumberOfRecordsLabel);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.pbAddPerson);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.cbFilterRows);
            this.Controls.Add(this.usersDataGridView);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbUserActiveStatus);
            this.Controls.Add(this.mtbFilterSearch);
            this.Name = "frmListUsers";
            this.Text = "frmListUsers";
            this.Load += new System.EventHandler(this.ManagerUsers_Load);
            this.PeopleContextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.usersDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbAddPerson)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MaskedTextBox mtbFilterSearch;
        private System.Windows.Forms.Label lblFilter;
        private System.Windows.Forms.Label lblNumberOfRecords;
        private System.Windows.Forms.Label lblNumberOfRecordsLabel;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.PictureBox pbAddPerson;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStripMenuItem makeAToolStripMenuItem;
        private System.Windows.Forms.ComboBox cbFilterRows;
        private System.Windows.Forms.ToolStripMenuItem sendEmailToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addNewPersonToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem showDetailsToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip PeopleContextMenuStrip;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.DataGridView usersDataGridView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbUserActiveStatus;
    }
}