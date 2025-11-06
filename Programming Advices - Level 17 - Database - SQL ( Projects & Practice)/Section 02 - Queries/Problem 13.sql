-- Get number of vehicles per Make, then shows onlt those who have manufactured more than 20K vehicle

-- SOLUTION USING 'JOINS' + 'Aggregate Functions'
-- CHECK 'DATA WITH BARAA' CHANNEL IN YOUTUBE,WITH VIDEO TITLE 'JONS' / 'Aggregate Functions' / 'Window Functions'

SELECT 
	Makes.Make,
	NumberOfVehicles = COUNT(*)
FROM Makes
	LEFT JOIN VehicleDetails 
	ON VehicleDetails.MakeID = Makes.MakeID
GROUP BY Makes.Make
HAVING COUNT(*) > 20000
ORDER BY NumberOfVehicles DESC;


-- My 2nd SOLUTION USING 'Correlated SubQuery' [USE 100% OF YOUR BRAIN] + Other Query for FILTRING with WHERE
-- CHECK 'DATA WITH BARAA' CHANNEL IN YOUTUBE,WITH VIDEO TITLE 'SubQueries'
SELECT *
FROM (
	SELECT
		Make,
		NumberOfVehicles = (SELECT COUNT(*) FROM VehicleDetails WHERE VehicleDetails.MakeID = ref.MakeID)
	FROM Makes ref
) AS Temp 
WHERE NumberOfVehicles > 20000
ORDER BY NumberOfVehicles DESC;