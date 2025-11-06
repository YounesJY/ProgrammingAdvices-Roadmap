-- Get all makes that have manufactured more than 12000 vehicles  between 1950 and 2000
-- With the total number of vehicles beside
-- Then calculate the percentage

-- SOLUTION USING 'SubQueries' TYPE 'SCALAR VALUE' in SELECT CLAUSE
-- ! CHECK 'DATA WITH BARAA' CHANNEL IN YOUTUBE,WITH VIDEO TITLE 'SubQueries'

SELECT 
	Temp.*, 
	Percentage = (1.0 * NumberOfVehicles / TotalVehicles),						   -- MySolution [I knew there was a casting method, so i wantched the video + ChatGPT]
	Percentage = (CAST(NumberOfVehicles AS FLOAT) / CAST(TotalVehicles AS FLOAT)), -- TeacherSolution [NOTE -> it's ENOUGH to cast ONE operand, no need to cast BOTH of them]
	Percentage = (CAST(NumberOfVehicles AS FLOAT) / TotalVehicles)				   -- BEST PRACTICE [Casting ONE operand is ENOUGH for implicit casting due to 'DATA TYPE PRECEDENCE']
FROM (
	SELECT 
		Makes.Make,
		NumberOfVehicles = COUNT(*) ,
		TotalVehicles = (SELECT COUNT(*) FROM VehicleDetails)
	FROM VehicleDetails
	INNER JOIN Makes
		ON VehicleDetails.MakeID = Makes.MakeID   
	WHERE VehicleDetails.YEAR BETWEEN 1950 AND 2000
	GROUP BY Makes.Make
) AS Temp
ORDER BY NumberOfVehicles DESC;
