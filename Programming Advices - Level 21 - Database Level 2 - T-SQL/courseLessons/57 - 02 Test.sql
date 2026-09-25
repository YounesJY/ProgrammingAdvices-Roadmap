USE C21_DB1;

--- -------------------------------------------
--- -------------------------------------------
SELECT * 
FROM StudentView; 

INSERT INTO 
	StudentView (StudentID, Name, Address, Course, Grade)
VALUES 
	(18, 'abbas', '789 Pine Rd', 'Physics', 50);


SELECT * 
FROM PersonalInfo 
WHERE StudentID = 18;

SELECT * 
FROM AcademicInfo 
WHERE StudentID = 18;

SELECT * 
FROM StudentView;
--- -------------------------------------------
--- -------------------------------------------