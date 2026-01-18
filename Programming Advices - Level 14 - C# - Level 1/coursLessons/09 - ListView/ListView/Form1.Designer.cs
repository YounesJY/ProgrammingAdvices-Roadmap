namespace ListView
{
    partial class ListViewForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ListViewForm));
            this.listView = new System.Windows.Forms.ListView();
            this.ID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.PersonName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.btnAddItem = new System.Windows.Forms.Button();
            this.btnFillWithRandomItems = new System.Windows.Forms.Button();
            this.btnRemoveItem = new System.Windows.Forms.Button();
            this.txtID = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.gpListView = new System.Windows.Forms.GroupBox();
            this.rbListViewTile = new System.Windows.Forms.RadioButton();
            this.rbListViewLargeIcons = new System.Windows.Forms.RadioButton();
            this.rbListViewSmallIcons = new System.Windows.Forms.RadioButton();
            this.rbListViewList = new System.Windows.Forms.RadioButton();
            this.rbListViewDetails = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbGenderFemale = new System.Windows.Forms.RadioButton();
            this.rbGenderMale = new System.Windows.Forms.RadioButton();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.gpListView.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // listView
            // 
            this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ID,
            this.PersonName});
            this.listView.FullRowSelect = true;
            this.listView.GridLines = true;
            this.listView.HideSelection = false;
            this.listView.LabelEdit = true;
            this.listView.LargeImageList = this.imageList1;
            this.listView.Location = new System.Drawing.Point(26, 170);
            this.listView.Name = "listView";
            this.listView.ShowItemToolTips = true;
            this.listView.Size = new System.Drawing.Size(758, 268);
            this.listView.SmallImageList = this.imageList1;
            this.listView.TabIndex = 0;
            this.listView.UseCompatibleStateImageBehavior = false;
            // 
            // ID
            // 
            this.ID.Text = "ID";
            // 
            // PersonName
            // 
            this.PersonName.Text = "Name";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "girlImage.jpg");
            this.imageList1.Images.SetKeyName(1, "boyImage.jpg");
            // 
            // btnAddItem
            // 
            this.btnAddItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnAddItem.Location = new System.Drawing.Point(298, 33);
            this.btnAddItem.Name = "btnAddItem";
            this.btnAddItem.Size = new System.Drawing.Size(104, 23);
            this.btnAddItem.TabIndex = 1;
            this.btnAddItem.Text = "Add";
            this.btnAddItem.UseVisualStyleBackColor = false;
            this.btnAddItem.Click += new System.EventHandler(this.btnAddItem_Click);
            // 
            // btnFillWithRandomItems
            // 
            this.btnFillWithRandomItems.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnFillWithRandomItems.Location = new System.Drawing.Point(298, 117);
            this.btnFillWithRandomItems.Name = "btnFillWithRandomItems";
            this.btnFillWithRandomItems.Size = new System.Drawing.Size(104, 23);
            this.btnFillWithRandomItems.TabIndex = 2;
            this.btnFillWithRandomItems.Text = "Fill With Randoms";
            this.btnFillWithRandomItems.UseVisualStyleBackColor = false;
            this.btnFillWithRandomItems.Click += new System.EventHandler(this.btnFillWithRandomItems_Click);
            // 
            // btnRemoveItem
            // 
            this.btnRemoveItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnRemoveItem.Location = new System.Drawing.Point(298, 77);
            this.btnRemoveItem.Name = "btnRemoveItem";
            this.btnRemoveItem.Size = new System.Drawing.Size(104, 23);
            this.btnRemoveItem.TabIndex = 3;
            this.btnRemoveItem.Text = "Remove";
            this.btnRemoveItem.UseVisualStyleBackColor = false;
            this.btnRemoveItem.Click += new System.EventHandler(this.btnRemoveItem_Click);
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(26, 33);
            this.txtID.Multiline = true;
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(233, 20);
            this.txtID.TabIndex = 4;
            this.txtID.Validating += new System.ComponentModel.CancelEventHandler(this.textBox_Validating);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(26, 80);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(233, 20);
            this.txtName.TabIndex = 5;
            this.txtName.Validating += new System.ComponentModel.CancelEventHandler(this.textBox_Validating);
            // 
            // gpListView
            // 
            this.gpListView.Controls.Add(this.rbListViewTile);
            this.gpListView.Controls.Add(this.rbListViewLargeIcons);
            this.gpListView.Controls.Add(this.rbListViewSmallIcons);
            this.gpListView.Controls.Add(this.rbListViewList);
            this.gpListView.Controls.Add(this.rbListViewDetails);
            this.gpListView.Location = new System.Drawing.Point(529, 33);
            this.gpListView.Name = "gpListView";
            this.gpListView.Size = new System.Drawing.Size(255, 116);
            this.gpListView.TabIndex = 6;
            this.gpListView.TabStop = false;
            this.gpListView.Text = "Chose the list view from here";
            // 
            // rbListViewTile
            // 
            this.rbListViewTile.AutoSize = true;
            this.rbListViewTile.Location = new System.Drawing.Point(18, 90);
            this.rbListViewTile.Name = "rbListViewTile";
            this.rbListViewTile.Size = new System.Drawing.Size(42, 17);
            this.rbListViewTile.TabIndex = 14;
            this.rbListViewTile.TabStop = true;
            this.rbListViewTile.Text = "Tile";
            this.rbListViewTile.UseVisualStyleBackColor = true;
            this.rbListViewTile.CheckedChanged += new System.EventHandler(this.rbListViewTile_CheckedChanged);
            // 
            // rbListViewLargeIcons
            // 
            this.rbListViewLargeIcons.AutoSize = true;
            this.rbListViewLargeIcons.Location = new System.Drawing.Point(155, 57);
            this.rbListViewLargeIcons.Name = "rbListViewLargeIcons";
            this.rbListViewLargeIcons.Size = new System.Drawing.Size(81, 17);
            this.rbListViewLargeIcons.TabIndex = 13;
            this.rbListViewLargeIcons.TabStop = true;
            this.rbListViewLargeIcons.Text = "Large Icons";
            this.rbListViewLargeIcons.UseVisualStyleBackColor = true;
            this.rbListViewLargeIcons.CheckedChanged += new System.EventHandler(this.rbListViewLargeIcons_CheckedChanged);
            // 
            // rbListViewSmallIcons
            // 
            this.rbListViewSmallIcons.AutoSize = true;
            this.rbListViewSmallIcons.Location = new System.Drawing.Point(18, 55);
            this.rbListViewSmallIcons.Name = "rbListViewSmallIcons";
            this.rbListViewSmallIcons.Size = new System.Drawing.Size(79, 17);
            this.rbListViewSmallIcons.TabIndex = 12;
            this.rbListViewSmallIcons.TabStop = true;
            this.rbListViewSmallIcons.Text = "Small Icons";
            this.rbListViewSmallIcons.UseVisualStyleBackColor = true;
            this.rbListViewSmallIcons.CheckedChanged += new System.EventHandler(this.rbListViewSmallIcons_CheckedChanged);
            // 
            // rbListViewList
            // 
            this.rbListViewList.AutoSize = true;
            this.rbListViewList.Location = new System.Drawing.Point(155, 19);
            this.rbListViewList.Name = "rbListViewList";
            this.rbListViewList.Size = new System.Drawing.Size(41, 17);
            this.rbListViewList.TabIndex = 11;
            this.rbListViewList.TabStop = true;
            this.rbListViewList.Text = "List";
            this.rbListViewList.UseVisualStyleBackColor = true;
            this.rbListViewList.CheckedChanged += new System.EventHandler(this.rbListViewList_CheckedChanged);
            // 
            // rbListViewDetails
            // 
            this.rbListViewDetails.AutoSize = true;
            this.rbListViewDetails.Location = new System.Drawing.Point(18, 19);
            this.rbListViewDetails.Name = "rbListViewDetails";
            this.rbListViewDetails.Size = new System.Drawing.Size(57, 17);
            this.rbListViewDetails.TabIndex = 10;
            this.rbListViewDetails.TabStop = true;
            this.rbListViewDetails.Text = "Details";
            this.rbListViewDetails.UseVisualStyleBackColor = true;
            this.rbListViewDetails.CheckedChanged += new System.EventHandler(this.rbListViewDetails_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(18, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "ID";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Name";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbGenderFemale);
            this.groupBox1.Controls.Add(this.rbGenderMale);
            this.groupBox1.Location = new System.Drawing.Point(26, 106);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(233, 43);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Gender:";
            // 
            // rbGenderFemale
            // 
            this.rbGenderFemale.AutoSize = true;
            this.rbGenderFemale.Location = new System.Drawing.Point(136, 20);
            this.rbGenderFemale.Name = "rbGenderFemale";
            this.rbGenderFemale.Size = new System.Drawing.Size(59, 17);
            this.rbGenderFemale.TabIndex = 11;
            this.rbGenderFemale.TabStop = true;
            this.rbGenderFemale.Text = "Female";
            this.rbGenderFemale.UseVisualStyleBackColor = true;
            // 
            // rbGenderMale
            // 
            this.rbGenderMale.AutoSize = true;
            this.rbGenderMale.Location = new System.Drawing.Point(6, 20);
            this.rbGenderMale.Name = "rbGenderMale";
            this.rbGenderMale.Size = new System.Drawing.Size(48, 17);
            this.rbGenderMale.TabIndex = 10;
            this.rbGenderMale.TabStop = true;
            this.rbGenderMale.Text = "Male";
            this.rbGenderMale.UseVisualStyleBackColor = true;
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // ListViewForm
            // 
            this.AcceptButton = this.btnAddItem;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gpListView);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.btnRemoveItem);
            this.Controls.Add(this.btnFillWithRandomItems);
            this.Controls.Add(this.btnAddItem);
            this.Controls.Add(this.listView);
            this.Name = "ListViewForm";
            this.Text = "ListViewForm";
            this.gpListView.ResumeLayout(false);
            this.gpListView.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.Button btnAddItem;
        private System.Windows.Forms.Button btnFillWithRandomItems;
        private System.Windows.Forms.Button btnRemoveItem;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.GroupBox gpListView;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rbListViewTile;
        private System.Windows.Forms.RadioButton rbListViewLargeIcons;
        private System.Windows.Forms.RadioButton rbListViewSmallIcons;
        private System.Windows.Forms.RadioButton rbListViewList;
        private System.Windows.Forms.RadioButton rbListViewDetails;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbGenderFemale;
        private System.Windows.Forms.RadioButton rbGenderMale;
        private System.Windows.Forms.ColumnHeader ID;
        private System.Windows.Forms.ColumnHeader PersonName;
        private System.Windows.Forms.ErrorProvider errorProvider;
    }
}

