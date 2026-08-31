using System;
using System.Data;
using DVLD_DataAccess;
using DVLD_Common;

namespace DVLD_Business
{
    public class TestAppointment
    {
        public enum enMode : byte { AddNew = 0, Update = 1 }


        public int TestAppointmentID { get; private set; }
        public int TestTypeID { get; set; }
        public int TestID { get { return _GetTestID(); } }
        public int LocalDrivingLicenseApplicationID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public float PaidFees { get; set; }
        public bool IsLocked { get; set; }

        private int _RetakeTestApplicationID;
        public int RetakeTestApplicationID
        {
            get { return _RetakeTestApplicationID; }
            set
            {
                this._RetakeTestApplicationID = value;
                this.RetakeTestAppInfo = (value != ValidationConstants.INVALID_ID) ? ApplicationInfo.Find(value) : null;
            }
        }
        public ApplicationInfo RetakeTestAppInfo { get; private set; }

        public int CreatedByUserID { get; set; }
        private enMode _Mode = enMode.AddNew;


        private TestAppointment(int TestAppointmentID, int TestTypeID, int LocalDrivingLicenseApplicationID, DateTime AppointmentDate, float PaidFees, int CreatedByUserID, bool IsLocked, int RetakeTestApplicationID)
        {
            this.TestAppointmentID = TestAppointmentID;
            this.TestTypeID = TestTypeID;
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.AppointmentDate = AppointmentDate;
            this.PaidFees = PaidFees;
            this.RetakeTestApplicationID = RetakeTestApplicationID;
            this.IsLocked = IsLocked;
            this.CreatedByUserID = CreatedByUserID;

            this._Mode = enMode.Update;
        }
        public TestAppointment() : this(ValidationConstants.INVALID_ID, (int)TestType.enTestType.VisionTest, ValidationConstants.INVALID_ID, DateTime.Now, 0, ValidationConstants.INVALID_ID, false, ValidationConstants.INVALID_ID)
        {
            this._Mode = enMode.AddNew;
        }


        private bool _AddNewTestAppointment()
        {
            this.TestAppointmentID = TestAppointmentData.AddNewTestAppointment(
                this.TestTypeID,
                this.LocalDrivingLicenseApplicationID,
                this.AppointmentDate,
                this.PaidFees,
                this.CreatedByUserID,
                this.RetakeTestApplicationID
            );

            return (this.TestAppointmentID != ValidationConstants.INVALID_ID);
        }
        private bool _UpdateTestAppointment()
        {
            return TestAppointmentData.UpdateTestAppointment(
                this.TestAppointmentID,
                this.TestTypeID,
                this.LocalDrivingLicenseApplicationID,
                this.AppointmentDate,
                this.PaidFees,
                this.CreatedByUserID,
                this.IsLocked,
                this.RetakeTestApplicationID
            );
        }
        private int _GetTestID()
        {
            return TestAppointmentData.GetTestID(this.TestAppointmentID);
        }

        public static DataTable GetAllTestAppointments()
        {
            return TestAppointmentData.GetAllTestAppointments();
        }
        public static TestAppointment Find(int TestAppointmentID)
        {
            int TestTypeID = (int)TestType.enTestType.VisionTest;
            int LocalDrivingLicenseApplicationID = ValidationConstants.INVALID_ID;
            DateTime AppointmentDate = DateTime.Now;
            float PaidFees = 0;
            int CreatedByUserID = ValidationConstants.INVALID_ID;
            bool IsLocked = false;
            int RetakeTestApplicationID = ValidationConstants.INVALID_ID;

            if (TestAppointmentData.GetTestAppointmentInfoByID(TestAppointmentID, ref TestTypeID, ref LocalDrivingLicenseApplicationID, ref AppointmentDate, ref PaidFees, ref CreatedByUserID, ref IsLocked, ref RetakeTestApplicationID))
                return new TestAppointment(TestAppointmentID, TestTypeID, LocalDrivingLicenseApplicationID, AppointmentDate, PaidFees, CreatedByUserID, IsLocked, RetakeTestApplicationID);

            return null;
        }
        public static TestAppointment GetLastTestAppointment(int LocalDrivingLicenseApplicationID, TestType.enTestType TestTypeID)
        {
            int TestAppointmentID = ValidationConstants.INVALID_ID;
            DateTime AppointmentDate = DateTime.Now;
            float PaidFees = 0;
            int CreatedByUserID = ValidationConstants.INVALID_ID;
            bool IsLocked = false;
            int RetakeTestApplicationID = ValidationConstants.INVALID_ID;

            if (TestAppointmentData.GetLastTestAppointment(
                LocalDrivingLicenseApplicationID, (int)TestTypeID,
                ref TestAppointmentID, ref AppointmentDate, ref PaidFees,
                ref CreatedByUserID, ref IsLocked, ref RetakeTestApplicationID))
            {
                return new TestAppointment(TestAppointmentID, (int)TestTypeID, LocalDrivingLicenseApplicationID,
                    AppointmentDate, PaidFees, CreatedByUserID, IsLocked, RetakeTestApplicationID);
            }

            return null;
        }
        public DataTable GetApplicationTestAppointmentsPerTestType(TestType.enTestType TestTypeID)
        {
            return TestAppointmentData.GetApplicationTestAppointmentsPerTestType(
                this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }
        public static DataTable GetApplicationTestAppointmentsPerTestType(int LocalDrivingLicenseApplicationID, TestType.enTestType TestTypeID)
        {
            return TestAppointmentData.GetApplicationTestAppointmentsPerTestType(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }
        public bool Save()
        {
            switch (this._Mode)
            {
                case enMode.AddNew:
                    if (_AddNewTestAppointment())
                    {
                        this._Mode = enMode.Update;
                        return true;
                    }
                    return false;

                case enMode.Update:
                    return _UpdateTestAppointment();

                default:
                    return false;
            }
        }
    }
}