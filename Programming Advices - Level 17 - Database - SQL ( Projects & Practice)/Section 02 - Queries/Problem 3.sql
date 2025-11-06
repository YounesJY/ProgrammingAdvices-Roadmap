-- Get the number of vehicles made between 1950 and 2000

SELECT COUNT(*) AS Number_Of_Vehicles -- WHY NOT COUNT(ID) ?
FROM VehicleDetails 
WHERE YEAR BETWEEN 1950 AND 2000;