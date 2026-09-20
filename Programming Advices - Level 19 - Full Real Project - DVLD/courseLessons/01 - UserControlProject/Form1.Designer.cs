namespace MyFirstUserControlProject
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
            this.button1 = new System.Windows.Forms.Button();
            this.ctrlSimpleCalc1 = new MyFirstUserControlProject.ctrlSimpleCalc();
            this.ctrlSimpleCalc2 = new MyFirstUserControlProject.ctrlSimpleCalc();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(675, 151);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ctrlSimpleCalc1
            // 
            this.ctrlSimpleCalc1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctrlSimpleCalc1.Location = new System.Drawing.Point(24, 14);
            this.ctrlSimpleCalc1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ctrlSimpleCalc1.Name = "ctrlSimpleCalc1";
            this.ctrlSimpleCalc1.Size = new System.Drawing.Size(274, 234);
            this.ctrlSimpleCalc1.TabIndex = 1;
            // 
            // ctrlSimpleCalc2
            // 
            this.ctrlSimpleCalc2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctrlSimpleCalc2.Location = new System.Drawing.Point(266, 72);
            this.ctrlSimpleCalc2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ctrlSimpleCalc2.Name = "ctrlSimpleCalc2";
            this.ctrlSimpleCalc2.Size = new System.Drawing.Size(283, 219);
            this.ctrlSimpleCalc2.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(305, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "label1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ctrlSimpleCalc2);
            this.Controls.Add(this.ctrlSimpleCalc1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private ctrlSimpleCalc ctrlSimpleCalc1;
        private ctrlSimpleCalc ctrlSimpleCalc2;
        private System.Windows.Forms.Label label1;
    }
}