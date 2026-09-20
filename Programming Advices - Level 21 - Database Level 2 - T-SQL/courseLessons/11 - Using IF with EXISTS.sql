use C21_DB1;

IF NOT EXISTS(SELECT * FROM Employees WHERE Name = 'John Smith')
	PRINT 'No, John Smith is not there.'
ELSE
	PRINT 'Yes, John Smith is there.'