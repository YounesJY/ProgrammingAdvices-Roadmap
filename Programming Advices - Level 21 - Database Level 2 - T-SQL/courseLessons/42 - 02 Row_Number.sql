use C21_DB1;
--- -------------------------------------------
--- -------------------------------------------
SELECT * 
FROM Students
ORDER BY Grade DESC;

-- Assigns a unique integer to each row within the result set.

SELECT 
	*, ROW_NUMBER() OVER (ORDER BY Grade DESC) AS RowNum
FROM 
    Students
ORDER BY Grade DESC;
--- -------------------------------------------
--- -------------------------------------------