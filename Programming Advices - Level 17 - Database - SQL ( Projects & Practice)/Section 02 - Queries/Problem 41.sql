-- Get all Makes that manufactures one of the Max 3 Engine CC


-- [MY SOLUTION] + [Built on top of previous solution]
-- SOLUTION : USING 'Nested SubQueries' [More Advanced + Step-By-Step Logic-Building + Easy To Debug] 

-- !! STEP 3 : Get all Makes with ID in that list
SELECT * 
FROM Makes 
WHERE MakeID IN (
	-- !! STEP 2 : -- Get MakeIDs list of makes that manufactured vehicles with one of the Max 3 Engine CC
	SELECT DISTINCT 
		MakeID
	FROM VehicleDetails
	WHERE Engine_CC IN (
		-- !! STEP 1 : -- Get the maximum 3 Engine CC
		SELECT DISTINCT TOP 3 
			Engine_CC 
		FROM VehicleDetails
		ORDER BY Engine_CC DESC
	)
)
ORDER BY MakeID;


-- [TEACHER SOLUTION]
-- SOLUTION : USING 'JOINS' [Simple logic + StrightForward]

-- !! STEP 2 : -- Get all makes that manufactured vehicles with one of the Max 3 Engine CC
SELECT DISTINCT
	Makes.* 
FROM Makes
INNER JOIN VehicleDetails
	ON VehicleDetails.MakeID = Makes.MakeID
WHERE Engine_CC IN (
		-- !! STEP 1 : -- Get the maximum 3 Engine CC
		SELECT DISTINCT TOP 3 
			Engine_CC 
		FROM VehicleDetails
		ORDER BY Engine_CC DESC
	)
ORDER BY MakeID;


-- ============================================================================================
-- !! NOTE : CHECK 'DATA WITH BARAA' CHANNEL IN YOUTUBE,WITH VIDEO TITLE 'JONS'/'SubQueries' !!
-- ============================================================================================