using System;
using Microsoft.Win32;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace DVLD_Business
{
    /// <summary>
    /// Global class that manages application-wide settings and credentials.
    /// Provides methods for storing, retrieving, and clearing user credentials using Windows Registry.
    /// </summary>
    internal static class Global
    {
        /// <summary>
        /// The current logged-in user instance.
        /// </summary>
        public static User currentLoggedInUser = new User();

        /// <summary>
        /// Registry path for storing application credentials and settings.
        /// </summary>
        private const string RegistryKeyPath = @"HKEY_CURRENT_USER\SOFTWARE\DVLD";

        /// <summary>
        /// Registry key name for storing the username.
        /// </summary>
        private const string UsernameKeyName = "username";

        /// <summary>
        /// Registry key name for storing the password.
        /// </summary>
        private const string PasswordKeyName = "password";

        /// <summary>
        /// Retrieves stored credentials from the Windows Registry.
        /// </summary>
        /// <param name="username">Output parameter that receives the stored username.</param>
        /// <param name="password">Output parameter that receives the stored password.</param>
        /// <returns>
        /// Returns true if credentials are found and retrieved successfully; otherwise, returns false.
        /// </returns>
        /// <remarks>
        /// This method attempts to read the username and password values from the Windows Registry
        /// at the path defined by <see cref="RegistryKeyPath"/>. If either value is empty or null,
        /// the method returns false.
        /// </remarks>
        internal static bool GetStoredCredentials(ref string username, ref string password)
        {
            try
            {
                username = Registry.GetValue(RegistryKeyPath, UsernameKeyName, null) as string;
                password = Registry.GetValue(RegistryKeyPath, PasswordKeyName, null) as string;

                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                    return false;

                return true;
            }
            catch (Exception exception)
            {
                System.Diagnostics.Debug.WriteLine($"Exception while retrieving stored credentials: {exception.Message}");
                return false;
            }
        }

        /// <summary>
        /// Stores the user's credentials in the Windows Registry for future use.
        /// </summary>
        /// <param name="username">The username to store in the Registry.</param>
        /// <param name="password">The password to store in the Registry.</param>
        /// <remarks>
        /// This method saves the provided username and password as Registry values in the
        /// Registry key path defined by <see cref="RegistryKeyPath"/>. The credentials are stored
        /// using the key names defined by <see cref="UsernameKeyName"/> and <see cref="PasswordKeyName"/>.
        /// If an exception occurs during the storage operation, it is logged to the Debug output.
        /// </remarks>
        internal static void RememberLoggedInUser(string username, string password)
        {
            try
            {
                Registry.SetValue(RegistryKeyPath, UsernameKeyName, username);
                Registry.SetValue(RegistryKeyPath, PasswordKeyName, password);
            }
            catch (Exception exception)
            {
                System.Diagnostics.Debug.WriteLine($"Exception while storing credentials: {exception.Message}");
            }
        }

        /// <summary>
        /// Clears the stored credentials from the Windows Registry.
        /// </summary>
        /// <remarks>
        /// This method removes the stored username and password by deleting their values
        /// from the Windows Registry at the path defined by <see cref="RegistryKeyPath"/>.
        /// The method sets the values to empty strings instead of deleting them to maintain
        /// the Registry structure. If an exception occurs during the clearing operation,
        /// it is logged to the Debug output.
        /// </remarks>
        internal static void ClearStoredCredentials()
        {
            try
            {
                Registry.SetValue(RegistryKeyPath, UsernameKeyName, string.Empty);
                Registry.SetValue(RegistryKeyPath, PasswordKeyName, string.Empty);
            }
            catch (Exception exception)
            {
                System.Diagnostics.Debug.WriteLine($"Exception while clearing credentials: {exception.Message}");
            }
        }
    }
}
