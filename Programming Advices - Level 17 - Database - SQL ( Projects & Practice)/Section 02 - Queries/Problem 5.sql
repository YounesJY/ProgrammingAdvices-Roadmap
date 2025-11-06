-- Get all makes that have manufactured more than 12000 vehicles  between 1950 and 2000 

-- SOLUTION WITH 'HAVING' [BEST PRACICE --> SIMPLE & READABLE]
SELECT 
	Makes.Make,
	COUNT(*) AS NumberOfVehicles
FROM VehicleDetails
INNER JOIN Makes
	ON VehicleDetails.MakeID = Makes.MakeID  
WHERE VehicleDetails.YEAR BETWEEN 1950 AND 2000
GROUP BY Makes.Make
HAVING COUNT(*) > 12000
ORDER BY NumberOfVehicles DESC;


-- SOLUTION WITH 'WHERE' [MORE ADVANCED - SubQuery-Based Solution --> MORE COMPLEXE & LESS READABLE]
SELECT *
FROM (
	SELECT 
		Makes.Make,
		COUNT(*) AS NumberOfVehicles
	FROM VehicleDetails
	INNER JOIN Makes
		ON VehicleDetails.MakeID = Makes.MakeID  
	WHERE VehicleDetails.YEAR BETWEEN 1950 AND 2000
	GROUP BY Makes.Make
	-- ORDER BY NumberOfVehicles DESC -- You CANT DO THIS, search the error:
	-- "The ORDER BY clause is invalid in views, inline functions, derived tables, subqueries, and common table expressions, unless TOP, OFFSET or FOR XML is also specified." 
) AS TEMP
WHERE NumberOfVehicles > 12000
ORDER BY NumberOfVehicles DESC

-- FOR MORE INFO ABOUT METHOD 2, SEARCH [SubQueries in YouTube channel 'Data With Baraa']
