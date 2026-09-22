use C21_DB1;

--- -------------------------------------------
--- -------------------------------------------
DECLARE @PersonID INT;

EXEC SP_AddNewPerson 
    @FirstName = 'John', 
    @LastName = 'Doe', 
    @Email = 'john.doe@example.com',
    @NewPersonID = @PersonID OUTPUT;

SELECT @PersonID AS NewPersonID;
--- -------------------------------------------
--- -------------------------------------------
USE [C21_DB1]
GO

DECLARE	@return_value int,
		@NewPersonID int

EXEC	@return_value = [dbo].[SP_AddNewPerson]
		@FirstName = N'Unis',
		@LastName = N'JY',
		@Email = N'HydraMail@eaxmple.com',
		@NewPersonID = @NewPersonID OUTPUT

SELECT	@NewPersonID as N'@NewPersonID'

SELECT	'Return Value' = @return_value

GO
--- -------------------------------------------
--- -------------------------------------------