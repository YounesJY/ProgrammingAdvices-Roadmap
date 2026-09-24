use C21_DB1;

--- -------------------------------------------
--- -------------------------------------------
-- Checking the log table
SELECT * FROM StudentInsertLog;

-- Inserting a new student
INSERT INTO Students (StudentID, Name, Subject, Grade)
VALUES (120, 'ALi Doe', 'Mathematics', 75);

-- Checking the log table
SELECT * FROM StudentInsertLog;
--- -------------------------------------------
--- -------------------------------------------