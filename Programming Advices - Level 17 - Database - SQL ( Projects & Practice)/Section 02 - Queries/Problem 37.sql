-- Get total vehicles that have Engin_CC above average


-- SOLUTION 1 [Shortest and more efficient] :
SELECT 
	 TotalVehiclesAboveAverageEngin_CC = COUNT(*) 
FROM VehicleDetails
WHERE Engine_CC > (SELECT AVG(Engine_CC) FROM VehicleDetails);


-- SOLUTION 2 [Built on top of previous solution] :
SELECT 
	TotalVehiclesAboveAverageEngin_CC = COUNT(*) 
FROM (
	-- Choose ID columns instead of all columns (*) 
	-- [As a best practice, only choose the needed columns for fast retrieve,
	-- otherwise you're only putting more stress on DataBase Engine,So more resources are being wasted]
	SELECT ID 
	FROM VehicleDetails
	WHERE Engine_CC > (SELECT AVG(Engine_CC) FROM VehicleDetails)
) AS Temp;

