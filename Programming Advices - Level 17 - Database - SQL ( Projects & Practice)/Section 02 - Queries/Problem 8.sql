-- Get all vehicles per Makes and per FuelTypes 

-- SOLUTION USING 'MultiLevel Groupment'with more than one column

SELECT 
	Makes.Make,					-- First Level
	FuelTypes.FuelTypeName,		-- Second Level
	NumberOfVehicles = COUNT(*)
FROM VehicleDetails
INNER JOIN Makes
	ON VehicleDetails.MakeID = Makes.MakeID  
INNER JOIN FuelTypes
	ON VehicleDetails.FuelTypeID = FuelTypes.FuelTypeID
WHERE VehicleDetails.YEAR BETWEEN 1950 AND 2000
GROUP BY  Makes.Make, FuelTypes.FuelTypeName 
ORDER BY  Makes.Make ASC;
--ORDER BY  Makes.Make ASC, NumberOfVehicles DESC; 

