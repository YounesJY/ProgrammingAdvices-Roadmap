USE C21_DB1;

--- -------------------------------------------
--- -------------------------------------------
--Test
--select * from StudentView

SELECT * 
FROM StudentView;

UPDATE StudentView
SET 
	Name = 'John',
	Course = 'IT',
	Grade = 96
WHERE StudentID = 1;

SELECT * 
FROM PersonalInfo 
WHERE StudentID = 1;

SELECT * 
FROM AcademicInfo 
WHERE StudentID = 1;
--- -------------------------------------------
--- -------------------------------------------