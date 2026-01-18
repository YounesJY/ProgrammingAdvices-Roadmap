using System;
using System.Drawing;
using System.Windows.Forms;

namespace NotifyIcon
{
    public partial class Form : System.Windows.Forms.Form
    {
        public Form()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WindowsNotification.Icon = SystemIcons.Exclamation;
            WindowsNotification.Text = "Windows";
            WindowsNotification.BalloonTipTitle = "Error ";
            WindowsNotification.BalloonTipText = "C: drive data has been deleted successfuly !";

            WindowsNotification.ShowBalloonTip(3000);
        }

        private void notifyIcon1_BalloonTipClicked(object sender, EventArgs e)
        {
            MessageBox.Show("Rebooting ...");
        }


    }
}
