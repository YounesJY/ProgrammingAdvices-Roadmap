using System;
using System.Data;
using System.Data.SqlClient;
using DVLD_Common;

namespace DVLD_DataAccess
{
    public static class ApplicationInfoData
    {
        /*
            this method retrieves all applications from the database and returns them as a DataTable.
            make sure you have a view named "ApplicationsList_View" in your database that contains the necessary columns for the applications.
        */
        public static DataTable GetAllApplications()
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            string query = @"
                    SELECT * 
                    FROM ApplicationsList_View 
                    ORDER BY ApplicationDate DESC
            ";
            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                    dataTable.Load(reader);

                reader.Close();
            }
            catch (Exception)
            {
                return dataTable;
            }
            finally
            {
                connection.Close();
            }

            return dataTable;
        }
        /*
            * ====================================================================================
            * ENUM VS LOOKUP TABLE - DESIGN DECISION
            * ====================================================================================
            * 
            * This class uses a lookup table (ApplicationStatus) for application status values.
            * This is an example of over-engineering for this specific use case.
            * 
            * 
            * ====================================================================================
            * THE TWO APPROACHES
            * ====================================================================================
            * 
            * Approach 1: Lookup Table (Current Implementation)
            * -------------------------------------------------
            *   CREATE TABLE ApplicationStatus (
            *       ApplicationStatusID TINYINT PRIMARY KEY,
            *       ApplicationStatusName NVARCHAR(50) NOT NULL,
            *       ApplicationStatusDescription NVARCHAR(200) NULL
            *   )
            *   
            *   INSERT INTO ApplicationStatus VALUES
            *       (1, 'New', 'Application has been created but not processed yet'),
            *       (2, 'Cancelled', 'Application has been cancelled'),
            *       (3, 'Completed', 'Application has been successfully completed')
            *   
            *   -- Applications table references lookup
            *   CREATE TABLE Applications (
            *       ApplicationStatusID TINYINT NOT NULL,
            *       FOREIGN KEY (ApplicationStatusID) REFERENCES ApplicationStatus(ApplicationStatusID)
            *   )
            * 
            * Approach 2: Enum + Check Constraint (Better Alternative)
            * --------------------------------------------------------
            *   -- Enum in C# code
            *   public enum enApplicationStatus : byte {
            *       New = 1,
            *       Cancelled = 2,
            *       Completed = 3
            *   }
            *   
            *   -- Check constraint in database for integrity
            *   ALTER TABLE Applications
            *   ADD CONSTRAINT CHK_ApplicationStatusID 
            *   CHECK (ApplicationStatusID IN (1, 2, 3))
            * 
            * 
            * ====================================================================================
            * COMPARISON
            * ====================================================================================
            * 
            * | Aspect                    | Lookup Table (Current) | Enum + Check Constraint |
            * |---------------------------|------------------------|-------------------------|
            * | Database Integrity        | FK ensures valid       | Check constraint        |
            * | Query Performance         | Slower (JOIN)          | Fast (direct int)       |
            * | Code Complexity           | More complex           | Simpler                 |
            * | Maintenance               | Need to maintain data  | Just enum               |
            * | Flexibility to add values | Easy                   | Requires code change   |
            * | Reporting                 | Has description        | Need mapping           |
            * 
            * 
            * ====================================================================================
            * WHEN TO USE EACH APPROACH
            * ====================================================================================
            * 
            * Use a Lookup Table when:
            *   - Values change frequently
            *   - Values are user-configurable
            *   - You need additional fields (descriptions, order, metadata)
            *   - Reporting requires descriptive names without code mapping
            *   - Multiple tables reference the same values
            * 
            * Use Enum + Check Constraint when:
            *   - Values are fixed business rules
            *   - Values are unlikely to change
            *   - You want simplicity and maximum performance
            *   - You already have the enum in code (DRY principle)
            * 
            * 
            * ====================================================================================
            * WHY A LOOKUP TABLE IS OVER-ENGINEERING HERE
            * ====================================================================================
            * 
            * In this project, ApplicationStatus has only 3 fixed values:
            *   1. New
            *   2. Cancelled
            *   3. Completed
            * 
            * These values are:
            *   - Business rules (not user-configurable)
            *   - Unlikely to change
            *   - Already defined as an enum in code (ApplicationInfo.enApplicationStatus)
            * 
            * The lookup table adds unnecessary complexity:
            *   - Extra table to maintain
            *   - Extra JOIN on every query
            *   - Duplicate definition of the same values (enum + table)
            *   - More code to manage
            * 
            * 
            * ====================================================================================
            * THE BETTER APPROACH (FOR REFERENCE)
            * ====================================================================================
            * 
            * Instead of a lookup table, use:
            * 
            *   1. Enum in C# code
            *      public enum enApplicationStatus : byte {
            *          New = 1,
            *          Cancelled = 2,
            *          Completed = 3
            *      }
            * 
            *   2. Check constraint in database
            *      ALTER TABLE Applications
            *      ADD CONSTRAINT CHK_ApplicationStatusID 
            *      CHECK (ApplicationStatusID IN (1, 2, 3))
            * 
            *   3. Use the enum directly without JOINs
            *      SELECT ApplicationID, ApplicationStatusID FROM Applications
            *      // Then map to enum in code
            * 
            * 
            * ====================================================================================
            * HYBRID APPROACH (Best of Both Worlds)
            * ====================================================================================
            * 
            * If you need both simplicity AND reporting, use:
            * 
            *   1. Enum + Check Constraint for the main table
            *   2. A VIEW for reporting that maps IDs to names
            * 
            *   CREATE VIEW vwApplicationDetails AS
            *   SELECT 
            *       ApplicationID,
            *       ApplicationStatusID,
            *       CASE ApplicationStatusID
            *           WHEN 1 THEN 'New'
            *           WHEN 2 THEN 'Cancelled'
            *           WHEN 3 THEN 'Completed'
            *       END AS ApplicationStatusName
            *   FROM Applications
            * 
            * This gives you:
            *   - Performance (no JOIN on insert/update)
            *   - Integrity (check constraint)
            *   - Reporting capability (view with mapping)
            *   - Code simplicity (enum in C#)
            * 
            * 
            * ====================================================================================
            * LESSON LEARNED
            * ====================================================================================
            * 
            * This current implementation uses a lookup table, which is over-engineering
            * for this specific case. However, it serves as an educational example of:
            * 
            *   1. How to implement a lookup table with foreign key
            *   2. When a lookup table is appropriate
            *   3. When a simple enum would be better
            *   4. The trade-offs between complexity and flexibility
            * 
            * For future projects:
            *   - Small, fixed sets of values -> Use enum + check constraint
            *   - Large, changing sets of values -> Use lookup table
            *   - User-configurable values -> Use lookup table
            * 
            * 
            * ====================================================================================
            * REFERENCES
            * ====================================================================================
            * 
            * - "When to use lookup tables" - Database Design Best Practices
            * - "Enum vs Lookup Table" - Stack Overflow
            * - "Database Normalization vs Denormalization" - Database Fundamentals
            * - "C# Enums vs Database Lookup Tables" - Software Architecture Patterns
            * 
            * ====================================================================================
            * NOTE
            * ====================================================================================
            * 
            * This implementation is kept for EDUCATIONAL PURPOSES to demonstrate
            * the lookup table approach. In a real production environment, the
            * simpler enum + check constraint approach would be recommended for
            * this specific use case.
            * 
            * ====================================================================================
        */
        public static bool GetApplicationByID(int ApplicationID, ref int ApplicantPersonID, ref DateTime ApplicationDate, ref int ApplicationTypeID,
            ref byte ApplicationStatusID, ref DateTime LastStatusDate, ref float PaidFees, ref int CreatedByUserID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            string query = @"
                    SELECT * 
                    FROM Applications 
                    WHERE ApplicationID = @ApplicationID
            ";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    ApplicantPersonID = (int)reader["ApplicantPersonID"];
                    ApplicationDate = (DateTime)reader["ApplicationDate"];
                    ApplicationTypeID = (int)reader["ApplicationTypeID"];
                    ApplicationStatusID = (byte)reader["ApplicationStatusID"];
                    LastStatusDate = (DateTime)reader["LastStatusDate"];
                    PaidFees = Convert.ToSingle(reader["PaidFees"]);
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                }
                else
                    isFound = false;

                reader.Close();
            }
            catch (Exception)
            {
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }
        public static int AddNewApplication(int ApplicantPersonID, DateTime ApplicationDate, int ApplicationTypeID,
            byte ApplicationStatusID, DateTime LastStatusDate, float PaidFees, int CreatedByUserID)
        {
            int ApplicationID = ValidationConstants.INVALID_ID;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            string query = @"
                    INSERT INTO 
                        Applications (ApplicantPersonID, ApplicationDate, ApplicationTypeID, ApplicationStatusID, LastStatusDate, PaidFees, CreatedByUserID)
                    VALUES 
                        (@ApplicantPersonID, @ApplicationDate, @ApplicationTypeID, @ApplicationStatusID, @LastStatusDate, @PaidFees, @CreatedByUserID);
                    SELECT SCOPE_IDENTITY();
            ";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
            command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@ApplicationStatusID", ApplicationStatusID);
            command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                    ApplicationID = insertedID;
            }
            catch (Exception)
            {
                ApplicationID = ValidationConstants.INVALID_ID;
            }
            finally
            {
                connection.Close();
            }

            return ApplicationID;
        }
        public static bool UpdateApplication(int ApplicationID, int ApplicantPersonID, DateTime ApplicationDate, int ApplicationTypeID,
            byte ApplicationStatusID, DateTime LastStatusDate, float PaidFees, int CreatedByUserID)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            string query = @"
                    UPDATE Applications
                    SET 
                        ApplicantPersonID = @ApplicantPersonID,
                        ApplicationDate = @ApplicationDate,
                        ApplicationTypeID = @ApplicationTypeID,
                        ApplicationStatusID = @ApplicationStatusID,
                        LastStatusDate = @LastStatusDate,
                        PaidFees = @PaidFees,
                        CreatedByUserID = @CreatedByUserID
                    WHERE ApplicationID = @ApplicationID
            ";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
            command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@ApplicationStatusID", ApplicationStatusID);
            command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }
        public static bool DeleteApplication(int ApplicationID)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            string query = @"
                    DELETE FROM Applications
                    WHERE ApplicationID = @ApplicationID
            ";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                //
            }
            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }

        public static bool IsApplicationExist(int ApplicationID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            string query = @"
                    SELECT Found = 1 
                    FROM Applications 
                    WHERE ApplicationID = @ApplicationID
            ";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                isFound = reader.HasRows;
                reader.Close();
            }
            catch (Exception)
            {
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }
        public static int GetActiveApplicationID(int PersonID, int ApplicationTypeID)
        {
            int ActiveApplicationID = ValidationConstants.INVALID_ID;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            /*
             * ==========================================
             * === Application Status Reference Table ===
             * ==========================================
                In the DB, we have:

                    ApplicationStatusID	ApplicationStatusName	ApplicationStatusDescription
                    1	                New	                    Application has been created but not processed yet
                    2	                Cancelled	            Application has been cancelled
                    3	                Completed	            Application has been successfully completed
                
                so we consider ApplicationStatusID = 1 as the active application.
            */

            string query = @"
                    SELECT 
                        ActiveApplicationID = ApplicationID 
                    FROM Applications 
                    WHERE 
                        ApplicantPersonID = @ApplicantPersonID 
                        AND ApplicationTypeID = @ApplicationTypeID 
                        AND ApplicationStatusID = 1
            ";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicantPersonID", PersonID);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int appID))
                    ActiveApplicationID = appID;
            }
            catch (Exception)
            {
                ActiveApplicationID = ValidationConstants.INVALID_ID;
            }
            finally
            {
                connection.Close();
            }

            return ActiveApplicationID;
        }
        public static bool DoesPersonHaveActiveApplication(int PersonID, int ApplicationTypeID)
        {
            return (GetActiveApplicationID(PersonID, ApplicationTypeID) != ValidationConstants.INVALID_ID);
        }
        public static int GetActiveApplicationIDForLicenseClass(int PersonID, int ApplicationTypeID, int LicenseClassID)
        {
            int ActiveApplicationID = ValidationConstants.INVALID_ID;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            string query = @"
                    SELECT 
                        ActiveApplicationID = Applications.ApplicationID
                    FROM Applications 
                    INNER JOIN LocalDrivingLicenseApplications 
                        ON Applications.ApplicationID = LocalDrivingLicenseApplications.ApplicationID
                    WHERE 
                        ApplicantPersonID = @ApplicantPersonID
                        AND ApplicationTypeID = @ApplicationTypeID
                        AND LocalDrivingLicenseApplications.LicenseClassID = @LicenseClassID
                        AND ApplicationStatusID = 1
            ";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicantPersonID", PersonID);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int appID))
                    ActiveApplicationID = appID;
            }
            catch (Exception)
            {
                return ActiveApplicationID;
            }
            finally
            {
                connection.Close();
            }

            return ActiveApplicationID;
        }
        public static bool UpdateStatus(int ApplicationID, short NewStatusID)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            string query = @"
                    UPDATE Applications
                    SET ApplicationStatusID = @NewStatusID,
                        LastStatusDate = @LastStatusDate
                    WHERE ApplicationID = @ApplicationID
            ";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@NewStatusID", NewStatusID);
            command.Parameters.AddWithValue("@LastStatusDate", DateTime.Now);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }
    }
}
