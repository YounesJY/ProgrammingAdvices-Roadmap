use C21_DB1;

--- -------------------------------------------
--- -------------------------------------------

WITH Numbers AS (
    SELECT 1 AS Number
	
	UNION ALL
    
	SELECT Number + 1 
	FROM Numbers 
	WHERE Number < 102
)

SELECT * 
FROM Numbers;
--- -------------------------------------------
--- -------------------------------------------