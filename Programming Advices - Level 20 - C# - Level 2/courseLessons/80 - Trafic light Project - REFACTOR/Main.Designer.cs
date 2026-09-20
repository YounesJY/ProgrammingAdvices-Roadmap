namespace trafik_light_Project
{
    partial class Main
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
            this.ctrlTraficLight3 = new trafik_light_Project.ctrlTraficLight();
            this.ctrlTraficLight2 = new trafik_light_Project.ctrlTraficLight();
            this.ctrlTraficLight1 = new trafik_light_Project.ctrlTraficLight();
            this.ctrlTraficLight4 = new trafik_light_Project.ctrlTraficLight();
            this.SuspendLayout();
            // 
            // ctrlTraficLight3
            // 
            this.ctrlTraficLight3.BackColor = System.Drawing.Color.White;
            this.ctrlTraficLight3.CurrentLight = trafik_light_Project.ctrlTraficLight.enLightEnum.Green;
            this.ctrlTraficLight3.GreenTime = 10;
            this.ctrlTraficLight3.Location = new System.Drawing.Point(520, 323);
            this.ctrlTraficLight3.Name = "ctrlTraficLight3";
            this.ctrlTraficLight3.OrangeTime = 5;
            this.ctrlTraficLight3.RedTime = 10;
            this.ctrlTraficLight3.Size = new System.Drawing.Size(58, 109);
            this.ctrlTraficLight3.TabIndex = 4;
            this.ctrlTraficLight3.LightOn += new System.EventHandler<trafik_light_Project.ctrlTraficLight.TraficLightEventArgs>(this.ctrlTraficLight_LightOn);
            // 
            // ctrlTraficLight2
            // 
            this.ctrlTraficLight2.BackColor = System.Drawing.Color.White;
            this.ctrlTraficLight2.CurrentLight = trafik_light_Project.ctrlTraficLight.enLightEnum.Red;
            this.ctrlTraficLight2.GreenTime = 10;
            this.ctrlTraficLight2.Location = new System.Drawing.Point(520, 95);
            this.ctrlTraficLight2.Name = "ctrlTraficLight2";
            this.ctrlTraficLight2.OrangeTime = 5;
            this.ctrlTraficLight2.RedTime = 10;
            this.ctrlTraficLight2.Size = new System.Drawing.Size(58, 109);
            this.ctrlTraficLight2.TabIndex = 3;
            this.ctrlTraficLight2.LightOn += new System.EventHandler<trafik_light_Project.ctrlTraficLight.TraficLightEventArgs>(this.ctrlTraficLight_LightOn);
            // 
            // ctrlTraficLight1
            // 
            this.ctrlTraficLight1.BackColor = System.Drawing.Color.White;
            this.ctrlTraficLight1.CurrentLight = trafik_light_Project.ctrlTraficLight.enLightEnum.Red;
            this.ctrlTraficLight1.GreenTime = 10;
            this.ctrlTraficLight1.Location = new System.Drawing.Point(285, 323);
            this.ctrlTraficLight1.Name = "ctrlTraficLight1";
            this.ctrlTraficLight1.OrangeTime = 5;
            this.ctrlTraficLight1.RedTime = 10;
            this.ctrlTraficLight1.Size = new System.Drawing.Size(58, 109);
            this.ctrlTraficLight1.TabIndex = 2;
            this.ctrlTraficLight1.LightOn += new System.EventHandler<trafik_light_Project.ctrlTraficLight.TraficLightEventArgs>(this.ctrlTraficLight_LightOn);
            // 
            // ctrlTraficLight4
            // 
            this.ctrlTraficLight4.BackColor = System.Drawing.Color.White;
            this.ctrlTraficLight4.CurrentLight = trafik_light_Project.ctrlTraficLight.enLightEnum.Green;
            this.ctrlTraficLight4.GreenTime = 10;
            this.ctrlTraficLight4.Location = new System.Drawing.Point(285, 95);
            this.ctrlTraficLight4.Name = "ctrlTraficLight4";
            this.ctrlTraficLight4.OrangeTime = 5;
            this.ctrlTraficLight4.RedTime = 10;
            this.ctrlTraficLight4.Size = new System.Drawing.Size(58, 109);
            this.ctrlTraficLight4.TabIndex = 1;
            this.ctrlTraficLight4.LightOn += new System.EventHandler<trafik_light_Project.ctrlTraficLight.TraficLightEventArgs>(this.ctrlTraficLight_LightOn);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(853, 548);
            this.Controls.Add(this.ctrlTraficLight3);
            this.Controls.Add(this.ctrlTraficLight2);
            this.Controls.Add(this.ctrlTraficLight1);
            this.Controls.Add(this.ctrlTraficLight4);
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Trafic Light";
            this.Load += new System.EventHandler(this.Form_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ctrlTraficLight ctrlTraficLight4;
        private ctrlTraficLight ctrlTraficLight1;
        private ctrlTraficLight ctrlTraficLight2;
        private ctrlTraficLight ctrlTraficLight3;
    }
}

