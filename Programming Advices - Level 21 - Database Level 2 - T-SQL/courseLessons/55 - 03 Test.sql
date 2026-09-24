USE C21_DB1;

--- -------------------------------------------
--- -------------------------------------------
-- Assuming there is a student with StudentID = 4
SELECT * 
FROM Students;

-- Attempting to delete a student
DELETE FROM Students
WHERE StudentID = 1;

-- Checking the status of the student record
SELECT * 
FROM Students;
--- -------------------------------------------
--- -------------------------------------------