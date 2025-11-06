-- Get total of Makes that runs with GAS

-- SOLUTION USING 'Outer/Main SubQueries' on top of the previous result set
-- CHECK 'DATA WITH BARAA' CHANNEL IN YOUTUBE,WITH VIDEO TITLE 'SubQueries'
SELECT 
	COUNT(*) AS TotalMakesRunsWithGAS
FROM (
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
) AS Temp

-- Teacher SOLUTION USING 'JOINS' +  'DISTINCT' Filter

SELECT 
	COUNT(*) AS TotalMakesRunsWithGAS
FROM (
	SELECT DISTINCT
		Makes.Make,
		FuelTypes.FuelTypeName
	FROM VehicleDetails 
		INNER JOIN Makes 
			ON VehicleDetails.MakeID = Makes.MakeID
		INNER JOIN FuelTypes
			ON VehicleDetails.FuelTypeID = FuelTypes.FuelTypeID
	WHERE FuelTypes.FuelTypeName = N'GAS' -- NOTE: USE 'N' with UNICODE strings/texts [NVARCHAR in general] for better performance and avoid errors
) AS Temp
