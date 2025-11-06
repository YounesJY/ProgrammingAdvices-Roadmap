-- Get percentage of vehicles that has no doors specified

-- SOLUTION 1
SELECT 
	PercentageOfVehiclesWithNoSpecifiedDoors = (CAST(COUNT(*) AS float) / (SELECT COUNT(*) FROM VehicleDetails)),
	PercentageOfVehiclesWithNoSpecifiedDoors = (CAST(COUNT(*) AS float) / (SELECT COUNT(*) FROM VehicleDetails)) * 100
FROM VehicleDetails
WHERE NumDoors IS NULL;

-- SOLUTION 2
SELECT 
	PercentageOfVehiclesWithNoSpecifiedDoors = (
		CAST(
			(SELECT COUNT(*) FROM VehicleDetails WHERE NumDoors IS NULL) AS float
		) 
			/ 
			(SELECT COUNT(*) FROM VehicleDetails)
	),
	
	PercentageOfVehiclesWithNoSpecifiedDoors = (
		CAST(
			(SELECT COUNT(*) FROM VehicleDetails WHERE NumDoors IS NULL) AS float
		) 
			/ 
			(SELECT COUNT(*) FROM VehicleDetails)
	) * 100;