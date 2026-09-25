use C21_DB1;

--- -------------------------------------------
--- -------------------------------------------
DECLARE @StudentID int, @Name nvarchar(50), @Grade int;
DECLARE scroll_cursor CURSOR STATIC SCROLL FOR
SELECT StudentID, Name, Grade FROM dbo.Students;


OPEN scroll_cursor;

FETCH NEXT FROM scroll_cursor
INTO @StudentID, @Name, @Grade;

WHILE @@FETCH_STATUS = 0
BEGIN
    PRINT 'Student Name: ' + @Name + ', Grade: ' + CAST(@Grade AS NVARCHAR(10));

    -- Fetch the previous row of data from the cursor (moving backward).
    -- This demonstrates the ability of the scrollable cursor to move in reverse.
	/*
		FETCH PRIOR FROM scroll_cursor 
		INTO @StudentID, @Name, @Grade;
	*/

    -- Fetch the next row of data from the cursor (moving forward).
    -- This is to demonstrate that the cursor can move back to the next row after moving backward.
    FETCH NEXT FROM scroll_cursor 
	INTO @StudentID, @Name, @Grade;
END

CLOSE scroll_cursor;
DEALLOCATE scroll_cursor; -- This step is important to free up resources used by the cursor.
--- -------------------------------------------
--- -------------------------------------------