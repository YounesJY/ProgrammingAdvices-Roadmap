use C21_DB1;

--Using CASE in ORDER BY (Custom Sorting)
--- -------------------------------------------
--- -------------------------------------------
SELECT *
FROM Sales
ORDER BY	
		CASE 
			WHEN SaleAmount > 150 THEN 1
			ELSE 2
		END;
--- -------------------------------------------
--- -------------------------------------------
SELECT 
	*,
	SortingField = 
		CASE 
			WHEN SaleAmount > 150 THEN 1
			ELSE 2
		END
FROM Sales
ORDER BY SortingField ASC, SaleAmount DESC;
--- -------------------------------------------
--- -------------------------------------------