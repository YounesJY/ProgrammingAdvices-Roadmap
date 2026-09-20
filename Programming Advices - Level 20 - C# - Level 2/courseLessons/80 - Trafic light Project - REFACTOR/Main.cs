using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace trafik_light_Project
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }
        private void Form_Load(object sender, EventArgs e)
        {
            // ctrlTraficLight1.CountDownStartValue = 10;
            //ctrlTraficLight1.Start();
            //ctrlTraficLight2.Start();
            //ctrlTraficLight3.Start();
            //ctrlTraficLight4.Start();

            Parallel.Invoke(new Action[] {
                ctrlTraficLight1.Start,
                ctrlTraficLight2.Start,
                ctrlTraficLight3.Start,
                ctrlTraficLight4.Start
            });


            //  ctrlTraficLight2.Start();
        }


        private void ctrlTraficLight_LightOn(object sender, ctrlTraficLight.TraficLightEventArgs e)
        {
            MessageBox.Show(e.CurrentLight.ToString());
        }
    }
}
