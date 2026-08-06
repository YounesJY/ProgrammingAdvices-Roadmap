using System;
using System.Data;
using DVLD_DataAccess;
using DVLD_Common;

namespace DVLD_Business
{
    public class LocalDrivingLicenseApplication : ApplicationInfo
    {
        /*
            *  You already have these enums in the base class Application, so you don't need to redefine them here.
        public enum enMode : byte { AddNew = 0, Update = 1 }
        private enMode _Mode = enMode.AddNew;
        */
        public int LocalDrivingLicenseApplicationID { get; set; }
        public int LicenseClassID { get; set; }
        public LicenseClass LicenseClassInfo { get; set; }
        public string PersonFullName
        {
            get
            {
                return base.ApplicantFullName;
            }
        }


        public LocalDrivingLicenseApplication()
        {
            this.LocalDrivingLicenseApplicationID = ValidationConstants.INVALID_ID;
            this.LicenseClassID = ValidationConstants.INVALID_ID;

            this._Mode = enMode.AddNew;
        }
        private LocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID, int ApplicationID, int ApplicantPersonID,
            DateTime ApplicationDate, int ApplicationTypeID, enApplicationStatus ApplicationStatus, DateTime LastStatusDate,
            float PaidFees, int CreatedByUserID, int LicenseClassID)
            : base(ApplicationID, ApplicantPersonID, ApplicationDate, ApplicationTypeID, ApplicationStatus, LastStatusDate, PaidFees, CreatedByUserID)
        {
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.LicenseClassID = LicenseClassID;
            this.LicenseClassInfo = LicenseClass.Find(LicenseClassID);

            this._Mode = enMode.Update;
        }

        private bool _AddNewLocalDrivingLicenseApplication()
        {
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationData.AddNewLocalDrivingLicenseApplication(this.ApplicationID, this.LicenseClassID);

            return (this.LocalDrivingLicenseApplicationID != ValidationConstants.INVALID_ID);
        }
        private bool _UpdateLocalDrivingLicenseApplication()
        {
            return LocalDrivingLicenseApplicationData.UpdateLocalDrivingLicenseApplication(this.LocalDrivingLicenseApplicationID, this.ApplicationID, this.LicenseClassID);
        }

        public static DataTable GetAllLocalDrivingLicenseApplications()
        {
            return LocalDrivingLicenseApplicationData.GetAllLocalDrivingLicenseApplications();
        }
        public static LocalDrivingLicenseApplication FindByLocalDrivingAppLicenseID(int LocalDrivingLicenseApplicationID)
        {
            int ApplicationID = ValidationConstants.INVALID_ID;
            int LicenseClassID = ValidationConstants.INVALID_ID;

            if (LocalDrivingLicenseApplicationData.GetLocalDrivingLicenseApplicationByID(LocalDrivingLicenseApplicationID, ref ApplicationID, ref LicenseClassID))
            {
                ApplicationInfo applicationInfo = ApplicationInfo.Find(ApplicationID);

                if (applicationInfo != null)
                {
                    return new LocalDrivingLicenseApplication(
                        LocalDrivingLicenseApplicationID, applicationInfo.ApplicationID,
                        applicationInfo.ApplicantPersonID, applicationInfo.ApplicationDate, applicationInfo.ApplicationTypeID,
                        applicationInfo.ApplicationStatus, applicationInfo.LastStatusDate,
                        applicationInfo.PaidFees, applicationInfo.CreatedByUserID, LicenseClassID);
                }
            }

            return null;
        }
        public static LocalDrivingLicenseApplication FindByApplicationID(int ApplicationID)
        {
            int LocalDrivingLicenseApplicationID = ValidationConstants.INVALID_ID;
            int LicenseClassID = ValidationConstants.INVALID_ID;

            if (LocalDrivingLicenseApplicationData.GetLocalDrivingLicenseApplicationByApplicationID(ApplicationID, ref LocalDrivingLicenseApplicationID, ref LicenseClassID))
            {
                ApplicationInfo applicationInfo = ApplicationInfo.Find(ApplicationID);

                if (applicationInfo != null)
                {
                    return new LocalDrivingLicenseApplication(
                        LocalDrivingLicenseApplicationID, applicationInfo.ApplicationID,
                        applicationInfo.ApplicantPersonID, applicationInfo.ApplicationDate, applicationInfo.ApplicationTypeID,
                        applicationInfo.ApplicationStatus, applicationInfo.LastStatusDate,
                        applicationInfo.PaidFees, applicationInfo.CreatedByUserID, LicenseClassID);
                }
            }

            return null;
        }
        public override bool Save()
        {
            // Call base class Save to handle Application table
            /* 
                We have a problem here because the base class Save method uses its own _Mode field, which is not the same as the derived class _Mode field.
            We need to synchronize them before calling base.Save().
                That's the problem. The base class Save method will use its own _Mode field, which may not reflect the correct state of the derived class.
            Should we keep only one _Mode field in the base class and remove the _Mode field from the derived class? That would simplify things.
                but then we would lose the ability to have different modes for different derived classes. We need to think about this design decision carefully.
             */
            base._Mode = (ApplicationInfo.enMode)_Mode; // ?? 

            if (!base.Save())
                return false;

            // Save LocalDrivingLicenseApplication specific data
            switch (this._Mode)
            {
                case enMode.AddNew:
                    if (_AddNewLocalDrivingLicenseApplication())
                    {
                        this._Mode = enMode.Update;
                        return true;
                    }
                    return false;

                case enMode.Update:
                    return _UpdateLocalDrivingLicenseApplication();
            }

            return false;
        }
        public override bool Delete()
        {
            bool isLocalDrivingApplicationDeleted = LocalDrivingLicenseApplicationData.DeleteLocalDrivingLicenseApplication(this.LocalDrivingLicenseApplicationID);

            if (!isLocalDrivingApplicationDeleted)
                return false;

            return base.Delete();
        }


        // These methods are commented out because they are not yet implemented in the LocalDrivingLicenseApplicationData class.
        // We can implement them later when we have the necessary data access methods.
        /*
        public bool DoesPassTestType(TestType.enTestType TestTypeID)
        {
            return LocalDrivingLicenseApplicationData.DoesPassTestType(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }
        public bool DoesPassPreviousTest(TestType.enTestType CurrentTestType)
        {
            switch (CurrentTestType)
            {
                case TestType.enTestType.VisionTest:
                    return true;

                case TestType.enTestType.WrittenTest:
                    return this.DoesPassTestType(TestType.enTestType.VisionTest);

                case TestType.enTestType.StreetTest:
                    return this.DoesPassTestType(TestType.enTestType.WrittenTest);

                default:
                    return false;
            }
        }
        public static bool DoesPassTestType(int LocalDrivingLicenseApplicationID, TestType.enTestType TestTypeID)
        {
            return LocalDrivingLicenseApplicationData.DoesPassTestType(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }
        public bool DoesAttendTestType(TestType.enTestType TestTypeID)
        {
            return LocalDrivingLicenseApplicationData.DoesAttendTestType(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }
        public byte TotalTrialsPerTest(TestType.enTestType TestTypeID)
        {
            return LocalDrivingLicenseApplicationData.TotalTrialsPerTest(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }
        public static byte TotalTrialsPerTest(int LocalDrivingLicenseApplicationID, TestType.enTestType TestTypeID)
        {
            return LocalDrivingLicenseApplicationData.TotalTrialsPerTest(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }
        public static bool AttendedTest(int LocalDrivingLicenseApplicationID, TestType.enTestType TestTypeID)
        {
            return LocalDrivingLicenseApplicationData.TotalTrialsPerTest(LocalDrivingLicenseApplicationID, (int)TestTypeID) > 0;
        }
        public bool AttendedTest(TestType.enTestType TestTypeID)
        {
            return LocalDrivingLicenseApplicationData.TotalTrialsPerTest(this.LocalDrivingLicenseApplicationID, (int)TestTypeID) > 0;
        }
        public static bool IsThereAnActiveScheduledTest(int LocalDrivingLicenseApplicationID, TestType.enTestType TestTypeID)
        {
            return LocalDrivingLicenseApplicationData.IsThereAnActiveScheduledTest(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }
        public bool IsThereAnActiveScheduledTest(TestType.enTestType TestTypeID)
        {
            return LocalDrivingLicenseApplicationData.IsThereAnActiveScheduledTest(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }
        public Test GetLastTestPerTestType(TestType.enTestType TestTypeID)
        {
            return Test.FindLastTestPerPersonAndLicenseClass(this.ApplicantPersonID, this.LicenseClassID, TestTypeID);
        }
        public byte GetPassedTestCount()
        {
            return Test.GetPassedTestCount(this.LocalDrivingLicenseApplicationID);
        }
        public static byte GetPassedTestCount(int LocalDrivingLicenseApplicationID)
        {
            return Test.GetPassedTestCount(LocalDrivingLicenseApplicationID);
        }
        public bool PassedAllTests()
        {
            return Test.PassedAllTests(this.LocalDrivingLicenseApplicationID);
        }
        public static bool PassedAllTests(int LocalDrivingLicenseApplicationID)
        {
            return Test.PassedAllTests(LocalDrivingLicenseApplicationID);
        }

        public int IssueLicenseForTheFirtTime(string Notes, int CreatedByUserID)
        {
            int DriverID = ValidationConstants.INVALID_ID;

            Driver driver = Driver.FindByPersonID(this.ApplicantPersonID);

            if (driver == null)
            {
                driver = new Driver();
                driver.PersonID = this.ApplicantPersonID;
                driver.CreatedByUserID = CreatedByUserID;

                if (driver.Save())
                {
                    DriverID = driver.DriverID;
                }
                else
                {
                    return ValidationConstants.INVALID_ID;
                }
            }
            else
            {
                DriverID = driver.DriverID;
            }

            License license = new License();
            license.ApplicationID = this.ApplicationID;
            license.DriverID = DriverID;
            license.LicenseClass = this.LicenseClassID;
            license.IssueDate = DateTime.Now;
            license.ExpirationDate = DateTime.Now.AddYears(this.LicenseClassInfo.DefaultValidityLength);
            license.Notes = Notes;
            license.PaidFees = this.LicenseClassInfo.ClassFees;
            license.IsActive = true;
            license.IssueReason = License.enIssueReason.FirstTime;
            license.CreatedByUserID = CreatedByUserID;

            if (license.Save())
            {
                this.SetComplete();
                return license.LicenseID;
            }

            return ValidationConstants.INVALID_ID;
        }
        public bool IsLicenseIssued()
        {
            return (GetActiveLicenseID() != ValidationConstants.INVALID_ID);
        }
        public int GetActiveLicenseID()
        {
            return License.GetActiveLicenseIDByPersonID(this.ApplicantPersonID, this.LicenseClassID);
        }
        */
    }
}
