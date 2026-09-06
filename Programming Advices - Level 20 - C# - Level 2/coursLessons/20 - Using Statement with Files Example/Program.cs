using System;
using System.IO;

class Program
{
    static void Main()
    {
        string filePath = "example.txt"; // Replace with the path to your text file.

        /*
            ==============
            == Remember ==  => StreamReader : TextReader implements the IDisposable Iface
            ==============


                // Summary:
                //     Provides a mechanism for releasing unmanaged resources.
                [ComVisible(true)]
                [__DynamicallyInvokable]
                public interface IDisposable
                {
                    //
                    // Summary:
                    //     Performs application-defined tasks associated with freeing, releasing, or resetting
                    //     unmanaged resources.
                    [__DynamicallyInvokable]
                    void Dispose();
                }

        */
        try
        {

            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;

                while ((line = reader.ReadLine()) != null)
                    Console.WriteLine(line);
            }
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine($"File not found: {filePath}");
        }
        catch (IOException ex)
        {
            Console.WriteLine($"An error occurred while reading the file: {ex.Message}");
        }
    }
}
