## @@ROWCOUNT

---

    `@@ROWCOUNT` is a system function in T-SQL (Transact-SQL used in Microsoft SQL Server) <mark>that returns the number of rows affected **<u>by the last statement executed</u>**</mark>. This function is <mark><u>**commonly used to determine how many rows were impacted by the previous operation**</u></mark>, such as an INSERT, UPDATE, DELETE, or SELECT statement.

---

### Understanding `@@ROWCOUNT`

1. Purpose: To get the number of rows affected by the most recently executed statement in your SQL script or batch.

2. Usage:
- - <mark>**<u>It must be checked immediately after the statement whose impact you want to measure</u>**</mark>, because any subsequent statement, including something as simple as a `PRINT` statement, will reset `@@ROWCOUNT` to the number of rows affected by that subsequent statement.
  - `@@ROWCOUNT` is often used to verify the success of a statement or to take conditional action depending on the number of rows affected.
1. Syntax:

```plsql
SELECT @@ROWCOUNT
```

---

### Example Usage

Consider a scenario where you update records in a table and want to check how many rows were updated.

```plsql
UPDATE Employees SET DepartmentID = 3 WHERE DepartmentID =4;
SELECT @@ROWCOUNT AS RowsAffected;
```

In this example:

- Immediately after, `@@ROWCOUNT` is used to return the number of rows that were updated by the `UPDATE` statement.

---

### <mark><u>**Practical Considerations**</u></mark>

- <mark>Immediate Check</mark>: Always check `@@ROWCOUNT` immediately after the relevant SQL statement, as its value is reset after each statement.
- <mark>Use in Conditional Logic</mark>: It's often used in conditional logic, such as in IF statements, to take different actions depending on the number of rows affected by a previous operation.
- <mark>Zero Rows Affected</mark>: If no rows are affected by the previous operation, `@@ROWCOUNT` returns 0. This can be useful to check whether a conditional update or delete actually changed any data.
- <mark>**<u>Compatibility</u>**</mark>: `@@ROWCOUNT` is widely supported and <mark><u>**is a standard part of T-SQL**</u></mark>, making it compatible with various versions of SQL Server.

---

### Conclusion

`@@ROWCOUNT` is a valuable tool in T-SQL for understanding the impact of SQL statements and controlling the flow of scripts based on how many rows are affected by certain operations. It's especially useful in data manipulation scenarios and in ensuring the effectiveness of SQL commands.

---
