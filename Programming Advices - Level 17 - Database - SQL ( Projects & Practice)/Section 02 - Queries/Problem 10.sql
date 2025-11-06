-- Get all Makes that runs with GAS

-- SOLUTION USING 'JOINS' + 'MultiLevel Groupment'
-- CHECK 'DATA WITH BARAA' CHANNEL IN YOUTUBE,WITH VIDEO TITLE 'JONS'
SELECT 
	Makes.Make,
	FuelTypes.FuelTypeName
FROM VehicleDetails 
	INNER JOIN Makes 
		ON VehicleDetails.MakeID = Makes.MakeID
	INNER JOIN FuelTypes
		ON VehicleDetails.FuelTypeID = FuelTypes.FuelTypeID
WHERE FuelTypes.FuelTypeName = N'GAS' -- NOTE: USE 'N' with UNICODE strings/texts [NVARCHAR in general] for better performance and avoid errors
GROUP BY Makes.Make, FuelTypes.FuelTypeName
ORDER BY Makes.Make ASC


-- Teacher SOLUTION USING 'JOINS' +  'DISTINCT' Filter

SELECT DISTINCT
	Makes.Make,
	FuelTypes.FuelTypeName
FROM VehicleDetails 
	INNER JOIN Makes 
		ON VehicleDetails.MakeID = Makes.MakeID
	INNER JOIN FuelTypes
		ON VehicleDetails.FuelTypeID = FuelTypes.FuelTypeID
WHERE FuelTypes.FuelTypeName = N'GAS' -- NOTE: USE 'N' with UNICODE strings/texts [NVARCHAR in general] for better performance and avoid errors
ORDER BY Makes.Make ASC
