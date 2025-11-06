-- Get the highest number of models manufactured

-- SOLUTION 1 : USING 'JOINS' + TOP [Simple logic + StrightForward]
SELECT TOP 1	
	Makes.Make,
	NumberOfModelsPerMake = COUNT(*)
FROM Makes
INNER JOIN MakeModels
	ON Makes.MakeID = MakeModels.MakeID
GROUP BY Makes.Make
ORDER BY NumberOfModelsPerMake DESC;


-- SOLUTION 1 : USING 'SubQuery' [Simple logic + StrightForward]
SELECT
	HighestNumberOfManufacturedModels = MAX(NumberOfModelsPerMake)
FROM (
	SELECT
		Makes.Make,
		NumberOfModelsPerMake = COUNT(*)
	FROM Makes
	INNER JOIN MakeModels
		ON Makes.MakeID = MakeModels.MakeID
	GROUP BY Makes.Make
) AS Temp;


-- ============================================================================================
-- !! NOTE : CHECK 'DATA WITH BARAA' CHANNEL IN YOUTUBE,WITH VIDEO TITLE 'JONS'/'SubQueries'/ !!
-- ============================================================================================