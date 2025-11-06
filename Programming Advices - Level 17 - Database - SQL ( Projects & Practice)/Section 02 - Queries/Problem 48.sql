-- Get the highest Manufacturers manufactured the highest number of models , 
-- Remember that they could be more than one manufacturer have the same high number of models


-- SOLUTION 1 : USING 'Nested SubQueries' [More Advanced + Step-By-Step Logic-Building + Easy To Debug]
-- ! STEP 2 : Get the Manufacturers that manufactured a number of models Equivalent to the value from the SubQuery
SELECT
	Makes.Make,
	NumberOfModelsPerMake = COUNT(*)
FROM Makes
INNER JOIN MakeModels
	ON Makes.MakeID = MakeModels.MakeID
GROUP BY Makes.Make
HAVING COUNT(*) = (
	-- ! STEP 1 : Get the highest number of models manufactured
	SELECT TOP 1	
		NumberOfModelsPerMake = COUNT(*)
	FROM Makes
	INNER JOIN MakeModels
		ON Makes.MakeID = MakeModels.MakeID
	GROUP BY Makes.Make
	ORDER BY NumberOfModelsPerMake DESC
);


-- =================================================== !! VERY IMPORTANT NOTE !! ================================================
-- !! Redundancy & Repetition, Redundancy & Repetition, Redundancy & Repetition, Redundancy & Repetition, Redundancy & Repetition
-- !! We Literally wrote the same Query 2 times to get the final Result <--> We wrote the same LOGIC twice
-- !! [1st time to 'Get the highest number of models manufactured']
-- !! [2nd time to 'Get the Manufacturers that manufactured a number of models Equivalent to the value from the SubQuery']
-- !! it's the SAME LOGIC/QUERY <--> WE REACH/HIT THE LIMITS OF USING [SubQueries] by repeating the same Logic/Query
-- !! THE SOLUTION --> move to the next stage and usea [CTE 'Common Table Expression']
-- ==============================================================================================================================


-- SOLUTION 2 : USING 'CTE 'Common Table Expression'' [More Advanced + Step-By-Step Logic-Building + Easy To Debug]
-- ! STEP 1 : Create ModelCounts table 1 time then use it twice in different locations (like a variable or view) --> [DRY 'DONT REPEAT YOURSELF']
With ModelCounts AS (
	SELECT 
		Makes.Make,
		NumberOfModels = COUNT(*)
	FROM Makes 
	INNER JOIN  MakeModels
		ON Makes.MakeID = MakeModels.MakeID
	GROUP BY Makes.Make
)

-- ! STEP 2 : Use the table 2 times for different goals
SELECT 
	Make,
	NumberOfModels 
FROM ModelCounts
WHERE NumberOfModels = (SELECT MAX(NumberOfModels) FROM ModelCounts);

-- !! [1st time to 'Get the highest number of models manufactured'] --> In 'SubQuery' with MAX(NumberOfModels)
-- !! [2nd time to 'Get the Manufacturers that manufactured a number of models Equivalent to the value from the SubQuery'] --> In 'MainQuery'


-- ==================================================================================================
-- !! NOTE : CHECK 'DATA WITH BARAA' CHANNEL IN YOUTUBE,WITH VIDEO TITLE 'JONS'/'SubQueries'/'CTE' !!
-- ==================================================================================================