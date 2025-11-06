-- Return found=1 if there is any vehicle made in year 1950


-- SOLUTION :
SELECT 
	Found = 1
WHERE  EXISTS(
	SELECT TOP 1 1
	FROM VehicleDetails
	WHERE VehicleDetails.YEAR = 1950 
);