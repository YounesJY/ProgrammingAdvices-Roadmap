-- Get all vehicles that have Engines > 3 Liters and have only 2 doors

-- SOLUTION
SELECT * 
FROM VehicleDetails
WHERE (Engine_Liter_Display > 3) AND (NumDoors = 2);