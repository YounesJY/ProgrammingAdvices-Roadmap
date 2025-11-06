-- Get all Vehicle_Display_Name, year and add extra column to calculate the age of the car then sort the results by age desc.


-- SOLUTION :
SELECT DISTINCT
	VehicleDetails.Vehicle_Display_Name,
	VehicleDetails.Year,
	CarAge = (YEAR(GETDATE()) - VehicleDetails.Year) -- GETDATE vs SYSDATETIME ?
FROM VehicleDetails
ORDER BY CarAge DESC;


-- ===================================================================================================
-- !! NOTE : CHECK 'DATA WITH BARAA' CHANNEL IN YOUTUBE,WITH VIDEO TITLE 'Date and Time Functions ' !!
-- ===================================================================================================

