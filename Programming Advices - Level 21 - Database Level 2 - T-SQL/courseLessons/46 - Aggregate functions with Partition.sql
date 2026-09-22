use C21_DB1;

--- -------------------------------------------
--- -------------------------------------------
-- You can apply aggregate functions over a window of rows specified by the PARTITION BY clause.
SELECT 
	*,
	GradeRankInSubject  = DENSE_RANK() OVER (PARTITION BY Subject ORDER BY Grade DESC),
	SubjectAvgGrade		= AVG(Grade)   OVER (PARTITION BY Subject),
	SubjectTotalGrade	= SUM(Grade)   OVER (PARTITION BY Subject)
FROM Students
ORDER BY Subject;
--- -------------------------------------------