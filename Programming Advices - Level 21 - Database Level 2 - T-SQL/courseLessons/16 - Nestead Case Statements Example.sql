use C21_DB1;

--- -------------------------------------------
--- -------------------------------------------
SELECT
    Name,
    Department,
    Salary,
    PerformanceRating,
    Bonus = CASE 
                WHEN Department = 'Sales' THEN
                    CASE 
                        WHEN PerformanceRating > 90 THEN Salary * 0.15
                        WHEN PerformanceRating BETWEEN 75 AND 90 THEN Salary * 0.10
                        ELSE Salary * 0.05
                    END
                WHEN Department = 'HR' THEN
                    CASE 
                        WHEN PerformanceRating > 90 THEN Salary * 0.10
                        WHEN PerformanceRating BETWEEN 75 AND 90 THEN Salary * 0.08
                        ELSE Salary * 0.04
                    END
                ELSE
                    CASE 
                        WHEN PerformanceRating > 90 THEN Salary * 0.08
                        WHEN PerformanceRating BETWEEN 75 AND 90 THEN Salary * 0.06
                        ELSE Salary * 0.03
                    END
            END
FROM Employees2;
--- -------------------------------------------
--- -------------------------------------------
SELECT
    Name,
    Department,
    Salary,
    PerformanceRating,
    Bonus = CASE
                WHEN PerformanceRating > 90 THEN
                    CASE
                        WHEN Department = 'Sales' THEN Salary * 0.15
                        WHEN Department = 'HR'    THEN Salary * 0.10
                        ELSE Salary * 0.08
                    END

                WHEN PerformanceRating BETWEEN 75 AND 90 THEN
                    CASE
                        WHEN Department = 'Sales' THEN Salary * 0.10
                        WHEN Department = 'HR'    THEN Salary * 0.08
                        ELSE Salary * 0.06
                    END

                ELSE
                    CASE
                        WHEN Department = 'Sales' THEN Salary * 0.05
                        WHEN Department = 'HR'    THEN Salary * 0.04
                        ELSE Salary * 0.03
                    END
            END
FROM Employees2;
--- -------------------------------------------
--- -------------------------------------------