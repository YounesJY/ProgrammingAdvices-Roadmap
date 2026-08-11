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

        private int _TestTypeID;
        public Test.enTestType TestTypeID
        {
            get { return (Test.enTestType)_TestTypeID; }
            set
            {
                _TestTypeID = (int)value;
                // Load TestType info when needed via static method
            }
        }

        public int LocalDrivingLicenseApplicationID { get; private set; }
        public DateTime AppointmentDate { get; private set; }
        public float PaidFees { get; private set; }
        public int CreatedByUserID { get; private set; }
        public bool IsLocked { get; private set; }

        private int _RetakeTestApplicationID;
        public int RetakeTestApplicationID
        {
            get { return _RetakeTestApplicationID; }
            set
            {
                _RetakeTestApplicationID = value;
                if (value != ValidationConstants.INVALID_ID)
                {
                    RetakeTestAppInfo = ApplicationInfo.Find(value);
                }
                else
                {
                    RetakeTestAppInfo = null;
                }
            }
        }
        public ApplicationInfo RetakeTestAppInfo { get; private set; }
        public int TestID
        {
            get { return _GetTestID(); }
        }
        private enMode _Mode = enMode.AddNew;


        public TestAppointment()
        {
            this.TestAppointmentID = ValidationConstants.INVALID_ID;
            this._TestTypeID = (int)Test.enTestType.VisionTest;
            this.LocalDrivingLicenseApplicationID = ValidationConstants.INVALID_ID;
            this.AppointmentDate = DateTime.Now;
            this.PaidFees = 0;
            this.CreatedByUserID = ValidationConstants.INVALID_ID;
            this.IsLocked = false;
            this.RetakeTestApplicationID = ValidationConstants.INVALID_ID;
            this.RetakeTestAppInfo = null;

            this._Mode = enMode.AddNew;
        }
        private TestAppointment(int TestAppointmentID, int TestTypeID, int LocalDrivingLicenseApplicationID,
            DateTime AppointmentDate, float PaidFees, int CreatedByUserID, bool IsLocked, int RetakeTestApplicationID)
        {
            this.TestAppointmentID = TestAppointmentID;
            this._TestTypeID = TestTypeID;
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.AppointmentDate = AppointmentDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            this.IsLocked = IsLocked;
            this.RetakeTestApplicationID = RetakeTestApplicationID;
            this.RetakeTestAppInfo = (RetakeTestApplicationID != ValidationConstants.INVALID_ID) ?
                ApplicationInfo.Find(RetakeTestApplicationID) : null;

            this._Mode = enMode.Update;
        }

        private bool _AddNewTestAppointment()
        {
            this.TestAppointmentID = TestAppointmentData.AddNewTestAppointment(
                this._TestTypeID,
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
                this._TestTypeID,
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
            return TestAppointmentData.GetTestID(TestAppointmentID);
        }

        public static DataTable GetAllTestAppointments()
        {
            return TestAppointmentData.GetAllTestAppointments();
        }
        public static TestAppointment Find(int TestAppointmentID)
        {
            int TestTypeID = (int)Test.enTestType.VisionTest;
            int LocalDrivingLicenseApplicationID = ValidationConstants.INVALID_ID;
            DateTime AppointmentDate = DateTime.Now;
            float PaidFees = 0;
            int CreatedByUserID = ValidationConstants.INVALID_ID;
            bool IsLocked = false;
            int RetakeTestApplicationID = ValidationConstants.INVALID_ID;

            if (TestAppointmentData.GetTestAppointmentInfoByID(
                TestAppointmentID, ref TestTypeID, ref LocalDrivingLicenseApplicationID,
                ref AppointmentDate, ref PaidFees, ref CreatedByUserID,
                ref IsLocked, ref RetakeTestApplicationID))
            {
                return new TestAppointment(TestAppointmentID, TestTypeID, LocalDrivingLicenseApplicationID,
                    AppointmentDate, PaidFees, CreatedByUserID, IsLocked, RetakeTestApplicationID);
            }

            return null;
        }
        public static TestAppointment GetLastTestAppointment(int LocalDrivingLicenseApplicationID, Test.enTestType TestTypeID)
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
        public DataTable GetApplicationTestAppointmentsPerTestType(Test.enTestType TestTypeID)
        {
            return TestAppointmentData.GetApplicationTestAppointmentsPerTestType(
                this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }
        public static DataTable GetApplicationTestAppointmentsPerTestType(int LocalDrivingLicenseApplicationID, Test.enTestType TestTypeID)
        {
            return TestAppointmentData.GetApplicationTestAppointmentsPerTestType(
                LocalDrivingLicenseApplicationID, (int)TestTypeID);
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