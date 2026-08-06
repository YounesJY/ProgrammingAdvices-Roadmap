using System;
using System.Data;
using DVLD_Common;
using DVLD_DataAccess;

namespace DVLD_Business
{
    public class Driver
    {
        public enum enMode : byte { AddNew = 0, Update = 1 }

        private enMode _Mode = enMode.AddNew;

        public int DriverID { get; private set; }
        public int PersonID { get; private set; }
        public int CreatedByUserID { get; private set; }
        public DateTime CreatedDate { get; private set; }

        public Person PersonInfo { get; private set; }

        public Driver()
        {
            this.DriverID = ValidationConstants.INVALID_ID;
            this.PersonID = ValidationConstants.INVALID_ID;
            this.CreatedByUserID = ValidationConstants.INVALID_ID;
            this.CreatedDate = DateTime.Now;

            this._Mode = enMode.AddNew;
        }
        private Driver(int DriverID, int PersonID, int CreatedByUserID, DateTime CreatedDate)
        {
            this.DriverID = DriverID;
            this.PersonID = PersonID;
            this.CreatedByUserID = CreatedByUserID;
            this.CreatedDate = CreatedDate;
            this.PersonInfo = Person.Find(PersonID);

            this._Mode = enMode.Update;
        }

        private bool _AddNewDriver()
        {
            this.DriverID = DriverData.AddNewDriver(this.PersonID, this.CreatedByUserID);

            return (this.DriverID != ValidationConstants.INVALID_ID);
        }
        private bool _UpdateDriver()
        {
            return DriverData.UpdateDriver(this.DriverID, this.PersonID, this.CreatedByUserID);
        }

        public static DataTable GetAllDrivers()
        {
            return DriverData.GetAllDrivers();
        }
        public static Driver FindByDriverID(int DriverID)
        {
            int PersonID = ValidationConstants.INVALID_ID;
            int CreatedByUserID = ValidationConstants.INVALID_ID;
            DateTime CreatedDate = DateTime.Now;

            if (DriverData.GetDriverInfoByDriverID(DriverID, ref PersonID, ref CreatedByUserID, ref CreatedDate))
                return new Driver(DriverID, PersonID, CreatedByUserID, CreatedDate);

            return null;
        }
        public static Driver FindByPersonID(int PersonID)
        {
            int DriverID = ValidationConstants.INVALID_ID;
            int CreatedByUserID = ValidationConstants.INVALID_ID;
            DateTime CreatedDate = DateTime.Now;

            if (DriverData.GetDriverInfoByPersonID(PersonID, ref DriverID, ref CreatedByUserID, ref CreatedDate))
                return new Driver(DriverID, PersonID, CreatedByUserID, CreatedDate);

            return null;
        }
        public bool Save()
        {
            switch (this._Mode)
            {
                case enMode.AddNew:
                    if (_AddNewDriver())
                    {
                        this._Mode = enMode.Update;
                        return true;
                    }
                    return false;

                case enMode.Update:
                    return _UpdateDriver();
            }

            return false;
        }
        public bool Delete()
        {
            return DriverData.DeleteDriver(this.DriverID);
        }

        public static DataTable GetLicenses(int DriverID)
        {
            return License.GetDriverLicenses(DriverID);
        }
        /*
            public static DataTable GetInternationalLicenses(int DriverID)
            {
                return InternationalLicense.GetDriverInternationalLicenses(DriverID);
            }
        */
    }
}
