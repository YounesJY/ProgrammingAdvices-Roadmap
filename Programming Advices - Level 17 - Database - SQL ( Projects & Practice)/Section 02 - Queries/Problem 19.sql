-- Get total vehicles per DriveTypeName Per Make then filter only results with total > 10,000


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
HAVING COUNT(*) > 10000
ORDER BY  Makes.Make ASC, TotalVehicles DESC;
