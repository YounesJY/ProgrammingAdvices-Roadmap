-- Get All Employees managed by 'Mohammed'


-- SOLUTION 1 : USING 'Self-JOINS' [More Advanced]
-- 'Self-JOINS' -> [the practice of joining a table with itself]
SELECT 
	Employees.*,
	Manager = Managers.Name
FROM Employees 
INNER JOIN Employees AS Managers
	ON Employees.ManagerID = Managers.EmployeeID
WHERE Managers.Name = N'Mohammed'


-- SOLUTION 2 : USING 'Correlated SubQuery' [More Advanced + Iteration-based --> For Each Record]
-- For each returned record (Employee), we're searching the ManagerName for this Employee on the 2nd table (copy) (on the fly), then move to the next record
-- =================================================== !! VERY IMPORTANT NOTE !! ===================================================================
-- !! The 'Correlated SubQuery' CAN'T be executed as a STANDALONE Query, BECAUSE it relays on an outer reference (ref) from the MAIN/OUTER Query  !!
-- !! If you got this error "The multi-part identifier "ref.ManagerID" could not be bound." then EVERYTHING IS GOOD ''\__(^_^)__/''				  !!
-- =================================================================================================================================================
SELECT 
	*,
	Manager = (SELECT Name FROM Employees WHERE Employees.EmployeeID = ref.ManagerID)
FROM Employees ref
WHERE ManagerID IS NOT NULL AND ManagerID = (SELECT EmployeeID FROM Employees WHERE Name = N'Mohammed');


-- ============================================================================================
-- !! NOTE : CHECK 'DATA WITH BARAA' CHANNEL IN YOUTUBE,WITH VIDEO TITLE 'JONS'/'SubQueries' !!
-- !! NOTE : SEARCH ABOUT 'SELF-JOIN' - THIS EXAMLPE ABOVE IS 'VERY COMMON' !!
-- ============================================================================================