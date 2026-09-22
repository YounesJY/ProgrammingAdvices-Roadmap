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

	CREATE TABLE People (
		PersonID INT PRIMARY KEY,
		FirstName VARCHAR(50),
		LastName VARCHAR(50),
		Email VARCHAR(75),
    );
    SET @NewPersonID = SCOPE_IDENTITY();
END
--- -------------------------------------------
--- -------------------------------------------