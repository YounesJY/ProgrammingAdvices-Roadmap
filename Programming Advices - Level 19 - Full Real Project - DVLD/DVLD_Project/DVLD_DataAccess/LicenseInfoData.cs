using DVLD_Common;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD_DataAccess
{
    public static class LicenseInfoData
    {
        public static DataTable GetAllLicenses()
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            string query = @"
                SELECT * 
                FROM Licenses 
                ORDER BY IssueDate DESC
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
        public static DataTable GetDriverLicenses(int DriverID)
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            string query = @"
                SELECT Licenses.LicenseID,
                    Licenses.ApplicationID,
                    LicenseClasses.ClassName, 
                    Licenses.IssueDate, 
                    Licenses.ExpirationDate, 
                    Licenses.IsActive
                FROM Licenses INNER JOIN LicenseClasses 
                    ON Licenses.LicenseClassID = LicenseClasses.LicenseClassID
                WHERE Licenses.DriverID = @DriverID
                ORDER BY Licenses.IsActive DESC, Licenses.ExpirationDate DESC
            ";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DriverID", DriverID);

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
        public static int GetActiveLicenseIDByPersonID(int PersonID, int LicenseClassID)
        {
            int LicenseID = ValidationConstants.INVALID_ID;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            string query = @"
                SELECT
                    Licenses.LicenseID
                FROM Licenses INNER JOIN Drivers 
                    ON Licenses.DriverID = Drivers.DriverID
                WHERE 
                    Licenses.LicenseClassID = @LicenseClassID 
                    AND Drivers.PersonID = @PersonID
                    AND Licenses.IsActive = 1
            ";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int returnedID))
                    LicenseID = returnedID;
            }
            catch (Exception)
            {
                LicenseID = ValidationConstants.INVALID_ID;
            }
            finally
            {
                connection.Close();
            }

            return LicenseID;
        }
        /*
            * ====================================================================================
            * ENUM VS LOOKUP TABLE - DESIGN DECISION
            * ====================================================================================
            * 
            * This class uses a lookup table (LicenseIssuanceStatus) for license issuance status values.
            * This is another example of over-engineering for this specific use case.
            * 
            * 
            * ====================================================================================
            * THE TWO APPROACHES
            * ====================================================================================
            * 
            * Approach 1: Lookup Table (Current Implementation)
            * -------------------------------------------------
            *   CREATE TABLE LicenseIssuanceStatus (
            *       LicenseIssuanceStatusID TINYINT PRIMARY KEY,
            *       StatusName NVARCHAR(50) NOT NULL,
            *       StatusDescription NVARCHAR(200) NULL
            *   )
            *   
            *   INSERT INTO LicenseIssuanceStatus VALUES
            *       (1, 'Active', 'License is currently active and valid'),
            *       (2, 'Expired', 'License has expired'),
            *       (3, 'Suspended', 'License has been suspended'),
            *       (4, 'Revoked', 'License has been revoked')
            *   
            *   -- Licenses table references lookup
            *   CREATE TABLE Licenses (
            *       LicenseIssuanceStatusID TINYINT NOT NULL,
            *       FOREIGN KEY (LicenseIssuanceStatusID) REFERENCES LicenseIssuanceStatus(LicenseIssuanceStatusID)
            *   )
            * 
            * Approach 2: Enum + Check Constraint (Better Alternative)
            * --------------------------------------------------------
            *   -- Enum in C# code
            *   public enum enLicenseIssuanceStatus : byte {
            *       Active = 1,
            *       Expired = 2,
            *       Suspended = 3,
            *       Revoked = 4
            *   }
            *   
            *   -- Check constraint in database for integrity
            *   ALTER TABLE Licenses
            *   ADD CONSTRAINT CHK_LicenseIssuanceStatusID 
            *   CHECK (LicenseIssuanceStatusID IN (1, 2, 3, 4))
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
            * In this project, LicenseIssuanceStatus has 4 fixed values:
            *   1. Active
            *   2. Expired
            *   3. Suspended
            *   4. Revoked
            * 
            * These values are:
            *   - Business rules (not user-configurable)
            *   - Unlikely to change
            *   - Already defined as an enum in code (LicenseInfo.enLicenseIssuanceStatus)
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
            *      public enum enLicenseIssuanceStatus : byte {
            *          Active = 1,
            *          Expired = 2,
            *          Suspended = 3,
            *          Revoked = 4
            *      }
            * 
            *   2. Check constraint in database
            *      ALTER TABLE Licenses
            *      ADD CONSTRAINT CHK_LicenseIssuanceStatusID 
            *      CHECK (LicenseIssuanceStatusID IN (1, 2, 3, 4))
            * 
            *   3. Use the enum directly without JOINs
            *      SELECT LicenseID, LicenseIssuanceStatusID FROM Licenses
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
            *   CREATE VIEW vwLicenseDetails AS
            *   SELECT 
            *       LicenseID,
            *       LicenseIssuanceStatusID,
            *       CASE LicenseIssuanceStatusID
            *           WHEN 1 THEN 'Active'
            *           WHEN 2 THEN 'Expired'
            *           WHEN 3 THEN 'Suspended'
            *           WHEN 4 THEN 'Revoked'
            *       END AS LicenseIssuanceStatusName
            *   FROM Licenses
            * 
            * This gives you:
            *   - Performance (no JOIN on insert/update)
            *   - Integrity (check constraint)
            *   - Reporting capability (view with mapping)
            *   - Code simplicity (enum in C#)
            * 
            * 
            * ====================================================================================
            * ISSUE REASON VS ISSUANCE STATUS - TWO SEPARATE CONCEPTS
            * ====================================================================================
            * 
            * This project has two separate lookup tables for licenses:
            * 
            *   1. IssueReason (LicenseInfo.enIssueReason)
            *      - Why the license was issued (FirstTime, Renew, DamagedReplacement, LostReplacement)
            *      - Set once when license is issued
            *      - Fixed set of 4 values
            * 
            *   2. LicenseIssuanceStatus (LicenseInfo.enLicenseIssuanceStatus)
            *      - Current state of the license (Active, Expired, Suspended, Revoked)
            *      - Can change over time
            *      - Fixed set of 4 values
            * 
            * Both are over-engineered as lookup tables for the same reasons.
            * 
            * 
            * ====================================================================================
            * LESSON LEARNED
            * ====================================================================================
            * 
            * This current implementation uses lookup tables for both IssueReason and
            * LicenseIssuanceStatus, which is over-engineering for these specific cases.
            * However, it serves as an educational example of:
            * 
            *   1. How to implement lookup tables with foreign keys
            *   2. When a lookup table is appropriate
            *   3. When a simple enum would be better
            *   4. The trade-offs between complexity and flexibility
            * 
            * For future projects:
            *   - Small, fixed sets of values ? Use enum + check constraint
            *   - Large, changing sets of values ? Use lookup table
            *   - User-configurable values ? Use lookup table
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
            * both IssueReason and LicenseIssuanceStatus.
            * 
            * ====================================================================================
        */
        public static bool GetLicenseInfoByID(int LicenseID, ref int ApplicationID, ref int DriverID, ref int LicenseClassID, ref DateTime IssueDate, ref DateTime ExpirationDate, ref string Notes, ref float PaidFees, ref bool IsActive, ref byte IssueReasonID, ref int CreatedByUserID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            string query = @"
                SELECT * 
                FROM Licenses 
                WHERE LicenseID = @LicenseID
            ";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    ApplicationID = (int)reader["ApplicationID"];
                    DriverID = (int)reader["DriverID"];
                    LicenseClassID = (int)reader["LicenseClassID"];
                    IssueDate = (DateTime)reader["IssueDate"];
                    ExpirationDate = (DateTime)reader["ExpirationDate"];
                    Notes = (reader["Notes"] != DBNull.Value) ? (string)reader["Notes"] : string.Empty;
                    PaidFees = (float)reader["PaidFees"];
                    IsActive = (bool)reader["IsActive"];
                    IssueReasonID = (byte)reader["IssueReasonID"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];

                    isFound = true;
                }

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
        public static int AddNewLicense(int ApplicationID, int DriverID, int LicenseClassID, DateTime IssueDate, DateTime ExpirationDate, string Notes, float PaidFees, bool IsActive, byte IssueReasonID, int CreatedByUserID)
        {
            int LicenseID = ValidationConstants.INVALID_ID;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            string query = @"
                INSERT INTO Licenses 
                    (ApplicationID, DriverID, LicenseClassID, IssueDate, ExpirationDate, Notes, PaidFees, IsActive, IssueReasonID, CreatedByUserID)
                VALUES 
                    (@ApplicationID, @DriverID, @LicenseClassID, @IssueDate, @ExpirationDate, @Notes, @PaidFees, @IsActive, @IssueReasonID, @CreatedByUserID);

                SELECT SCOPE_IDENTITY();
            ";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            command.Parameters.AddWithValue("@IssueDate", IssueDate);
            command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            command.Parameters.AddWithValue("@Notes", (Notes != string.Empty) ? Notes : (object)DBNull.Value);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@IssueReasonID", IssueReasonID);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int returnedID))
                    LicenseID = returnedID;
            }
            catch (Exception)
            {
                LicenseID = ValidationConstants.INVALID_ID;
            }
            finally
            {
                connection.Close();
            }

            return LicenseID;
        }
        public static bool UpdateLicense(int LicenseID, int ApplicationID, int DriverID, int LicenseClassID, DateTime IssueDate, DateTime ExpirationDate, string Notes, float PaidFees, bool IsActive, byte IssueReasonID, int CreatedByUserID)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            string query = @"
                UPDATE Licenses 
                SET ApplicationID = @ApplicationID, 
                    DriverID = @DriverID,
                    LicenseClassID = @LicenseClassID,
                    IssueDate = @IssueDate,
                    ExpirationDate = @ExpirationDate,
                    Notes = @Notes,
                    PaidFees = @PaidFees,
                    IsActive = @IsActive,
                    IssueReasonID = @IssueReasonID,
                    CreatedByUserID = @CreatedByUserID
                WHERE LicenseID = @LicenseID
            ";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            command.Parameters.AddWithValue("@IssueDate", IssueDate);
            command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            command.Parameters.AddWithValue("@Notes", (Notes != string.Empty) ? Notes : (object)DBNull.Value);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@IssueReasonID", IssueReasonID);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                rowsAffected = 0;
            }
            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }
        public static bool DeleteLicense(int LicenseID)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            string query = @"
                DELETE FROM Licenses 
                WHERE LicenseID = @LicenseID
            ";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                rowsAffected = 0;
            }
            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }
        public static bool DeactivateLicense(int LicenseID)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            string query = @"
                UPDATE Licenses 
                SET IsActive = 0
                WHERE LicenseID = @LicenseID
            ";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                rowsAffected = 0;
            }
            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }
    }
}