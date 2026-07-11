namespace DVLD_Project.People
{
    partial class AddNewPerson
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
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.ctrlPersonDetails = new DVLD_Project.People.ctrlPersonDetails();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("JetBrainsMonoNL NF", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label1.Location = new System.Drawing.Point(229, 157);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(314, 47);
            this.label1.TabIndex = 1;
            this.label1.Text = "Add New Person";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DVLD_Project.Properties.Resources.isolated_young_handsome_man_different_poses_white_background_illustration;
            this.pictureBox1.Location = new System.Drawing.Point(271, -18);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(228, 195);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // ctrlPersonDetails
            // 
            this.ctrlPersonDetails.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ctrlPersonDetails.Location = new System.Drawing.Point(0, 198);
            this.ctrlPersonDetails.Name = "ctrlPersonDetails";
            this.ctrlPersonDetails.Size = new System.Drawing.Size(800, 252);
            this.ctrlPersonDetails.TabIndex = 3;
            // 
            // AddNewPerson
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.ctrlPersonDetails);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "AddNewPerson";
            this.Text = "AddNewPerson";
            this.Load += new System.EventHandler(this.AddNewPerson_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private ctrlPersonDetails ctrlPersonDetails;
    }
}