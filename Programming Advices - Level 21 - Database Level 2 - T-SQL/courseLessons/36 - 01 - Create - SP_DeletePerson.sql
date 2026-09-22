--- -------------------------------------------
--- -------------------------------------------
CREATE PROCEDURE usp_DeletePerson
    @PersonID INT
AS
BEGIN
    DELETE FROM People
	WHERE PersonID = @PersonID
END
--- -------------------------------------------
--- -------------------------------------------