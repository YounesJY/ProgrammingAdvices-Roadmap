# SET NOCOUNT ON

## What it does

Tells SQL Server to <mark>stop sending the "`n rows affected`" message back to the client</mark> after every statement.

That's it. One line, one purpose.

---

## Without `SET NOCOUNT ON`

Every `INSERT`, `UPDATE`, `DELETE`, `SELECT` returns a count of affected rows as a "done-in-proc" message on the same connection.

The client sees extra `(N rows affected)` messages interspersed with the actual results.

---

## With `SET NOCOUNT ON`

SQL Server suppresses those messages. The client still gets the actual result set from the `SELECT`, but no extra "(N rows affected)" noise.

---

## Why it matters

<mark>**1. Performance.**</mark>

Each "done-in-proc" message is a small round-trip to the client. In a loop-heavy or multi-statement procedure, those messages add up.

Small per call, but compounding across thousands of calls per second.

**2. Client noise.**

In SSMS or a client app, without `NOCOUNT ON` you'd see:

```
(4 rows affected)

(4 rows affected)
```

Interspersed with the actual results. `SET NOCOUNT ON` cleans up the output.

---

## The correct pattern

<mark>**Use** `SET NOCOUNT ON` at the top **of every stored procedure**</mark>. <mark>**Industry standard**</mark>. <mark>Microsoft's own templates **include it**</mark>.

```sql
ALTER PROCEDURE [dbo].[SP_GetAllStudents]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT Id, Name, Age, Grade
    FROM Students;
END;
```

---

## What it doesn't affect

| Feature                          | Affected?                                    |
| -------------------------------- | -------------------------------------------- |
| `@@ROWCOUNT` inside the SP       | No — still works normally                    |
| `ExecuteScalar()` in ADO.NET     | No — still returns first column of first row |
| `ExecuteNonQuery()` return value | No — still returns rows affected by DML      |
| `SqlDataReader` result set       | No — reads the result set normally           |
| Output parameters                | No                                           |

`SET NOCOUNT ON` is harmless and beneficial. Nothing in the C# code needs to change.

---

## Other SET options in the script

SSMS writes two more `SET` statements before every stored procedure script:

```sql
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
```

These are **connection-level settings**, not part of the SP body. They tell SQL Server how to interpret the script during creation.

| Setting                    | Effect                                                                          |
| -------------------------- | ------------------------------------------------------------------------------- |
| `SET ANSI_NULLS ON`        | Comparison with `NULL` uses ANSI semantics (`NULL = NULL` is unknown, not true) |
| `SET QUOTED_IDENTIFIER ON` | Double quotes are identifier delimiters, not string literals                    |

**Leave both `ON`.** They're required for modern SQL Server behavior. Setting them `OFF` can break the SP later.

The `GO` between each statement is an SSMS batch separator, not T-SQL syntax.

---

## Summary

| Question                                        | Answer                                                           |
| ----------------------------------------------- | ---------------------------------------------------------------- |
| What does it do?                                | Stops "`n rows affected`" messages from being sent to the client |
| Why use it?                                     | <mark>**Performance + cleaner client output**</mark>             |
| Where to put it?                                | <mark>**Top of every stored procedure**</mark>                   |
| Does it change results?                         | No                                                               |
| Does it affect `ExecuteScalar`/`ExecuteReader`? | No                                                               |
| Is it standard?                                 | <mark>**Yes** — included in Microsoft's SP templates</mark>      |


