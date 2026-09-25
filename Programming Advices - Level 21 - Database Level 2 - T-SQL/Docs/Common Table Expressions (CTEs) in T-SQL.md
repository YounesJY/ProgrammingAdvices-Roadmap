# Common Table Expressions (CTEs) in T-SQL

---

## 1. What is a CTE?

    A **Common Table Expression** is <mark>a named temporary result set that exists only for the duration of a single statement</mark>. You define it with `WITH`, then reference it in the statement that follows — like a named subquery that makes complex queries readable.

CTEs can be referenced in `SELECT`, `INSERT`, `UPDATE`, `DELETE`, and `MERGE`.

---

## 2. Basic Syntax

```sql
WITH CTE_Name (Column1, Column2, ...) AS
(
    -- CTE query definition
)
-- Query using the CTE
SELECT * FROM CTE_Name;
```

<mark>The column list is optional</mark>. If omitted, the CTE inherits column names from the inner `SELECT`.

---

## 3. A Simple CTE

```sql
WITH SalesStaff AS
(
    SELECT EmployeeId, Name, Sales
    FROM Employees6
    WHERE Department = 'Sales'
)
SELECT * FROM SalesStaff;
```

`SalesStaff` is the CTE. The main query treats it like a table.

---

## 4. CTEs vs. Subqueries

Both produce an intermediate result set. The differences:

| Aspect                      | CTE                                                                 | Subquery                                                          |
| --------------------------- | ------------------------------------------------------------------- | ----------------------------------------------------------------- |
| **Readability**             | <mark><u>**Named**</u>, <u>**defined at the top**</u></mark>        | Inline, <mark><u>**nested inside the query**</u></mark>           |
| **Reuse in same statement** | Yes — <mark>reference the CTE name <u>**multiple times**</u></mark> | No — <u>**you'd repeat the subquery text**</u>                    |
| **Recursion**               | <mark><u>**Yes**</u></mark> (`WITH x AS (... UNION ALL ...)`)       | No                                                                |
| **Main query shape**        | <mark><u>**CTE at the top**</u></mark>, main query below            | Subquery sits <mark>**inside**</mark> `FROM` / `WHERE` / `SELECT` |
| **Optimizer behavior**      | Same as a subquery (inlined, not materialized)                      | Same                                                              |
| **Multiple CTEs**           | Yes — comma-separated after `WITH`                                  | <mark>**<u>Nested subqueries get deep fast</u>**</mark>           |

**When to prefer a CTE:** <mark>**<u>the query has multiple logical stages</u>**</mark>, or <mark><u>**the same intermediate result is used in multiple places**</u></mark>.

**When to prefer a subquery:** the intermediate result <mark><u>**is used once and is short**</u></mark> — a CTE adds syntax for no benefit.

**Rewriting the same thing both ways:**

```sql
-- Subquery
SELECT s.Name, s.Grade, a.AvgGrade
FROM Students s
JOIN (
    SELECT 
        Subject,
        AvgGrade = AVG(Grade)
    FROM Students
    GROUP BY Subject
) AS a ON a.Subject = s.Subject;

-- CTE
WITH SubjectAverages AS (
    SELECT 
        Subject,
        AvgGrade = AVG(Grade)
    FROM Students
    GROUP BY Subject
)
SELECT s.Name, s.Grade, a.AvgGrade
FROM Students s
JOIN SubjectAverages a 
    ON a.Subject = s.Subject;
```

<mark><u>**Same execution plan**</u></mark>. <mark><u>**Different readability**</u></mark>. <u>**The CTE version <mark>scales better as the query gets longer</mark>**</u>.

---

## 5. CTEs vs. Temp Tables vs. Table Variables

The three tools look similar at first — all "hold an intermediate result" — but they behave very differently.

| Aspect                         | CTE                           | Temp Table (`#t`)                   | Table Variable (`@t`)                |
| ------------------------------ | ----------------------------- | ----------------------------------- | ------------------------------------ |
| **Nature**                     | Named query (a macro)         | <mark>Real table in `tempdb`</mark> | <mark>Real table in `tempdb`</mark>  |
| **Materialized?**              | No — inlined                  | Yes                                 | Yes                                  |
| **Scope**                      | <mark>Single statement</mark> | Session                             | Batch / procedure / function         |
| **Lifetime**                   | <mark>One statement</mark>    | Until dropped or session ends       | Until batch ends                     |
| **Modifiable**                 | <mark>No</mark>               | Yes                                 | Yes                                  |
| **Indexes**                    | <mark>No</mark>               | Yes (can create)                    | Only PK / UNIQUE at declaration      |
| **Statistics**                 | Yes (from underlying tables)  | Yes                                 | **No** — always estimated at 1 row   |
| **Recursion**                  | <mark>Yes</mark>              | No                                  | No                                   |
| **Transaction log impact**     | <mark>Minimal</mark>          | Full                                | Reduced                              |
| **Reusable across statements** | No                            | Yes                                 | Yes                                  |
| **Good for large sets**        | Yes (optimizer sees through)  | Yes                                 | No — 1-row estimate causes bad plans |

### When to use each

**CTE:**

- Intermediate result used **once**, in the immediately following statement.
- You want the optimizer to see through it (inlining).
- Recursive queries.
- Readability is the priority.

**Temp table (`#t`):**

- Large intermediate sets (thousands+ rows).
- Need indexes on the intermediate data.
- Reused across multiple statements or batches.
- Need **statistics** for the optimizer to pick a good plan.

**Table variable (`@t`):**

- Small intermediate sets (few hundred rows).
- Reused across multiple statements **within the same batch**.
- Inside a **function** (temp tables can't be used there; table variables can).
- You want to avoid transaction log overhead for the intermediate data.

### The performance trap

<mark>Table variables are the biggest trap of the three</mark>. Because they have **no statistics**, the optimizer always assumes they contain **exactly 1 row**. For a small table variable, the estimate is fine. <mark>For a large one, the estimate is catastrophically wrong</mark> — joins get planned as nested loops when hash joins would win, parallelism is disabled, and queries that should take milliseconds can take seconds.

<mark><u>**Rule of thumb:**</u></mark> if the intermediate set exceeds a few hundred rows, use a `#temp` table. It has statistics, indexes, and real cardinality estimates.

CTEs are somewhere between: they're not materialized by default, so the optimizer sees the underlying tables' statistics — but if the CTE is referenced multiple times, the optimizer might materialize it anyway, or might re-execute the query each time. For one-shot use, CTEs are effectively free.

### Same problem, three solutions

**Problem:** compute average grade per subject, then join back to students.

```sql
-- CTE
WITH SubjectAverages AS (
    SELECT Subject, AvgGrade = AVG(Grade)
    FROM Students
    GROUP BY Subject
)
SELECT s.Name, a.AvgGrade
FROM Students s
JOIN SubjectAverages a ON a.Subject = s.Subject;
```

```sql
-- Temp table
CREATE TABLE #SubjectAverages (Subject NVARCHAR(50), AvgGrade INT);

INSERT INTO #SubjectAverages (Subject, AvgGrade)
SELECT Subject, AVG(Grade)
FROM Students
GROUP BY Subject;

SELECT s.Name, a.AvgGrade
FROM Students s
JOIN #SubjectAverages a ON a.Subject = s.Subject;

DROP TABLE #SubjectAverages;
```

```sql
-- Table variable
DECLARE @SubjectAverages TABLE (Subject NVARCHAR(50), AvgGrade INT);

INSERT INTO @SubjectAverages (Subject, AvgGrade)
SELECT Subject, AVG(Grade)
FROM Students
GROUP BY Subject;

SELECT s.Name, a.AvgGrade
FROM Students s
JOIN @SubjectAverages a ON a.Subject = s.Subject;
```

> <mark><u>**All three produce the same result. The differences are performance (statistics), scope (statement vs. batch), and reusability (single statement vs. multiple statements).**</u></mark>

---

## 6. Best Practices

- **Readability first.** Use CTEs to break complex queries into named stages.
- **Performance awareness.** <mark><u>**CTEs are inlined, not materialized**</u></mark> — for large data used multiple times, a `#temp` table may be faster.
- **Recursion limits.** <mark><u>**Recursive CTEs can loop infinitely**</u></mark>; SQL Server caps recursion at 100 by default (`OPTION (MAXRECURSION n)` to override).
- **Don't overuse.** A single-use CTE that's never referenced twice <mark>is often clearer as a plain subquery</mark>.

---

## 7. Summary

| Aspect                     | CTE                          |
| -------------------------- | ---------------------------- |
| Named temporary result set | Yes                          |
| Scope                      | Single statement             |
| Materialized               | No (inlined by default)      |
| Reusable in same statement | Yes                          |
| Recursion                  | Yes                          |
| Statistics                 | From underlying tables       |
| Good for large data        | Yes (optimizer sees through) |
| Good for multiple uses     | Only within one statement    |

**One-liner:** a CTE is a query label — <mark>use it when you want readability and reuse **within a single statement**</mark>. Use a temp table or table variable <mark><u>**when you need to store the result**</u></mark>, <u>modify it, or reference it across multiple statements</u>.

---

## 8. Next Steps

- Rewrite a subquery-heavy query using CTEs and compare readability.
- Try a recursive CTE — compute the factorial of 5, or walk an employee hierarchy.
- Compare the execution plan of the same query using a CTE vs. a subquery — they should be identical.
- Compare a CTE vs. a temp table over a million-row intermediate result — observe the plan difference.
- Try a table variable with 10,000 rows inside a `JOIN` and check the estimated vs. actual row counts in the execution plan. The 1-row estimate is visible.


