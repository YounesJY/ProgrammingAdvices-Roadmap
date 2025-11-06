-- Get all vehicles that their Body is 'Coupe' or 'Hatchback' or 'Sedan'


-- SOLUTION 1 : USING 'JOINS' [Simple logic + StrightForward]

SELECT * 
FROM VehicleDetails
INNER JOIN Bodies
	ON VehicleDetails.BodyID = Bodies.BodyID
WHERE Bodies.BodyName IN (N'Coupe', N'Hatchback', N'Sedan');


-- SOLUTION 2 : USING 'Nested SubQueries' [More Advanced + Step-By-Step Logic-Building + Easy To Debug] 
-- ! STEP 3 : Get the BodyName for each row [Iterative Approach] using a 'Coorelated SubQuery' [More Advanced + Iteration-Based]
SELECT 
	*,
	BodyName = (SELECT BodyName FROM Bodies WHERE Bodies.BodyID = Temp.BodyID) -- [Coorelated SubQuery] --> For each row, we resolve the BodyName
FROM (
	-- ! STEP 2 : All vehilces that have been manufatured with one of these bodies 
	SELECT *
	FROM VehicleDetails
	WHERE BodyID IN (
		-- ! STEP 1 : Get the list of IDs with BodyName is 'Coupe' or 'Hatchback' or 'Sedan'
		SELECT BodyID FROM Bodies WHERE BodyName IN (N'Coupe', N'Hatchback', N'Sedan')
	)
) AS Temp;


-- ===================================================================================================
-- !! NOTE : CHECK 'DATA WITH BARAA' CHANNEL IN YOUTUBE,WITH VIDEO TITLE 'JONS'/'SubQueries' !!
-- ===================================================================================================
