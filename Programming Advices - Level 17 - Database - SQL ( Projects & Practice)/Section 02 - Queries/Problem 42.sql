-- Get a table of unique Engine_CC and calculate tax per Engine CC as follows:
	-- 0 to 1000    Tax = 100
	-- 1001 to 2000 Tax = 200
	-- 2001 to 4000 Tax = 300
	-- 4001 to 6000 Tax = 400
	-- 6001 to 8000 Tax = 500
	-- Above 8000   Tax = 600
	-- Otherwise    Tax = 0


-- SOLUTION 1 :	[Simple logic + StrightForward]
SELECT DISTINCT 
	Engine_CC,
	TaxPerEngine = (
	CASE
		WHEN Engine_CC BETWEEN 0 AND 1000 THEN 100
		WHEN Engine_CC BETWEEN 1001 AND 2000 THEN 200
		WHEN Engine_CC BETWEEN 2001 AND 4000 THEN 300
		WHEN Engine_CC BETWEEN 4001 AND 6000 THEN 400
		WHEN Engine_CC BETWEEN 6001 AND 8000 THEN 500
		WHEN Engine_CC > 8000 THEN 600
		ELSE 0

	END
	)
FROM VehicleDetails
ORDER BY Engine_CC ASC;

	
-- SOLUTION 2 :	USING 'SubQuery' [Simple logic + StrightForward]
SELECT 
	Temp.*,
	TaxPerEngine = (
		CASE
			WHEN Engine_CC BETWEEN 0 AND 1000 THEN 100
			WHEN Engine_CC BETWEEN 1001 AND 2000 THEN 200
			WHEN Engine_CC BETWEEN 2001 AND 4000 THEN 300
			WHEN Engine_CC BETWEEN 4001 AND 6000 THEN 400
			WHEN Engine_CC BETWEEN 6001 AND 8000 THEN 500
			WHEN Engine_CC > 8000 THEN 600
			ELSE 0
		END
	)
FROM (
	SELECT DISTINCT 
	Engine_CC
	FROM VehicleDetails
) AS Temp
ORDER BY Engine_CC ASC;


-- SOLUTION 3 : USING 'Coorelated SubQuery' [More Advanced+ Iteration-based --> For Each Record]
-- For each returned record (Engine_CC), we're calculating the tax for that Engine (on the fly), then move to the next record
-- =================================================== !! VERY IMPORTANT NOTE !! ===================================================================
-- !! The 'Coorelated SubQuery' CAN'T be executed as a STANDALONE Query, BECAUSE it depends on an outer reference (ref) from the MAIN/OUTER Query !!
-- !! If you got this error "The multi-part identifier "ref.Engine_CC" could not be bound." then EVERYTHING IS GOOD ''\__(^_^)__/''				  !!
-- =================================================================================================================================================
SELECT DISTINCT 
	Engine_CC,
	TaxPerEngine = ( 
		SELECT 
			CASE
				WHEN ref.Engine_CC BETWEEN 0 AND 1000 THEN 100
				WHEN ref.Engine_CC BETWEEN 1001 AND 2000 THEN 200
				WHEN ref.Engine_CC BETWEEN 2001 AND 4000 THEN 300
				WHEN ref.Engine_CC BETWEEN 4001 AND 6000 THEN 400
				WHEN ref.Engine_CC BETWEEN 6001 AND 8000 THEN 500
				WHEN ref.Engine_CC > 8000 THEN 600
				ELSE 0
			END
	)
FROM VehicleDetails as ref
ORDER BY Engine_CC ASC;


-- =====================================================================================
-- !! NOTE : CHECK 'DATA WITH BARAA' CHANNEL IN YOUTUBE,WITH VIDEO TITLE 'SubQueries' !!
-- =====================================================================================