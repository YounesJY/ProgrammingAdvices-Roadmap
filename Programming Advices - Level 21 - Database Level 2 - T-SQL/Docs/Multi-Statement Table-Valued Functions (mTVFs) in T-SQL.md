# Multi-Statement Table-Valued Functions (mTVFs) in T-SQL

---

## 1. What is an mTVF?

A **Multi-Statement Table-Valued Function** is a user-defined function that:

- Declares a return **table variable** with an explicit column list
- Uses a `BEGIN...END` body containing **multiple statements**
- Populates the return variable with `INSERT` statements
- Ends with `RETURN` (bare, no argument)

Unlike an inline TVF, <mark>an mTVF is **not inlined** into the calling query</mark>. <mark>The optimizer treats it as an opaque black box with a **fixed 1-row cardinality estimate**</mark> — which is the root of its performance problems.

---

## 2. How It Differs From an iTVF

| Aspect               | iTVF                                          | mTVF                                        |
| -------------------- | --------------------------------------------- | ------------------------------------------- |
| Body                 | Single `SELECT`                               | `BEGIN...END` with multiple statements      |
| Return declaration   | `RETURNS TABLE`                               | `RETURNS @t TABLE (...)` + explicit columns |
| Body ends with       | `RETURN (SELECT ...)`                         | `RETURN` (bare)                             |
| Optimizer visibility | <mark>Full (inlined)</mark>                   | <mark>None (black box)</mark>               |
| Row estimate         | <mark>Real (based on underlying stats)</mark> | <mark>Always 1</mark>                       |
| Parallelism          | <mark>Yes</mark>                              | Usually no                                  |
| Predicate pushdown   | <mark>Yes</mark>                              | No                                          |

**Practical consequence:** an mTVF that returns 10,000 rows still tells the optimizer "I return 1 row." Joins against it get planned as if the mTVF were tiny. <mark>This can produce catastrophic plan choices — nested loops over the mTVF output instead of hash joins</mark>, for example.

**SQL Server 2019+ does not fix mTVFs.** <mark>Only scalar UDFs got inlining. mTVFs are still opaque</mark>.

---

## 3. Read-Only, Same as iTVF

mTVFs cannot modify **base tables**:

- No `INSERT`, `UPDATE`, `DELETE`, `MERGE` against real tables
- No DDL
- No `EXEC` of a data-modifying procedure
- No temp tables (`#temp`), no dynamic SQL, no `TRY/CATCH`, no transactions

**What they *can* do:** <mark>insert into their own **local table variable** (`@Result`)</mark>. That variable <mark>is the function's internal workspace and dies when the function returns</mark>. This is not "modifying data" in the meaningful sense — <mark>it's just building the result</mark>.

So `INSERT INTO @Result ...` is fine. `INSERT INTO dbo.Students ...` is not.

---

## 4. The Scenario

```sql
CREATE TABLE Students (
    StudentID INT PRIMARY KEY,
    Name      NVARCHAR(50),
    Subject   NVARCHAR(50),
    Grade     INT
);

CREATE TABLE Teachers (
    TeacherID INT PRIMARY KEY,
    Name      NVARCHAR(50),
    Subject   NVARCHAR(50)
);
```

Two tables: `Students` and `Teachers`, both with a `Subject` column to join on.

---

## 5. Creating an mTVF

```sql
CREATE FUNCTION dbo.GetTopPerformingStudents()
RETURNS @Result TABLE (
    StudentID INT,
    Name      NVARCHAR(50),
    Subject   NVARCHAR(50),
    Grade     INT
)
AS
BEGIN
    INSERT INTO @Result (StudentID, Name, Subject, Grade)
    SELECT TOP 3 StudentID, Name, Subject, Grade
    FROM Students
    ORDER BY Grade DESC;

    RETURN;
END;
```

**Breakdown:**

| Piece                         | Meaning                                                                        |
| ----------------------------- | ------------------------------------------------------------------------------ |
| `RETURNS @Result TABLE (...)` | Declares a return table variable with explicit columns — **required for mTVF** |
| `BEGIN...END`                 | Multi-statement body                                                           |
| `INSERT INTO @Result ...`     | Populates the return variable from a query                                     |
| `RETURN;`                     | Bare `RETURN` — no argument, no parentheses                                    |

**Inconsistency in the source material:** the prose says "top 5 performing students," but the code says `TOP 3`. The code is what runs. Fix the prose or the code to match.

---

## 6. Using an mTVF

**Basic call:**

```sql
SELECT *
FROM dbo.GetTopPerformingStudents();
```

The function is invoked with parentheses (no arguments here) and treated as a table source.

**Joining with other tables:**

```sql
SELECT 
    t.Name AS TeacherName,
    s.Name AS StudentName,
    s.Grade
FROM Teachers AS t
JOIN dbo.GetTopPerformingStudents() AS s
    ON t.Subject = s.Subject;
```

This works, but the optimizer has no idea how many rows `s` returns. It assumes 1. If the actual row count is large, the plan will be wrong.

**When this matters:** if `Teachers` has 100 rows and the mTVF returns 3 rows, the "1-row estimate" is close enough. If the mTVF returns 100,000 rows, the plan could be disastrously slow. The fix is often to bypass the function and write the same logic inline — or, better, refactor to an iTVF if the body can be expressed as a single query.

---

## <mark><u>**7. When to Use an mTVF**</u></mark>

Honest answer: <mark><u>rarely</u></mark>. In 95% of cases where you reach for an mTVF, one of these is true:

- The logic fits in a single `SELECT` → use an **iTVF** instead.
- You need to modify data → use a **stored procedure**.
- You need transactional control, `TRY/CATCH`, temp tables → use a **stored procedure**.
- You're using it once → write it **inline**.

<mark><u>**Genuine mTVF use cases:**</u></mark>

- The result set genuinely requires multiple statements to build (e.g., a loop or a series of `INSERT`s into the result variable).
- <mark><u>**You accept the performance cost in exchange for encapsulation**</u></mark>.
- You're on an older SQL Server version with no alternatives.

Even then, <u>**always check the execution plan**</u>. If you see the mTVF's actual row count diverging badly from the estimated 1, the plan is likely suboptimal.

---

## 8. Common Pitfalls

**1. The 1-row cardinality estimate.**

The single biggest problem. Every mTVF is estimated to return 1 row, no matter how many it actually returns. This causes:

- Wrong join types (nested loops where hash joins would win).
- Wrong join order.
- Missing parallelism.

**Mitigation:** sometimes wrapping the mTVF call in a subquery with `OPTION (RECOMPILE)` helps the optimizer sniff the actual row count at runtime. Or bypass the function entirely.

**2. No predicate pushdown.**

```sql
SELECT * FROM dbo.GetTopPerformingStudents() WHERE Grade > 90;
```

You'd expect SQL Server to push `Grade > 90` into the function so it only processes qualifying rows. It can't — the function is opaque. It materializes the entire result, then filters. For small result sets that's fine. For large ones it's wasteful.

iTVFs handle this correctly because they're inlined; predicates flow into the inner query naturally.

**3. Materialization overhead.**

The return table variable is fully materialized before the caller sees it. Not a huge cost for small results, but it prevents streaming.

**4. Blocked parallelism.**

Queries involving mTVFs usually run single-threaded, even if the rest of the plan could parallelize. This caps performance on large workloads.

**5. Schema coupling.**

The return table's column list is fixed at creation. If the underlying tables change, the function still returns the old shape. You have to `ALTER FUNCTION` to update it.

**6. `RETURN` inside conditional branches.**

Multiple `RETURN` statements inside an mTVF body are legal but easy to get wrong. Every path must return the `@Result` variable. Since `RETURN` in mTVFs is bare (no argument), the variable is always the output — but forgetting to populate it on some path yields an empty result silently.

---

## 9. Alternatives to Consider

| Need                            | Prefer                                     |
| ------------------------------- | ------------------------------------------ |
| Filter a table by parameter     | **iTVF**                                   |
| Aggregate then return           | **iTVF** (single `SELECT` with `GROUP BY`) |
| Procedural logic to build a set | **mTVF** — but reconsider                  |
| Modify data based on parameters | **Stored procedure**                       |
| Return multiple result sets     | **Stored procedure**                       |
| One-off query                   | **Inline query**                           |
| Reusable parameterized view     | **iTVF**                                   |

**Rule:** if the logic can be expressed as one `SELECT`, use an iTVF. If it can't, ask whether it belongs in the application layer instead of the database.

---

## 10. Summary

| Aspect                          | mTVF                                                                 |
| ------------------------------- | -------------------------------------------------------------------- |
| Modifies base tables            | No                                                                   |
| Body                            | `BEGIN...END` with multiple statements                               |
| Returns                         | A table variable with an explicit schema                             |
| Inlined by optimizer            | No                                                                   |
| Cardinality estimate            | Always 1 row                                                         |
| Parallelizable                  | Usually no                                                           |
| Predicate pushdown              | No                                                                   |
| Preferred over iTVF             | Almost never                                                         |
| Preferred over stored procedure | Only if you truly need a set-returning function with procedural body |
| When to use                     | Rare — procedural set construction, and you accept the cost          |
| When NOT to use                 | Most of the time                                                     |

---

## 11. Practical Recommendation

**For SQL Server 2019+:**

1. **iTVF** whenever the logic fits in a single `SELECT`.
2. **Stored procedure** when you need writes, transactions, or `TRY/CATCH`.
3. **mTVF** only when neither fits — and verify the plan afterward.
4. **Scalar UDF** only when a single value is needed and inlining succeeds.

**If you're just starting out:** learn iTVFs and stored procedures well. Treat mTVFs as a legacy tool you might encounter in an old codebase but rarely write yourself.

---

## 12. Next Steps

- Compare the execution plan of an mTVF against an equivalent iTVF or inline query. Look for the mTVF's `Table Valued Function` operator and the estimated vs. actual row counts.
- Try pushing a `WHERE` clause onto an mTVF result and confirm it doesn't get pushed down.
- Refactor the `GetTopPerformingStudents` example as an iTVF — note that `SELECT TOP 3 ... ORDER BY Grade DESC` **can** be expressed as a single `SELECT`, so it doesn't actually need to be an mTVF. This is the lesson: many mTVFs are mTVFs by accident, not necessity.
- Try the same refactor as a stored procedure — see how the caller-side syntax changes (no `FROM proc_name`; instead `EXEC proc_name`).

---
