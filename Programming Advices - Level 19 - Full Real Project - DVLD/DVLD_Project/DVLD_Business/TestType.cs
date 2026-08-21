using System;
using System.Data;
using DVLD_DataAccess;
using DVLD_Common;

namespace DVLD_Business
{
    public class TestType
    {
        public enum enMode : byte { AddNew = 0, Update = 1 }
        public enum enTestType { VisionTest = 1, WrittenTest = 2, StreetTest = 3 }

        public enTestType TestTypeID { get; private set; }
        public string TestTypeTitle { get; set; }
        public string TestTypeDescription { get; set; }
        public float TestTypeFees { get; set; }

        private enMode _Mode = enMode.AddNew;


        private TestType(enTestType ID, string Title, string Description, float Fees)
        {
            this.TestTypeID = ID;
            this.TestTypeTitle = Title;
            this.TestTypeDescription = Description;
            this.TestTypeFees = Fees;

            this._Mode = enMode.Update;
        }
        public TestType() : this(enTestType.VisionTest, string.Empty, string.Empty, 0)
        {
            this._Mode = enMode.AddNew;
        }


        private bool _AddNewTestType()
        {
            this.TestTypeID = (enTestType)TestTypeData.AddNewTestType(
                this.TestTypeTitle,
                this.TestTypeDescription,
                this.TestTypeFees
            );
            return this.TestTypeID != (enTestType)(ValidationConstants.INVALID_ID);
        }
        private bool _UpdateTestType()
        {
            return TestTypeData.UpdateTestType(
                (int)this.TestTypeID,
                this.TestTypeTitle,
                this.TestTypeDescription,
                this.TestTypeFees
            );
        }

        public static DataTable GetAllTestTypes()
        {
            return TestTypeData.GetAllTestTypes();
        }
        public static TestType Find(enTestType TestTypeID)
        {
            string Title = string.Empty;
            string Description = string.Empty;
            float Fees = 0;

            if (TestTypeData.GetTestTypeInfoByID((int)TestTypeID, ref Title, ref Description, ref Fees))
                return new TestType(TestTypeID, Title, Description, Fees);

            return null;
        }

        public bool Save()
        {
            switch (this._Mode)
            {
                case enMode.AddNew:
                    if (_AddNewTestType())
                    {
                        this._Mode = enMode.Update;
                        return true;
                    }
                    return false;

                case enMode.Update:
                    return _UpdateTestType();

                default:
                    return false;
            }
        }
    }
}