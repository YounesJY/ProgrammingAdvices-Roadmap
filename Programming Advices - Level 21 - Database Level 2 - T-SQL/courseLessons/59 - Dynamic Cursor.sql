use C21_DB1;

--- -------------------------------------------
--- -------------------------------------------
DECLARE @StudentID int, @Name nvarchar(50), @Grade int;
DECLARE dynamic_cursor CURSOR DYNAMIC FOR
	SELECT StudentID, Name, Grade 
	FROM dbo.Students;


OPEN dynamic_cursor;

FETCH NEXT FROM dynamic_cursor 
INTO @StudentID, @Name, @Grade;

-- Enter a loop that will continue as long as the previous fetch was successful.
-- @@FETCH_STATUS returns 0 if the fetch was successful.
WHILE @@FETCH_STATUS = 0
BEGIN
    PRINT 'Student Name: ' + @Name + ', Grade: ' + CAST(@Grade AS NVARCHAR(10));
    
	FETCH NEXT FROM dynamic_cursor 
	INTO @StudentID, @Name, @Grade;
END


CLOSE dynamic_cursor;
DEALLOCATE dynamic_cursor; -- This step is important to free up resources used by the cursor.
--- -------------------------------------------
--- -------------------------------------------