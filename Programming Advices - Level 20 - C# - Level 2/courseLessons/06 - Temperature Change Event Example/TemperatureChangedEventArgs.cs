using System;

public class TemperatureChangedEventArgs : EventArgs
{
    public double OldTemperature { get; }
    public double NewTemperature { get; }
    public double Diffrence { get; }


    public TemperatureChangedEventArgs(double OldTemperature, double NewTemperature)
    {
        this.OldTemperature = OldTemperature;
        this.NewTemperature = NewTemperature;
        this.Diffrence = NewTemperature - OldTemperature;
    }
}
