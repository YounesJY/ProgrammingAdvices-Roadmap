use C21_DB1;

--- -------------------------------------------
--- -------------------------------------------
-- Checking the log table
SELECT * 
FROM Students 

SELECT *
FROM StudentUpdateLog;


-- Updating the grade of the student
UPDATE Students
SET Grade = 84
WHERE StudentID = 1;

-- Checking the log table
SELECT *
FROM StudentUpdateLog;
--- -------------------------------------------
--- -------------------------------------------