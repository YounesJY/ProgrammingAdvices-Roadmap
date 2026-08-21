using System;
using System.Data;
using DVLD_Common;
using DVLD_DataAccess;


namespace DVLD_Business
{
    public class Test
    {
        public enum enMode : byte { AddNew = 0, Update = 1 }
        //   public enum enTestType { VisionTest = 1, WrittenTest = 2, StreetTest = 3 }

        public int TestID { get; private set; }

        private int _TestAppointmentID;
        public int TestAppointmentID
        {
            get { return _TestAppointmentID; }
            set
            {
                this._TestAppointmentID = value;
                this.TestAppointmentInfo = (value != ValidationConstants.INVALID_ID) ? TestAppointment.Find(value) : null;
            }
        }
        public TestAppointment TestAppointmentInfo { get; private set; }

        public bool TestResult { get; set; }
        public string Notes { get; set; }
        public int CreatedByUserID { get; set; }
        private enMode _Mode = enMode.AddNew;


        public Test()
        {
            this.TestID = ValidationConstants.INVALID_ID;
            this.TestAppointmentID = ValidationConstants.INVALID_ID;
            this.TestAppointmentInfo = null;
            this.TestResult = false;
            this.Notes = string.Empty;
            this.CreatedByUserID = ValidationConstants.INVALID_ID;

            this._Mode = enMode.AddNew;
        }
        private Test(int TestID, int TestAppointmentID, bool TestResult, string Notes, int CreatedByUserID)
        {
            this.TestID = TestID;
            this.TestAppointmentID = TestAppointmentID;
            this.TestAppointmentInfo = TestAppointment.Find(TestAppointmentID);
            this.TestResult = TestResult;
            this.Notes = Notes;
            this.CreatedByUserID = CreatedByUserID;

            this._Mode = enMode.Update;
        }

        private bool _AddNewTest()
        {
            this.TestID = TestData.AddNewTest(
                this.TestAppointmentID,
                this.TestResult,
                this.Notes,
                this.CreatedByUserID
            );

            return (this.TestID != ValidationConstants.INVALID_ID);
        }
        private bool _UpdateTest()
        {
            return TestData.UpdateTest(
                this.TestID,
                this.TestAppointmentID,
                this.TestResult,
                this.Notes,
                this.CreatedByUserID
            );
        }

        public static DataTable GetAllTests()
        {
            return TestData.GetAllTests();
        }
        public static Test Find(int TestID)
        {
            int TestAppointmentID = ValidationConstants.INVALID_ID;
            bool TestResult = false;
            string Notes = string.Empty;
            int CreatedByUserID = ValidationConstants.INVALID_ID;

            if (TestData.GetTestInfoByID(TestID, ref TestAppointmentID, ref TestResult,
                ref Notes, ref CreatedByUserID))
            {
                return new Test(TestID, TestAppointmentID, TestResult, Notes, CreatedByUserID);
            }

            return null;
        }
        public static Test FindLastTestPerPersonAndLicenseClass(int PersonID, int LicenseClassID, TestType.enTestType TestTypeID)
        {
            int TestID = ValidationConstants.INVALID_ID;
            int TestAppointmentID = ValidationConstants.INVALID_ID;
            bool TestResult = false;
            string Notes = string.Empty;
            int CreatedByUserID = ValidationConstants.INVALID_ID;

            if (TestData.GetLastTestByPersonAndTestTypeAndLicenseClass(
                PersonID, LicenseClassID, (int)TestTypeID,
                ref TestID, ref TestAppointmentID, ref TestResult,
                ref Notes, ref CreatedByUserID))
            {
                return new Test(TestID, TestAppointmentID, TestResult, Notes, CreatedByUserID);
            }

            return null;
        }
        public bool Save()
        {
            switch (this._Mode)
            {
                case enMode.AddNew:
                    if (_AddNewTest())
                    {
                        this._Mode = enMode.Update;
                        return true;
                    }
                    return false;

                case enMode.Update:
                    return _UpdateTest();

                default:
                    return false;
            }
        }

        public static byte GetPassedTestCount(int LocalDrivingLicenseApplicationID)
        {
            return TestData.GetPassedTestCount(LocalDrivingLicenseApplicationID);
        }
        public static bool PassedAllTests(int LocalDrivingLicenseApplicationID)
        {
            return GetPassedTestCount(LocalDrivingLicenseApplicationID) == 3;
        }
    }
}