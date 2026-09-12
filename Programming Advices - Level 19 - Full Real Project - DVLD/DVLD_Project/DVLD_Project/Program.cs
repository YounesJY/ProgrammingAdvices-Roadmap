using DVLD_Project.People;
using System;
using System.Windows.Forms;


namespace DVLD_Project
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // EventLogger.Configure("DVLD_Application", "DVLD_Events");
            Application.Run(new LoginFrom());
        }
    }
}