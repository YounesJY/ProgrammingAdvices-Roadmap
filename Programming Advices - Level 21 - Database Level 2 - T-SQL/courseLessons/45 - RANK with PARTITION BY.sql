use C21_DB1;

--- -------------------------------------------
--- -------------------------------------------
SELECT 
	*, 
    RANK() OVER (ORDER BY Grade DESC) AS GradeRank
FROM 
    Students
ORDER BY Grade DESC;



SELECT 
    *, 
    RANK() OVER (PARTITION BY Subject ORDER BY Grade DESC) AS GradeRankInSubject
FROM 
    Students;
--- -------------------------------------------
--- -------------------------------------------