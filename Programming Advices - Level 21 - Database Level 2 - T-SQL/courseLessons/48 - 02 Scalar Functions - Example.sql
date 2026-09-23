use C21_DB1;
--- -------------------------------------------
--- -------------------------------------------
-- Using User-Defined Funtion
SELECT 
	*,
	AveradgeGrade = dbo.GetAverageGrade(Subject)
FROM Teachers;


-- Using a Correlated Sub-Query [Wait, is every Correlated Sub-Query can be turned into a User-Defined funtion ?]
SELECT 
	*,
	AveradgeGrade = (
		SELECT AVG(Grade)
		FROM Students
		WHERE Subject = Teachers.Subject
	)
FROM Teachers;


-- Using Joins
SELECT 
	Teachers.*,
	Students.AveradgeGrade
FROM Teachers LEFT OUTER JOIN (
	SELECT 
		Subject,
		AveradgeGrade = AVG(Grade)
	FROM Students
	GROUP BY Subject
) AS Students
	ON Teachers.Subject = Students.Subject;
--- -------------------------------------------
--- -------------------------------------------