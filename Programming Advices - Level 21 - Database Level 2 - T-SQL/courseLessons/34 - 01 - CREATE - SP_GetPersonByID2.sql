---	This the 2st approach to return a record from a SP using OUTPUT parameters.


--- -------------------------------------------
--- -------------------------------------------
CREATE PROCEDURE usp_GetPersonByID2
    @PersonID INT,
    @FirstName NVARCHAR(100) OUTPUT,
    @LastName NVARCHAR(100) OUTPUT,
    @Email NVARCHAR(255) OUTPUT,
    @IsFound BIT OUTPUT  -- Additional parameter to indicate if a record was found
AS
BEGIN
    IF EXISTS(SELECT 1 FROM People WHERE PersonID = @PersonID)
    BEGIN
        SELECT 
            @FirstName = FirstName, 
            @LastName = LastName, 
            @Email = Email
        FROM People 
        WHERE PersonID = @PersonID;

        SET @IsFound = 1;  -- Set to 1 (true) if a record is found
    END
    ELSE
        SET @IsFound = 0;  -- Set to 0 (false) if no record is found
END
--- -------------------------------------------
--- -------------------------------------------