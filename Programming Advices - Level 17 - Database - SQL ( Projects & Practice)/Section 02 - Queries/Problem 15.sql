-- Write a query to get all Makes with make ends with 'W'.

SELECT *
FROM Makes
WHERE Make LIKE N'%W'; -- Use LIKE operator + N for UNICODE charachters (nvarchar)