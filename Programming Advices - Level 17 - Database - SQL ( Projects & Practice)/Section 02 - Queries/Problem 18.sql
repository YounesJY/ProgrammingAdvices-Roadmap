-- Get total vehicles per DriveTypeName Per Make and order them per make asc then per total Desc

-- SOLUTION 1 : USING 'JOINS' + 'MultiLevel Grouping '[Simple logic + StrightForward]
SELECT DISTINCT 
	Makes.Make,
	DriveTypes.DriveTypeName,
	TotalVehicles = COUNT(*)
FROM VehicleDetails
INNER JOIN Makes
	ON  VehicleDetails.MakeID = Makes.MakeID
INNER JOIN DriveTypes
	ON  VehicleDetails.DriveTypeID = DriveTypes.DriveTypeID
GROUP BY Makes.Make,  DriveTypes.DriveTypeName
ORDER BY  Makes.Make ASC, TotalVehicles DESC;