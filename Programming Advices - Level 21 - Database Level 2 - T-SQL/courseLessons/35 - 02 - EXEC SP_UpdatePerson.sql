USE C21_DB1;

--- -------------------------------------------
--- -------------------------------------------
EXEC usp_UpdatePerson 
    @PersonID = 1, -- The ID of the person you want to update
    @FirstName = 'Ali',
    @LastName = 'Ahmed',
    @Email = 'Ali@example.com';

SELECT * 
FROM People 
WHERE PersonID=1;
--- -------------------------------------------
--- -------------------------------------------