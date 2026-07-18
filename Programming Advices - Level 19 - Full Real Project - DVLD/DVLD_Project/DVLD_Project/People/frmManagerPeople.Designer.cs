namespace DVLD_Project.People
{
    partial class frmManagerPeople
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
            this.label1 = new System.Windows.Forms.Label();
            this.peopleDataGridView = new System.Windows.Forms.DataGridView();
            this.PeopleContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.addNewPersonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.sendEmailToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.makeAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pbAddPerson = new System.Windows.Forms.PictureBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblNumberOfRecords = new System.Windows.Forms.Label();
            this.lblNumberOfRecordsValue = new System.Windows.Forms.Label();
            this.lblFilter = new System.Windows.Forms.Label();
            this.cbFilterRows = new System.Windows.Forms.ComboBox();
            this.mtbFilterSeach = new System.Windows.Forms.MaskedTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.peopleDataGridView)).BeginInit();
            this.PeopleContextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbAddPerson)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("JetBrainsMonoNL NF", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(348, 165);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(223, 36);
            this.label1.TabIndex = 0;
            this.label1.Text = "Manage People";
            // 
            // peopleDataGridView
            // 
            this.peopleDataGridView.AllowUserToAddRows = false;
            this.peopleDataGridView.AllowUserToDeleteRows = false;
            this.peopleDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.peopleDataGridView.ContextMenuStrip = this.PeopleContextMenuStrip;
            this.peopleDataGridView.Location = new System.Drawing.Point(4, 252);
            this.peopleDataGridView.Name = "peopleDataGridView";
            this.peopleDataGridView.Size = new System.Drawing.Size(841, 198);
            this.peopleDataGridView.TabIndex = 2;
            this.peopleDataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.peopleDataGridView_CellDoubleClick);
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
            // showDetailsToolStripMenuItem
            // 
            this.showDetailsToolStripMenuItem.Name = "showDetailsToolStripMenuItem";
            this.showDetailsToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.showDetailsToolStripMenuItem.Text = "Show Details";
            this.showDetailsToolStripMenuItem.Click += new System.EventHandler(this.showDetailsToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(159, 6);
            // 
            // addNewPersonToolStripMenuItem
            // 
            this.addNewPersonToolStripMenuItem.Name = "addNewPersonToolStripMenuItem";
            this.addNewPersonToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.addNewPersonToolStripMenuItem.Text = "Add New Person";
            this.addNewPersonToolStripMenuItem.Click += new System.EventHandler(this.addNewPersonToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.editToolStripMenuItem.Text = "Edit";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(159, 6);
            // 
            // sendEmailToolStripMenuItem
            // 
            this.sendEmailToolStripMenuItem.Name = "sendEmailToolStripMenuItem";
            this.sendEmailToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.sendEmailToolStripMenuItem.Text = "Send Email";
            this.sendEmailToolStripMenuItem.Click += new System.EventHandler(this.sendEmailToolStripMenuItem_Click);
            // 
            // makeAToolStripMenuItem
            // 
            this.makeAToolStripMenuItem.Name = "makeAToolStripMenuItem";
            this.makeAToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.makeAToolStripMenuItem.Text = "Make A  Call";
            this.makeAToolStripMenuItem.Click += new System.EventHandler(this.makeAToolStripMenuItem_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DVLD_Project.Properties.Resources.Manage_People1;
            this.pictureBox1.Location = new System.Drawing.Point(375, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(169, 158);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // pbAddPerson
            // 
            this.pbAddPerson.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbAddPerson.Image = global::DVLD_Project.Properties.Resources.addPerson;
            this.pbAddPerson.Location = new System.Drawing.Point(802, 200);
            this.pbAddPerson.Name = "pbAddPerson";
            this.pbAddPerson.Size = new System.Drawing.Size(43, 42);
            this.pbAddPerson.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbAddPerson.TabIndex = 3;
            this.pbAddPerson.TabStop = false;
            this.pbAddPerson.Click += new System.EventHandler(this.pbAddPerson_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(875, 452);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 37;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblNumberOfRecords
            // 
            this.lblNumberOfRecords.AutoSize = true;
            this.lblNumberOfRecords.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumberOfRecords.Location = new System.Drawing.Point(0, 456);
            this.lblNumberOfRecords.Name = "lblNumberOfRecords";
            this.lblNumberOfRecords.Size = new System.Drawing.Size(86, 19);
            this.lblNumberOfRecords.TabIndex = 38;
            this.lblNumberOfRecords.Text = "# Records: ";
            // 
            // lblNumberOfRecordsValue
            // 
            this.lblNumberOfRecordsValue.AutoSize = true;
            this.lblNumberOfRecordsValue.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumberOfRecordsValue.Location = new System.Drawing.Point(92, 456);
            this.lblNumberOfRecordsValue.Name = "lblNumberOfRecordsValue";
            this.lblNumberOfRecordsValue.Size = new System.Drawing.Size(16, 19);
            this.lblNumberOfRecordsValue.TabIndex = 39;
            this.lblNumberOfRecordsValue.Text = "?";
            // 
            // lblFilter
            // 
            this.lblFilter.AutoSize = true;
            this.lblFilter.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFilter.Location = new System.Drawing.Point(0, 223);
            this.lblFilter.Name = "lblFilter";
            this.lblFilter.Size = new System.Drawing.Size(68, 19);
            this.lblFilter.TabIndex = 40;
            this.lblFilter.Text = "Filter By";
            // 
            // cbFilterRows
            // 
            this.cbFilterRows.FormattingEnabled = true;
            this.cbFilterRows.Items.AddRange(new object[] {
            "None",
            "PersonID",
            "NationalNumber",
            "FirstName",
            "SecondName",
            "ThirdName",
            "LastName",
            "Gender",
            "Address",
            "Phone",
            "Email",
            "Nationality"});
            this.cbFilterRows.Location = new System.Drawing.Point(74, 221);
            this.cbFilterRows.Name = "cbFilterRows";
            this.cbFilterRows.Size = new System.Drawing.Size(121, 21);
            this.cbFilterRows.TabIndex = 41;
            this.cbFilterRows.SelectedIndexChanged += new System.EventHandler(this.cbFilterRows_SelectedIndexChanged);
            // 
            // mtbFilterSeach
            // 
            this.mtbFilterSeach.Location = new System.Drawing.Point(221, 222);
            this.mtbFilterSeach.Name = "mtbFilterSeach";
            this.mtbFilterSeach.Size = new System.Drawing.Size(121, 20);
            this.mtbFilterSeach.TabIndex = 42;
            this.mtbFilterSeach.TextChanged += new System.EventHandler(this.mtbFilterSeach_TextChanged);
            // 
            // frmManagerPeople
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(851, 486);
            this.Controls.Add(this.mtbFilterSeach);
            this.Controls.Add(this.cbFilterRows);
            this.Controls.Add(this.lblFilter);
            this.Controls.Add(this.lblNumberOfRecordsValue);
            this.Controls.Add(this.lblNumberOfRecords);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.pbAddPerson);
            this.Controls.Add(this.peopleDataGridView);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "frmManagerPeople";
            this.Text = "ManagerPeople";
            this.Load += new System.EventHandler(this.ManagerPeople_Load);
            ((System.ComponentModel.ISupportInitialize)(this.peopleDataGridView)).EndInit();
            this.PeopleContextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbAddPerson)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.DataGridView peopleDataGridView;
        private System.Windows.Forms.ContextMenuStrip PeopleContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem showDetailsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem addNewPersonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem sendEmailToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem makeAToolStripMenuItem;
        private System.Windows.Forms.PictureBox pbAddPerson;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblNumberOfRecords;
        private System.Windows.Forms.Label lblNumberOfRecordsValue;
        private System.Windows.Forms.Label lblFilter;
        private System.Windows.Forms.ComboBox cbFilterRows;
        private System.Windows.Forms.MaskedTextBox mtbFilterSeach;
    }
}