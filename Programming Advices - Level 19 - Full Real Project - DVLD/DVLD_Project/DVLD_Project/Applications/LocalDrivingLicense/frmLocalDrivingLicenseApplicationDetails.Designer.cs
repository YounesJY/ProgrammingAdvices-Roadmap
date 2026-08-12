namespace DVLD_Project.Applications.LocalDrivingLicense
{
    partial class frmLocalDrivingLicenseApplicationDetails
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
            this.btnClose = new System.Windows.Forms.Button();
            this.ctrlLocalDrivingApplicationDetails = new DVLD_Project.Applications.LocalDrivingLicense.Controls.ctrlLocalDrivingApplicationDetails();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClose.Image = global::DVLD_Project.Properties.Resources.Close_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(697, 367);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(126, 37);
            this.btnClose.TabIndex = 18;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // ctrlLocalDrivingApplicationDetails
            // 
            this.ctrlLocalDrivingApplicationDetails.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctrlLocalDrivingApplicationDetails.Location = new System.Drawing.Point(0, 0);
            this.ctrlLocalDrivingApplicationDetails.Name = "ctrlLocalDrivingApplicationDetails";
            this.ctrlLocalDrivingApplicationDetails.Size = new System.Drawing.Size(826, 359);
            this.ctrlLocalDrivingApplicationDetails.TabIndex = 19;
            // 
            // frmLocalDrivingLicenseApplicationDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(826, 410);
            this.Controls.Add(this.ctrlLocalDrivingApplicationDetails);
            this.Controls.Add(this.btnClose);
            this.Name = "frmLocalDrivingLicenseApplicationDetails";
            this.Text = "frmLocalDrivingLicenseApplicationDetails";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmLocalDrivingLicenseApplicationDetails_FormClosing);
            this.Load += new System.EventHandler(this.frmLocalDrivingLicenseApplicationDetails_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnClose;
        private Controls.ctrlLocalDrivingApplicationDetails ctrlLocalDrivingApplicationDetails;
    }
}