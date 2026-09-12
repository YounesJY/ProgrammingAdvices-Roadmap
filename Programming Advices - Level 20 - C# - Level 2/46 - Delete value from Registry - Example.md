## Delete value from Registry - Example

In C#, you can use the `Microsoft.Win32` namespace to interact with the Windows Registry. Here's an example of how you can delete a registry value using C#:

```csharp
using Microsoft.Win32;
using System;
class Program
{
    static void Main()
    {
        // Specify the registry key path and value name
        string keyPath = @"SOFTWARE\YourSoftware";
        string valueName = "YourValueName";
        try
        {
            // Open the registry key in read/write mode with explicit registry view
            using (RegistryKey baseKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64))
            {
                using (RegistryKey key = baseKey.OpenSubKey(keyPath, true))
                {
                    if (key != null)
                    {
                        // Delete the specified value
                        key.DeleteValue(valueName);
                        Console.WriteLine($"Successfully deleted value '{valueName}' from registry key '{keyPath}'");
                    }
                    else
                    {
                        Console.WriteLine($"Registry key '{keyPath}' not found");
                    }
                }
            }
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine("UnauthorizedAccessException: Run the program with administrative privileges.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
        Console.ReadKey();
    }
}
```

**Explanation:**

- Namespaces:

using Microsoft.Win32;
using System;

- - `Microsoft.Win32`: This namespace provides classes that allow you to interact with the Windows Registry. In this case, it's used for registry-related operations.
  - `System`: This namespace provides fundamental types, including the `Console` class used for console-based input and output.

- Registry Key and Value Information:

```csharp
string keyPath = @"SOFTWARE\YourSoftware";
string valueName = "YourValueName";
```

- - Specifies the registry key path (`keyPath`) and the name of the value (`valueName`) you want to delete.

- Opening Registry Key with Explicit Registry View:

```csharp
using (RegistryKey baseKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64))
{
    using (RegistryKey key = baseKey.OpenSubKey(keyPath, true))
    {
        // Code to work with the registry key
    }
}
```

- - `RegistryKey.OpenBaseKey`: Opens the specified base registry key in the specified view (64-bit in this case).
  - `using` statement ensures that the resources associated with the registry keys are properly released when the block is exited.
  - `OpenSubKey`: Opens a subkey with the specified name or returns `null` if the operation failed.

- Checking for Key Existence and Deleting a Value:

```csharp
if (key != null)
{
    // Delete the specified value
    key.DeleteValue(valueName);
    Console.WriteLine($"Successfully deleted value '{valueName}' from registry key '{keyPath}'");
}
else
{
    Console.WriteLine($"Registry key '{keyPath}' not found");
}

- - Checks if the registry key (`key`) exists. If it does, it proceeds to delete the specified value (`valueName`) and prints a success message. If the key does not exist, it prints a message indicating that the key was not found.

- Handling UnauthorizedAccessException:

catch (UnauthorizedAccessException)
{
    Console.WriteLine("UnauthorizedAccessException: Run the program with administrative privileges.");
}
```

- - Catches and handles the `UnauthorizedAccessException`, printing a message indicating that the program needs to be run with administrative privileges.

In summary, this program demonstrates how to delete a registry value under `HKEY_CURRENT_USER\SOFTWARE`. It includes error handling for cases where the key is not found or when the program lacks the necessary privileges to access the registry. The explicit use of the 64-bit registry view is also included to handle cases where registry redirection might be in effect.
