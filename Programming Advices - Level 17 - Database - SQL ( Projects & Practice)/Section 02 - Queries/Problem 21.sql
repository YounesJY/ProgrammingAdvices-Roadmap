-- Get Total Vehicles that number of doors is not specified

SELECT 
	TotalVehiclesWithNoSpecifiedDoors = COUNT(*) 
FROM VehicleDetails
WHERE NumDoors IS NULL;