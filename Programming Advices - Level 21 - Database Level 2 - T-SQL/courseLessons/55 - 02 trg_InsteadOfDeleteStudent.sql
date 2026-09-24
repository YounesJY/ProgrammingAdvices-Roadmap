--- -------------------------------------------
--- -------------------------------------------
CREATE TRIGGER trg_InsteadOfDeleteStudent ON Students
INSTEAD OF DELETE
AS
BEGIN
  
  -- Marking the record as inactive instead of deleting 
    UPDATE Students
	SET IsActive = 0 -- [SOFT DELETE]  instead of [HARD DELETE]
    FROM Students S
    INNER JOIN deleted D 
		ON S.StudentID = D.StudentID;

END;
--- -------------------------------------------
--- -------------------------------------------