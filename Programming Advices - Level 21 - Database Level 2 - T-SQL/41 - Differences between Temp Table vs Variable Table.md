## Differences between Temp Table vs Variable Table

---

    In T-SQL, which is the SQL server's extension for SQL, two common ways to store data temporarily are through temporary tables and table variables. Here's a lesson that outlines the differences between them:

- Definition and Scope:
  - Temporary Tables: Created using the `CREATE TABLE` statement, with the table name prefixed by `#` for local temporary tables (visible only in the current session) or `##` for global temporary tables (visible to all sessions). They are stored in the tempdb database.
  - Table Variables: Declared using the `DECLARE` statement and have a similar structure to permanent tables. The syntax is `DECLARE @TableName TABLE (column definitions)`. They have a limited scope and are typically used within the function, stored procedure, or batch in which they are declared.
- Lifetime:
  - Temporary Tables: <mark>Exist until they are explicitly dropped using</mark> the `DROP TABLE` command <mark>or until the session/connection that created them is closed</mark>.
  - Table Variables: <mark>Automatically cleaned up at the end of the batch, function, or stored procedure in which they are defined</mark>.
- Performance and Usage:
  - Temporary Tables: <mark>Suitable **<u>for larger datasets and complex operations</u>**</mark>, like joining with other tables. <mark>They **<u>support indexes, statistics</u>**, and can result in better query performance for large data sets</mark>.
  - Table Variables: <mark>Better **<u>for smaller datasets and simpler operations</u>**</mark>. They have lower overhead but <mark>**<u>lack some of the optimizations</u>** available to temporary tables</mark>, like <u>**precompiled execution plans and statistics**</u>.
- Transaction Logs:
  - Temporary Tables: <mark>**<u>Fully logged in the transaction log</u>**</mark>, which **<u>can impact performance for large data manipulation operations</u>**.
  - Table Variables: <mark><u>**Have minimal logging and do not participate in transactions**</u></mark>. This means that if a transaction is rolled back, <mark>changes made to a table variable within that transaction <u>**are not rolled back**</u></mark>.
- Use Cases:
  - Temporary Tables: Ideal <mark>for complex operations</mark>, <mark>**<u>temporary storage of data that requires rollback capabilities</u>**</mark>, and <mark>when **<u>working with a large number of rows</u>**</mark>.
  - Table Variables: <mark>Useful for quick, temporary storage of **<u>a small amount of data</u>**</mark> that **<u>*does not require transactional rollbacks or heavy-duty operations*</u>**.

---

Understanding when to use temporary tables versus table variables is crucial for optimizing performance and resource utilization in SQL Server databases.

---
