-- Get all vehicales that runs with GAS

-- SOLUTION USING [NON CORRELATED] 'SubQueries' TYPE 'SCALAR VALUE' in WHERE CLAUSE 
-- + [CORRELATED-SubQuery] in SELECT CLAUSE to get the FeulName [No sense since we already know the FuelName :-)]
-- CHECK 'DATA WITH BARAA' CHANNEL IN YOUTUBE,WITH VIDEO TITLE 'SubQueries'
SELECT 
	VehicleDetails.*,
	FuelTypeName = 'GAS'
	-- FuelTypeName = (SELECT FuelTypeName  FROM FuelTypes where VehicleDetails.FuelTypeID = FuelTypes.FuelTypeID) -- This one is a Correlated-SubQuery [More Complex + Slower ?]
FROM VehicleDetails 
WHERE VehicleDetails.FuelTypeID = (SELECT FuelTypeID  FROM FuelTypes where FuelTypeName = N'GAS') -- NOTE: USE 'N' with UNICODE strings/texts [NVARCHAR in general] for better performance and avoid errors


-- SOLUTION USING 'JOINS'
-- CHECK 'DATA WITH BARAA' CHANNEL IN YOUTUBE,WITH VIDEO TITLE 'JOINS'

SELECT 
	VehicleDetails.*,
	FuelTypes.FuelTypeName
FROM VehicleDetails
INNER JOIN FuelTypes
	ON VehicleDetails.FuelTypeID = FuelTypes.FuelTypeID  
WHERE FuelTypes.FuelTypeName = N'GAS'
