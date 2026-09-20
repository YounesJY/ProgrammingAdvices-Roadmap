/*
	Example: Employee Report Generation in T-SQL
	This script demonstrates the declaration, initialization, and use of variables in T-SQL.
	It generates a report for a specific department, including the department name, reporting period, and total employees hired within that period.
	This comprehensive script gives a practical insight into how variables can be effectively used in T-SQL to create dynamic and flexible SQL scripts.
*/
  
use C21_DB1;

DECLARE @EmpName varchar(50)
DECLARE @StartDate DATE = '2023-01-01';
DECLARE @EndDate DATE = '2023-02-01';
DECLARE @EmpHireDate date

SELECT TOP 1 
    @EmpName = Name, 
    @EmpHireDate = HireDate
FROM Employees
WHERE HireDate >= @StartDate AND HireDate < @EndDate
ORDER BY HireDate DESC;   -- newest first, take 1

PRINT 'The last emp got hired at Jan: ' + @EmpName
+ ', Hire Date: ' + CAST(@EmpHireDate AS varchar);


-- Step 1: Declare variables
DECLARE @DepartmentID INT;
DECLARE @StartDate DATE;
DECLARE @EndDate DATE;
DECLARE @TotalEmployees INT;
DECLARE @DepartmentName VARCHAR(50);

-- Step 2: Initialize variables
SET @DepartmentID = 3;
SET @StartDate = '2023-01-01';
SET @EndDate = '2023-12-31';

-- ------------------------------------------------------
-- ------------------------------------------------------
-- Step 3: Retrieve department name based on department ID
SELECT @DepartmentName = Name 
FROM Departments 
WHERE DepartmentID = @DepartmentID;

-- Step 4: Calculate the total number of employees in the specified department
SELECT @TotalEmployees = COUNT(*) 
FROM Employees 
WHERE DepartmentID = @DepartmentID 
AND HireDate BETWEEN @StartDate AND @EndDate;
-- ------------------------------------------------------
-- ------------------------------------------------------
											
-- Step 5: Print the report
PRINT 'Department Report';
PRINT 'Department Name: ' + @DepartmentName;
PRINT 'Reporting Period: ' + CAST(@StartDate AS VARCHAR) + ' to ' + CAST(@EndDate AS VARCHAR);
PRINT 'Total Employees Hired in ' + CAST(YEAR(@StartDate) AS VARCHAR) + ': ' + CAST(@TotalEmployees AS VARCHAR);