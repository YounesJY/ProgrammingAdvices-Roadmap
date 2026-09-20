using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _8Pool
{
    public partial class PoolTable : UserControl
    {
        public class TableCompletedEventArgs : EventArgs
        {
            public string TimeText { get; }
            public int TimeInSeconds { get; }
            public float RatePerHour { get; }
            public float TotalFees { get; }


            public TableCompletedEventArgs(string TimeText, int TimeInSeconds, float RatePerHour, float TotalFees)
            {
                this.TimeText = TimeText;
                this.TimeInSeconds = TimeInSeconds;
                this.RatePerHour = RatePerHour;
                this.TotalFees = TotalFees;
            }
        }

        public event EventHandler<TableCompletedEventArgs> OnTableComplete;


        private string _TableTitle = "Table";
        [Category("Pool Config")]
        [Description("The table Name.")]
        public string TableTitle
        {
            get
            {
                return _TableTitle;
            }
            set
            {
                _TableTitle = value;

                grpTable.Text = value;

                // The Invalidate method calls the OnPaint method, which redraws
                // the control.  
                Invalidate();
            }
        }


        private string _TablePlayer = "Player";
        [Category("Pool Config")]
        [Description("The Player Name.")]
        public string TablePlayer
        {
            get
            {
                return _TablePlayer;
            }
            set
            {
                _TablePlayer = value;
                lblName.Text = value;

                // The Invalidate method calls the OnPaint method, which redraws the control.  
                Invalidate();
            }
        }


        [Category("Pool Config")]
        [Description("Rate Per Hour.")]
        public float HourlyRate { get; set; } = 10.00F;

        private int _Seconds;


        public PoolTable()
        {
            InitializeComponent();
        }
        private void PoolTable_Load(object sender, EventArgs e)
        {
            grpTable.Text = _TableTitle;
            lblName.Text = _TablePlayer;
        }


        protected virtual void RaiseOnTableComplete(TableCompletedEventArgs e)
        {
            OnTableComplete?.Invoke(this, e);
        }
        public void RaiseOnTableComplete(string TimeText, int TimeInSeconds, float RatePerHour, float TotalFees)
        {
            RaiseOnTableComplete(new TableCompletedEventArgs(TimeText, TimeInSeconds, RatePerHour, TotalFees));
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            ++this._Seconds;

            TimeSpan time = TimeSpan.FromSeconds(this._Seconds);
            string str = time.ToString(@"hh\:mm\:ss");
            lblTime.Text = str;
            lblTime.Refresh();
        }
        private void btnEnd_Click(object sender, EventArgs e)
        {
            timer.Stop();

            float TotalFees = ((float)this._Seconds / 60 / 60) * HourlyRate;
            grpTable.Text = "Table";
            lblName.Text = "Player";
            lblTime.Text = "00:00:00";
            btnStartStop.Text = "Start";
            this._Seconds = 0;

            RaiseOnTableComplete(lblTime.Text, this._Seconds, HourlyRate, TotalFees);
        }
        private void btnStartStop_Click(object sender, EventArgs e)
        {
            if (btnStartStop.Text == "Start")
            {
                btnStartStop.Text = "Stop";
                timer.Start();
            }
            else
            {
                btnStartStop.Text = "Start";
                timer.Stop();
            }
        }
        private void toolStripTextBox_TextChanged(object sender, EventArgs e)
        {
            lblName.Text = toolStripTextBox.Text;
        }
    }
}
