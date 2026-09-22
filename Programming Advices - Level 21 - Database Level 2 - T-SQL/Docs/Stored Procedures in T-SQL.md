## Stored Procedures in T-SQL

---

#### **Introduction to Stored Procedures**

    Stored procedures in T-SQL are a powerful feature of SQL Server. They <mark>allow you to encapsulate SQL code, which can be executed repeatedly</mark>. Stored procedures are beneficial for several reasons:

- <mark><u>**Performance**</u></mark>: <u>**They are compiled and stored in the database**</u>, <u>**leading to faster execution times**</u>.
- <mark>**Security**</mark>: They provide an additional layer of security by restricting direct access to the data.
- <mark>**<u>Maintainability</u>**</mark>: <mark>Centralizing business logic</mark> in stored procedures <u>**makes changes easier and more consistent**</u>.  

---

#### **What can you write inside Stored Procedure**

    In T-SQL, which is the SQL language variant used by Microsoft SQL Server, <mark><u>**stored procedures can contain a wide range of SQL statements, control structures, and special features**</u></mark>. Here's a detailed list of what you can write inside stored procedures in T-SQL:

- **<u>SQL Queries and DML Statements</u>**: This includes `SELECT`, `INSERT`, `UPDATE`, `DELETE`, and `MERGE` statements for data querying and manipulation.
- <u>**Variable Declarations and Assignments**</u>: You can declare local variables using the `DECLARE` statement and set values with the `SET` or `SELECT` statements.
- <u>**Control Flow Statements**</u>:
  - `IF...ELSE`: For conditional logic.
  - `WHILE`: For looping.
  - `BEGIN...END`: To define blocks of code.
  - `WAITFOR`: To delay execution.
  - `GOTO`: For jumping to a labeled point in the procedure (though generally discouraged due to readability concerns).
- <u>**Error Handling and <mark>Transactions</mark>**</u>:
  - `TRY...CATCH`: For catching and handling exceptions.
  - `TRANSACTION` Management: Using `BEGIN TRANSACTION`, `COMMIT`, and `ROLLBACK` to handle transactions.
- <mark>Dynamic SQL Execution</mark>: Using `EXEC` or `sp_executesql` to execute dynamically built SQL strings.
- <mark><u>**Calling Other Stored Procedures and Functions**</u></mark>: You can call other stored procedures or user-defined functions within a stored procedure.
- <mark><u>**Temporary Tables and Table Variables**</u></mark>: You can create and use temporary tables and table variables for intermediate data storage and manipulation.
- <mark><u>**Cursor Management**</u></mark>: Although generally less efficient than set-based operations, cursors for row-by-row processing are supported in T-SQL.
- <mark><u>**System Stored Procedures and Functions Calls**</u></mark>: T-SQL allows calling system stored procedures and functions for various tasks.
- <mark>Output Parameters</mark>: Stored procedures can have output parameters to return data back to the caller.
- <mark>RAISERROR or THROW</mark>: For generating custom error messages.
- Use of Table-Valued Parameters: Allows passing tables as parameters to stored procedures.
- <mark>***Common Table Expressions (CTEs)***</mark>: These can be defined within stored procedures for recursive queries or organizing complex queries.
- **<u>Use of DDL Statements</u>**: Such as `CREATE`, `ALTER`, or `DROP`, typically for temporary objects or within dynamic SQL.
- <u>**XML Handling**</u>: T-SQL supports XML data manipulation and querying.
- <u>**Text and Image Manipulation**</u>: Though older and less recommended, T-SQL supports manipulation of text and image data types.

---

It's important to use best practices while writing stored procedures in T-SQL, such as avoiding unnecessary cursors, ensuring proper error handling, and preventing SQL injection when using dynamic SQL. The capabilities and syntax may evolve with different versions of SQL Server, so always refer to the specific version's documentation for the most accurate information.

---

### NAMING

### The Official Guidance

Microsoft's documentation directly states: **"We recommend that you do not create any stored procedures using sp_ as a prefix."**[-4](https://learn.microsoft.com/zh-cn/previous-versions/sql/sql-server-2008/ms190669\(v=sql.100\)?redirectedfrom=MSDN#1)[-17](https://learn.microsoft.com/en-au/previous-versions/sql/sql-server-2005/ms190669\(v=sql.90\)#1). This is also enforced as a code analysis rule (SR0016) in Visual Studio and SQL Server Data Tools[-1](https://learn.microsoft.com/ko-kr/previous-versions/visualstudio/visual-studio-2010/dd172115\(v=vs.100\))[-2](https://learn.microsoft.com/zh-cn/previous-versions/visualstudio/visual-studio-2010/dd172115%28v%3dvs.100%29)[-3](https://learn.microsoft.com/cs-cz/previous-versions/visualstudio/visual-studio-2010/dd172115\(v=vs.100\)#1).

### ⚠️ Why It's Discouraged

The `sp_` prefix is reserved by SQL Server to designate **system stored procedures**[-1](https://learn.microsoft.com/ko-kr/previous-versions/visualstudio/visual-studio-2010/dd172115\(v=vs.100\))[-2](https://learn.microsoft.com/zh-cn/previous-versions/visualstudio/visual-studio-2010/dd172115%28v%3dvs.100%29). Using it for your own procedures creates two main problems:

- **Naming Conflicts**: Your procedure name could clash with a future system procedure. If that happens, calls to your procedure (without a schema prefix like `dbo.`) will bind to the system procedure instead, potentially breaking your application[-1](https://learn.microsoft.com/ko-kr/previous-versions/visualstudio/visual-studio-2010/dd172115\(v=vs.100\))[-4](https://learn.microsoft.com/zh-cn/previous-versions/sql/sql-server-2008/ms190669\(v=sql.100\)?redirectedfrom=MSDN#1).

- **Performance Overhead**: When SQL Server sees a procedure starting with `sp_`, it first searches for it in the **master** database. This extra lookup can cause a performance hit, especially for frequently called procedures[-6](https://learn.microsoft.com/bg-bg/archive/blogs/jenss/a-long-but-not-missed-friend-revisited-prefixing-stored-procedures-with-sp_#1)[-8](https://www.ssw.com.au/rules/avoid-starting-user-stored-procedures-with-system-prefix-sp_-or-dt_)[-16](https://docs.sqlenlight.com/sa0015/?t=1784442888318).

### ✅ What to Use Instead

Microsoft's recommended fix is to **use a different prefix or no prefix at all**[-1](https://learn.microsoft.com/ko-kr/previous-versions/visualstudio/visual-studio-2010/dd172115\(v=vs.100\))[-2](https://learn.microsoft.com/zh-cn/previous-versions/visualstudio/visual-studio-2010/dd172115%28v%3dvs.100%29). Common alternatives include:

- **`usp_`** (User Stored Procedure)[-1](https://learn.microsoft.com/ko-kr/previous-versions/visualstudio/visual-studio-2010/dd172115\(v=vs.100\))[-2](https://learn.microsoft.com/zh-cn/previous-versions/visualstudio/visual-studio-2010/dd172115%28v%3dvs.100%29)

- **No prefix** (e.g., `AddNewPerson`)

- **A meaningful module prefix** (e.g., `STU_` for student-related procedures)

---
