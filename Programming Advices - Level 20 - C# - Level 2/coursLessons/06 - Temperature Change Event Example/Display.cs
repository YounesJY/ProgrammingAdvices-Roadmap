using System;

public class Display
{
    public void Subscribe(Thermostat thermostat)
    {
        thermostat.TemperatureChanged += HandleTemperatureChange;
    }
    public void HandleTemperatureChange(object sender, TemperatureChangedEventArgs e)
    {
        Console.WriteLine(" -> Temperature changed:");
        Console.WriteLine($"Temperature changed from {e.OldTemperature}°C");
        Console.WriteLine($"Temperature changed to {e.NewTemperature}°C");
        Console.WriteLine($"Temperature Differance to {e.Diffrence}°C");
    }
}
