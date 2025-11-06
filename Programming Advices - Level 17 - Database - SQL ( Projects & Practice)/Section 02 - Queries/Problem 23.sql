-- Get MakeID , Make, SubModelName for all vehicles that have SubModelName 'Elite'


-- SOLUTION 1 : USING 'JOINS' [Simple logic + StrightForward]
SELECT DISTINCT
	Makes.MakeID,
	Makes.Make,
	SubModels.SubModelName
FROM VehicleDetails
INNER JOIN Makes
	ON VehicleDetails.MakeID = Makes.MakeID
INNER JOIN SubModels
	ON VehicleDetails.SubModelID = SubModels.SubModelID
WHERE SubModels.SubModelName = N'Elite'
ORDER BY Makes.MakeID;

-- SOLUTION 2 : USING 'Nested SubQueries' [More Advanced + Step-By-Step Logic-Building + Easy To Debug] 
-- ! STEP 3 : Get All Makes info for these IDs from SubQuery 2
SELECT 
	Makes.*,
	SubModelName = 'Elite'
FROM Makes
WHERE MakeID IN (
	-- ! STEP 2 : All Makes IDs that manufactured vehicles with these IDs from SubQuery 1
	SELECT DISTINCT
		MakeID
	FROM VehicleDetails
	WHERE SubModelID IN (
		-- ! STEP 1 : Get the list of IDs with SubModelName = 'Elite'
		SELECT SubModelID FROM SubModels WHERE SubModelName = N'Elite'
	)
)
ORDER BY MakeID;


-- ===================================================================================================
-- !! NOTE : CHECK 'DATA WITH BARAA' CHANNEL IN YOUTUBE,WITH VIDEO TITLE 'JONS'/'SubQueries' !!
-- ===================================================================================================


