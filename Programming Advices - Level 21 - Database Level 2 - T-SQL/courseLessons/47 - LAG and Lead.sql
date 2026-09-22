use C21_DB1;

--- -------------------------------------------
--- -------------------------------------------
SELECT  * 
FROM Students
ORDER BY Grade DESC;


SELECT 
    StudentID, 
    Name, 
	PreviousGrade = LAG(Grade, 1) OVER (ORDER BY Grade DESC),
    Grade,
    NextGrade = Lead(Grade, 1) OVER (ORDER BY Grade DESC)
FROM Students
ORDER BY Grade DESC;

--- -------------------------------------------
--- -------------------------------------------