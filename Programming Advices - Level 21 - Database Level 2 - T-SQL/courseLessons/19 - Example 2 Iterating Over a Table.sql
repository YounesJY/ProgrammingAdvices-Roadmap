use C21_DB1;

--- -------------------------------------------
--- -------------------------------------------
-- This loop iterates over each employee in the Employees table.

DECLARE @EmployeeID INT;
DECLARE @Name varchar(50);
DECLARE @MaxID INT;
			
-- Initialize the starting point
SELECT @EmployeeID = MIN(EmployeeID) FROM Employees;
-- Find the maximum EmployeeID
SELECT @MaxID = MAX(EmployeeID) FROM Employees;

-- Loop through employees
WHILE (@EmployeeID IS NOT NULL) AND (@EmployeeID <= @MaxID)
BEGIN
    -- Perform an operation, e.g., print employee's name
    SELECT @Name=Name FROM Employees WHERE EmployeeID = @EmployeeID;
	PRINT @Name;

	-- SET @EmployeeID = @EmployeeID + 1;

    /*
        Imagine your table has had DELETE operations. Not all IDs still exist in the table.
        So "SET @EmployeeID = @EmployeeID + 1" is not guaranteed to land on a valid ID.
        Instead, we fetch the next existing ID: the minimum ID greater than the current one.
        If no such ID exists, MIN(...) returns NULL, and the WHILE condition terminates the loop.
    */
	SELECT @EmployeeID = MIN(EmployeeID) FROM Employees WHERE EmployeeID > @EmployeeID;
END
--- -------------------------------------------
--- -------------------------------------------