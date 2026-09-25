use C21_DB1;
--- -------------------------------------------
--- -------------------------------------------
DECLARE @StudentID int, @Name nvarchar(50), @Grade int;
DECLARE static_cursor CURSOR STATIC FOR
	SELECT StudentID, Name, Grade 
	FROM dbo.Students;


OPEN static_cursor;

FETCH NEXT FROM static_cursor 
INTO @StudentID, @Name, @Grade;

-- Enter a loop that will continue as long as the previous fetch was successful.
-- @@FETCH_STATUS returns 0 if the fetch was successful.
WHILE @@FETCH_STATUS = 0
BEGIN
    PRINT 'Student Name: ' + @Name + ', Grade: ' + CAST(@Grade AS NVARCHAR(10));
    
	FETCH NEXT FROM static_cursor 
	INTO @StudentID, @Name, @Grade;
END


CLOSE static_cursor;
DEALLOCATE static_cursor; -- This step is important to free up resources used by the cursor.
--- -------------------------------------------
--- -------------------------------------------