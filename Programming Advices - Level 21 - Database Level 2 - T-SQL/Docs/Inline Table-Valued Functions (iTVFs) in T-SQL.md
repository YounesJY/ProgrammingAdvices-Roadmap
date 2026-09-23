# Inline Table-Valued Functions (iTVFs) in T-SQL

---

## 1. What is an iTVF?

An **Inline Table-Valued Function** is a user-defined function that:

- Declares `RETURNS TABLE`
- <mark>Contains exactly one `RETURN (SELECT ...)` statement</mark>
- Is **inlined** by the optimizer into the calling query — no function boundary at runtime

<mark>It behaves like a parameterized view</mark>. You call it in `FROM` / `JOIN` / `APPLY` and treat its output as a regular table.

---

## 2. Read-Only by Design

<mark>iTVFs **cannot modify data**</mark>. The following are all forbidden inside an iTVF:

- `INSERT`, `UPDATE`, `DELETE`, `MERGE`
- `TRUNCATE`
- DDL (`CREATE`, `ALTER`, `DROP`)
- `EXEC` of a data-modifying procedure
- `TRY/CATCH`, transactions, temp tables, dynamic SQL

**Why:** functions in SQL Server <mark><u>**are expected to be deterministic and side-effect-free**</u></mark>. <mark>**<u>The optimizer relies on this to inline them</u>**</mark>, push predicates through, **<u>and parallelize the plan</u>**. <mark>If an iTVF could change the database state, <u>**none of those optimizations would be safe**</u></mark>.

<mark>If you need to modify data, use a **stored procedure**, not a function.</mark>

---

## 3. The Scenario

```sql
CREATE TABLE Students (
    StudentID INT PRIMARY KEY,
    Name      NVARCHAR(50),
    Subject   NVARCHAR(50),
    Grade     INT
);
```

Columns: `StudentID` (PK), `Name`, `Subject`, `Grade`.

---

## 4. Creating an iTVF

```sql
CREATE FUNCTION dbo.GetStudentsBySubject
(
    @Subject NVARCHAR(50)
)
RETURNS TABLE
AS
RETURN
(
    SELECT *
    FROM Students
    WHERE Subject = @Subject
);
```

**Breakdown:**

| Piece                      | Meaning                                                                           |
| -------------------------- | --------------------------------------------------------------------------------- |
| `dbo.GetStudentsBySubject` | Function name, schema-qualified                                                   |
| `@Subject NVARCHAR(50)`    | Input parameter                                                                   |
| `RETURNS TABLE`            | Declares a table-returning function (no column list — inferred from the `SELECT`) |
| `AS RETURN ( ... )`        | The single query that defines the output                                          |
| No `BEGIN...END`           | iTVFs never use `BEGIN...END` — that's a **multi-statement TVF** signature        |

The parameter name `@Subject` and the column `Subject` differ only by the `@`. This is fine — the `WHERE` clause resolves `Subject` to the column and `@Subject` to the parameter. Still, naming the parameter `@SubjectFilter` or similar avoids confusion in longer queries.

---

## 5. Using an iTVF

**Example 1 — Filter by parameter:**

```sql
SELECT *
FROM dbo.GetStudentsBySubject('Math');
```

Returns all students studying Math.

**Example 2 — Aggregate over the function's output:**

```sql
SELECT AVG(Grade)
FROM dbo.GetStudentsBySubject('Science');
```

Returns the average grade among Science students.

**Example 3 — Join with other tables:**

```sql
SELECT s.Name, s.Grade
FROM dbo.GetStudentsBySubject('Math') AS s
JOIN Teachers AS t ON t.Subject = 'Math'
WHERE s.Grade > 80;
```

Because the function is inlined, this is equivalent to writing the same `SELECT` inline with a `WHERE Subject = 'Math'` filter — the optimizer sees the whole thing as one query.

---

## 6. Why iTVFs Are Preferred Over Other "Function" Forms

| Function type                  | Runtime behavior                                              | Performance                                                                                 |
| ------------------------------ | ------------------------------------------------------------- | ------------------------------------------------------------------------------------------- |
| **Scalar UDF**                 | Row-by-row (pre-2019); inlined if simple (2019+)              | <mark>Slow <u>**unless inlined**</u></mark>                                                 |
| **Inline TVF (iTVF)**          | <mark>Fully inlined into the calling query</mark>             | <mark>As fast as writing the query inline</mark>                                            |
| **Multi-statement TVF (mTVF)** | Materialized into a table variable; optimizer estimates 1 row | <mark>Usually slow</mark> — <mark>predicates can't be pushed through, no parallelism</mark> |

<mark>**iTVF is the sweet spot** <u>when you need</u></mark>:

- A set-returning function
- One `SELECT`'s worth of logic
- <mark><u>**Optimizer visibility and parallelizability**</u></mark>
- Reuse across multiple queries

<mark>The optimizer treats the iTVF as a **macro**, <u>not a black box</u></mark>. Predicate pushdown, join reordering, and parallelism <u>**all work as if you wrote the query directly**</u>.

---

## <mark><u>**7. When to Use an iTVF**</u></mark>

**Good fits:**

- <mark>A parameterized view</mark>: "give me rows matching X."
- Encapsulating a common join or filter used in many queries.
- Centralizing a complex `WHERE` clause or subquery that's reused.
- Providing a clean interface to a set of rows without exposing base tables.

**Bad fits:**

- Logic that needs procedural steps (`IF`, `WHILE`, temp tables) → that's an mTVF <u>**or a procedure**</u>.
- Data modification → procedure.
- A one-off query → don't wrap it; write it inline.
- Returning a single scalar → use a scalar UDF (or better, inline the expression).

---

## 8. Common Pitfalls

**1. Using `SELECT *` inside the function.**

Column list is inferred at creation. If `Students` later gains a column, the function's output shape changes silently. Prefer an explicit column list:

```sql
RETURN
(
    SELECT StudentID, Name, Subject, Grade
    FROM Students
    WHERE Subject = @Subject
);
```

**2. Forgetting schema qualification at the call site.**

`FROM GetStudentsBySubject('Math')` can fail if the caller's default schema isn't `dbo`. Always write `FROM dbo.GetStudentsBySubject('Math')`.

**3. Parameter name colliding with a column name.**

```sql
CREATE FUNCTION dbo.fn_X (@Name NVARCHAR(50))
RETURNS TABLE AS
RETURN (SELECT * FROM T WHERE Name = @Name);
```

Works, but readability suffers. Consider `@NameFilter`, `@NameParam`, or similar.

**4. Wrapping logic that should just be a view.**

If the function has no parameters, use a `VIEW`. iTVFs are for *parameterized* set logic.

**5. Wrapping logic that should just be inline.**

If the query is used once, don't wrap it. The function adds no value and makes debugging harder.

**6. Expecting the optimizer to always inline.**

In SQL Server 2019+, most iTVFs are inlined. But some patterns can defeat inlining (e.g., certain recursive CTEs, some `ORDER BY` + `TOP` interactions). If performance matters, check the execution plan — look for a `Table Valued Function` operator, which means it wasn't inlined.

---

## 9. iTVF vs. mTVF — Quick Reference

| Feature              | iTVF                        | mTVF                                                          |
| -------------------- | --------------------------- | ------------------------------------------------------------- |
| Body                 | Single `SELECT`             | `BEGIN...END` with multiple statements                        |
| Return declaration   | `RETURNS TABLE`             | `RETURNS @t TABLE (...)`, then `INSERT @t ...`, then `RETURN` |
| Optimizer visibility | <mark>Full (inlined)</mark> | None (<mark>opaque, 1-row estimate</mark>)                    |
| Parallelism          | Yes                         | Usually no                                                    |
| Predicate pushdown   | Yes                         | No                                                            |
| Performance          | <mark>Fast</mark>           | <mark>Often slow</mark>                                       |
| Use case             | <mark>Filtered view</mark>  | Procedural set construction                                   |

**Rule:** use iTVF whenever you can; <mark>**avoid mTVF** <u>unless you truly need procedural logic</u></mark>, and even then consider whether the logic belongs in a procedure instead.

---

## 10. Summary

| Aspect                                  | iTVF                                                    |
| --------------------------------------- | ------------------------------------------------------- |
| Modifies data                           | No                                                      |
| Body                                    | One `SELECT`                                            |
| Returns                                 | A table                                                 |
| Callable from `FROM` / `JOIN` / `APPLY` | Yes                                                     |
| Inlined by optimizer                    | Yes                                                     |
| Parameterized                           | Yes                                                     |
| Parallelizable                          | Yes                                                     |
| Preferred over scalar UDF (for sets)    | Yes                                                     |
| Preferred over mTVF                     | Yes                                                     |
| When to use                             | Reusable parameterized query returning rows             |
| When NOT to use                         | Single-use queries, data modification, procedural logic |

---

## 11. Next Steps

- Join an iTVF with other tables and inspect the execution plan — confirm it's inlined (no `Table Valued Function` operator).
- Try `CROSS APPLY` / `OUTER APPLY` against an iTVF to see how per-row parameterization works.
- Compare an iTVF's performance against the equivalent inline query — they should be identical when inlining succeeds.
- Compare against an mTVF doing the same work — the plan difference is instructive.

---
