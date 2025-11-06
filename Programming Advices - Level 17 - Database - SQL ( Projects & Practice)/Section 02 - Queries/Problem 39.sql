-- Get the maximum 3 Engine CC


-- SOLUTION 1 [Shortest and more efficient] :
SELECT DISTINCT TOP 3 
	Engine_CC 
FROM VehicleDetails
ORDER BY Engine_CC DESC;


-- SOLUTION 2 [Built on top of previous solution] :
SELECT TOP 3 *
FROM (
	SELECT DISTINCT
		Engine_CC 
	FROM VehicleDetails
) AS Temp
ORDER BY Engine_CC DESC;
