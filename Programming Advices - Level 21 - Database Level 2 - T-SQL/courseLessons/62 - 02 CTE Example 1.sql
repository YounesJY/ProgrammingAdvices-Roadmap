use C21_DB1;

--- -------------------------------------------
--- -------------------------------------------
--- Sub Query
SELECT * 
FROM
(
	SELECT 
		EmployeeId,
		Name,
		Sales
	FROM Employees6
    WHERE Department = 'Sales'
) AS SalesStaff ;


--- CTE
WITH SalesStaff AS
(
	SELECT 
		EmployeeId,
		Name,
		Sales
	FROM Employees6
    WHERE Department = 'Sales'
)

SELECT * 
FROM SalesStaff;
--- -------------------------------------------
--- -------------------------------------------