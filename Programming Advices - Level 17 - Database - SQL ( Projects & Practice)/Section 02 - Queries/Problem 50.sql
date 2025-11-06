-- Get all Fuel Types , each time the result should be showed in random order


-- SOLUTION : USING 'GUID/UUID'
SELECT * 
FROM FuelTypes
ORDER BY NEWID();