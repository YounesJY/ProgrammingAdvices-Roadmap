-- Get the highest 3 manufacturers that make the highest number of models


-- SOLUTION 1 : USING 'JOINS' [Simple logic + StrightForward]
SELECT TOP 3	
	Makes.Make,
	NumberOfModelsPerMake = COUNT(*)
FROM Makes
INNER JOIN MakeModels
	ON Makes.MakeID = MakeModels.MakeID
GROUP BY Makes.Make
ORDER BY NumberOfModelsPerMake DESC;
	   

-- SOLUTION 2 : USING 'Coorelated SubQuery' [More Advanced + Iteration-based --> For Each Record]
-- For each returned record (Make), we're calculating the NumberOfModelsPerMake for this Make (on the fly), then move to the next record
-- =================================================== !! VERY IMPORTANT NOTE !! ===================================================================
-- !! The 'Coorelated SubQuery' CAN'T be executed as a STANDALONE Query, BECAUSE it relays on an outer reference (ref) from the MAIN/OUTER Query  !!
-- !! If you got this error "The multi-part identifier "ref.MakeID" could not be bound." then EVERYTHING IS GOOD ''\__(^_^)__/''				  !!
-- =================================================================================================================================================
SELECT TOP 3
	Make,
	NumberOfModelsPerMake = (
				SELECT COUNT(*)
				FROM MakeModels
				WHERE MakeModels.MakeID = ref.MakeID
	)
FROM Makes AS ref
WHERE MakeID IN (SELECT DISTINCT MakeID FROM MakeModels)
ORDER BY NumberOfModelsPerMake DESC;
-- !! WHERE is playing the role of 'INNER JOIN', it will keep only the Makes That has manufactured a model (not all makes made a model ?)
-- !! By removing WHERE, you're getting the EQUIVALENT OF USING 'LEFT/RIGHT' JOIN by keeping all makes REGUARDLESS if they did/didn't make any models
-- !! In our current DB 'VehicleMakesDB' ALL Makes have a Model, So actually the [WHERE + SubQuery FILER ] is Useless 
		

-- ============================================================================================
-- !! NOTE : CHECK 'DATA WITH BARAA' CHANNEL IN YOUTUBE,WITH VIDEO TITLE 'JONS'/'SubQueries' !!
-- ============================================================================================											  