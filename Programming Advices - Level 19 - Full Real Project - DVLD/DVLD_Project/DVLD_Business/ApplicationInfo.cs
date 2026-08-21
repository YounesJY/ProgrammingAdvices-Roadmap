using System;
using System.Data;
using DVLD_DataAccess;
using DVLD_Common;

namespace DVLD_Business
{
    /*
            * ===================================
            * ======= REFACTORING NOTE: =========
            * ===================================
        This class has been renamed from "Application" to "ApplicationInfo" to avoid confusion with the System.Windows.Forms.Application class.
    */
    public class ApplicationInfo
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

        /*
            * HIGH PRIORITY: [THE CORE PROBLEM]
            * PLEASE READ THIS CAREFULLY AND UNDERSTAND IT. This is the core problem of this class, and it needs to be fixed.
            * KEEP IN MIND THAT THIS IS A HIGH PRIORITY ISSUE, AND IT NEEDS TO BE FIXED AS SOON AS POSSIBLE. 
            * Remember that this class is the core of the application, and it needs to be designed properly to ensure that the application works correctly.
          
            There's a serious problem with the design of this class. The Application class is tightly coupled with the Person and User classes, which makes it difficult to test and maintain.
        It would be better to use dependency injection to pass in the necessary dependencies, rather than having the Application class directly call static methods on the Person and User classes.
        This would make the code more modular and easier to test.
        [BUT currently we didn't learn about dependency injection]


            Another problem [THE CORE PROBLEM] is that we store the ApplicationTypeID,ApplicantPersonID and CreatedByUserID as integers,
        but we also store the full ApplicationTypeInfo, Person and User objects.
        This is redundant and can lead to inconsistencies if the IDs and objects get out of sync. 

        =============================================
        === CORE PROBLEM PROVEN & EXPLAINED BELOW ===
        =============================================

        Here's an example of how this can lead to inconsistencies [rows where * is null], this is an example captured during debugging,:

        -localDrivingLicenseApplication	{DVLD_Business.LocalDrivingLicenseApplication}	DVLD_Business.LocalDrivingLicenseApplication
		    ApplicantFullName	                "Unknown"	                string
		    ApplicantPersonID	                13	                        int
		    ApplicantPersonInfo	              * null	                    DVLD_Business.Person
    		ApplicationDate	                    {8/7/2026 5:37:03 PM}	    System.DateTime
		    ApplicationID	                    3004	                    int
		    ApplicationStatusID	                New	                        DVLD_Business.ApplicationInfo.enApplicationStatus
		    ApplicationTypeID	                1	                        int
		    ApplicationTypeInfo	              * null	                    DVLD_Business.ApplicationType
		    CreatedByUserID	                    1028	                    int
		    CreatedByUserInfo	              * null	                    DVLD_Business.User
    		LastStatusDate	                    {8/7/2026 5:37:03 PM}	    System.DateTime
		    LicenseClassID	                    1	                        int
		    LicenseClassInfo	              * null	                    DVLD_Business.LicenseClass
		    LocalDrivingLicenseApplicationID	3003	                    int
		    PaidFees	                        15	                        float
		    PersonFullName	                  * "Unknown"	                string
		    StatusText	                        "New"	                    string
		    _Mode                           	Update	                    DVLD_Business.ApplicationInfo.enMode
		    _Mode	                            Update	                    DVLD_Business.ApplicationInfo.enMode

        as you can see: 
            The ApplicantPersonID is 13, but the ApplicantPersonInfo is null. This means that the Person object was not loaded correctly, and the full name is "Unknown".
            same thing with ApplicationTypeInfo, LicenseClassInfo and CreatedByUserInfo, they are all null, which means that the objects were not loaded correctly.
            
            We have to fix this problem by either:
                1. Remove the full objects (ApplicantPersonInfo, ApplicationTypeInfo, CreatedByUserInfo) and only keep the IDs (ApplicantPersonID, ApplicationTypeID, CreatedByUserID). This will make the class simpler and avoid inconsistencies.
                2. Make these objects lazy-loaded, so that they are only loaded when needed. This will make the class more efficient and avoid unnecessary database calls.
            this can be done by a property getter that checks if the object is null, and if so, loads it from the database using the ID. This will make the class more efficient and avoid unnecessary database calls.
            exmaple:
                    public Person ApplicantPersonInfo
                    {
                        get
                        {
                            if (_ApplicantPersonInfo == null && _ApplicantPersonID != ValidationConstants.INVALID_ID)
                            {
                                _ApplicantPersonInfo = Person.Find(_ApplicantPersonID);
                            }
                            return _ApplicantPersonInfo;
                        }
                    }
                3. Auto load the full objects when the IDs are set, so that they are always in sync. This will make the class more efficient and avoid unnecessary database calls.
            and that's by edit the setters of the IDs to load the full objects when the IDs are set.
            WE'RE GOING TO FIX THIS PROBLEM BY IMPLEMENTING OPTION 3, which is the best option for this case.

            ONE GOOD THING about this solution is that it shows you these full objects auto-updated even in debugging mode when you chnges the associated IDs.
        */
        public int ApplicationID { get; private set; }
        private int _ApplicantPersonID;
        public int ApplicantPersonID
        {
            get { return _ApplicantPersonID; }
            set
            {
                this._ApplicantPersonID = value;
                this.ApplicantPersonInfo = (value != ValidationConstants.INVALID_ID) ? Person.Find(value) : null;
            }
        }
        public Person ApplicantPersonInfo { get; private set; }
        public string ApplicantFullName
        {
            get
            {
                return ApplicantPersonInfo != null ? ApplicantPersonInfo.FullName : "Unknown";
            }
        }
        public DateTime ApplicationDate { get; set; }

        private int _ApplicationTypeID;
        public int ApplicationTypeID
        {
            get { return _ApplicationTypeID; }
            set
            {
                this._ApplicationTypeID = value;
                this.ApplicationTypeInfo = (value != ValidationConstants.INVALID_ID) ? ApplicationType.Find(value) : null;
            }
        }
        public ApplicationType ApplicationTypeInfo { get; private set; }
        public enApplicationStatus ApplicationStatusID { get; set; }
        public string StatusText
        {
            get
            {
                switch (ApplicationStatusID)
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
        public DateTime LastStatusDate { get; set; }
        public float PaidFees { get; set; }
        private int _CreatedByUserID;
        public int CreatedByUserID
        {
            get { return _CreatedByUserID; }
            set
            {
                this._CreatedByUserID = value;
                this.CreatedByUserInfo = (value != ValidationConstants.INVALID_ID) ? User.FindByUserID(value) : null;
            }
        }
        public User CreatedByUserInfo { get; private set; }
        protected enMode _Mode = enMode.AddNew;


        public ApplicationInfo()
        {
            this.ApplicationID = ValidationConstants.INVALID_ID;
            this.ApplicantPersonID = ValidationConstants.INVALID_ID;
            this.ApplicationDate = DateTime.Now;
            this.ApplicationTypeID = ValidationConstants.INVALID_ID;
            this.ApplicationStatusID = enApplicationStatus.New;
            this.LastStatusDate = DateTime.Now;
            this.PaidFees = 0;
            this.CreatedByUserID = ValidationConstants.INVALID_ID;

            this._Mode = enMode.AddNew;
        }
        protected ApplicationInfo(int ApplicationID, int ApplicantPersonID, DateTime ApplicationDate, int ApplicationTypeID,
            enApplicationStatus ApplicationStatusID, DateTime LastStatusDate, float PaidFees, int CreatedByUserID)
        {
            this.ApplicationID = ApplicationID;
            this.ApplicantPersonID = ApplicantPersonID;
            this.ApplicationDate = ApplicationDate;
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationStatusID = ApplicationStatusID;
            this.LastStatusDate = LastStatusDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;

            this._Mode = enMode.Update;
        }

        private bool _AddNewApplication()
        {
            this.ApplicationID = ApplicationInfoData.AddNewApplication(
                this.ApplicantPersonID, this.ApplicationDate,
                this.ApplicationTypeID, (byte)this.ApplicationStatusID,
                this.LastStatusDate, this.PaidFees, this.CreatedByUserID
            );

            return (this.ApplicationID != ValidationConstants.INVALID_ID);
        }
        private bool _UpdateApplication()
        {
            return ApplicationInfoData.UpdateApplication(
                this.ApplicationID, this.ApplicantPersonID, this.ApplicationDate,
                this.ApplicationTypeID, (byte)this.ApplicationStatusID,
                this.LastStatusDate, this.PaidFees, this.CreatedByUserID
            );
        }


        public static bool IsApplicationExist(int ApplicationID)
        {
            return ApplicationInfoData.IsApplicationExist(ApplicationID);
        }
        public static ApplicationInfo Find(int ApplicationID)
        {
            int ApplicantPersonID = ValidationConstants.INVALID_ID;
            DateTime ApplicationDate = DateTime.Now;
            int ApplicationTypeID = ValidationConstants.INVALID_ID;
            byte ApplicationStatusID = (byte)enApplicationStatus.New;
            DateTime LastStatusDate = DateTime.Now;
            float PaidFees = 0;
            int CreatedByUserID = ValidationConstants.INVALID_ID;

            if (ApplicationInfoData.GetApplicationByID(ApplicationID, ref ApplicantPersonID, ref ApplicationDate, ref ApplicationTypeID, ref ApplicationStatusID, ref LastStatusDate, ref PaidFees, ref CreatedByUserID))
                return new ApplicationInfo(ApplicationID, ApplicantPersonID, ApplicationDate, ApplicationTypeID, (enApplicationStatus)ApplicationStatusID, LastStatusDate, PaidFees, CreatedByUserID);

            return null;
        }

        public bool Cancel()
        {
            return ApplicationInfoData.UpdateStatus(ApplicationID, (byte)enApplicationStatus.Cancelled);
        }
        public bool SetComplete()
        {
            return ApplicationInfoData.UpdateStatus(ApplicationID, (byte)enApplicationStatus.Completed);
        }
        public virtual bool Save()
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
        public virtual bool Delete()
        {
            return ApplicationInfoData.DeleteApplication(this.ApplicationID);
        }


        public bool DoesPersonHaveActiveApplication(int ApplicationTypeID)
        {
            return DoesPersonHaveActiveApplication(this.ApplicantPersonID, ApplicationTypeID);
        }
        public static bool DoesPersonHaveActiveApplication(int PersonID, int ApplicationTypeID)
        {
            return ApplicationInfoData.DoesPersonHaveActiveApplication(PersonID, ApplicationTypeID);
        }
        public int GetActiveApplicationID(enApplicationType ApplicationTypeID)
        {
            return GetActiveApplicationID(this.ApplicantPersonID, ApplicationTypeID);
        }
        public static int GetActiveApplicationID(int PersonID, enApplicationType ApplicationTypeID)
        {
            return ApplicationInfoData.GetActiveApplicationID(PersonID, (int)ApplicationTypeID);
        }
        public static int GetActiveApplicationIDForLicenseClass(int PersonID, enApplicationType ApplicationTypeID, int LicenseClassID)
        {
            return ApplicationInfoData.GetActiveApplicationIDForLicenseClass(PersonID, (int)ApplicationTypeID, LicenseClassID);
        }
    }
}
