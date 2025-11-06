-- Get all Vehicle_Display_Name, NumDoors and add extra column to describe number of doors by words,
-- And if door is null display 'Not Set'

---- All the constant values for NumDoors
-- SELECT DISTINCT NumDoors
-- FROM VehicleDetails;

SELECT DISTINCT
	Vehicle_Display_Name,
	NumDoors,
	DoorsDescription = (
	CASE
		WHEN NumDoors IS NULL THEN 'Not Set'
		WHEN NumDoors = 0 THEN 'No Doors'
		WHEN NumDoors = 1 THEN 'One Door'
		WHEN NumDoors = 2 THEN 'Two Doors'
		WHEN NumDoors = 3 THEN 'Three Doors'
		WHEN NumDoors = 4 THEN 'Four Doors'
		WHEN NumDoors = 5 THEN 'Five Doors'
		WHEN NumDoors = 6 THEN 'Six Doors'
		WHEN NumDoors = 7 THEN 'Seven Doors'
		WHEN NumDoors = 8 THEN 'Eight Doors'
		ELSE 'Unknown'
	END
	)
FROM VehicleDetails
ORDER BY NumDoors;