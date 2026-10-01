use C21_DB1;

--- -------------------------------------------
--- -------------------------------------------
WITH DuplicateEmails AS (
    SELECT 
        Email, 
        DuplicateEmail = COUNT(*)
    FROM Contacts
    GROUP BY Email
    HAVING COUNT(*) > 1
)
/*
	SELECT 
		c.ContactID,
		c.Name,
		c.Email
	FROM Contacts c
	INNER JOIN DuplicateEmails de 
		ON c.Email = de.Email;
*/


SELECT 
	ContactID,
	Name,
	Email
FROM Contacts
WHERE Email IN (SELECT email from DuplicateEmails); 

--- -------------------------------------------
--- -------------------------------------------