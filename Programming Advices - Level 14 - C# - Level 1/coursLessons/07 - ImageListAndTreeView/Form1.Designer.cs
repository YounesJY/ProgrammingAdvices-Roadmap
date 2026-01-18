namespace ImageListAndTreeView
{
    partial class Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form));
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Node3");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Node4", 1, 1);
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Node0", 1, 1, new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2});
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Node5", 1, 1);
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Node1", new System.Windows.Forms.TreeNode[] {
            treeNode4});
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Node2");
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.treeView = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "logo.jpg");
            this.imageList.Images.SetKeyName(1, "profile.jpg");
            // 
            // treeView
            // 
            this.treeView.CheckBoxes = true;
            this.treeView.Cursor = System.Windows.Forms.Cursors.Hand;
            this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView.FullRowSelect = true;
            this.treeView.HotTracking = true;
            this.treeView.ImageIndex = 0;
            this.treeView.ImageList = this.imageList;
            this.treeView.Location = new System.Drawing.Point(0, 0);
            this.treeView.Name = "treeView";
            treeNode1.Name = "Node3";
            treeNode1.Text = "Node3";
            treeNode2.ImageIndex = 1;
            treeNode2.Name = "Node4";
            treeNode2.SelectedImageIndex = 1;
            treeNode2.Text = "Node4";
            treeNode3.ImageIndex = 1;
            treeNode3.Name = "Node0";
            treeNode3.SelectedImageIndex = 1;
            treeNode3.Text = "Node0";
            treeNode4.ImageIndex = 1;
            treeNode4.Name = "Node5";
            treeNode4.SelectedImageIndex = 1;
            treeNode4.Text = "Node5";
            treeNode5.Name = "Node1";
            treeNode5.Text = "Node1";
            treeNode6.Name = "Node2";
            treeNode6.Text = "Node2";
            this.treeView.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode3,
            treeNode5,
            treeNode6});
            this.treeView.SelectedImageIndex = 0;
            this.treeView.Size = new System.Drawing.Size(800, 450);
            this.treeView.TabIndex = 0;
            this.treeView.DoubleClick += new System.EventHandler(this.treeView_DoubleClick);
            // 
            // Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.treeView);
            this.Name = "Form";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.TreeView treeView;
    }
}

