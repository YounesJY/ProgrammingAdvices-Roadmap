-- Get number of vehicles per Make, then order them from High to low 

-- SOLUTION USING 'JOINS' + 'Aggregate Functions'
-- CHECK 'DATA WITH BARAA' CHANNEL IN YOUTUBE,WITH VIDEO TITLE 'JONS' / 'Aggregate Functions' / 'Window Functions'

SELECT 
	Makes.Make,
	NumberOfVehicles = COUNT(*)
FROM Makes
	LEFT JOIN VehicleDetails 
	ON VehicleDetails.MakeID = Makes.MakeID
GROUP BY Makes.Make
ORDER BY NumberOfVehicles DESC;


-- My 2nd SOLUTION USING 'Correlated SubQuery' [USE 100% OF YOUR BRAIN]
-- CHECK 'DATA WITH BARAA' CHANNEL IN YOUTUBE,WITH VIDEO TITLE 'SubQueries'
SELECT
	Make,
	NumberOfVehicles = (SELECT COUNT(*) FROM VehicleDetails WHERE VehicleDetails.MakeID = ref.MakeID)
FROM Makes ref
ORDER BY NumberOfVehicles DESC;
