using System.Runtime.Versioning;
using NAudio.CoreAudioApi;

namespace AudioControlApi
{
    [SupportedOSPlatform("windows")]
    class Program
    {
        private static MMDeviceEnumerator enumerator = null!;
        private static MMDevice device = null!;
        private const float VolumeStep = 0.05f;

        private static void RunMainLoop()
        {
            while (true)
            {
                ConsoleKey key = Console.ReadKey(intercept: true).Key;

                switch (key)
                {
                    case ConsoleKey.OemPlus:
                    case ConsoleKey.Add:
                        AdjustVolume(VolumeStep);
                        break;

                    case ConsoleKey.OemMinus:
                    case ConsoleKey.Subtract:
                        AdjustVolume(-VolumeStep);
                        break;

                    case ConsoleKey.S:
                        SetSpecificVolume();
                        break;

                    case ConsoleKey.Q:
                        Console.WriteLine("Exiting...");
                        return;
                }
            }
        }
        private static void AdjustVolume(float delta)
        {
            float current = device.AudioEndpointVolume.MasterVolumeLevelScalar;
            float updated = Math.Clamp(current + delta, 0.0f, 1.0f);

            device.AudioEndpointVolume.MasterVolumeLevelScalar = updated;

            Console.WriteLine($"Volume adjusted to: {updated * 100:F0}%");
        }
        private static void SetSpecificVolume()
        {
            Console.Write("Enter volume (0-100): ");
            string? input = Console.ReadLine();

            if (!float.TryParse(input, out float percentage) || percentage < 0 || percentage > 100)
            {
                Console.WriteLine("Invalid input. Enter a number between 0 and 100.");
                return;
            }

            float updated = Math.Clamp(percentage / 100.0f, 0.0f, 1.0f);
            device.AudioEndpointVolume.MasterVolumeLevelScalar = updated;

            Console.WriteLine($"Volume set to: {updated * 100:F0}%");
        }


        static void Main()
        {
            Console.WriteLine("Audio Control Console Application");
            Console.WriteLine("Press '+' to increase, '-' to decrease, 's' to set, 'q' to quit.");

            enumerator = new MMDeviceEnumerator();
            device = enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);

            RunMainLoop();
        }
    }
}