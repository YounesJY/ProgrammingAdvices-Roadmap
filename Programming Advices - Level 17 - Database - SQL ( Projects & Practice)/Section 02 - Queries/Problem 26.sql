-- Get all vehicles that their body is 'Sport Utility' and Year > 2020


-- SOLUTION
SELECT * 
FROM VehicleDetails
INNER JOIN Bodies
	ON VehicleDetails.BodyID = Bodies.BodyID
WHERE (Bodies.BodyName = N'Sport Utility') AND (VehicleDetails.Year > 2020);