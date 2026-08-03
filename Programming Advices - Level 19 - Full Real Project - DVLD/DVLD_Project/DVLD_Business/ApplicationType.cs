using System;
using System.Data;
using DVLD_DataAccess;
using DVLD_Common;

namespace DVLD_Business
{
    public class ApplicationType
    {
        public enum enMode : byte { AddNew = 0, Update = 1 }

        private enMode _Mode = enMode.AddNew;

        public int ApplicationTypeID { get; private set; }
        public string ApplicationTypeTitle { get; private set; }
        public float ApplicationFees { get; private set; }


        private ApplicationType(int ApplicationTypeID, string ApplicationTypeTitle, float ApplicationFees)
        {
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationTypeTitle = ApplicationTypeTitle;
            this.ApplicationFees = ApplicationFees;
            this._Mode = enMode.Update;
        }
        public ApplicationType()
            : this(ValidationConstants.INVALID_ID, string.Empty, 0)
        {
            this._Mode = enMode.AddNew;
        }


        public static DataTable GetAllApplicationTypes()
        {
            return ApplicationTypeDataAccess.GetAllApplicationTypes();
        }

        public static ApplicationType Find(int ApplicationTypeID)
        {
            string ApplicationTypeTitle = string.Empty;
            float ApplicationFees = 0;

            if (ApplicationTypeDataAccess.GetApplicationTypeInfoByID(ApplicationTypeID, ref ApplicationTypeTitle, ref ApplicationFees))
                return new ApplicationType(ApplicationTypeID, ApplicationTypeTitle, ApplicationFees);

            return null;
        }

        private bool _AddNewApplicationType()
        {
            this.ApplicationTypeID = ApplicationTypeDataAccess.AddNewApplicationType(
                this.ApplicationTypeTitle,
                this.ApplicationFees
            );

            return (this.ApplicationTypeID != ValidationConstants.INVALID_ID);
        }

        private bool _UpdateApplicationType()
        {
            return ApplicationTypeDataAccess.UpdateApplicationType(
                this.ApplicationTypeID,
                this.ApplicationTypeTitle,
                this.ApplicationFees
            );
        }

        private bool _DeleteApplicationType()
        {
            return ApplicationTypeDataAccess.DeleteApplicationType(this.ApplicationTypeID);
        }

        public bool Save()
        {
            switch (this._Mode)
            {
                case enMode.AddNew:
                    if (_AddNewApplicationType())
                    {
                        this._Mode = enMode.Update;
                        return true;
                    }
                    return false;

                case enMode.Update:
                    return _UpdateApplicationType();
            }

            return false;
        }

        public static bool Delete(int ApplicationTypeID)
        {
            return ApplicationTypeDataAccess.DeleteApplicationType(ApplicationTypeID);
        }
    }
}
