using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business
{
    internal static class Global
    {
        public static User currentLoggedInUser = new User();

        internal static void RememberLoggedInUser(string username, string password)
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
        internal static (string username, string password) GetStoredCredentials()
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
                            return (parts[0], parts[1]);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error reading login record: {ex.Message}");
            }
            return (null, null);
        }
        internal static void ClearStoredCredentials()
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
    }
}
