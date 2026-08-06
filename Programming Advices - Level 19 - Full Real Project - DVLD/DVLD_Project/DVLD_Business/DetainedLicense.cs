using System;
using System.Data;
using DVLD_Common;
using DVLD_DataAccess;

namespace DVLD_Business
{
    public class DetainedLicense
    {
        public enum enMode : byte { AddNew = 0, Update = 1 }

        private enMode _Mode = enMode.AddNew;

        public int DetainID { get; private set; }
        public int LicenseID { get; private set; }
        public DateTime DetainDate { get; private set; }
        public float FineFees { get; private set; }
        public int CreatedByUserID { get; private set; }
        public bool IsReleased { get; private set; }
        public DateTime ReleaseDate { get; private set; }
        public int ReleasedByUserID { get; private set; }
        public int ReleaseApplicationID { get; private set; }

        public User CreatedByUserInfo { get; private set; }
        public User ReleasedByUserInfo { get; private set; }

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

        private DetainedLicense(int DetainID, int LicenseID, DateTime DetainDate,
            float FineFees, int CreatedByUserID, bool IsReleased, DateTime ReleaseDate,
            int ReleasedByUserID, int ReleaseApplicationID)
        {
            this.DetainID = DetainID;
            this.LicenseID = LicenseID;
            this.DetainDate = DetainDate;
            this.FineFees = FineFees;
            this.CreatedByUserID = CreatedByUserID;
            this.CreatedByUserInfo = User.FindByUserID(this.CreatedByUserID);
            this.IsReleased = IsReleased;
            this.ReleaseDate = ReleaseDate;
            this.ReleasedByUserID = ReleasedByUserID;
            this.ReleaseApplicationID = ReleaseApplicationID;
            this.ReleasedByUserInfo = User.FindByUserID(this.ReleasedByUserID);

            this._Mode = enMode.Update;
        }

        private bool _AddNewDetainedLicense()
        {
            this.DetainID = DetainedLicenseData.AddNewDetainedLicense(
                this.LicenseID, this.DetainDate, this.FineFees, this.CreatedByUserID
            );

            return (this.DetainID != ValidationConstants.INVALID_ID);
        }

        private bool _UpdateDetainedLicense()
        {
            return DetainedLicenseData.UpdateDetainedLicense(
                this.DetainID, this.LicenseID, this.DetainDate, this.FineFees, this.CreatedByUserID
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

            if (DetainedLicenseData.GetDetainedLicenseInfoByID(DetainID, ref LicenseID, ref DetainDate,
                ref FineFees, ref CreatedByUserID, ref IsReleased, ref ReleaseDate,
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

            if (DetainedLicenseData.GetDetainedLicenseInfoByLicenseID(LicenseID, ref DetainID, ref DetainDate,
                ref FineFees, ref CreatedByUserID, ref IsReleased, ref ReleaseDate,
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
            }

            return false;
        }

        public bool ReleaseDetainedLicense(int ReleasedByUserID, int ReleaseApplicationID)
        {
            return DetainedLicenseData.ReleaseDetainedLicense(this.DetainID, ReleasedByUserID, ReleaseApplicationID);
        }

        public bool Delete()
        {
            return DetainedLicenseData.DeleteDetainedLicense(this.DetainID);
        }
    }
}
