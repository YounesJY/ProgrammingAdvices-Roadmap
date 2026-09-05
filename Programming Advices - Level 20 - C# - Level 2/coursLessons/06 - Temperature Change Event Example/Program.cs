using System;


public class Program
{
    static void Main()
    {
        Thermostat thermostat = new Thermostat();
        Display display = new Display();

        /*
            if you use a direct unsafe delegate like:  
                public event EventHandler<TemperatureChangedEventArgs> TemperatureChanged;
        you'll be able to repreduce all the 2 major issues [Direct invocation - Remove all subscribers]k  
        
        thermostat.TemperatureChanged = display.HandleTemperatureChange;
            thermostat.TemperatureChanged = (sender, e) =>
            { Some logic .........} ;
            thermostat.TemperatureChanged = null ;
        */
        display.Subscribe(thermostat);

        thermostat.SetTemperature(25);
        thermostat.SetTemperature(30);
        thermostat.SetTemperature(30);
        thermostat.SetTemperature(30);
        thermostat.SetTemperature(30);
        thermostat.SetTemperature(30);
        thermostat.SetTemperature(30);

        Console.ReadLine();
    }
}
