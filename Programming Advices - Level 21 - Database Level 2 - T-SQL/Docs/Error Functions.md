## Error Functions

### **Understanding Error Functions in T-SQL**

---

#### **Introduction**

    In T-SQL (Transact-SQL used in Microsoft SQL Server), <mark><u>**error handling is an essential aspect of writing robust and reliable database applications**</u></mark>. SQL Server provides several functions that can be used within a CATCH block of a TRY...CATCH construct to retrieve detailed information about errors. Understanding these functions is crucial for diagnosing and responding to errors effectively.

---

#### **Error Functions Overview**

- **ERROR_NUMBER()**
  - Purpose: Returns the error number of the error that caused the CATCH block to be executed.
  - Usage: Useful for identifying the specific error that occurred.
- **ERROR_SEVERITY()**
  - Purpose: Returns the severity level of the error.
  - Usage: Helps in understanding the nature and seriousness of the error. Severity levels range from 0 to 25.
- **ERROR_STATE()**
  - Purpose: Returns the state number of the error.
  - Usage: Useful for providing additional information about the error or to distinguish between errors with the same number.
- **ERROR_PROCEDURE()**
  - Purpose: Returns the name of the stored procedure or trigger in which the error occurred.
  - Usage: Essential for identifying the source of the error in complex systems with multiple procedures and triggers.
- **ERROR_LINE()**
  - Purpose: Returns the line number where the error occurred.
  - Usage: Helps in pinpointing the exact location in the code where the error was raised, facilitating quicker debugging.
- **ERROR_MESSAGE()**
  - Purpose: Provides the complete text of the error message.
  - Usage: Offers a detailed description of the error, which is valuable for understanding what went wrong.

---

#### **Practical Example**

To illustrate the use of these error functions, let's consider a simple code that can generate an error:

```plsql
    BEGIN TRY
        -- Intentional division by zero error
        SELECT 1 / 0;
    END TRY
    BEGIN CATCH
        SELECT 
            ERROR_NUMBER() AS ErrorNumber,
            ERROR_SEVERITY() AS ErrorSeverity,
            ERROR_STATE() AS ErrorState,
            ERROR_PROCEDURE() AS ErrorProcedure,
            ERROR_LINE() AS ErrorLine,
            ERROR_MESSAGE() AS ErrorMessage;
    END CATCH
```

When this code is executed, it will raise a division by zero error. The CATCH block will catch this error and use the error functions to return detailed information about the error.

---

#### **Conclusion**

Understanding and using these error functions effectively allows T-SQL developers to write more reliable and maintainable code by providing comprehensive error diagnostics. This information can be used for logging, debugging, or even to inform users about the nature of an issue in a more user-friendly manner. Remember, thorough error handling is a hallmark of high-quality database programming.

---
