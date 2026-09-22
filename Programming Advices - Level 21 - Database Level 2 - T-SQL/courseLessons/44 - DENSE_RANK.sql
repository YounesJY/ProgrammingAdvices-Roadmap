use C21_DB1;

--- -------------------------------------------
--- -------------------------------------------
--Assigns a rank to each row based on the values in the specified column.
SELECT 
	*,
	ROW_NUMBER() OVER (ORDER BY Grade DESC) AS RowNum,
    RANK()		 OVER (ORDER BY Grade DESC) AS GradeRank,
    DENSE_RANK() OVER (ORDER BY Grade DESC) AS GradeRank
FROM 
    Students;
--- -------------------------------------------
--- -------------------------------------------