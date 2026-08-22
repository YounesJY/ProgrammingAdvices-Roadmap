namespace DVLD_Project.Drivers
{
    partial class frmListDrivers
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pbDriverImage = new System.Windows.Forms.PictureBox();
            this.cbFilterRows = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvDrivers = new System.Windows.Forms.DataGridView();
            this.lblNumberOfRecords = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.mtbFilterSearch = new System.Windows.Forms.MaskedTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbDriverImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDrivers)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblTitle.Location = new System.Drawing.Point(213, 163);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(446, 39);
            this.lblTitle.TabIndex = 125;
            this.lblTitle.Text = "Manage Drivers";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pbDriverImage
            // 
            this.pbDriverImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbDriverImage.Image = global::DVLD_Project.Properties.Resources.Driver_Main;
            this.pbDriverImage.InitialImage = null;
            this.pbDriverImage.Location = new System.Drawing.Point(296, 4);
            this.pbDriverImage.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pbDriverImage.Name = "pbDriverImage";
            this.pbDriverImage.Size = new System.Drawing.Size(280, 159);
            this.pbDriverImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbDriverImage.TabIndex = 124;
            this.pbDriverImage.TabStop = false;
            // 
            // cbFilterRows
            // 
            this.cbFilterRows.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFilterRows.FormattingEnabled = true;
            this.cbFilterRows.Items.AddRange(new object[] {
            "None",
            "DriverID",
            "PersonID",
            "FullName",
            "NationalNumber"});
            this.cbFilterRows.Location = new System.Drawing.Point(113, 219);
            this.cbFilterRows.Name = "cbFilterRows";
            this.cbFilterRows.Size = new System.Drawing.Size(210, 21);
            this.cbFilterRows.TabIndex = 130;
            this.cbFilterRows.SelectedIndexChanged += new System.EventHandler(this.cbFilterBy_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 220);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 20);
            this.label1.TabIndex = 128;
            this.label1.Text = "Filter By:";
            // 
            // dgvDrivers
            // 
            this.dgvDrivers.AllowUserToAddRows = false;
            this.dgvDrivers.AllowUserToDeleteRows = false;
            this.dgvDrivers.AllowUserToResizeRows = false;
            this.dgvDrivers.BackgroundColor = System.Drawing.Color.White;
            this.dgvDrivers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDrivers.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvDrivers.Location = new System.Drawing.Point(8, 247);
            this.dgvDrivers.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dgvDrivers.MultiSelect = false;
            this.dgvDrivers.Name = "dgvDrivers";
            this.dgvDrivers.ReadOnly = true;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDrivers.RowHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDrivers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDrivers.Size = new System.Drawing.Size(827, 234);
            this.dgvDrivers.TabIndex = 127;
            this.dgvDrivers.TabStop = false;
            // 
            // lblNumberOfRecords
            // 
            this.lblNumberOfRecords.AutoSize = true;
            this.lblNumberOfRecords.Location = new System.Drawing.Point(124, 514);
            this.lblNumberOfRecords.Name = "lblNumberOfRecords";
            this.lblNumberOfRecords.Size = new System.Drawing.Size(19, 13);
            this.lblNumberOfRecords.TabIndex = 133;
            this.lblNumberOfRecords.Text = "??";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(8, 507);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 20);
            this.label2.TabIndex = 132;
            this.label2.Text = "# Records:";
            // 
            // btnClose
            // 
            this.btnClose.AutoEllipsis = true;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Image = global::DVLD_Project.Properties.Resources.Close_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(700, 491);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(135, 36);
            this.btnClose.TabIndex = 131;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // mtbFilterSearch
            // 
            this.mtbFilterSearch.Location = new System.Drawing.Point(348, 220);
            this.mtbFilterSearch.Name = "mtbFilterSearch";
            this.mtbFilterSearch.Size = new System.Drawing.Size(210, 20);
            this.mtbFilterSearch.TabIndex = 134;
            this.mtbFilterSearch.TextChanged += new System.EventHandler(this.mtbFilterSearch_TextChanged);
            // 
            // frmListDrivers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(842, 536);
            this.Controls.Add(this.mtbFilterSearch);
            this.Controls.Add(this.lblNumberOfRecords);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.cbFilterRows);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvDrivers);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.pbDriverImage);
            this.Name = "frmListDrivers";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmListDrivers";
            this.Load += new System.EventHandler(this.frmListDrivers_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbDriverImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDrivers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.PictureBox pbDriverImage;
        private System.Windows.Forms.ComboBox cbFilterRows;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvDrivers;
        private System.Windows.Forms.Label lblNumberOfRecords;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.MaskedTextBox mtbFilterSearch;
    }
}