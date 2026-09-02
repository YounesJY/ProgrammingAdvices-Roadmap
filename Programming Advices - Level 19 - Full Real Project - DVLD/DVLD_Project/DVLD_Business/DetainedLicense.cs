using System;
using System.Data;
using DVLD_DataAccess;
using DVLD_Common;

namespace DVLD_Business
{
    public class DetainedLicense
    {
        public enum enMode : byte { AddNew = 0, Update = 1 }

        public int DetainID { get; private set; }

        private int _licenseID = ValidationConstants.INVALID_ID;
        public int LicenseID
        {
            get { return this._licenseID; }
            set
            {
                this._licenseID = value;
                /*
                This will cause a stackoverflow error due to a circular dependancy :) 
                this.LicenseInfo = (value != ValidationConstants.INVALID_ID) ? LicenseInfo.Find(value) : null;
                */
            }
        }
        public LicenseInfo LicenseInfo
        {
            get
            {
                return (this.LicenseID != ValidationConstants.INVALID_ID) ? LicenseInfo.Find(this.LicenseID) : null;
            }
        }

        public DateTime DetainDate { get; set; }
        public float FineFees { get; set; }

        private int _createdByUserID = ValidationConstants.INVALID_ID;
        public int CreatedByUserID
        {
            get { return this._createdByUserID; }
            set
            {
                this._createdByUserID = value;
                this.CreatedByUserInfo = (value != ValidationConstants.INVALID_ID) ? User.FindByUserID(value) : null;
            }
        }
        public User CreatedByUserInfo { get; private set; }

        public bool IsReleased { get; set; }
        public DateTime ReleaseDate { get; set; }

        private int _releasedByUserID = ValidationConstants.INVALID_ID;
        public int ReleasedByUserID
        {
            get { return this._releasedByUserID; }
            set
            {
                this._releasedByUserID = value;
                this.ReleasedByUserInfo = (value != ValidationConstants.INVALID_ID) ? User.FindByUserID(value) : null;
            }
        }
        public User ReleasedByUserInfo { get; private set; }

        private int _releaseApplicationID = ValidationConstants.INVALID_ID;
        public int ReleaseApplicationID
        {
            get { return this._releaseApplicationID; }
            set
            {
                this._releaseApplicationID = value;
                this.ReleaseApplicationInfo = (value != ValidationConstants.INVALID_ID) ? ApplicationInfo.Find(value) : null;
            }
        }
        public ApplicationInfo ReleaseApplicationInfo { get; private set; }

        private enMode _Mode = enMode.AddNew;


        private DetainedLicense(int DetainID, int LicenseID, DateTime DetainDate, float FineFees, int CreatedByUserID, bool IsReleased, DateTime ReleaseDate, int ReleasedByUserID, int ReleaseApplicationID)
        {
            this.DetainID = DetainID;
            this.LicenseID = LicenseID;
            this.DetainDate = DetainDate;
            this.FineFees = FineFees;
            this.CreatedByUserID = CreatedByUserID;
            this.IsReleased = IsReleased;
            this.ReleaseDate = ReleaseDate;
            this.ReleasedByUserID = ReleasedByUserID;
            this.ReleaseApplicationID = ReleaseApplicationID;

            this._Mode = enMode.Update;
        }
        public DetainedLicense()
        {
            this.DetainID = ValidationConstants.INVALID_ID;
            this.LicenseID = ValidationConstants.INVALID_ID;
            this.DetainDate = DateTime.Now;
            this.FineFees = 0;
            this.CreatedByUserID = ValidationConstants.INVALID_ID;
            this.IsReleased = false;
            this.ReleaseDate = DateTime.MaxValue;
            this.ReleasedByUserID = ValidationConstants.INVALID_ID;
            this.ReleaseApplicationID = ValidationConstants.INVALID_ID;

            this._Mode = enMode.AddNew;
        }

        private bool _AddNewDetainedLicense()
        {
            this.DetainID = DetainedLicenseData.AddNewDetainedLicense(
                this.LicenseID,
                this.DetainDate,
                this.FineFees,
                this.CreatedByUserID
            );

            return (this.DetainID != ValidationConstants.INVALID_ID);
        }
        private bool _UpdateDetainedLicense()
        {
            return DetainedLicenseData.UpdateDetainedLicense(
                this.DetainID,
                this.LicenseID,
                this.DetainDate,
                this.FineFees,
                this.CreatedByUserID
            );
        }

        public static DataTable GetAllDetainedLicenses()
        {
            return DetainedLicenseData.GetAllDetainedLicenses();
        }
        public static DetainedLicense Find(int DetainID)
        {
            int LicenseID = ValidationConstants.INVALID_ID;
            DateTime DetainDate = DateTime.Now;
            float FineFees = 0;
            int CreatedByUserID = ValidationConstants.INVALID_ID;
            bool IsReleased = false;
            DateTime ReleaseDate = DateTime.MaxValue;
            int ReleasedByUserID = ValidationConstants.INVALID_ID;
            int ReleaseApplicationID = ValidationConstants.INVALID_ID;

            if (DetainedLicenseData.GetDetainedLicenseInfoByID(
                DetainID, ref LicenseID, ref DetainDate,
                ref FineFees, ref CreatedByUserID,
                ref IsReleased, ref ReleaseDate,
                ref ReleasedByUserID, ref ReleaseApplicationID))
            {
                return new DetainedLicense(DetainID, LicenseID, DetainDate,
                    FineFees, CreatedByUserID, IsReleased, ReleaseDate,
                    ReleasedByUserID, ReleaseApplicationID);
            }

            return null;
        }
        public static DetainedLicense FindByLicenseID(int LicenseID)
        {
            int DetainID = ValidationConstants.INVALID_ID;
            DateTime DetainDate = DateTime.Now;
            float FineFees = 0;
            int CreatedByUserID = ValidationConstants.INVALID_ID;
            bool IsReleased = false;
            DateTime ReleaseDate = DateTime.MaxValue;
            int ReleasedByUserID = ValidationConstants.INVALID_ID;
            int ReleaseApplicationID = ValidationConstants.INVALID_ID;

            if (DetainedLicenseData.GetDetainedLicenseInfoByLicenseID(
                LicenseID, ref DetainID, ref DetainDate,
                ref FineFees, ref CreatedByUserID,
                ref IsReleased, ref ReleaseDate,
                ref ReleasedByUserID, ref ReleaseApplicationID))
            {
                return new DetainedLicense(DetainID, LicenseID, DetainDate,
                    FineFees, CreatedByUserID, IsReleased, ReleaseDate,
                    ReleasedByUserID, ReleaseApplicationID);
            }

            return null;
        }

        public static bool IsLicenseDetained(int LicenseID)
        {
            return DetainedLicenseData.IsLicenseDetained(LicenseID);
        }
        public bool ReleaseDetainedLicense(int ReleasedByUserID, int ReleaseApplicationID)
        {
            return DetainedLicenseData.ReleaseDetainedLicense(
                this.DetainID,
                ReleasedByUserID,
                ReleaseApplicationID
            );
        }

        public bool Save()
        {
            switch (this._Mode)
            {
                case enMode.AddNew:
                    if (_AddNewDetainedLicense())
                    {
                        this._Mode = enMode.Update;
                        return true;
                    }
                    return false;

                case enMode.Update:
                    return _UpdateDetainedLicense();

                default:
                    return false;
            }
        }
    }
}