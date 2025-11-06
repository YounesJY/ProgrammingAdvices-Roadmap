-- Get all employees that have manager or does not have manager along with Manager's name, incase no manager name show null


-- ===================================================================================================
-- !! NOTE : both solutions are based on previous solutions from problem 51 with very small changes !!
-- ===================================================================================================

-- SOLUTION 1 : USING 'Self-JOINS' [More Advanced]
-- 'Self-JOINS' -> [the practice of joining a table with itself]
SELECT 
	Employees.EmployeeID,
	Employees.Name,
	Manager = Managers.Name
FROM Employees 
LEFT JOIN Employees AS Managers
	ON Employees.ManagerID = Managers.EmployeeID;


-- SOLUTION 2 : USING 'Coorelated SubQuery' [More Advanced + Iteration-based --> For Each Record]
-- For each returned record (Employee), we're searching the ManagerName for this Employee on the 2nd table (copy) (on the fly), then move to the next record
-- =================================================== !! VERY IMPORTANT NOTE !! ===================================================================
-- !! The 'Coorelated SubQuery' CAN'T be executed as a STANDALONE Query, BECAUSE it relays on an outer reference (ref) from the MAIN/OUTER Query  !!
-- !! If you got this error "The multi-part identifier "ref.ManagerID" could not be bound." then EVERYTHING IS GOOD ''\__(^_^)__/''				  !!
-- =================================================================================================================================================
SELECT 
	EmployeeID,
	Name,
	Manager = (SELECT Name FROM Employees WHERE Employees.EmployeeID = ref.ManagerID)
FROM Employees ref;


-- ============================================================================================
-- !! NOTE : CHECK 'DATA WITH BARAA' CHANNEL IN YOUTUBE,WITH VIDEO TITLE 'JONS'/'SubQueries' !!
-- !! NOTE : SEARCH ABOUT 'SELF-JOIN' - THIS EXAMLPE ABOVE IS 'VERY COMMON' !!
-- ============================================================================================