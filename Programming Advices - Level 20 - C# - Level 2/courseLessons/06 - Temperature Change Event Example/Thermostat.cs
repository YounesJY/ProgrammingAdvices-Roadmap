using System;

public class Thermostat
{
    public event EventHandler<TemperatureChangedEventArgs> TemperatureChanged;

    private double OldTemperature;
    private double currentTemperature;


    protected virtual void OnTemperatureChanged(TemperatureChangedEventArgs e)
    {
        TemperatureChanged?.Invoke(this, e);
    }
    private void OnTemperatureChanged(double OldTemperature, double currentTemperature)
    {
        OnTemperatureChanged(new TemperatureChangedEventArgs(OldTemperature, currentTemperature));
    }
    public void SetTemperature(double newTemperature)
    {
        if (newTemperature != currentTemperature)
        {
            OldTemperature = currentTemperature;
            currentTemperature = newTemperature;
            OnTemperatureChanged(OldTemperature, currentTemperature);
        }
    }
}
