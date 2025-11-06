-- Get all Vehicle_Display_Name, year, Age for vehicles that their age between 15 and 25 years old 

-- SOLUTION 1 :  USING 'SubQueries' + Built on top of previous solution from [Problem 31]
-- ! STEP 2 Filter and keep only those with age between 15 and 25 years old:
SELECT DISTINCT *
FROM (
	-- ! STEP 1 : Get all Vehicle_Display_Name, year, Age for all vehicles 
	SELECT 
		VehicleDetails.Vehicle_Display_Name,
		VehicleDetails.Year,
		CarAge = (YEAR(GETDATE()) - VehicleDetails.Year) -- GETDATE vs SYSDATETIME ?
	FROM VehicleDetails
) AS Temp
WHERE CarAge BETWEEN 15 AND 25 
ORDER BY CarAge DESC;


-- SOLUTION 2 : USING 'SubQueries'
-- ! STEP 2 :  Get all [Vehicle_Display_Name, year, Age] for vehicles with ID in that list from the SubQuery
SELECT DISTINCT
	VehicleDetails.Vehicle_Display_Name,
	VehicleDetails.Year,
	CarAge = (YEAR(GETDATE()) - VehicleDetails.Year) -- GETDATE vs SYSDATETIME ?
FROM VehicleDetails
WHERE ID IN (
	-- ! STEP 1 : Get the IDs list of vehicles with age between 15 and 25 years old: 
	SELECT ID
	FROM VehicleDetails
	WHERE (YEAR(GETDATE()) - VehicleDetails.Year) BETWEEN 15 AND 25
)
ORDER BY CarAge DESC;


-- =====================================================================================
-- !! NOTE : CHECK 'DATA WITH BARAA' CHANNEL IN YOUTUBE,WITH VIDEO TITLE 'SubQueries' !!
-- =====================================================================================

