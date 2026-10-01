use C21_DB1;

--- -------------------------------------------
--- -------------------------------------------
DECLARE @StartDate DATE = '2026-09-01'; -- Start of the date range
DECLARE @EndDate DATE = '2026-09-30';   -- End of the date range

WITH DateSeries AS (
    -- Anchor member: Start with the initial date
    SELECT 
		DateValue = @StartDate

    UNION ALL

    -- Recursive member: Add one day in each iteration
    SELECT 
		DATEADD(day, 1, DateValue)
    FROM DateSeries
    WHERE DateValue < @EndDate
)

SELECT *
FROM DateSeries;
--- -------------------------------------------
--- -------------------------------------------