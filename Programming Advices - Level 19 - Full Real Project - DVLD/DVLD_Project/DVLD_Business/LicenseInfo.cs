using System;
using System.Data;
using DVLD_Common;
using DVLD_DataAccess;

namespace DVLD_Business
{
    /*
            * ===================================
            * ======= REFACTORING NOTE: =========
            * ===================================
        This class has been renamed from "License" to "LicenseInfo" to avoid confusion with the System.Windows.Forms.Application class.
    */
    public class LicenseInfo
    {
        public enum enMode : byte { AddNew = 0, Update = 1 }
        public enum enIssueReason : byte { FirstTime = 1, Renew = 2, DamagedReplacement = 3, LostReplacement = 4 }

        private enMode _Mode = enMode.AddNew;

        public int LicenseID { get; private set; }
        public int ApplicationID { get; set; }
        public int DriverID { get; set; }
        public int LicenseClassID { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Notes { get; set; }
        public float PaidFees { get; set; }
        public bool IsActive { get; set; }
        public enIssueReason IssueReason { get; set; }
        public int CreatedByUserID { get; set; }

        public Driver DriverInfo { get; private set; }
        public LicenseClass LicenseClassInfo { get; private set; }
        public DetainedLicense DetainedInfo { get; private set; }

        public string IssueReasonText
        {
            get
            {
                return _GetIssueReasonText(this.IssueReason);
            }
        }
        public bool IsDetained
        {
            get
            {
                return DetainedLicense.IsLicenseDetained(this.LicenseID);
            }
        }

        public LicenseInfo()
        {
            this.LicenseID = ValidationConstants.INVALID_ID;
            this.ApplicationID = ValidationConstants.INVALID_ID;
            this.DriverID = ValidationConstants.INVALID_ID;
            this.LicenseClassID = ValidationConstants.INVALID_ID;
            this.IssueDate = DateTime.Now;
            this.ExpirationDate = DateTime.Now;
            this.Notes = string.Empty;
            this.PaidFees = 0;
            this.IsActive = true;
            this.IssueReason = enIssueReason.FirstTime;
            this.CreatedByUserID = ValidationConstants.INVALID_ID;

            this._Mode = enMode.AddNew;
        }
        private LicenseInfo(int LicenseID, int ApplicationID, int DriverID, int LicenseClassID,
            DateTime IssueDate, DateTime ExpirationDate, string Notes,
            float PaidFees, bool IsActive, enIssueReason IssueReason, int CreatedByUserID)
        {
            this.LicenseID = LicenseID;
            this.ApplicationID = ApplicationID;
            this.DriverID = DriverID;
            this.LicenseClassID = LicenseClassID;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.Notes = Notes;
            this.PaidFees = PaidFees;
            this.IsActive = IsActive;
            this.IssueReason = IssueReason;
            this.CreatedByUserID = CreatedByUserID;

            this.DriverInfo = Driver.FindByDriverID(this.DriverID);
            this.LicenseClassInfo = LicenseClass.Find(this.LicenseClassID);
            this.DetainedInfo = DetainedLicense.FindByLicenseID(this.LicenseID);

            this._Mode = enMode.Update;
        }

        private bool _AddNewLicense()
        {
            this.LicenseID = LicenseInfoData.AddNewLicense(
                this.ApplicationID, this.DriverID, this.LicenseClassID,
                this.IssueDate, this.ExpirationDate, this.Notes, this.PaidFees,
                this.IsActive, (byte)this.IssueReason, this.CreatedByUserID
            );

            return (this.LicenseID != ValidationConstants.INVALID_ID);
        }
        private bool _UpdateLicense()
        {
            return LicenseInfoData.UpdateLicense(
                this.LicenseID, this.ApplicationID, this.DriverID, this.LicenseClassID,
                this.IssueDate, this.ExpirationDate, this.Notes, this.PaidFees,
                this.IsActive, (byte)this.IssueReason, this.CreatedByUserID
            );
        }

        public static DataTable GetAllLicenses()
        {
            return LicenseInfoData.GetAllLicenses();
        }
        public static DataTable GetDriverLicenses(int DriverID)
        {
            return LicenseInfoData.GetDriverLicenses(DriverID);
        }
        public static int GetActiveLicenseIDByPersonID(int PersonID, int LicenseClassID)
        {
            return LicenseInfoData.GetActiveLicenseIDByPersonID(PersonID, LicenseClassID);
        }
        public static LicenseInfo Find(int LicenseID)
        {
            int ApplicationID = ValidationConstants.INVALID_ID;
            int DriverID = ValidationConstants.INVALID_ID;
            int LicenseClassID = ValidationConstants.INVALID_ID;
            DateTime IssueDate = DateTime.Now;
            DateTime ExpirationDate = DateTime.Now;
            string Notes = string.Empty;
            float PaidFees = 0;
            bool IsActive = false;
            byte IssueReason = 0;
            int CreatedByUserID = ValidationConstants.INVALID_ID;

            if (LicenseInfoData.GetLicenseInfoByID(LicenseID, ref ApplicationID, ref DriverID, ref LicenseClassID,
                ref IssueDate, ref ExpirationDate, ref Notes, ref PaidFees, ref IsActive, ref IssueReason, ref CreatedByUserID))
            {
                return new LicenseInfo(LicenseID, ApplicationID, DriverID, LicenseClassID,
                    IssueDate, ExpirationDate, Notes, PaidFees, IsActive, (enIssueReason)IssueReason, CreatedByUserID);
            }

            return null;
        }
        public bool Save()
        {
            switch (this._Mode)
            {
                case enMode.AddNew:
                    if (_AddNewLicense())
                    {
                        this._Mode = enMode.Update;
                        return true;
                    }
                    return false;

                case enMode.Update:
                    return _UpdateLicense();
            }

            return false;
        }
        public bool Delete()
        {
            return LicenseInfoData.DeleteLicense(this.LicenseID);
        }
        
        public bool Deactivate()
        {
            return LicenseInfoData.DeactivateLicense(this.LicenseID);
        }
        private static string _GetIssueReasonText(enIssueReason IssueReason)
        {
            switch (IssueReason)
            {
                case enIssueReason.FirstTime:
                    return "First Time";
                case enIssueReason.Renew:
                    return "Renew";
                case enIssueReason.DamagedReplacement:
                    return "Damaged Replacement";
                case enIssueReason.LostReplacement:
                    return "Lost Replacement";
                default:
                    return "Unknown";
            }
        }
    }
}
