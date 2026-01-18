namespace PizzaProject
{
    partial class PizzaProject
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
            this.ProjectLabel = new System.Windows.Forms.Label();
            this.gpOrderSummary = new System.Windows.Forms.GroupBox();
            this.orderSummaryQuantity = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPrice = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.orderSummaryWhereToEat = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.orderSummaryCrustType = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.orderSummaryToppings = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.orderSummarySize = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.gpOperations = new System.Windows.Forms.GroupBox();
            this.btnOperationsReset = new System.Windows.Forms.Button();
            this.btnOperationsOrderPizza = new System.Windows.Forms.Button();
            this.tcPizzaOptions = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.gpSize = new System.Windows.Forms.GroupBox();
            this.rbSizeLarge = new System.Windows.Forms.RadioButton();
            this.rbSizeMedium = new System.Windows.Forms.RadioButton();
            this.rbSizeSmall = new System.Windows.Forms.RadioButton();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.gpToppings = new System.Windows.Forms.GroupBox();
            this.chbToppingsGreenPapers = new System.Windows.Forms.CheckBox();
            this.chbToppingsTomatoes = new System.Windows.Forms.CheckBox();
            this.chbToppingsOlives = new System.Windows.Forms.CheckBox();
            this.chbToppingsMushRooms = new System.Windows.Forms.CheckBox();
            this.chbToppingsOnion = new System.Windows.Forms.CheckBox();
            this.chbToppingsExtraChees = new System.Windows.Forms.CheckBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.gpCrustType = new System.Windows.Forms.GroupBox();
            this.rbCrustTypeThickCrust = new System.Windows.Forms.RadioButton();
            this.rbCrustTypeThinCrust = new System.Windows.Forms.RadioButton();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.gpWhereToEat = new System.Windows.Forms.GroupBox();
            this.rbWhereToEatTakeOut = new System.Windows.Forms.RadioButton();
            this.rbWhereToEatIn = new System.Windows.Forms.RadioButton();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.nudQuantity = new System.Windows.Forms.NumericUpDown();
            this.gpOrderSummary.SuspendLayout();
            this.gpOperations.SuspendLayout();
            this.tcPizzaOptions.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.gpSize.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.gpToppings.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.gpCrustType.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.gpWhereToEat.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuantity)).BeginInit();
            this.SuspendLayout();
            // 
            // ProjectLabel
            // 
            this.ProjectLabel.AutoSize = true;
            this.ProjectLabel.BackColor = System.Drawing.Color.Transparent;
            this.ProjectLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ProjectLabel.Font = new System.Drawing.Font("Ravie", 32.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProjectLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.ProjectLabel.Location = new System.Drawing.Point(258, 67);
            this.ProjectLabel.Name = "ProjectLabel";
            this.ProjectLabel.Size = new System.Drawing.Size(532, 57);
            this.ProjectLabel.TabIndex = 0;
            this.ProjectLabel.Text = "Make Your Pizza";
            // 
            // gpOrderSummary
            // 
            this.gpOrderSummary.BackColor = System.Drawing.Color.Transparent;
            this.gpOrderSummary.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.gpOrderSummary.Controls.Add(this.orderSummaryQuantity);
            this.gpOrderSummary.Controls.Add(this.label4);
            this.gpOrderSummary.Controls.Add(this.txtPrice);
            this.gpOrderSummary.Controls.Add(this.label10);
            this.gpOrderSummary.Controls.Add(this.orderSummaryWhereToEat);
            this.gpOrderSummary.Controls.Add(this.label8);
            this.gpOrderSummary.Controls.Add(this.orderSummaryCrustType);
            this.gpOrderSummary.Controls.Add(this.label5);
            this.gpOrderSummary.Controls.Add(this.orderSummaryToppings);
            this.gpOrderSummary.Controls.Add(this.label3);
            this.gpOrderSummary.Controls.Add(this.orderSummarySize);
            this.gpOrderSummary.Controls.Add(this.label1);
            this.gpOrderSummary.Location = new System.Drawing.Point(563, 127);
            this.gpOrderSummary.Name = "gpOrderSummary";
            this.gpOrderSummary.Size = new System.Drawing.Size(220, 342);
            this.gpOrderSummary.TabIndex = 2;
            this.gpOrderSummary.TabStop = false;
            this.gpOrderSummary.Text = "Order Summary";
            // 
            // orderSummaryQuantity
            // 
            this.orderSummaryQuantity.AutoSize = true;
            this.orderSummaryQuantity.Location = new System.Drawing.Point(113, 226);
            this.orderSummaryQuantity.Name = "orderSummaryQuantity";
            this.orderSummaryQuantity.Size = new System.Drawing.Size(13, 13);
            this.orderSummaryQuantity.TabIndex = 13;
            this.orderSummaryQuantity.Text = "1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(8, 222);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 17);
            this.label4.TabIndex = 12;
            this.label4.Text = "Quantity";
            // 
            // txtPrice
            // 
            this.txtPrice.AutoSize = true;
            this.txtPrice.Font = new System.Drawing.Font("Ravie", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.txtPrice.Location = new System.Drawing.Point(18, 270);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(80, 50);
            this.txtPrice.TabIndex = 9;
            this.txtPrice.Tag = "0";
            this.txtPrice.Text = "0$";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(8, 253);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(73, 17);
            this.label10.TabIndex = 8;
            this.label10.Text = "Total Price";
            // 
            // orderSummaryWhereToEat
            // 
            this.orderSummaryWhereToEat.AutoSize = true;
            this.orderSummaryWhereToEat.Location = new System.Drawing.Point(113, 185);
            this.orderSummaryWhereToEat.Name = "orderSummaryWhereToEat";
            this.orderSummaryWhereToEat.Size = new System.Drawing.Size(52, 13);
            this.orderSummaryWhereToEat.TabIndex = 7;
            this.orderSummaryWhereToEat.Text = "Take Out";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(8, 181);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(90, 17);
            this.label8.TabIndex = 6;
            this.label8.Text = "Where To Eat";
            // 
            // orderSummaryCrustType
            // 
            this.orderSummaryCrustType.AutoSize = true;
            this.orderSummaryCrustType.Location = new System.Drawing.Point(113, 141);
            this.orderSummaryCrustType.Name = "orderSummaryCrustType";
            this.orderSummaryCrustType.Size = new System.Drawing.Size(55, 13);
            this.orderSummaryCrustType.TabIndex = 5;
            this.orderSummaryCrustType.Text = "Thin Crust";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(8, 137);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 17);
            this.label5.TabIndex = 4;
            this.label5.Text = "Crust Type";
            // 
            // orderSummaryToppings
            // 
            this.orderSummaryToppings.Location = new System.Drawing.Point(57, 94);
            this.orderSummaryToppings.MaximumSize = new System.Drawing.Size(400, 400);
            this.orderSummaryToppings.MinimumSize = new System.Drawing.Size(125, 60);
            this.orderSummaryToppings.Name = "orderSummaryToppings";
            this.orderSummaryToppings.Size = new System.Drawing.Size(149, 60);
            this.orderSummaryToppings.TabIndex = 3;
            this.orderSummaryToppings.Text = "No Toppings";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(8, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Toppings";
            // 
            // orderSummarySize
            // 
            this.orderSummarySize.AutoSize = true;
            this.orderSummarySize.Location = new System.Drawing.Point(113, 32);
            this.orderSummarySize.Name = "orderSummarySize";
            this.orderSummarySize.Size = new System.Drawing.Size(30, 13);
            this.orderSummarySize.TabIndex = 1;
            this.orderSummarySize.Text = "small";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Size";
            // 
            // gpOperations
            // 
            this.gpOperations.BackColor = System.Drawing.Color.Transparent;
            this.gpOperations.Controls.Add(this.btnOperationsReset);
            this.gpOperations.Controls.Add(this.btnOperationsOrderPizza);
            this.gpOperations.Location = new System.Drawing.Point(261, 412);
            this.gpOperations.Name = "gpOperations";
            this.gpOperations.Size = new System.Drawing.Size(296, 57);
            this.gpOperations.TabIndex = 4;
            this.gpOperations.TabStop = false;
            // 
            // btnOperationsReset
            // 
            this.btnOperationsReset.Location = new System.Drawing.Point(174, 19);
            this.btnOperationsReset.Name = "btnOperationsReset";
            this.btnOperationsReset.Size = new System.Drawing.Size(75, 28);
            this.btnOperationsReset.TabIndex = 1;
            this.btnOperationsReset.Text = "Reset";
            this.btnOperationsReset.UseVisualStyleBackColor = true;
            this.btnOperationsReset.Click += new System.EventHandler(this.btnOperationsReset_Click);
            // 
            // btnOperationsOrderPizza
            // 
            this.btnOperationsOrderPizza.Location = new System.Drawing.Point(43, 19);
            this.btnOperationsOrderPizza.Name = "btnOperationsOrderPizza";
            this.btnOperationsOrderPizza.Size = new System.Drawing.Size(81, 28);
            this.btnOperationsOrderPizza.TabIndex = 0;
            this.btnOperationsOrderPizza.Text = "Order Pizza";
            this.btnOperationsOrderPizza.UseVisualStyleBackColor = true;
            this.btnOperationsOrderPizza.Click += new System.EventHandler(this.btnOperationsOrderPizza_Click);
            // 
            // tcPizzaOptions
            // 
            this.tcPizzaOptions.Controls.Add(this.tabPage1);
            this.tcPizzaOptions.Controls.Add(this.tabPage2);
            this.tcPizzaOptions.Controls.Add(this.tabPage3);
            this.tcPizzaOptions.Controls.Add(this.tabPage4);
            this.tcPizzaOptions.Controls.Add(this.tabPage5);
            this.tcPizzaOptions.Location = new System.Drawing.Point(261, 127);
            this.tcPizzaOptions.Name = "tcPizzaOptions";
            this.tcPizzaOptions.SelectedIndex = 0;
            this.tcPizzaOptions.Size = new System.Drawing.Size(296, 283);
            this.tcPizzaOptions.TabIndex = 7;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Transparent;
            this.tabPage1.Controls.Add(this.gpSize);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(288, 257);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Size";
            // 
            // gpSize
            // 
            this.gpSize.BackColor = System.Drawing.Color.Transparent;
            this.gpSize.Controls.Add(this.rbSizeLarge);
            this.gpSize.Controls.Add(this.rbSizeMedium);
            this.gpSize.Controls.Add(this.rbSizeSmall);
            this.gpSize.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gpSize.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.gpSize.Location = new System.Drawing.Point(3, 3);
            this.gpSize.Name = "gpSize";
            this.gpSize.Size = new System.Drawing.Size(282, 251);
            this.gpSize.TabIndex = 1;
            this.gpSize.TabStop = false;
            this.gpSize.Text = "Size";
            // 
            // rbSizeLarge
            // 
            this.rbSizeLarge.AutoSize = true;
            this.rbSizeLarge.Location = new System.Drawing.Point(42, 95);
            this.rbSizeLarge.Name = "rbSizeLarge";
            this.rbSizeLarge.Size = new System.Drawing.Size(48, 17);
            this.rbSizeLarge.TabIndex = 2;
            this.rbSizeLarge.TabStop = true;
            this.rbSizeLarge.Tag = "40";
            this.rbSizeLarge.Text = "large";
            this.rbSizeLarge.UseVisualStyleBackColor = true;
            this.rbSizeLarge.CheckedChanged += new System.EventHandler(this.rbSizeLarge_CheckedChanged);
            // 
            // rbSizeMedium
            // 
            this.rbSizeMedium.AutoSize = true;
            this.rbSizeMedium.Location = new System.Drawing.Point(42, 61);
            this.rbSizeMedium.Name = "rbSizeMedium";
            this.rbSizeMedium.Size = new System.Drawing.Size(61, 17);
            this.rbSizeMedium.TabIndex = 1;
            this.rbSizeMedium.TabStop = true;
            this.rbSizeMedium.Tag = "30";
            this.rbSizeMedium.Text = "medium";
            this.rbSizeMedium.UseVisualStyleBackColor = true;
            this.rbSizeMedium.CheckedChanged += new System.EventHandler(this.rbSizeMedium_CheckedChanged);
            // 
            // rbSizeSmall
            // 
            this.rbSizeSmall.AutoSize = true;
            this.rbSizeSmall.Location = new System.Drawing.Point(42, 27);
            this.rbSizeSmall.Name = "rbSizeSmall";
            this.rbSizeSmall.Size = new System.Drawing.Size(48, 17);
            this.rbSizeSmall.TabIndex = 0;
            this.rbSizeSmall.TabStop = true;
            this.rbSizeSmall.Tag = "20";
            this.rbSizeSmall.Text = "small";
            this.rbSizeSmall.UseVisualStyleBackColor = true;
            this.rbSizeSmall.CheckedChanged += new System.EventHandler(this.rbSizeSmall_CheckedChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.Transparent;
            this.tabPage2.Controls.Add(this.gpToppings);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(288, 257);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Toppings";
            // 
            // gpToppings
            // 
            this.gpToppings.BackColor = System.Drawing.Color.Transparent;
            this.gpToppings.Controls.Add(this.chbToppingsGreenPapers);
            this.gpToppings.Controls.Add(this.chbToppingsTomatoes);
            this.gpToppings.Controls.Add(this.chbToppingsOlives);
            this.gpToppings.Controls.Add(this.chbToppingsMushRooms);
            this.gpToppings.Controls.Add(this.chbToppingsOnion);
            this.gpToppings.Controls.Add(this.chbToppingsExtraChees);
            this.gpToppings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gpToppings.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.gpToppings.Location = new System.Drawing.Point(3, 3);
            this.gpToppings.Name = "gpToppings";
            this.gpToppings.Size = new System.Drawing.Size(282, 251);
            this.gpToppings.TabIndex = 2;
            this.gpToppings.TabStop = false;
            this.gpToppings.Text = "Toppings";
            // 
            // chbToppingsGreenPapers
            // 
            this.chbToppingsGreenPapers.AutoSize = true;
            this.chbToppingsGreenPapers.Location = new System.Drawing.Point(147, 95);
            this.chbToppingsGreenPapers.Name = "chbToppingsGreenPapers";
            this.chbToppingsGreenPapers.Size = new System.Drawing.Size(97, 17);
            this.chbToppingsGreenPapers.TabIndex = 5;
            this.chbToppingsGreenPapers.Tag = "5";
            this.chbToppingsGreenPapers.Text = "Green Pappers";
            this.chbToppingsGreenPapers.UseVisualStyleBackColor = true;
            this.chbToppingsGreenPapers.CheckedChanged += new System.EventHandler(this.chbToppingsGreenPapers_CheckedChanged);
            // 
            // chbToppingsTomatoes
            // 
            this.chbToppingsTomatoes.AutoSize = true;
            this.chbToppingsTomatoes.Location = new System.Drawing.Point(13, 95);
            this.chbToppingsTomatoes.Name = "chbToppingsTomatoes";
            this.chbToppingsTomatoes.Size = new System.Drawing.Size(73, 17);
            this.chbToppingsTomatoes.TabIndex = 4;
            this.chbToppingsTomatoes.Tag = "5";
            this.chbToppingsTomatoes.Text = "Tomatoes";
            this.chbToppingsTomatoes.UseVisualStyleBackColor = true;
            this.chbToppingsTomatoes.CheckedChanged += new System.EventHandler(this.chbToppingsTomatoes_CheckedChanged);
            // 
            // chbToppingsOlives
            // 
            this.chbToppingsOlives.AutoSize = true;
            this.chbToppingsOlives.Location = new System.Drawing.Point(147, 61);
            this.chbToppingsOlives.Name = "chbToppingsOlives";
            this.chbToppingsOlives.Size = new System.Drawing.Size(55, 17);
            this.chbToppingsOlives.TabIndex = 3;
            this.chbToppingsOlives.Tag = "5";
            this.chbToppingsOlives.Text = "Olives";
            this.chbToppingsOlives.UseVisualStyleBackColor = true;
            this.chbToppingsOlives.CheckedChanged += new System.EventHandler(this.chbToppingsOlives_CheckedChanged);
            // 
            // chbToppingsMushRooms
            // 
            this.chbToppingsMushRooms.AutoSize = true;
            this.chbToppingsMushRooms.Location = new System.Drawing.Point(13, 61);
            this.chbToppingsMushRooms.Name = "chbToppingsMushRooms";
            this.chbToppingsMushRooms.Size = new System.Drawing.Size(85, 17);
            this.chbToppingsMushRooms.TabIndex = 2;
            this.chbToppingsMushRooms.Tag = "5";
            this.chbToppingsMushRooms.Text = "MushRooms";
            this.chbToppingsMushRooms.UseVisualStyleBackColor = true;
            this.chbToppingsMushRooms.CheckedChanged += new System.EventHandler(this.chbToppingsMushRooms_CheckedChanged);
            // 
            // chbToppingsOnion
            // 
            this.chbToppingsOnion.AutoSize = true;
            this.chbToppingsOnion.Location = new System.Drawing.Point(147, 27);
            this.chbToppingsOnion.Name = "chbToppingsOnion";
            this.chbToppingsOnion.Size = new System.Drawing.Size(54, 17);
            this.chbToppingsOnion.TabIndex = 1;
            this.chbToppingsOnion.Tag = "5";
            this.chbToppingsOnion.Text = "Onion";
            this.chbToppingsOnion.UseVisualStyleBackColor = true;
            this.chbToppingsOnion.CheckedChanged += new System.EventHandler(this.chbToppingsOnion_CheckedChanged);
            // 
            // chbToppingsExtraChees
            // 
            this.chbToppingsExtraChees.AutoSize = true;
            this.chbToppingsExtraChees.Location = new System.Drawing.Point(13, 27);
            this.chbToppingsExtraChees.Name = "chbToppingsExtraChees";
            this.chbToppingsExtraChees.Size = new System.Drawing.Size(83, 17);
            this.chbToppingsExtraChees.TabIndex = 0;
            this.chbToppingsExtraChees.Tag = "5";
            this.chbToppingsExtraChees.Text = "Extra Chees";
            this.chbToppingsExtraChees.UseVisualStyleBackColor = true;
            this.chbToppingsExtraChees.CheckedChanged += new System.EventHandler(this.chbToppingsExtraChees_CheckedChanged);
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.Transparent;
            this.tabPage3.Controls.Add(this.gpCrustType);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(288, 257);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Crust Type";
            // 
            // gpCrustType
            // 
            this.gpCrustType.BackColor = System.Drawing.Color.Transparent;
            this.gpCrustType.Controls.Add(this.rbCrustTypeThickCrust);
            this.gpCrustType.Controls.Add(this.rbCrustTypeThinCrust);
            this.gpCrustType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gpCrustType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gpCrustType.Location = new System.Drawing.Point(3, 3);
            this.gpCrustType.Name = "gpCrustType";
            this.gpCrustType.Size = new System.Drawing.Size(282, 251);
            this.gpCrustType.TabIndex = 2;
            this.gpCrustType.TabStop = false;
            this.gpCrustType.Text = "Crust Type";
            // 
            // rbCrustTypeThickCrust
            // 
            this.rbCrustTypeThickCrust.AutoSize = true;
            this.rbCrustTypeThickCrust.Location = new System.Drawing.Point(42, 73);
            this.rbCrustTypeThickCrust.Name = "rbCrustTypeThickCrust";
            this.rbCrustTypeThickCrust.Size = new System.Drawing.Size(79, 17);
            this.rbCrustTypeThickCrust.TabIndex = 4;
            this.rbCrustTypeThickCrust.TabStop = true;
            this.rbCrustTypeThickCrust.Tag = "10";
            this.rbCrustTypeThickCrust.Text = "Thick Crust";
            this.rbCrustTypeThickCrust.UseVisualStyleBackColor = true;
            this.rbCrustTypeThickCrust.CheckedChanged += new System.EventHandler(this.rbCrustTypeThinkCrust_CheckedChanged);
            // 
            // rbCrustTypeThinCrust
            // 
            this.rbCrustTypeThinCrust.AutoSize = true;
            this.rbCrustTypeThinCrust.Location = new System.Drawing.Point(42, 41);
            this.rbCrustTypeThinCrust.Name = "rbCrustTypeThinCrust";
            this.rbCrustTypeThinCrust.Size = new System.Drawing.Size(73, 17);
            this.rbCrustTypeThinCrust.TabIndex = 3;
            this.rbCrustTypeThinCrust.TabStop = true;
            this.rbCrustTypeThinCrust.Tag = "0";
            this.rbCrustTypeThinCrust.Text = "Thin Crust";
            this.rbCrustTypeThinCrust.UseVisualStyleBackColor = true;
            this.rbCrustTypeThinCrust.CheckedChanged += new System.EventHandler(this.rbCrustTypeThinCrust_CheckedChanged);
            // 
            // tabPage4
            // 
            this.tabPage4.BackColor = System.Drawing.Color.Transparent;
            this.tabPage4.Controls.Add(this.gpWhereToEat);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(288, 257);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Where To Eat";
            // 
            // gpWhereToEat
            // 
            this.gpWhereToEat.BackColor = System.Drawing.Color.Transparent;
            this.gpWhereToEat.Controls.Add(this.rbWhereToEatTakeOut);
            this.gpWhereToEat.Controls.Add(this.rbWhereToEatIn);
            this.gpWhereToEat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gpWhereToEat.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.gpWhereToEat.Location = new System.Drawing.Point(3, 3);
            this.gpWhereToEat.Name = "gpWhereToEat";
            this.gpWhereToEat.Size = new System.Drawing.Size(282, 251);
            this.gpWhereToEat.TabIndex = 2;
            this.gpWhereToEat.TabStop = false;
            this.gpWhereToEat.Text = "Where To Eat";
            // 
            // rbWhereToEatTakeOut
            // 
            this.rbWhereToEatTakeOut.Location = new System.Drawing.Point(147, 32);
            this.rbWhereToEatTakeOut.Name = "rbWhereToEatTakeOut";
            this.rbWhereToEatTakeOut.Size = new System.Drawing.Size(85, 17);
            this.rbWhereToEatTakeOut.TabIndex = 1;
            this.rbWhereToEatTakeOut.TabStop = true;
            this.rbWhereToEatTakeOut.Tag = "0";
            this.rbWhereToEatTakeOut.Text = "TakeOut";
            this.rbWhereToEatTakeOut.UseVisualStyleBackColor = true;
            this.rbWhereToEatTakeOut.CheckedChanged += new System.EventHandler(this.rbWhereToEatTakeOut_CheckedChanged);
            // 
            // rbWhereToEatIn
            // 
            this.rbWhereToEatIn.Location = new System.Drawing.Point(16, 32);
            this.rbWhereToEatIn.Name = "rbWhereToEatIn";
            this.rbWhereToEatIn.Size = new System.Drawing.Size(85, 17);
            this.rbWhereToEatIn.TabIndex = 0;
            this.rbWhereToEatIn.TabStop = true;
            this.rbWhereToEatIn.Tag = "0";
            this.rbWhereToEatIn.Text = "Eat In";
            this.rbWhereToEatIn.UseVisualStyleBackColor = true;
            this.rbWhereToEatIn.CheckedChanged += new System.EventHandler(this.rbWhereToEatIn_CheckedChanged);
            // 
            // tabPage5
            // 
            this.tabPage5.BackColor = System.Drawing.Color.Transparent;
            this.tabPage5.Controls.Add(this.groupBox1);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(288, 257);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Quantity";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.nudQuantity);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(282, 251);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Quantity";
            // 
            // nudQuantity
            // 
            this.nudQuantity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudQuantity.Cursor = System.Windows.Forms.Cursors.Hand;
            this.nudQuantity.Font = new System.Drawing.Font("Showcard Gothic", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudQuantity.Location = new System.Drawing.Point(13, 19);
            this.nudQuantity.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.nudQuantity.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudQuantity.Name = "nudQuantity";
            this.nudQuantity.Size = new System.Drawing.Size(73, 37);
            this.nudQuantity.TabIndex = 6;
            this.nudQuantity.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudQuantity.ValueChanged += new System.EventHandler(this.nudQuantity_ValueChanged);
            // 
            // PizzaProject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImage = global::PizzaProjectUpdated.Properties.Resources.pizzaBackground;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1011, 544);
            this.Controls.Add(this.tcPizzaOptions);
            this.Controls.Add(this.ProjectLabel);
            this.Controls.Add(this.gpOrderSummary);
            this.Controls.Add(this.gpOperations);
            this.DoubleBuffered = true;
            this.Name = "PizzaProject";
            this.Text = "Pizza Order";
            this.Load += new System.EventHandler(this.PizzaProjectForm_Load);
            this.gpOrderSummary.ResumeLayout(false);
            this.gpOrderSummary.PerformLayout();
            this.gpOperations.ResumeLayout(false);
            this.tcPizzaOptions.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.gpSize.ResumeLayout(false);
            this.gpSize.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.gpToppings.ResumeLayout(false);
            this.gpToppings.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.gpCrustType.ResumeLayout(false);
            this.gpCrustType.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.gpWhereToEat.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudQuantity)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label ProjectLabel;
        private System.Windows.Forms.GroupBox gpOrderSummary;
        private System.Windows.Forms.GroupBox gpOperations;
        private System.Windows.Forms.Button btnOperationsReset;
        private System.Windows.Forms.Button btnOperationsOrderPizza;
        private System.Windows.Forms.Label txtPrice;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label orderSummaryWhereToEat;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label orderSummaryCrustType;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label orderSummaryToppings;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label orderSummarySize;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label orderSummaryQuantity;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TabControl tcPizzaOptions;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox gpSize;
        private System.Windows.Forms.RadioButton rbSizeLarge;
        private System.Windows.Forms.RadioButton rbSizeMedium;
        private System.Windows.Forms.RadioButton rbSizeSmall;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox gpToppings;
        private System.Windows.Forms.CheckBox chbToppingsGreenPapers;
        private System.Windows.Forms.CheckBox chbToppingsTomatoes;
        private System.Windows.Forms.CheckBox chbToppingsOlives;
        private System.Windows.Forms.CheckBox chbToppingsMushRooms;
        private System.Windows.Forms.CheckBox chbToppingsOnion;
        private System.Windows.Forms.CheckBox chbToppingsExtraChees;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.GroupBox gpCrustType;
        private System.Windows.Forms.RadioButton rbCrustTypeThickCrust;
        private System.Windows.Forms.RadioButton rbCrustTypeThinCrust;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.GroupBox gpWhereToEat;
        private System.Windows.Forms.RadioButton rbWhereToEatTakeOut;
        private System.Windows.Forms.RadioButton rbWhereToEatIn;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown nudQuantity;
    }
}

