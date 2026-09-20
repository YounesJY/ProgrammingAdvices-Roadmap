DECLARE @Age INT = 25;
DECLARE @Salary DECIMAL(10,2) = 50000;

IF (@Age > 18 AND @Salary >= 50000)
		PRINT 'Eligible for the loan';
ELSE
		PRINT 'Not eligible for the loan';

--------------------------
--------------------------
DECLARE @Grade CHAR(1) = 'B';
DECLARE @AttendancePercentage INT = 75;

IF @Grade = 'A' OR @AttendancePercentage > 70
		PRINT 'Qualified for extra-curricular activities';
ELSE
		PRINT 'Not qualified for extra-curricular activities';
--------------------------
--------------------------
DECLARE @CustomerStatus NVARCHAR(10) = 'Inactive';

IF NOT (@CustomerStatus = 'Active')
		PRINT 'Send re-engagement email';
ELSE
		PRINT 'Customer is active';
--------------------------
--------------------------