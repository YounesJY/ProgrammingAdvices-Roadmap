use C21_DB1;

--- -------------------------------------------
--- -------------------------------------------
WITH SalesTotals AS 
(
	/*
		SELECT 
			EmployeeID, 
			TotalSales = SUM(SaleAmount)
		FROM SalesRecords
		GROUP BY EmployeeID
	*/
    SELECT DISTINCT 
        EmployeeID, 
        TotalSales = SUM(SaleAmount) OVER(PARTITION BY EmployeeID)
    FROM SalesRecords
),  

RankedSales AS (
    SELECT
		*,
        RANK() OVER (ORDER BY TotalSales DESC) AS SalesRank
    FROM SalesTotals
)
SELECT *
FROM RankedSales;
--- -------------------------------------------
--- -------------------------------------------