-- Get all vehicles that have Engin_CC below average


-- SOLUTION :
SELECT *
FROM VehicleDetails
WHERE Engine_CC < (SELECT AVG(Engine_CC) FROM VehicleDetails);