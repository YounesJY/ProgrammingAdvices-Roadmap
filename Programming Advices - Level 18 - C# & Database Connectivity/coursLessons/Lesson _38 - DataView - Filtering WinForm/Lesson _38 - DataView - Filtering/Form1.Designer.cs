namespace Lesson__38___DataView___Filtering
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
            this.components = new System.ComponentModel.Container();
            this.lblRowFilter = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.toolTipRowFilter = new System.Windows.Forms.ToolTip(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.dgvEmployees = new System.Windows.Forms.DataGridView();
            this.txtRowFilter = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmployees)).BeginInit();
            this.SuspendLayout();
            // 
            // lblRowFilter
            // 
            this.lblRowFilter.AutoSize = true;
            this.lblRowFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.75F, System.Drawing.FontStyle.Bold);
            this.lblRowFilter.Location = new System.Drawing.Point(178, 9);
            this.lblRowFilter.Name = "lblRowFilter";
            this.lblRowFilter.Size = new System.Drawing.Size(167, 29);
            this.lblRowFilter.TabIndex = 2;
            this.lblRowFilter.Text = "Filter Criteria";
            this.toolTipRowFilter.SetToolTip(this.lblRowFilter, "Tip: Press Enter to apply filter | Example: Country = \'Jordan\'");
            // 
            // panel1
            // 
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.Controls.Add(this.dgvEmployees);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 67);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(554, 278);
            this.panel1.TabIndex = 4;
            // 
            // toolTipRowFilter
            // 
            this.toolTipRowFilter.ToolTipTitle = "Tip: Press Enter to apply filter | Example: Country = \'Jordan\'";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.txtRowFilter);
            this.panel2.Location = new System.Drawing.Point(0, 38);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(552, 23);
            this.panel2.TabIndex = 5;
            // 
            // dgvEmployees
            // 
            this.dgvEmployees.AllowUserToOrderColumns = true;
            this.dgvEmployees.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvEmployees.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvEmployees.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvEmployees.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEmployees.Location = new System.Drawing.Point(2, 0);
            this.dgvEmployees.Name = "dgvEmployees";
            this.dgvEmployees.Size = new System.Drawing.Size(551, 278);
            this.dgvEmployees.TabIndex = 8;
            // 
            // txtRowFilter
            // 
            this.txtRowFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtRowFilter.Location = new System.Drawing.Point(0, 0);
            this.txtRowFilter.Name = "txtRowFilter";
            this.txtRowFilter.Size = new System.Drawing.Size(552, 20);
            this.txtRowFilter.TabIndex = 5;
            this.toolTipRowFilter.SetToolTip(this.txtRowFilter, "Tip: Press Enter to apply filter | Example: Country = \'Jordan\'");
            this.txtRowFilter.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRowFilter_KeyDown);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(554, 345);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblRowFilter);
            this.Controls.Add(this.panel2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmployees)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblRowFilter;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolTip toolTipRowFilter;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dgvEmployees;
        private System.Windows.Forms.TextBox txtRowFilter;
    }
}

