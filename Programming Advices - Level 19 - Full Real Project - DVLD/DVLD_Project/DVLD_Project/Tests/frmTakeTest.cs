using DVLD_Business;
using DVLD_Common;
using System;
using System.Windows.Forms;

namespace DVLD_Project.Tests
{
    public partial class frmTakeTest : Form
    {
        public event Action<object, int> OnTestAppointmentTaken;
        public event Action<object, int> OnTestPassed;
        public event Action<object, int> OnAllTestPassed;

        private int _TestAppointmentID = ValidationConstants.INVALID_ID;
        private TestAppointment _TestAppointment
        {
            get { return ctrlSheduledTestDetails._TestAppointment; }
            set { ctrlSheduledTestDetails._TestAppointment = value; }
        }
        private ApplicationInfo _RetakeTestApplication = null;
        private bool IsRetakingTest = false;


        public frmTakeTest(int testAppointmentID)
        {
            InitializeComponent();
            this._TestAppointmentID = testAppointmentID;
            this._TestAppointment = TestAppointment.Find(this._TestAppointmentID);
        }
        private void frmTakeTest_Load(object sender, EventArgs e)
        {
            ctrlSheduledTestDetails.LoadTestDetails(this._TestAppointmentID);
            if (this._TestAppointment.IsLocked)
                ShowLockedTest();
        }



        private void ShowLockedTest()
        {
            MessageBox.Show("This test appointment is already locked and cannot be modified.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            ctrlSheduledTestDetails.TestID = this._TestAppointment.TestID.ToString();
            ctrlSheduledTestDetails.Enabled = false;
            lblUserMessage.Visible = true;

            Test lockedTest = Test.Find(this._TestAppointment.TestID);

            bool isPassedTest = (lockedTest.TestResult == true);
            rbPass.Checked = isPassedTest;
            rbFail.Checked = !isPassedTest;
            txtNotes.Text = lockedTest.Notes;
            rbPass.Enabled = false;
            rbFail.Enabled = false;
            txtNotes.Enabled = false;
            btnSave.Enabled = false;
        }
        private void CompleteApplication()
        {
            LocalDrivingLicenseApplication application = LocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(this._TestAppointment.LocalDrivingLicenseApplicationID);
            if (application == null)
                return;

            application.ApplicationStatusID = ApplicationInfo.enApplicationStatus.Completed;
            if (application.Save())
            {
                MessageBox.Show("Congratulations! All tests have been passed successfully. The license can now be issued.",
                                "All Tests Passed",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                OnAllTestPassed?.Invoke(this, application.LocalDrivingLicenseApplicationID);
            }
        }
        private bool IsStreetTest()
        {
            return this._TestAppointment.TestTypeID == (int)TestType.enTestType.StreetTest;
        }
        private void HandleTestPassed(Test test)
        {
            OnTestPassed?.Invoke(this, test.TestID);

            if (IsStreetTest())
                CompleteApplication();
        }
        private bool SaveRetakeApplicationIfNeeded()
        {
            this._RetakeTestApplication = ApplicationInfo.Find(this._TestAppointment.RetakeTestApplicationID);
            IsRetakingTest = (this._RetakeTestApplication != null);

            if (!IsRetakingTest)
                return true;

            this._RetakeTestApplication.ApplicationStatusID = ApplicationInfo.enApplicationStatus.Completed;
            if (!this._RetakeTestApplication.Save())
            {
                MessageBox.Show("Failed to save retake test application.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this._TestAppointmentID <= 0)
            {
                MessageBox.Show($"Invalid TestAppointmentID = {this._TestAppointmentID}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (this._TestAppointment == null)
            {
                MessageBox.Show($"No test appointment with testAppointmentID = {_TestAppointment}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            bool isPassedTest = rbPass.Checked;
            Test test = new Test()
            {
                TestAppointmentID = this._TestAppointmentID,
                TestResult = isPassedTest,
                Notes = this.txtNotes.Text.Trim(),
                CreatedByUserID = Global.currentLoggedInUser.UserID
            };
            this._TestAppointment.IsLocked = true;


            /*
                * ====================================================================================
                * TRANSACTION CONSISTENCY ISSUE - EDUCATIONAL NOTE
                * ====================================================================================
                * 
                * The btnSave_Click method performs two separate database operations:
                * 1. Saving the Test record
                * 2. Updating the Retake Application status (if applicable)
                * 
                * 
                * ====================================================================================
                * THE PROBLEM
                * ====================================================================================
                * 
                * These two operations are not atomic. If one succeeds and the other fails,
                * the database becomes inconsistent:
                * 
                * Scenario A (Update Retake Application First):
                *   1. Update Retake Application -> Success
                *   2. Save Test -> Failed (e.g., network error, constraint violation)
                *   
                *   Result: Retake application status is "Completed", but no test record exists.
                * 
                * Scenario B (Save Test First):
                *   1. Save Test -> Success
                *   2. Update Retake Application -> Failed
                *   
                *   Result: Test record exists, but retake application status remains "New".
                * 
                * 
                * ====================================================================================
                * WHY THIS HAPPENS
                * ====================================================================================
                * 
                * Each Save() method executes its own independent SQL command.
                * There is no transaction wrapping both operations together.
                * 
                * In a transaction, both operations would be treated as one unit:
                * 
                *   BEGIN TRANSACTION;
                *     UPDATE Applications SET ApplicationStatusID = 3 WHERE ApplicationID = @ID;
                *     INSERT INTO Tests (TestAppointmentID, TestResult, Notes, CreatedByUserID)
                *     VALUES (@TestAppointmentID, @TestResult, @Notes, @CreatedByUserID);
                *   COMMIT;
                * 
                * With a transaction, either both operations succeed or both are rolled back.
                * 
                * 
                * ====================================================================================
                * THE CORRECT SOLUTION - SQL TRANSACTIONS
                * ====================================================================================
                * 
                * In a production application, this should be handled using SQL Transactions:
                * 
                *   using (SqlTransaction transaction = connection.BeginTransaction())
                *   {
                *       try
                *       {
                *           // Operation 1
                *           // Operation 2
                *           transaction.Commit();
                *       }
                *       catch
                *       {
                *           transaction.Rollback();
                *       }
                *   }
                * 
                * Benefits of Transactions:
                *   Atomicity - All operations succeed or none do
                *   Consistency - Database remains in a valid state
                *   Isolation - Operations do not interfere with each other
                *   Durability - Committed changes are permanent
                * 
                * 
                * ====================================================================================
                * CURRENT EDUCATIONAL WORKAROUND
                * ====================================================================================
                * 
                * Since this is an educational project and transactions have not been covered yet,
                * a simple if-else compensation pattern is used:
                * 
                *   if (UpdateRetakeApplication())
                *   {
                *       if (SaveTest())
                *       {
                *           // Both succeeded
                *           return true;
                *       }
                *       else
                *       {
                *           // Test failed - rollback retake application manually
                *           RollbackRetakeApplication();
                *           return false;
                *       }
                *   }
                * 
                * This is not a perfect solution as there remains a small window for failure,
                * but it demonstrates the concept of compensation and rollback logic.
                * 
                * 
                * ====================================================================================
                * KNOWLEDGE GAINED
                * ====================================================================================
                * 
                * 1. Why transactions exist - To maintain data consistency across multiple operations
                * 2. When to use them - Any time multiple related operations need to be atomic
                * 3. How they work - BEGIN TRANSACTION, COMMIT, ROLLBACK
                * 4. What happens without them - Data inconsistency and orphaned records
                * 
                * 
                * ====================================================================================
                * FUTURE REFACTORING
                * ====================================================================================
                * 
                * When transactions are covered in the course, this method should be refactored to:
                * 
                *   1. Move both operations to the Data Access Layer
                *   2. Wrap them in a SqlTransaction
                *   3. Remove the manual compensation logic
                * 
                * 
                * ====================================================================================
                * COMMON REAL-WORLD EXAMPLES
                * ====================================================================================
                * 
                * - Bank transfers - Deduct from one account, add to another
                * - Order processing - Create order, update inventory, process payment
                * - User registration - Create user account, create profile, send confirmation
                * 
                * In all these cases, operations should either all succeed or all fail.
                * 
                * ====================================================================================
            */
            if (!SaveRetakeApplicationIfNeeded())
                return;

            // this will auto set TestAppointment to locked by the SQL QUERY
            if (!test.Save())
            {
                MessageBox.Show("Failed to record the test. Please try again.",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                if (IsRetakingTest)
                {
                    this._RetakeTestApplication.ApplicationStatusID = ApplicationInfo.enApplicationStatus.New;
                    if (!this._RetakeTestApplication.Save())
                    {
                        MessageBox.Show("Failed to record the test AND failed to rollback the retake application status. " +
                                        "Please contact support. (Data may be inconsistent)",
                                        "Critical Error",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);
                    }
                }
                return;
            }

            MessageBox.Show("Test has been taken and recorded successfully.",
                            "Success",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);


            OnTestAppointmentTaken?.Invoke(this, test.TestAppointmentID);
            if (test.TestResult)
                HandleTestPassed(test);

            /*
                if (!test.TestResult)
                    return;

                OnTestPassed?.Invoke(this, test.TestID);
                if (this._TestAppointment.TestTypeID != (int)TestType.enTestType.StreetTest)
                    return;

                LocalDrivingLicenseApplication application = LocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(this._TestAppointment.LocalDrivingLicenseApplicationID);
                if (application == null)
                    return;

                application.ApplicationStatusID = ApplicationInfo.enApplicationStatus.Completed;
                if (!application.Save())
                    return;

                MessageBox.Show("Congratulations! All tests have been passed successfully. The license can now be issued.",
                                "All Tests Passed",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                OnAllTestPassed?.Invoke(this, application.LocalDrivingLicenseApplicationID); 
            */
        }
    }
}
