# T-SQL — Variables & Parameters

---

## 1. SET vs. SELECT for Assigning Variables

Both assign values, but they differ in **standard compliance**, **error handling**, and **behavior when no rows match**.

| Aspect                   | SET                            | SELECT                                              |
| ------------------------ | ------------------------------ | --------------------------------------------------- |
| **Standard**             | ANSI standard                  | T-SQL extension                                     |
| **Multiple vars**        | One per statement              | Multiple in one statement (`SELECT @a=1, @b=2`)     |
| **No matching rows**     | Variable stays **unchanged**   | Variable stays **unchanged** (if row not found)     |
| **Query returns >1 row** | **Error**                      | <mark>Silently assigns from **last row**</mark>     |
| **@@ROWCOUNT / @@ERROR** | Resets these before assignment | Preserves them for capture                          |
| **Performance**          | Equal for 1 var                | Faster for 3+ vars (single query vs. multiple SETs) |

> <mark>**Recommendation**: Use **SET** for scalar assignments. It's standard, safer (fails loudly on multi-row queries), and clearer.</mark>

Use **SELECT** when:

- Assigning multiple variables at once
- You need to capture `@@ROWCOUNT` or `@@ERROR` from the previous DML statement immediately

```sql
DECLARE @var1 INT = 99;              -- DECLARE initialization (2008+)
DECLARE @var2 NVARCHAR(255);
SET @var2 = N'string';                -- SET: preferred for scalar
DECLARE @var3 NVARCHAR(20);
SELECT @var3 = lastname FROM HR.Employees WHERE empid = 1;  -- SELECT: single row
```

---

## 2. Variable Scope

<mark>T-SQL variables are **local to the batch** — both in visibility and lifetime.</mark>

- A variable is visible **only within the same batch** where it was declared.
- It is **automatically destroyed** when the batch ends (`GO`).
- You **cannot** declare a variable in one batch and reference it in another.
- <mark>Stored procedure parameters are local to that procedure.</mark>

```sql
DECLARE @x INT = 10;
-- @x usable here
GO
-- @x no longer exists
```

**Key distinction**:

- `DECLARE @PersonID` → T-SQL local variable, <mark>batch-scoped</mark>
- `CREATE PROCEDURE ... @PersonID` → Stored procedure parameter, <mark>procedure-scoped</mark>
- `command.Parameters.Add("@PersonID", ...)` → ADO.NET parameter, client-side object

---

## 3. Special Variables (@@) — System Functions

    <mark>These are **global system variables** (actually system functions)</mark>. Users can read them but **cannot modify** them.

| Variable      | Returns                                                          | Typical Use                                                        |
| ------------- | ---------------------------------------------------------------- | ------------------------------------------------------------------ |
| `@@IDENTITY`  | Last identity value **inserted** <mark>in current session</mark> | Get auto-generated PK after INSERT (⚠️ scope issues with triggers) |
| `@@ROWCOUNT`  | Rows **affected** by the **last statement**                      | Check if UPDATE/DELETE hit anything                                |
| `@@ERROR`     | **Error code** of the last statement (0 = success)               | <mark>**<u>Legacy</u>** error checking</mark>                      |
| `@@TRANCOUNT` | Active transaction count on current connection                   | Check if a transaction is open                                     |

> <mark>**Critical rule**: `@@ERROR` and `@@ROWCOUNT` must be captured **immediately** after the statement you're checking — any subsequent statement resets them.</mark>

```sql
UPDATE Employees SET Salary = Salary * 1.1 WHERE Department = 'Sales';
IF @@ROWCOUNT = 0 PRINT 'No rows updated.';
IF @@ERROR <> 0 PRINT 'Error occurred.';
```

> <mark>**<u>Modern note</u>**: `@@ERROR` <u>is legacy</u>. Prefer `TRY...CATCH` blocks for error handling in modern T-SQL. `@@IDENTITY` <u>has scope bugs with triggers</u> — prefer `SCOPE_IDENTITY()`.</mark>

---

## 4. String Interpolation in PRINT

**No native `$""` interpolation in T-SQL.** You must concatenate with `+`, and cast non-string values explicitly.

```sql
-- Manual concatenation (required)
DECLARE @Name NVARCHAR(50) = N'Younes';
DECLARE @Age INT = 25;
PRINT N'Name: ' + @Name + N', Age: ' + CAST(@Age AS NVARCHAR(10));
```

**Alternative**: `RAISERROR` supports `printf`-style substitution via `%s`, `%d`, etc., but it's designed for **error messages**, not general output:

```sql
RAISERROR(N'Name: %s, Age: %d', 0, 1, @Name, @Age) WITH NOWAIT;
```

The `FORMAT()` function (SQL Server 2012+) can format dates/numbers as strings, but it's for formatting values, not template interpolation.

---

## 5. Casting for Concatenation

When concatenating non-string data with strings, you **must** convert explicitly. SQL Server will not implicitly convert `INT` to `NVARCHAR` in a concatenation context for `PRINT` (it may work in `SELECT`, but not reliably).

| Function        | Syntax                              | Notes                                                  |
| --------------- | ----------------------------------- | ------------------------------------------------------ |
| **CAST**        | `CAST(expr AS type)`                | ANSI standard, simpler                                 |
| **CONVERT**     | `CONVERT(type, expr [, style])`     | T-SQL specific, supports style formatting (esp. dates) |
| **TRY_CAST**    | `TRY_CAST(expr AS type)`            | Returns NULL on failure (2012+)                        |
| **TRY_CONVERT** | `TRY_CONVERT(type, expr [, style])` | Returns NULL on failure (2012+)                        |
| **FORMAT**      | `FORMAT(value, format [, culture])` | Flexible formatting (2012+), slower                    |

```sql
-- Concatenation requires CAST/CONVERT
PRINT N'The price is ' + CAST(price AS VARCHAR(12)) FROM titles;  -- works
PRINT N'The price is ' + price;                                    -- fails (implicit conv. not applied here)
```

**Best practice for concatenation**: `CAST` for simple cases; `CONVERT` when you need date/number styles.

---

## Summary

| Topic                   | Key Point                                                                      |
| ----------------------- | ------------------------------------------------------------------------------ |
| **SET vs SELECT**       | SET for safety/standard; SELECT for multi-var assignment or @@ROWCOUNT capture |
| **Scope**               | Variables live and die within a single batch (`GO` boundary)                   |
| **@@ variables**        | Read-only system globals; `@@ERROR`/`@@ROWCOUNT` reset after next statement    |
| **PRINT interpolation** | Not available; use `+` concatenation with explicit `CAST`                      |
| **Casting**             | `CAST` (standard), `CONVERT` (styles), `TRY_CAST` (safe, 2012+)                |

---

---

### VARIABLES

SET vs Select for assigning vars a value ? why 2 diff approachs ?

AVriables scope (batch, funtions...)

Special Vars @@ and Identity/ RowCount/ Error, what ar these and what's used for ?

string interpolation during PRINT , is it possible like C# $"" style rather than concat + ?

hot to cast data in order to concat or other scinarios ?

- "DECLARE @PersonID" → T-SQL Local Variable
- "CREATE PROCEDURE ... @PersonID" → Stored Procedure Parameter
- "command.Parameters.Add("@PersonID", ...)" → ADO.NET Parameter

---

---
