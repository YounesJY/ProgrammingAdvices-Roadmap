-- Get all vehicles that have the Maximum Engine_CC


-- SOLUTION :
SELECT *
FROM VehicleDetails
WHERE Engine_CC = (SELECT MAX(Engine_CC) FROM VehicleDetails);