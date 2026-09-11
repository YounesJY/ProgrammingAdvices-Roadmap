using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace DVLD_Business
{
    internal static class Global
    {
        public static User currentLoggedInUser = new User();

        internal static bool GetStoredCredentials(ref string username, ref string password, bool usingRegistry = false)
        {
            if (!usingRegistry)
            {
                try
                {
                    string loginFilePath = "login.txt";
                    if (System.IO.File.Exists($"D:\\Study\\IT\\Programming Advices\\Programming Advices - Level 19 - Full Real Project - DVLD\\DVLD_Project\\DVLD_Project\\{loginFilePath}"))
                    {
                        var lines = System.IO.File.ReadAllLines($"D:\\Study\\IT\\Programming Advices\\Programming Advices - Level 19 - Full Real Project - DVLD\\DVLD_Project\\DVLD_Project\\{loginFilePath}");
                        if (lines.Length > 0)
                        {
                            var lastLine = lines[lines.Length - 1];
                            var parts = lastLine.Split(',');
                            if (parts.Length == 2)
                            {
                                username = parts[0];
                                password = parts[1];
                                return true;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error reading login record: {ex.Message}");
                }
                return false;
            }
            else
            {
                string keyName = @"HKEY_CURRENT_USER\SOFTWARE\DVLD";
                string[] valueName = { "username", "password" };


                try
                {
                    username = Registry.GetValue(keyName, valueName[0], null) as string;
                    password = Registry.GetValue(keyName, valueName[1], null) as string;

                    if (String.IsNullOrEmpty(username) && String.IsNullOrEmpty(password))
                        return false;
                }
                catch (Exception exception)
                {
                    System.Diagnostics.Debug.WriteLine($"Exception : {exception}");
                }

                return true;
            }
        }
        internal static void RememberLoggedInUser(string username, string password, bool usingRegistry = false)
        {
            if (!usingRegistry)
            {
                try
                {
                    string loginFilePath = "login.txt";
                    string csvLine = $"{username},{password}";
                    System.IO.File.AppendAllText($"D:\\Study\\IT\\Programming Advices\\Programming Advices - Level 19 - Full Real Project - DVLD\\DVLD_Project\\DVLD_Project\\{loginFilePath}", csvLine + Environment.NewLine);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error writing login record: {ex.Message}");
                }
            }
            else
            {
                string keyName = @"HKEY_CURRENT_USER\SOFTWARE\DVLD";
                string[] valueName = { "username", "password" };
                string[] valueData = { username, password };


                try
                {
                    Registry.SetValue(keyName, valueName[0], valueData[0]);
                    Registry.SetValue(keyName, valueName[1], valueData[1]);
                }
                catch (Exception exception)
                {
                    System.Diagnostics.Debug.WriteLine($"Exception : {exception}");
                }
            }
        }
        internal static void ClearStoredCredentials(bool usingRegistry = false)
        {
            if (!usingRegistry)
            {
                try
                {
                    string loginFilePath = "login.txt";
                    if (System.IO.File.Exists($"D:\\Study\\IT\\Programming Advices\\Programming Advices - Level 19 - Full Real Project - DVLD\\DVLD_Project\\DVLD_Project\\{loginFilePath}"))
                        System.IO.File.Delete($"D:\\Study\\IT\\Programming Advices\\Programming Advices - Level 19 - Full Real Project - DVLD\\DVLD_Project\\DVLD_Project\\{loginFilePath}");
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error clearing login record: {ex.Message}");
                }
            }
            else
            {
                string keyName = @"HKEY_CURRENT_USER\SOFTWARE\DVLD";
                string[] valueName = { "username", "password" };


                try
                {
                    Registry.SetValue(keyName, valueName[0], String.Empty);
                    Registry.SetValue(keyName, valueName[1], String.Empty);
                }
                catch (Exception exception)
                {
                    System.Diagnostics.Debug.WriteLine($"Exception : {exception}");
                }
            }
        }
    }
}
