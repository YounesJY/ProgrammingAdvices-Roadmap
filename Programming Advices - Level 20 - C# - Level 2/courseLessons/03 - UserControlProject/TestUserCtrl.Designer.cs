namespace MyFirstUserControlProject
{
    partial class TestUserCtrl
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
            this.ctrlSimpleCalculator = new MyFirstUserControlProject.ctrlSimpleCalculator();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(355, 335);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Show Result";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ctrlSimpleCalculator
            // 
            this.ctrlSimpleCalculator.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctrlSimpleCalculator.Location = new System.Drawing.Point(270, 39);
            this.ctrlSimpleCalculator.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ctrlSimpleCalculator.Name = "ctrlSimpleCalculator";
            this.ctrlSimpleCalculator.OperationResult = "??5";
            this.ctrlSimpleCalculator.Size = new System.Drawing.Size(274, 234);
            this.ctrlSimpleCalculator.TabIndex = 2;
            // 
            // TestUserCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.ctrlSimpleCalculator);
            this.Controls.Add(this.button1);
            this.Name = "TestUserCtrl";
            this.Text = "TestUserCtrl";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private ctrlSimpleCalculator ctrlSimpleCalculator;
    }
}