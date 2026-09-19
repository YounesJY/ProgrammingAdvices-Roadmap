using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using trafik_light_Project.Properties;

namespace trafik_light_Project
{
    public partial class ctrlTraficLight : UserControl
    {
        public enum enLightEnum { Red, Orange, Green }
        public class TraficLightEventArgs : EventArgs
        {
            public enLightEnum CurrentLight { get; }
            public int LightDuration { get; }


            public TraficLightEventArgs(enLightEnum CurrentLight, int LightDuration)
            {
                this.CurrentLight = CurrentLight;
                this.LightDuration = LightDuration;
            }
        }

        public event EventHandler<TraficLightEventArgs> RedLightOn;
        public event EventHandler<TraficLightEventArgs> RedLightOff;
        public event EventHandler<TraficLightEventArgs> OrangeLightOn;
        public event EventHandler<TraficLightEventArgs> GreenLightOn;
        public event EventHandler<TraficLightEventArgs> GreenLightOff;


        public int RedTime { get; set; } = 10;
        public int OrangeTime { get; set; } = 3;
        public int GreenTime { get; set; } = 10;

        private enLightEnum _CurrentLight = enLightEnum.Red;
        public enLightEnum CurrentLight
        {
            get
            {
                return this._CurrentLight;
            }
            set
            {
                this._CurrentLight = value;
                SetTraficLightData();
            }
        }

        private int _CurrentCountDownValue;
        private enLightEnum _LightAfterOrangeGreenOrRed;


        public ctrlTraficLight()
        {
            InitializeComponent();
        }


        public int GetCurrentTime()
        {
            switch (this._CurrentLight)
            {
                case enLightEnum.Red:
                    return RedTime;
                case enLightEnum.Orange:
                    return OrangeTime;
                case enLightEnum.Green:
                    return GreenTime;
                default: return RedTime;
            }
        }
        public void Start()
        {
            this._CurrentCountDownValue = GetCurrentTime();
            LightTimer.Start();
        }
        public void Stop()
        {
            LightTimer.Stop();
        }


        protected virtual void RaiseRedLightOn(TraficLightEventArgs e)
        {
            RedLightOn?.Invoke(this, e);
        }
        public void RaiseRedLightOn()
        {
            RaiseRedLightOn(new TraficLightEventArgs(enLightEnum.Red, RedTime));
        }
        protected virtual void RaiseRedLightOff(TraficLightEventArgs e)
        {
            RedLightOn?.Invoke(this, e);
        }
        public void RaiseRedLightOff()
        {
            RaiseRedLightOff(new TraficLightEventArgs(enLightEnum.Orange, OrangeTime));
        }


        protected virtual void RaiseOrangeLightOn(TraficLightEventArgs e)
        {
            OrangeLightOn?.Invoke(this, e);
        }
        public void RaiseOrangeLightOn()
        {
            RaiseOrangeLightOn(new TraficLightEventArgs(enLightEnum.Orange, OrangeTime));
        }


        protected virtual void RaiseGreenLightOn(TraficLightEventArgs e)
        {
            GreenLightOn?.Invoke(this, e);
        }
        public void RaiseGreenLightOn()
        {
            RaiseGreenLightOn(new TraficLightEventArgs(enLightEnum.Green, GreenTime));
        }
        protected virtual void RaiseGreenLightOff(TraficLightEventArgs e)
        {
            RedLightOn?.Invoke(this, e);
        }
        public void RaiseGreenLightOff()
        {
            RaiseGreenLightOff(new TraficLightEventArgs(enLightEnum.Orange, OrangeTime));
        }


        private void _ChangeLight()
        {
            if (this._CurrentLight == enLightEnum.Red || this._CurrentLight == enLightEnum.Green)
            {
                // [BE AWARE ABOUT THE ORDER, set the next light before updating the current light (RED/GREEN) to (ORANGE)]
                this._LightAfterOrangeGreenOrRed = (this._CurrentLight == enLightEnum.Red) ? enLightEnum.Green : enLightEnum.Red;
                CurrentLight = enLightEnum.Orange;
                RaiseOrangeLightOn();
            }
            else
            {
                if (this._LightAfterOrangeGreenOrRed == enLightEnum.Green)
                {
                    CurrentLight = enLightEnum.Green;
                    RaiseGreenLightOn();
                }
                else
                {
                    CurrentLight = enLightEnum.Red;
                    RaiseRedLightOn();
                }
            }
        }
        private void LightTimer_Tick(object sender, EventArgs e)
        {
            lblCountDown.Text = this._CurrentCountDownValue.ToString();
            if (this._CurrentCountDownValue == 0)
            {
                // LightTimer.Stop();
                _ChangeLight();
            }
            else
                --this._CurrentCountDownValue;
        }
        private void SetTraficLightData()
        {
            switch (this._CurrentLight)
            {
                case enLightEnum.Red:
                    pbLight.Image = Resources.Red;
                    this._CurrentCountDownValue = RedTime;
                    lblCountDown.ForeColor = Color.Red;
                    break;
                case enLightEnum.Orange:
                    pbLight.Image = Resources.Orange;
                    this._CurrentCountDownValue = OrangeTime;
                    lblCountDown.ForeColor = Color.Orange;
                    break;
                case enLightEnum.Green:
                    pbLight.Image = Resources.Green;
                    this._CurrentCountDownValue = GreenTime;
                    lblCountDown.ForeColor = Color.Green;
                    break;
                default:
                    pbLight.Image = Resources.Red;
                    this._CurrentCountDownValue = RedTime;
                    lblCountDown.ForeColor = Color.Red;
                    break;
            }

            lblCountDown.Text = this._CurrentCountDownValue.ToString();
        }
    }
}
