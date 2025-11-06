-- Get all makes that have manufactured more than 12000 vehicles  between 1950 and 2000
-- With the total number of vehicles beside

-- SOLUTION USING 'SubQueries' TYPE 'SCALAR VALUE' in SELECT CLAUSE
-- CHECK 'DATA WITH BARAA' CHANNEL IN YOUTUBE,WITH VIDEO TITLE 'SubQueries'
SELECT 
	Makes.Make,
	NumberOfVehicles = COUNT(*) ,
	TotalVehicles = (SELECT COUNT(*) FROM VehicleDetails)
FROM VehicleDetails
INNER JOIN Makes
	ON VehicleDetails.MakeID = Makes.MakeID  
WHERE VehicleDetails.YEAR BETWEEN 1950 AND 2000
GROUP BY Makes.Make
ORDER BY NumberOfVehicles DESC;

