-- Get Make and Total Number Of Doors Manufactured Per Make


-- SOLUTION 1 : USING 'JOINS' [Simple logic + StrightForward]
SELECT 
	Makes.Make,
	TotalNumberOfManufacturedDoors = SUM(VehicleDetails.NumDoors)
FROM VehicleDetails
INNER JOIN Makes
	ON VehicleDetails.MakeID = Makes.MakeID
GROUP BY Makes.Make
ORDER BY Makes.Make ASC; -- ORDER BY TotalNumberOfManufacturedDoors DESC


-- SOLUTION 2 : USING 'Coorelated SubQuery' [More Advanced + Iteration-based --> For Each Record]
-- For each returned record (Make), we're calculating the TotalNumberOfManufacturedDoors for this Make (on the fly), then move to the next record
-- =================================================== !! VERY IMPORTANT NOTE !! ===================================================================
-- !! The 'Coorelated SubQuery' CAN'T be executed as a STANDALONE Query, BECAUSE it relays on an outer reference (ref) from the MAIN/OUTER Query  !!
-- !! If you got this error "The multi-part identifier "ref.MakeID" could not be bound." then EVERYTHING IS GOOD ''\__(^_^)__/''				  !!
-- =================================================================================================================================================
SELECT 
	Make,
	TotalNumberOfManufacturedDoors = (
				SELECT SUM(VehicleDetails.NumDoors)
				FROM VehicleDetails
				WHERE VehicleDetails.MakeID = ref.MakeID
	)
FROM Makes AS ref
WHERE MakeID IN (SELECT DISTINCT MakeID FROM VehicleDetails)
-- !! WHERE is playing the role of 'INNER JOIN', it will keep only the Makes That has manufactured vehicles (not all makes made vehicles)
-- !! By removing WHERE, you're getting the EQUIVALENT OF USING 'LEFT/RIGHT' JOIN by keeping all makes REGUARDLESS if they did/didn't make any vehicles
ORDER BY ref.Make ASC; -- ORDER BY TotalNumberOfManufacturedDoors DESC


-- ============================================================================================
-- !! NOTE : CHECK 'DATA WITH BARAA' CHANNEL IN YOUTUBE,WITH VIDEO TITLE 'JONS'/'SubQueries' !!
-- ============================================================================================