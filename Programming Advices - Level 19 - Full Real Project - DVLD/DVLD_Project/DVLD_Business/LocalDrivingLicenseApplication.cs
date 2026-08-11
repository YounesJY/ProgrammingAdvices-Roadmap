using System;
using System.Data;
using DVLD_DataAccess;
using DVLD_Common;

namespace DVLD_Business
{
    public class LocalDrivingLicenseApplication : ApplicationInfo
    {
        /*
            You already have these enums in the base class Application, so you don't need to redefine them here.
        public enum enMode : byte { AddNew = 0, Update = 1 }
        private enMode _Mode = enMode.AddNew;

            ===================================================================================
            =================================== REFACTORING NOTE: =============================
            ====== VERY IMPORTANT: DO NOT REMOVE THE _Mode FIELD FROM THIS DERIVED CLASS ======
            ===================================================================================

            THE COMMENT ABOVE IS TRUE, BUT I THINK IT'S STILL USEFUL TO HAVE A LOCAL _Mode FIELD IN THIS DERIVED CLASS, BECAUSE IT ALLOWS US TO TRACK THE STATE OF THIS SPECIFIC CLASS SEPARATELY FROM THE BASE CLASS. 
            THE REASON WHY WE HAVE A LOCAL _Mode FIELD IS BECAUSE THE BASE CLASS ApplicationInfo ALSO HAS A _Mode FIELD, AND WE WANT TO KEEP TRACK OF THE STATE OF THIS DERIVED CLASS SEPARATELY.
            THE DIFFERENCE BETWEEN THE TWO _Mode FIELDS IS THAT THE BASE CLASS _Mode FIELD TRACKS THE STATE OF THE APPLICATION TABLE, WHILE THIS DERIVED CLASS _Mode FIELD TRACKS THE STATE OF THE LocalDrivingLicenseApplication TABLE.
            THIS DIFFERENCE IS LISTED INSIDE THE Save() METHOD BELOW, WHERE WE SYNCHRONIZE THE TWO _Mode FIELDS BEFORE CALLING base.Save().

        */
        public int LocalDrivingLicenseApplicationID { get; private set; }
        private int _LicenseClassID;
        public int LicenseClassID
        {
            get { return _LicenseClassID; }
            set
            {
                _LicenseClassID = value;
                LicenseClassInfo = LicenseClass.Find(value);
            }
        }
        public LicenseClass LicenseClassInfo { get; private set; }
        public string PersonFullName
        {
            get
            {
                return base.ApplicantFullName;
            }
        }
        private new enMode _Mode = enMode.AddNew;

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
                        applicationInfo.ApplicationStatusID, applicationInfo.LastStatusDate,
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
                        applicationInfo.ApplicationStatusID, applicationInfo.LastStatusDate,
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

            /*
                READ THIS: The _Mode field in the derived class is used to track the state of the LocalDrivingLicenseApplication table, while the _Mode field in the base class is used to track the state of the Application table.
                WE CAN'T REMOVE THE _Mode FIELD FROM THE DERIVED CLASS BECAUSE IT IS USED TO TRACK THE STATE OF THE LocalDrivingLicenseApplication TABLE.
                THIS WILL CAUSE ISSUES WHEN WE TRY TO SAVE OR UPDATE THE LocalDrivingLicenseApplication TABLE, BECAUSE THE BASE CLASS Save METHOD WILL NOT KNOW WHETHER TO INSERT OR UPDATE THE LocalDrivingLicenseApplication TABLE.
                THE ApplicationInfo save() Method will override the _Mode field in the derived class, which will cause issues when we try to save or update the LocalDrivingLicenseApplication table.
                
                [CORE PROBLEM]
                For examle, if we are adding a new LocalDrivingLicenseApplication, the derived class _Mode field will be set to AddNew,
            but the base class _Mode field will be set to Update after calling base.Save(),
            which will cause issues when we try to save or update the LocalDrivingLicenseApplication table.
                
            WHILE DEBUGGING, 
                this._Mode	AddNew	DVLD_Business.ApplicationInfo.enMode
		        base._Mode	Update	DVLD_Business.ApplicationInfo.enMode


             */

            base._Mode = (ApplicationInfo.enMode)this._Mode;
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

            LicenseInfo license = new LicenseInfo();
            license.ApplicationID = this.ApplicationID;
            license.DriverID = DriverID;
            license.LicenseClassID = this.LicenseClassID;
            license.IssueDate = DateTime.Now;
            license.ExpirationDate = DateTime.Now.AddYears(this.LicenseClassInfo.DefaultValidityLength);
            license.Notes = Notes;
            license.PaidFees = this.LicenseClassInfo.ClassFees;
            license.IsActive = true;
            license.IssueReason = LicenseInfo.enIssueReason.FirstTime;
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
            return LicenseInfo.GetActiveLicenseIDByPersonID(this.ApplicantPersonID, this.LicenseClassID);
        }
        
    }
}
