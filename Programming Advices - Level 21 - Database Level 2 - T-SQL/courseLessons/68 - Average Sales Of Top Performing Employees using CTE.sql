use C21_DB1;

--- -------------------------------------------
--- -------------------------------------------
WITH TotalSales AS (
    SELECT 
        EmployeeID, 
        SUM(SaleAmount) AS TotalSales
    FROM SalesRecords
    GROUP BY EmployeeID
),
TopSalesEmployees AS (
    SELECT TOP 3 *
    FROM TotalSales
    ORDER BY TotalSales DESC
)

SELECT 
	 AverageTopSales = AVG(TotalSales)
FROM TopSalesEmployees;
--- -------------------------------------------
--- -------------------------------------------