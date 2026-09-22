use C21_DB1;

--- -------------------------------------------
--- -------------------------------------------
SELECT 
	*,
    ROW_NUMBER() OVER (ORDER BY Grade DESC) AS RowNum
FROM 
    Students
ORDER BY Grade DESC;


--Assigns a rank to each row based on the values in the specified column.

SELECT 
	*,
	ROW_NUMBER() OVER (ORDER BY Grade DESC) AS RowNum,
    RANK() OVER (ORDER BY Grade DESC) AS GradeRank
FROM 
    Students
ORDER BY Grade DESC;
	-- Students order by grade desc; -- Is that a typo mistake ?? 
--- -------------------------------------------
--- -------------------------------------------