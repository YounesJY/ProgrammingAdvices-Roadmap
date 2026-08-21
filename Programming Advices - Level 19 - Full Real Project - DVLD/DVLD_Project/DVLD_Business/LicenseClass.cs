using DVLD_Common;
using DVLD_DataAccess;
using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;

namespace DVLD_Business
{
    public class LicenseClass
    {
        public enum enLicenseClass
        {
            SmallMotorcycle = 0,
            HeavyMotorcycleLicense = 1,
            OrdinaryDrivingLicense = 2,
            Commercial = 3,
            Agricultural = 4,
            SmallAndMediumBus = 5,
            TruckAndHeavyVehicle = 6
        }
        public enum enMode : byte { AddNew = 0, Update = 1 }
        private enMode _Mode = enMode.AddNew;

        /*
            WHY we didn't use an enum for LicenseClassID just like we did for ApplicationType
        */
        public int LicenseClassID { get; private set; }
        public string ClassName { get; private set; }
        public string ClassDescription { get; private set; }
        public byte MinimumAllowedAge { get; private set; }
        public byte DefaultValidityLength { get; private set; }
        public float ClassFees { get; private set; }


        public LicenseClass()
        {
            this.LicenseClassID = ValidationConstants.INVALID_ID;
            this.ClassName = string.Empty;
            this.ClassDescription = string.Empty;
            this.MinimumAllowedAge = 18;
            this.DefaultValidityLength = 10;
            this.ClassFees = 0;

            this._Mode = enMode.AddNew;
        }
        private LicenseClass(int LicenseClassID, string ClassName, string ClassDescription, byte MinimumAllowedAge, byte DefaultValidityLength, float ClassFees)
        {
            this.LicenseClassID = LicenseClassID;
            this.ClassName = ClassName;
            this.ClassDescription = ClassDescription;
            this.MinimumAllowedAge = MinimumAllowedAge;
            this.DefaultValidityLength = DefaultValidityLength;
            this.ClassFees = ClassFees;

            this._Mode = enMode.Update;
        }

        private bool _AddNewLicenseClass()
        {
            this.LicenseClassID = LicenseClassData.AddNewLicenseClass(
                this.ClassName, this.ClassDescription,
                this.MinimumAllowedAge, this.DefaultValidityLength, this.ClassFees
            );

            return (this.LicenseClassID != ValidationConstants.INVALID_ID);
        }
        private bool _UpdateLicenseClass()
        {
            return LicenseClassData.UpdateLicenseClass(
                this.LicenseClassID, this.ClassName, this.ClassDescription,
                this.MinimumAllowedAge, this.DefaultValidityLength, this.ClassFees
            );
        }

        public static DataTable GetAllLicenseClasses()
        {
            return LicenseClassData.GetAllLicenseClasses();
        }
        public static LicenseClass Find(int LicenseClassID)
        {
            string ClassName = string.Empty;
            string ClassDescription = string.Empty;
            byte MinimumAllowedAge = 18;
            byte DefaultValidityLength = 10;
            float ClassFees = 0;

            if (LicenseClassData.GetLicenseClassInfoByID(LicenseClassID, ref ClassName, ref ClassDescription, ref MinimumAllowedAge, ref DefaultValidityLength, ref ClassFees))
                return new LicenseClass(LicenseClassID, ClassName, ClassDescription, MinimumAllowedAge, DefaultValidityLength, ClassFees);

            return null;
        }
        public static LicenseClass Find(string ClassName)
        {
            int LicenseClassID = ValidationConstants.INVALID_ID;
            string ClassDescription = string.Empty;
            byte MinimumAllowedAge = 18;
            byte DefaultValidityLength = 10;
            float ClassFees = 0;

            if (LicenseClassData.GetLicenseClassInfoByClassName(ClassName, ref LicenseClassID, ref ClassDescription, ref MinimumAllowedAge, ref DefaultValidityLength, ref ClassFees))
                return new LicenseClass(LicenseClassID, ClassName, ClassDescription, MinimumAllowedAge, DefaultValidityLength, ClassFees);

            return null;
        }
        public bool Save()
        {
            switch (this._Mode)
            {
                case enMode.AddNew:
                    if (_AddNewLicenseClass())
                    {
                        this._Mode = enMode.Update;
                        return true;
                    }
                    return false;

                case enMode.Update:
                    return _UpdateLicenseClass();
            }

            return false;
        }
    }
}
