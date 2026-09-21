## THROW Statement

---

    The `THROW` statement in T-SQL (Transact-SQL) is<mark> used to generate an error and send it back to the calling application</mark>. It's <mark><u>particularly useful for handling errors in stored procedures, triggers, or batches</u></mark>. Here's a basic lesson on how to use `THROW`, along with an example.

---

### **Understanding** `THROW` **Statement**

1. Purpose: Used to raise an exception and transfer control to a CATCH block of a TRY...CATCH construct in your SQL code.
2. Syntax:

```sql
THROW [error_number, message, state];
```

- error_number: A constant or variable <mark>between 50000 and 2147483647</mark>.
- message: The error message text. It should be a string less than 2048 characters.
- state: A constant or <mark>variable between 0 and 255</mark>.

**<mark>Key Points</mark>**:**

- - If you don’t specify these arguments, <mark><u>**the current error is passed on**</u></mark>.
  - You cannot use `THROW` to throw an error that is caught by a CATCH block outside of the current scope: This means that if an error is raised using `THROW` within a `TRY` block, <mark>**it must be caught by the corresponding `CATCH`**</mark> block <mark><u>**that is in the same scope as the `TRY` block**</u></mark>. <mark><u>If there's a nested `TRY...CATCH` (one inside another)</u></mark>, an error thrown inside the inner `TRY` block <mark><u>**cannot be caught by the `CATCH` block of the outer `TRY...CATCH` structure**</u></mark>. It has to be caught within the same level of nesting.

### **Example: Updating Product Inventory**

#### **Scenario:**

We have a this code that updates the stock quantity of a product in the inventory. The procedure should raise an error if the new stock quantity is negative, as this would not be a valid scenario in most inventory systems.

#### **Stored Procedure with** `THROW`**:**

```plsql
  declare @NewStockQty INT;
    set @NewStockQty=-5;
    -- Start a TRY block
    BEGIN TRY
        -- Check if NewStockQty is negative
        IF @NewStockQty < 0
            THROW 51000, 'Stock quantity cannot be negative.', 1;
        -- Proceed with updating stock (example code)
        UPDATE Products SET StockQuantity = @NewStockQty WHERE ProductID = 1;
    END TRY
    -- Start a CATCH block to handle the error
    BEGIN CATCH
        SELECT 
            ERROR_NUMBER() AS ErrorNumber,
            ERROR_MESSAGE() AS ErrorMessage;
    END CATCH
```

#### Explanation:

- In this code we first enter a TRY block.
- We then check if `@NewStockQty` is negative. If it is, we use the `THROW` statement to raise an error. The error number is 51000, and we provide a custom error message stating that the stock quantity cannot be negative. The state is set to 1.
- If the stock quantity is valid, the procedure updates the `Products` table with the new stock quantity.
- If an error is thrown, control passes to the CATCH block, which captures and returns the error information.

This example demonstrates how `THROW` can be effectively used to ensure data integrity and prevent invalid operations in database procedures. It's particularly useful in scenarios where business rules dictate specific constraints that are not directly enforced by the database schema itself.

Remember, proper error handling in SQL is crucial for writing robust and reliable applications. The `THROW` statement is a powerful tool for this purpose, especially in combination with the TRY...CATCH construct.

---
