use C21_DB1;

--- -------------------------------------------
--- -------------------------------------------
WITH EmployeeTreeHierarchy AS (
    -- Anchor member: This selects the root of the hierarchy (CEO in this case) and starts at Level 0
    SELECT 
		EmployeeID, 
		ManagerID,
		Name, 
		Hierarchy = CAST(Name AS NVARCHAR(MAX)),
		LEVEL = 1
    FROM Employees7
    WHERE ManagerID IS NULL

    UNION ALL

    -- Recursive member: This part of the CTE builds the hierarchy and increments the Level by 1
    SELECT	
		e.EmployeeID,
		e.ManagerID,
		e.Name, 
		Hierarchy = CONCAT(ETH.Hierarchy, ' -> ',e.Name), 
		Level = ETH.Level + 1 
    FROM Employees7 e
    INNER JOIN EmployeeTreeHierarchy ETH
		ON e.ManagerID = ETH.EmployeeID
)
-- This SELECT statement retrieves the hierarchical data with Level

SELECT * FROM EmployeeTreeHierarchy
ORDER BY Level ASC;
--- -------------------------------------------
--- -------------------------------------------