using System;
using System.Data;
using DVLD_DataAccess;
using DVLD_Common;

namespace DVLD_Business
{
    public class Application
    {
        public enum enMode : byte { AddNew = 0, Update = 1 }
        /*
            this is Wrong 
                Why we haven't put this enum inside ApplicationType class? Because ApplicationType is a separate entity that represents the type of application,
                while enApplicationType is an enumeration that represents specific application types. Keeping them separate allows for better organization and clarity in the code.
            I'll edit this later whenever I understand the whole project and its structure, but for now, I'll keep it here.
        */
        public enum enApplicationType { NewDrivingLicense = 1, RenewDrivingLicense = 2, ReplaceLostDrivingLicense = 3, ReplaceDamagedDrivingLicense = 4, ReleaseDetainedDrivingLicense = 5, NewInternationalLicense = 6, RetakeTest = 7 }
        public enum enApplicationStatus { New = 1, Cancelled = 2, Completed = 3 }

        private enMode _Mode = enMode.AddNew;

        public int ApplicationID { get; private set; }
        public int ApplicantPersonID { get; private set; }
        public string ApplicantFullName
        {
            get
            {
                Person person = Person.Find(ApplicantPersonID);
                return (person != null) ? person.FullName : "Unknown";
            }
        }
        public DateTime ApplicationDate { get; private set; }
        public int ApplicationTypeID { get; private set; }
        public ApplicationType ApplicationTypeInfo { get; private set; }
        public enApplicationStatus ApplicationStatus { get; private set; }
        public string StatusText
        {
            get
            {
                switch (ApplicationStatus)
                {
                    case enApplicationStatus.New:
                        return "New";
                    case enApplicationStatus.Cancelled:
                        return "Cancelled";
                    case enApplicationStatus.Completed:
                        return "Completed";
                    default:
                        return "Unknown";
                }
            }
        }
        public DateTime LastStatusDate { get; private set; }
        public float PaidFees { get; private set; }
        public int CreatedByUserID { get; private set; }
        public User CreatedByUserInfo { get; private set; }


        public Application()
        {
            this.ApplicationID = ValidationConstants.INVALID_ID;
            this.ApplicantPersonID = ValidationConstants.INVALID_ID;
            this.ApplicationDate = DateTime.Now;
            this.ApplicationTypeID = ValidationConstants.INVALID_ID;
            this.ApplicationStatus = enApplicationStatus.New;
            this.LastStatusDate = DateTime.Now;
            this.PaidFees = 0;
            this.CreatedByUserID = ValidationConstants.INVALID_ID;

            this._Mode = enMode.AddNew;
        }
        private Application(int ApplicationID, int ApplicantPersonID, DateTime ApplicationDate, int ApplicationTypeID,
            enApplicationStatus ApplicationStatus, DateTime LastStatusDate, float PaidFees, int CreatedByUserID)
        {
            this.ApplicationID = ApplicationID;
            this.ApplicantPersonID = ApplicantPersonID;
            this.ApplicationDate = ApplicationDate;
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationTypeInfo = ApplicationType.Find(ApplicationTypeID);
            this.ApplicationStatus = ApplicationStatus;
            this.LastStatusDate = LastStatusDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            this.CreatedByUserInfo = User.FindByUserID(CreatedByUserID);

            this._Mode = enMode.Update;
        }

        private bool _AddNewApplication()
        {
            this.ApplicationID = ApplicationData.AddNewApplication(
                this.ApplicantPersonID, this.ApplicationDate,
                this.ApplicationTypeID, (byte)this.ApplicationStatus,
                this.LastStatusDate, this.PaidFees, this.CreatedByUserID
            );

            return (this.ApplicationID != ValidationConstants.INVALID_ID);
        }
        private bool _UpdateApplication()
        {
            return ApplicationData.UpdateApplication(
                this.ApplicationID, this.ApplicantPersonID, this.ApplicationDate,
                this.ApplicationTypeID, (byte)this.ApplicationStatus,
                this.LastStatusDate, this.PaidFees, this.CreatedByUserID
            );
        }


        public static bool IsApplicationExist(int ApplicationID)
        {
            return ApplicationData.IsApplicationExist(ApplicationID);
        }
        public static Application Find(int ApplicationID)
        {
            int ApplicantPersonID = ValidationConstants.INVALID_ID;
            DateTime ApplicationDate = DateTime.Now;
            int ApplicationTypeID = ValidationConstants.INVALID_ID;
            byte ApplicationStatus = 1;
            DateTime LastStatusDate = DateTime.Now;
            float PaidFees = 0;
            int CreatedByUserID = ValidationConstants.INVALID_ID;

            if (ApplicationData.GetApplicationInfoByID(ApplicationID, ref ApplicantPersonID, ref ApplicationDate, ref ApplicationTypeID, ref ApplicationStatus, ref LastStatusDate, ref PaidFees, ref CreatedByUserID))
                return new Application(ApplicationID, ApplicantPersonID, ApplicationDate, ApplicationTypeID, (enApplicationStatus)ApplicationStatus, LastStatusDate, PaidFees, CreatedByUserID);

            return null;
        }

        public bool Delete()
        {
            return ApplicationData.DeleteApplication(this.ApplicationID);
        }
        public bool Cancel()
        {
            return ApplicationData.UpdateStatus(ApplicationID, (byte)enApplicationStatus.Cancelled);
        }
        public bool SetComplete()
        {
            return ApplicationData.UpdateStatus(ApplicationID, (byte)enApplicationStatus.Completed);
        }
        public bool Save()
        {
            switch (this._Mode)
            {
                case enMode.AddNew:
                    if (_AddNewApplication())
                    {
                        this._Mode = enMode.Update;
                        return true;
                    }
                    return false;

                case enMode.Update:
                    return _UpdateApplication();
            }

            return false;
        }






        public bool DoesPersonHaveActiveApplication(int ApplicationTypeID)
        {
            return DoesPersonHaveActiveApplication(this.ApplicantPersonID, ApplicationTypeID);
        }
        public static bool DoesPersonHaveActiveApplication(int PersonID, int ApplicationTypeID)
        {
            return ApplicationData.DoesPersonHaveActiveApplication(PersonID, ApplicationTypeID);
        }
        public int GetActiveApplicationID(enApplicationType ApplicationTypeID)
        {
            return GetActiveApplicationID(this.ApplicantPersonID, ApplicationTypeID);
        }
        public static int GetActiveApplicationID(int PersonID, enApplicationType ApplicationTypeID)
        {
            return ApplicationData.GetActiveApplicationID(PersonID, (int)ApplicationTypeID);
        }
        public static int GetActiveApplicationIDForLicenseClass(int PersonID, enApplicationType ApplicationTypeID, int LicenseClassID)
        {
            return ApplicationData.GetActiveApplicationIDForLicenseClass(PersonID, (int)ApplicationTypeID, LicenseClassID);
        }
    }
}
