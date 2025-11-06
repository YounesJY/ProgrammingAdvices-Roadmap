-- Get total Makes that Mantufactures DriveTypeName=FWD

-- SOLUTION 1 : USING 'JOINS' [Simple logic + StrightForward]
SELECT 
	TotalMakesWithFWD = COUNT(*)
FROM (
	SELECT DISTINCT 
		Makes.*,
		DriveTypes.DriveTypeName
	FROM VehicleDetails
	INNER JOIN Makes
		ON  VehicleDetails.MakeID = Makes.MakeID
	INNER JOIN DriveTypes
		ON  VehicleDetails.DriveTypeID = DriveTypes.DriveTypeID
	WHERE DriveTypes.DriveTypeName = N'FWD'
)AS temp
-- SOLUTION 2 : USING 'Nested SubQueries' [More Advanced + Step-By-Step Logic-Building + Easy To Debug]

-- ! STEP 3 : Get all makes in that list of IDs 
SELECT 
	*,
	DriveTypeName = 'FWD'
FROM Makes 
WHERE MakeID IN (
	-- ! STEP 2 : Get the ID list of makes that have manufactured vehicles with that DriveTypeID From the 1st SubQuery
	SELECT DISTINCT MakeID
	FROM VehicleDetails
	WHERE DriveTypeID = (
		-- ! STEP 1 : Get ID of DriveTypeName = 'FWD'
		SELECT DriveTypeID FROM DriveTypes WHERE DriveTypeName = 'FWD'
	)
);


-- ============================================================================================
-- !! NOTE : CHECK 'DATA WITH BARAA' CHANNEL IN YOUTUBE,WITH VIDEO TITLE 'JONS'/'SubQueries' !!
-- ============================================================================================


