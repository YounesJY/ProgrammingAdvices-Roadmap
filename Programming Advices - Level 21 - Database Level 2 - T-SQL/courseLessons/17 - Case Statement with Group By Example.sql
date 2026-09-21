use C21_DB1;

--- -------------------------------------------
--- -------------------------------------------
SELECT
    PerformanceCategory,
    NumberOfEmployees = COUNT(*),
    AverageSalary = AVG(Salary)
FROM
(
		SELECT
			Name,
			Salary,
			PerformanceCategory = 
				CASE
					WHEN PerformanceRating >= 80 THEN 'High'
					WHEN PerformanceRating >= 60 THEN 'Medium'
					ELSE 'Low'
				END
		FROM Employees2	-- ORDER BY 	PerformanceCategory
	) AS PerformanceTable
GROUP BY PerformanceCategory;
--- -------------------------------------------
--- -------------------------------------------

SELECT 
	CASE 
		WHEN NULL = NULL THEN 'HOW ?'
		ELSE 0 
	END;   
--- -------------------------------------------
--- -------------------------------------------