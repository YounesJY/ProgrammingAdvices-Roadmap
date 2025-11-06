-- Get all vehicles that has one of the Max 3 Engine CC


-- SOLUTION :
SELECT * 
FROM VehicleDetails
WHERE Engine_CC IN (
	SELECT TOP 3
	Engine_CC 
	FROM VehicleDetails
	ORDER BY Engine_CC DESC
)


-- ================================================================ -- !! IMPORTANT NOTE !! -- =================================================================================
-- Do you still remember 'this error below' from Problem 05 ?
-- "The ORDER BY clause is invalid in views, inline functions, derived tables, subqueries, and common table expressions, unless TOP, OFFSET or FOR XML is also specified." 
-- Well, it's no longer valid here, WHY ? --> IT's Because we're using TOP
-- !! The SubQuery MUST be FULLY executed in order to EXECUTE THE MAIN/OUTER Query,
-- !! ORDER BY should be executed, it's MANDATORY, it SHOULDN'T be IGNORED
-- =============================================================================================================================================================================

