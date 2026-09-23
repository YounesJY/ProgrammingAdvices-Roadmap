USE C21_DB1;

--- -------------------------------------------
--- -------------------------------------------
-- Example 1: Join Students and Teachers based on shared subject
SELECT 
	Students.StudentID,
	StudentName = Students.Name,
	TeacherName = Teachers.Name,
	Students.Grade
FROM dbo.GetStudentsBySubject('Math') Students
JOIN Teachers  
	ON Students.Subject = Teachers.Subject
--- -------------------------------------------
--- -------------------------------------------