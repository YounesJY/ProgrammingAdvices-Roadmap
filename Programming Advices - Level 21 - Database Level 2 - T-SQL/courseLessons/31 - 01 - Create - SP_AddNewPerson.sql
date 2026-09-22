--- -------------------------------------------
--- -------------------------------------------
/*
	CREATE TABLE People (
		PersonID INT PRIMARY KEY IDENTITY(1, 1),
		FirstName VARCHAR(50),
		LastName VARCHAR(50),
		Email VARCHAR(75),
    );
*/
--- -------------------------------------------
--- -------------------------------------------
CREATE PROCEDURE SP_AddNewPerson
    @FirstName NVARCHAR(100),
    @LastName NVARCHAR(100),
    @Email NVARCHAR(255),
    @NewPersonID INT OUTPUT
AS
BEGIN
    INSERT INTO People(FirstName, LastName, Email)
    VALUES (@FirstName, @LastName, @Email);

    SET @NewPersonID = SCOPE_IDENTITY();
END
--- -------------------------------------------
--- -------------------------------------------