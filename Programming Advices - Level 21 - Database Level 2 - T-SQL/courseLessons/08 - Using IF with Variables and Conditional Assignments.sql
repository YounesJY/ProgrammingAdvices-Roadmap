-- Using IF with Variables
-- Variables can be used within an IF statement for dynamic conditions.

DECLARE @age INT = 25;
-- SET @age = 25;

IF @age >= 18
    PRINT 'Adult'
ELSE
    PRINT 'Minor'

-- Conditional Assignment
-- IF statements are often used for conditional assignment to variables.
DECLARE @max INT;
Declare @a int = 10, @b int = 20;
-- set @a = 20;
-- set @b = 10;

IF @a > @b
    SET @max = @a
ELSE
    SET @max = @b

Print @max;