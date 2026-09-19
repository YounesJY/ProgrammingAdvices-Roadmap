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
            this.ctrlTraficLight = new trafik_light_Project.ctrlTraficLight();
            this.SuspendLayout();
            // 
            // ctrlTraficLight
            // 
            this.ctrlTraficLight.BackColor = System.Drawing.Color.White;
            this.ctrlTraficLight.CurrentLight = trafik_light_Project.ctrlTraficLight.enLightEnum.Green;
            this.ctrlTraficLight.GreenTime = 8;
            this.ctrlTraficLight.Location = new System.Drawing.Point(101, 23);
            this.ctrlTraficLight.Name = "ctrlTraficLight";
            this.ctrlTraficLight.OrangeTime = 3;
            this.ctrlTraficLight.RedTime = 10;
            this.ctrlTraficLight.Size = new System.Drawing.Size(105, 181);
            this.ctrlTraficLight.TabIndex = 1;
            this.ctrlTraficLight.RedLightOn += new System.EventHandler<trafik_light_Project.ctrlTraficLight.TraficLightEventArgs>(this.ctrlTraficLight_RedLightOn);
            this.ctrlTraficLight.OrangeLightOn += new System.EventHandler<trafik_light_Project.ctrlTraficLight.TraficLightEventArgs>(this.ctrlTraficLight_OrangeLightOn);
            this.ctrlTraficLight.GreenLightOn += new System.EventHandler<trafik_light_Project.ctrlTraficLight.TraficLightEventArgs>(this.ctrlTraficLight_GreenLightOn);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(297, 346);
            this.Controls.Add(this.ctrlTraficLight);
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Trafic Light";
            this.Load += new System.EventHandler(this.Form_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ctrlTraficLight ctrlTraficLight;
    }
}

