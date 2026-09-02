using System;
using System.Data;
using DVLD_DataAccess;
using DVLD_Common;

namespace DVLD_Business
{
    public class InternationalDrivingLicenseApplication : ApplicationInfo
    {
        public int InternationalLicenseID { get; private set; }

        private int _driverID = ValidationConstants.INVALID_ID;
        public int DriverID
        {
            get { return this._driverID; }
            set
            {
                this._driverID = value;
                this.DriverInfo = (value != ValidationConstants.INVALID_ID) ? Driver.FindByDriverID(value) : null;
            }
        }
        public Driver DriverInfo { get; private set; }

        private int _issuedUsingLocalLicenseID = ValidationConstants.INVALID_ID;
        public int IssuedUsingLocalLicenseID
        {
            get { return this._issuedUsingLocalLicenseID; }
            set
            {
                this._issuedUsingLocalLicenseID = value;
                this.LocalLicenseInfo = (value != ValidationConstants.INVALID_ID) ? LicenseInfo.Find(value) : null;
            }
        }
        public LicenseInfo LocalLicenseInfo { get; private set; }

        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsActive { get; set; }

        private new enMode _Mode = enMode.AddNew;


        private InternationalDrivingLicenseApplication(
            int ApplicationID, int ApplicantPersonID, DateTime ApplicationDate, enApplicationStatus ApplicationStatus, DateTime LastStatusDate,
            float PaidFees, int CreatedByUserID, int InternationalLicenseID, int DriverID, int IssuedUsingLocalLicenseID,
            DateTime IssueDate, DateTime ExpirationDate, bool IsActive)
            : base(ApplicationID, ApplicantPersonID, ApplicationDate, (int)enApplicationType.NewInternationalLicense, ApplicationStatus, LastStatusDate, PaidFees, CreatedByUserID)
        {
            this.InternationalLicenseID = InternationalLicenseID;
            this.DriverID = DriverID;
            this.IssuedUsingLocalLicenseID = IssuedUsingLocalLicenseID;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.IsActive = IsActive;

            this._Mode = enMode.Update;
        }
        public InternationalDrivingLicenseApplication()
        {
            this.ApplicationTypeID = (int)enApplicationType.NewInternationalLicense;

            this.InternationalLicenseID = ValidationConstants.INVALID_ID;
            this.DriverID = ValidationConstants.INVALID_ID;
            this.IssuedUsingLocalLicenseID = ValidationConstants.INVALID_ID;
            this.IssueDate = DateTime.Now;
            this.ExpirationDate = DateTime.Now;
            this.IsActive = true;

            this._Mode = enMode.AddNew;
        }

        private bool _AddNewInternationalLicense()
        {
            this.InternationalLicenseID = InternationalLicenseData.AddNewInternationalLicense(
                this.ApplicationID,
                this.DriverID,
                this.IssuedUsingLocalLicenseID,
                this.IssueDate,
                this.ExpirationDate,
                this.IsActive,
                this.CreatedByUserID
            );

            return (this.InternationalLicenseID != ValidationConstants.INVALID_ID);
        }
        private bool _UpdateInternationalLicense()
        {
            return InternationalLicenseData.UpdateInternationalLicense(
                this.InternationalLicenseID,
                this.ApplicationID,
                this.DriverID,
                this.IssuedUsingLocalLicenseID,
                this.IssueDate,
                this.ExpirationDate,
                this.IsActive,
                this.CreatedByUserID
            );
        }

        public static DataTable GetAllInternationalLicenses()
        {
            return InternationalLicenseData.GetAllInternationalLicenses();
        }
        public static DataTable GetDriverInternationalLicenses(int DriverID)
        {
            return InternationalLicenseData.GetDriverInternationalLicenses(DriverID);
        }
        public static int GetActiveInternationalLicenseIDByDriverID(int DriverID)
        {
            return InternationalLicenseData.GetActiveInternationalLicenseIDByDriverID(DriverID);
        }
        public new static InternationalDrivingLicenseApplication Find(int InternationalLicenseID)
        {
            int ApplicationID = ValidationConstants.INVALID_ID;
            int DriverID = ValidationConstants.INVALID_ID;
            int IssuedUsingLocalLicenseID = ValidationConstants.INVALID_ID;
            DateTime IssueDate = DateTime.Now;
            DateTime ExpirationDate = DateTime.Now;
            bool IsActive = false;
            int CreatedByUserID = ValidationConstants.INVALID_ID;

            if (InternationalLicenseData.GetInternationalLicenseInfoByID(InternationalLicenseID, ref ApplicationID, ref DriverID, ref IssuedUsingLocalLicenseID, ref IssueDate, ref ExpirationDate, ref IsActive, ref CreatedByUserID))
            {
                ApplicationInfo application = ApplicationInfo.Find(ApplicationID);
                if (application != null)
                {
                    return new InternationalDrivingLicenseApplication(
                        application.ApplicationID,
                        application.ApplicantPersonID,
                        application.ApplicationDate,
                        application.ApplicationStatusID,
                        application.LastStatusDate,
                        application.PaidFees,
                        application.CreatedByUserID,
                        InternationalLicenseID,
                        DriverID,
                        IssuedUsingLocalLicenseID,
                        IssueDate,
                        ExpirationDate,
                        IsActive
                    );
                }
            }

            return null;
        }

        public override bool Save()
        {
            // Sync base class mode with current mode
            base._Mode = (ApplicationInfo.enMode)this._Mode;

            // Save base application first
            if (!base.Save())
                return false;

            // Save international license specific data
            switch (this._Mode)
            {
                case enMode.AddNew:
                    if (_AddNewInternationalLicense())
                    {
                        this._Mode = enMode.Update;
                        return true;
                    }
                    return false;

                case enMode.Update:
                    return _UpdateInternationalLicense();

                default:
                    return false;
            }
        }
    }
}