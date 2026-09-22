/*
	This the 1st approach to return a record from a SP as result set.
	Currently, we're not using an OUTPUT parameter here
*/

--- -------------------------------------------
--- -------------------------------------------
CREATE PROCEDURE usp_GetPersonByID
    @PersonID INT
AS
BEGIN
    SELECT * 
	FROM People 
	WHERE PersonID = @PersonID
END
--- -------------------------------------------
--- -------------------------------------------