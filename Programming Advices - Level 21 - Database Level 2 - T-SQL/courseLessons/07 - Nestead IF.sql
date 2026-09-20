Declare  @score int = 95;
-- set @score = 75;

IF @score >= 90
		PRINT 'Grade A'
ELSE
	BEGIN
		IF @score >= 80
				PRINT 'Grade B'
		ELSE
				PRINT 'Grade C or lower'
	END