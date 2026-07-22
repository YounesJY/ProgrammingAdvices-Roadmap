namespace DVLD_Project.People
{
    partial class ctrlPersonCardWithFilters
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Filter = new System.Windows.Forms.GroupBox();
            this.btnAddNewPerson = new System.Windows.Forms.Button();
            this.btnFind = new System.Windows.Forms.Button();
            this.mtbFilterSeach = new System.Windows.Forms.MaskedTextBox();
            this.cbFilterRows = new System.Windows.Forms.ComboBox();
            this.lblFilter = new System.Windows.Forms.Label();
            this.ctrlPersonCard = new DVLD_Project.People.ctrlPersonCard();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.Filter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // Filter
            // 
            this.Filter.Controls.Add(this.btnAddNewPerson);
            this.Filter.Controls.Add(this.btnFind);
            this.Filter.Controls.Add(this.mtbFilterSeach);
            this.Filter.Controls.Add(this.cbFilterRows);
            this.Filter.Controls.Add(this.lblFilter);
            this.Filter.Location = new System.Drawing.Point(65, 3);
            this.Filter.Name = "Filter";
            this.Filter.Size = new System.Drawing.Size(585, 55);
            this.Filter.TabIndex = 1;
            this.Filter.TabStop = false;
            this.Filter.Text = "groupBox1";
            // 
            // btnAddNewPerson
            // 
            this.btnAddNewPerson.BackgroundImage = global::DVLD_Project.Properties.Resources.Add_Person_40;
            this.btnAddNewPerson.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAddNewPerson.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAddNewPerson.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddNewPerson.Location = new System.Drawing.Point(495, 12);
            this.btnAddNewPerson.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnAddNewPerson.Name = "btnAddNewPerson";
            this.btnAddNewPerson.Size = new System.Drawing.Size(44, 37);
            this.btnAddNewPerson.TabIndex = 47;
            this.btnAddNewPerson.UseVisualStyleBackColor = true;
            this.btnAddNewPerson.Click += new System.EventHandler(this.btnAddNewPerson_Click);
            // 
            // btnFind
            // 
            this.btnFind.BackgroundImage = global::DVLD_Project.Properties.Resources.SearchPerson;
            this.btnFind.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnFind.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFind.Location = new System.Drawing.Point(444, 12);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(44, 37);
            this.btnFind.TabIndex = 46;
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // mtbFilterSeach
            // 
            this.mtbFilterSeach.Location = new System.Drawing.Point(240, 20);
            this.mtbFilterSeach.Name = "mtbFilterSeach";
            this.mtbFilterSeach.Size = new System.Drawing.Size(121, 20);
            this.mtbFilterSeach.TabIndex = 45;
            this.mtbFilterSeach.Validating += new System.ComponentModel.CancelEventHandler(this.mtbFilterSeach_Validating);
            // 
            // cbFilterRows
            // 
            this.cbFilterRows.FormattingEnabled = true;
            this.cbFilterRows.Items.AddRange(new object[] {
            "PersonID",
            "NationalNumber"});
            this.cbFilterRows.Location = new System.Drawing.Point(92, 20);
            this.cbFilterRows.Name = "cbFilterRows";
            this.cbFilterRows.Size = new System.Drawing.Size(121, 21);
            this.cbFilterRows.TabIndex = 44;
            this.cbFilterRows.SelectedIndexChanged += new System.EventHandler(this.cbFilterRows_SelectedIndexChanged);
            // 
            // lblFilter
            // 
            this.lblFilter.AutoSize = true;
            this.lblFilter.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFilter.Location = new System.Drawing.Point(18, 21);
            this.lblFilter.Name = "lblFilter";
            this.lblFilter.Size = new System.Drawing.Size(68, 19);
            this.lblFilter.TabIndex = 43;
            this.lblFilter.Text = "Filter By";
            // 
            // ctrlPersonCard
            // 
            this.ctrlPersonCard.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ctrlPersonCard.Location = new System.Drawing.Point(0, 75);
            this.ctrlPersonCard.Name = "ctrlPersonCard";
            this.ctrlPersonCard.Size = new System.Drawing.Size(729, 252);
            this.ctrlPersonCard.TabIndex = 0;
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // ctrlPersonCardWithFilters
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Controls.Add(this.Filter);
            this.Controls.Add(this.ctrlPersonCard);
            this.Name = "ctrlPersonCardWithFilters";
            this.Size = new System.Drawing.Size(729, 327);
            this.Load += new System.EventHandler(this.ctrlPersonCardWithFilters_Load);
            this.Filter.ResumeLayout(false);
            this.Filter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ctrlPersonCard ctrlPersonCard;
        private System.Windows.Forms.GroupBox Filter;
        private System.Windows.Forms.MaskedTextBox mtbFilterSeach;
        private System.Windows.Forms.ComboBox cbFilterRows;
        private System.Windows.Forms.Label lblFilter;
        private System.Windows.Forms.Button btnAddNewPerson;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.ErrorProvider errorProvider;
    }
}
