-- Get all vehicles that have the minimum Engine_CC


-- SOLUTION :
SELECT *
FROM VehicleDetails
WHERE Engine_CC = (SELECT MIN(Engine_CC) FROM VehicleDetails);