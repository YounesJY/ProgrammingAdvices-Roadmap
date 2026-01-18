namespace ImageExercice
{
    partial class formChooseImage
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
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.imageLabel = new System.Windows.Forms.Label();
            this.cbList = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbList);
            this.groupBox1.Controls.Add(this.pictureBox);
            this.groupBox1.Controls.Add(this.imageLabel);
            this.groupBox1.Location = new System.Drawing.Point(102, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(637, 438);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // pictureBox
            // 
            this.pictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox.Image = global::chooseImage.Properties.Resources.boyImage;
            this.pictureBox.Location = new System.Drawing.Point(63, 66);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(498, 308);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox.TabIndex = 1;
            this.pictureBox.TabStop = false;
            // 
            // imageLabel
            // 
            this.imageLabel.Font = new System.Drawing.Font("Segoe UI Black", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.imageLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.imageLabel.Location = new System.Drawing.Point(63, 16);
            this.imageLabel.Name = "imageLabel";
            this.imageLabel.Size = new System.Drawing.Size(498, 47);
            this.imageLabel.TabIndex = 2;
            this.imageLabel.Text = "BOY";
            this.imageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbList
            // 
            this.cbList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbList.FormattingEnabled = true;
            this.cbList.Items.AddRange(new object[] {
            "Boy",
            "Girl",
            "Books",
            "Pen"});
            this.cbList.Location = new System.Drawing.Point(229, 380);
            this.cbList.Name = "cbList";
            this.cbList.Size = new System.Drawing.Size(167, 21);
            this.cbList.TabIndex = 3;
            this.cbList.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // formChooseImage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.groupBox1);
            this.Name = "formChooseImage";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label imageLabel;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.ComboBox cbList;
    }
}

