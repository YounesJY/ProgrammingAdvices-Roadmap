namespace DVLD_Project.Users
{
    partial class frmUserInfo
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
            this.ctrlUserCard = new DVLD_Project.Users.Controls.ctrlUserCard();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("JetBrainsMonoNL NFM", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label1.Location = new System.Drawing.Point(239, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(286, 49);
            this.label1.TabIndex = 2;
            this.label1.Text = "User Details";
            // 
            // ctrlUserCard
            // 
            this.ctrlUserCard.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ctrlUserCard.Location = new System.Drawing.Point(0, 60);
            this.ctrlUserCard.Name = "ctrlUserCard";
            this.ctrlUserCard.Size = new System.Drawing.Size(765, 341);
            this.ctrlUserCard.TabIndex = 3;
            // 
            // frmUserInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(765, 401);
            this.Controls.Add(this.ctrlUserCard);
            this.Controls.Add(this.label1);
            this.Name = "frmUserInfo";
            this.Text = "User Info";
            this.Load += new System.EventHandler(this.frmUserInfo_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private Controls.ctrlUserCard ctrlUserCard;
    }
}