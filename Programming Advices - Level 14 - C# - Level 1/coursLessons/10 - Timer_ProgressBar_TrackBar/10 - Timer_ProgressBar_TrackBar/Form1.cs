using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _10___Timer_ProgressBar_TrackBar
{
    public partial class FormTimerExample : Form
    {

        int totalMinutes = 0;
        int totalSeconds = 0;

        public FormTimerExample()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            lblTimeLeft.Text = $"{trackBar.Value.ToString()}:00";
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = false;
            trackBar.Enabled = false;
            rbReboot.Enabled = false;
            rbShutdown.Enabled = false;

            totalMinutes = trackBar.Value;
            totalSeconds = trackBar.Value * 60;

            progressBar.Minimum = 0;
            progressBar.Maximum = totalSeconds;

            timer.Enabled = true;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            ++progressBar.Value;
            --totalSeconds;

            lblTimeLeft.Text = $"{totalSeconds / 60}: {(totalSeconds % 60).ToString("00")}";

            if (totalSeconds <= 0)
            {
                timer.Enabled = false;

                if (rbReboot.Checked)
                {
                    MessageBox.Show("You system is about to reboot :}", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    System.Diagnostics.Process.Start("shutdown", "/s /t 0");
                }
                else
                {
                    MessageBox.Show("You system is about to shotdown :}", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    System.Diagnostics.Process.Start("shutdown", "/r /t 0");
                }
            }
        }

        private void FormTimerExample_Load(object sender, EventArgs e)
        {
            lblTimeLeft.Text = trackBar.Value.ToString();
        }
    }
}

