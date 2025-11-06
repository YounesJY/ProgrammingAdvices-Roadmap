-- Write a query to get all Makes with make starts with 'B'.

SELECT *
FROM Makes
WHERE Make LIKE N'B%'; -- Use LIKE operator + N for UNICODE charachters (nvarchar)