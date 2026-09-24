--- -------------------------------------------
--- -------------------------------------------
CREATE TRIGGER trg_AfterUpdateStudent ON Students
AFTER UPDATE
AS
BEGIN
  IF UPDATE(Grade)  -- Checks if the 'Grade' column was part of the update
    BEGIN
        INSERT INTO 
			StudentUpdateLog(StudentID, OldGrade, NewGrade)
		SELECT 
			inserted.StudentID,
			OldGrade = deleted.Grade,
			NewGrade = inserted.Grade
        FROM inserted INNER JOIN deleted  
			ON inserted.StudentID = deleted.StudentID;
		/*
			The "inserted" and "deleted" tables: Special tables ,
			where "inserted" contains the new values and "deleted" contains the old values of the updated rows.
		*/
    END
END;
--- -------------------------------------------
--- -------------------------------------------