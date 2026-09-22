## Example Performing a Bank Transfer

---

#### **Setting Up the Database**

First, create two tables: `Accounts` and `Transactions`.

```plsql
-- Create Accounts Table
CREATE TABLE Accounts (
    AccountID INT PRIMARY KEY,
    Balance DECIMAL(10, 2)
);
-- Create Transactions Table
CREATE TABLE Transactions (
    TransactionID INT PRIMARY KEY IDENTITY(1,1),
    FromAccount INT,
    ToAccount INT,
    Amount DECIMAL(10, 2),
    Date DATETIME
);
-- Insert Sample Data into Accounts
INSERT INTO Accounts (AccountID, Balance) VALUES (1, 500.00); -- Account 1
INSERT INTO Accounts (AccountID, Balance) VALUES (2, 300.00); -- Account 2
```

---

#### **Example: Performing a Bank Transfer**

We'll transfer $100 from Account 1 to Account 2 and record this transaction.

```plsql
BEGIN TRANSACTION;
BEGIN TRY
    -- Subtract $100 from Account 1
    UPDATE Accounts SET Balance = Balance - 100 WHERE AccountID = 1;
    -- Add $100 to Account 2
    UPDATE Accounts SET Balance = Balance + 100 WHERE AccountID = 2;
    -- Log the transaction
    INSERT INTO Transactions (FromAccount, ToAccount, Amount, Date) VALUES (1, 2, 100, GETDATE());
    -- Commit the transaction
    COMMIT;
END TRY
BEGIN CATCH
    -- Rollback in case of error
    ROLLBACK;
    -- Error handling code here
END CATCH;
```

In this script:

- The `BEGIN TRANSACTION` starts the transaction.
- `BEGIN TRY...END TRY` handles successful execution.
- `BEGIN CATCH...END CATCH` handles any errors, rolling back the transaction if necessary.
- `COMMIT` confirms the transaction; `ROLLBACK` undoes it in case of errors.

---
