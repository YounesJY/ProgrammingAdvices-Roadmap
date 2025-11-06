-- Get the number of vehicles made between 1950 and 2000 per make, 
-- Ordered by number of vehicles descending

SELECT 
	Makes.Make,
	COUNT(*) AS Number_Of_Vehicles
FROM VehicleDetails
INNER JOIN Makes
	ON VehicleDetails.MakeID = Makes.MakeID  
WHERE VehicleDetails.YEAR BETWEEN 1950 AND 2000
GROUP BY Makes.Make
ORDER BY Number_Of_Vehicles DESC;