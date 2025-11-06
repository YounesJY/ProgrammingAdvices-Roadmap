namespace ImageExercice
{
    partial class Form1
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbImageGirl = new System.Windows.Forms.RadioButton();
            this.rbImageBoy = new System.Windows.Forms.RadioButton();
            this.rbImagePen = new System.Windows.Forms.RadioButton();
            this.rbImageBooks = new System.Windows.Forms.RadioButton();
            this.imageLabel = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pictureBox);
            this.groupBox1.Controls.Add(this.groupBox2);
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
            this.pictureBox.Image = global::ImageExercice.Properties.Resources.boyImage;
            this.pictureBox.Location = new System.Drawing.Point(63, 66);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(498, 308);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox.TabIndex = 1;
            this.pictureBox.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbImageGirl);
            this.groupBox2.Controls.Add(this.rbImageBoy);
            this.groupBox2.Controls.Add(this.rbImagePen);
            this.groupBox2.Controls.Add(this.rbImageBooks);
            this.groupBox2.Location = new System.Drawing.Point(63, 380);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(498, 44);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            // 
            // rbImageGirl
            // 
            this.rbImageGirl.AutoSize = true;
            this.rbImageGirl.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbImageGirl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.rbImageGirl.Location = new System.Drawing.Point(160, 19);
            this.rbImageGirl.Name = "rbImageGirl";
            this.rbImageGirl.Size = new System.Drawing.Size(48, 21);
            this.rbImageGirl.TabIndex = 3;
            this.rbImageGirl.TabStop = true;
            this.rbImageGirl.Tag = "Girl";
            this.rbImageGirl.Text = "Girl";
            this.rbImageGirl.UseVisualStyleBackColor = true;
            this.rbImageGirl.CheckedChanged += new System.EventHandler(this.rbImageGirl_CheckedChanged);
            // 
            // rbImageBoy
            // 
            this.rbImageBoy.AutoSize = true;
            this.rbImageBoy.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbImageBoy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.rbImageBoy.Location = new System.Drawing.Point(40, 19);
            this.rbImageBoy.Name = "rbImageBoy";
            this.rbImageBoy.Size = new System.Drawing.Size(49, 21);
            this.rbImageBoy.TabIndex = 2;
            this.rbImageBoy.TabStop = true;
            this.rbImageBoy.Tag = "Boy";
            this.rbImageBoy.Text = "Boy";
            this.rbImageBoy.UseVisualStyleBackColor = true;
            this.rbImageBoy.CheckedChanged += new System.EventHandler(this.rbImageBoy_CheckedChanged);
            // 
            // rbImagePen
            // 
            this.rbImagePen.AutoSize = true;
            this.rbImagePen.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbImagePen.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.rbImagePen.Location = new System.Drawing.Point(408, 19);
            this.rbImagePen.Name = "rbImagePen";
            this.rbImagePen.Size = new System.Drawing.Size(49, 21);
            this.rbImagePen.TabIndex = 1;
            this.rbImagePen.TabStop = true;
            this.rbImagePen.Tag = "Pen";
            this.rbImagePen.Text = "Pen";
            this.rbImagePen.UseVisualStyleBackColor = true;
            this.rbImagePen.CheckedChanged += new System.EventHandler(this.rbImagePen_CheckedChanged);
            // 
            // rbImageBooks
            // 
            this.rbImageBooks.AutoSize = true;
            this.rbImageBooks.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbImageBooks.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.rbImageBooks.Location = new System.Drawing.Point(277, 19);
            this.rbImageBooks.Name = "rbImageBooks";
            this.rbImageBooks.Size = new System.Drawing.Size(63, 21);
            this.rbImageBooks.TabIndex = 0;
            this.rbImageBooks.TabStop = true;
            this.rbImageBooks.Tag = "Books";
            this.rbImageBooks.Text = "Books";
            this.rbImageBooks.UseVisualStyleBackColor = true;
            this.rbImageBooks.CheckedChanged += new System.EventHandler(this.rbImageBooks_CheckedChanged);
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbImageGirl;
        private System.Windows.Forms.RadioButton rbImageBoy;
        private System.Windows.Forms.RadioButton rbImagePen;
        private System.Windows.Forms.RadioButton rbImageBooks;
        private System.Windows.Forms.Label imageLabel;
        private System.Windows.Forms.PictureBox pictureBox;
    }
}

