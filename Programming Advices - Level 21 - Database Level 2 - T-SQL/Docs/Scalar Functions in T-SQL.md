# Scalar Functions in T-SQL

---

## 1. What is a Scalar Function?

<mark>A **Scalar UDF** is a user-defined function that returns a **single value** per call.</mark>

- Declares `RETURNS <type>` (an explicit scalar type: `INT`, `NVARCHAR(50)`, `DECIMAL(18,2)`, etc.)
- Body is a `BEGIN...END` block
- Ends with `RETURN @value`
- Callable **anywhere an expression is allowed**: `SELECT`, `WHERE`, `JOIN ON`, `HAVING`, `ORDER BY`, computed columns, `CHECK` constraints

Unlike iTVFs, <mark>scalar UDFs are **not** set-returning</mark> — they produce one value for the current row's inputs.

---

## 2. Read-Only, Same as All Functions

Scalar UDFs cannot modify data:

- No `INSERT`, `UPDATE`, `DELETE`, `MERGE`
- No DDL
- No `EXEC` of a data-modifying procedure
- No `TRY/CATCH`, no transactions, no temp tables, no dynamic SQL

<mark><u>**They can `DECLARE` local variables and `SELECT` into them. That's it.**</u></mark>

<mark><u>**If you need side effects, use a stored procedure**</u>.</mark>

---

## 3. The Scenario

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

`Students` and `Teachers`, joined by `Subject`.

---

## 4. Creating a Scalar Function

```sql
CREATE FUNCTION dbo.GetAverageGrade(@Subject NVARCHAR(50))
RETURNS INT
AS
BEGIN
    DECLARE @AverageGrade INT;

    SELECT @AverageGrade = AVG(Grade)
    FROM Students
    WHERE Subject = @Subject;

    RETURN @AverageGrade;
END;
```

**Breakdown:**

| Piece                                   | Meaning                            |
| --------------------------------------- | ---------------------------------- |
| `dbo.GetAverageGrade`                   | Function name, schema-qualified    |
| `@Subject NVARCHAR(50)`                 | Input parameter                    |
| `RETURNS INT`                           | Declares a scalar return type      |
| `DECLARE @AverageGrade INT;`            | Local variable to hold the result  |
| `SELECT @AverageGrade = AVG(Grade) ...` | Assigns the computed value         |
| `RETURN @AverageGrade;`                 | Sends the value back to the caller |

---

## 5. A Bug Worth Noting

**The return type is `INT`, but `AVG(Grade)` returns `INT`-derived values that can have decimals.**

In T-SQL, `AVG` over an `INT` column returns an `INT` (integer division). So:

- `AVG` of `(85, 90, 87)` → `87` (truncated from `87.33`)

If you want decimal precision, either:

- Declare the return type as `DECIMAL(5,2)` (or similar), **and**
- Cast inside: `SELECT @AverageGrade = AVG(CAST(Grade AS DECIMAL(5,2)))`

```sql
CREATE FUNCTION dbo.GetAverageGrade(@Subject NVARCHAR(50))
RETURNS DECIMAL(5,2)
AS
BEGIN
    DECLARE @AverageGrade DECIMAL(5,2);

    SELECT @AverageGrade = AVG(CAST(Grade AS DECIMAL(5,2)))
    FROM Students
    WHERE Subject = @Subject;

    RETURN @AverageGrade;
END;
```

Without the cast, the `INT` averaging loses fractional grades. This is easy to miss because `AVG` "looks" like it should produce a decimal — but T-SQL's type inference says otherwise.

Also: if no rows match `@Subject`, `AVG` returns `NULL`, and `@AverageGrade` is `NULL`. The function returns `NULL`. Decide if that's the behavior you want — the caller will need to handle `NULL`.

---

## 6. Using a Scalar Function

**In `SELECT`:**

```sql
SELECT 
    Name,
    dbo.GetAverageGrade(Subject) AS AverageGrade
FROM Teachers;
```

For each row of `Teachers`, the function runs once with that row's `Subject`. Output: teacher name + average grade for their subject.

**In `WHERE`:**

```sql
SELECT Name, Subject
FROM Teachers
WHERE dbo.GetAverageGrade(Subject) > 80;
```

Filters teachers whose subject's average exceeds 80.

**In `ORDER BY`:**

```sql
SELECT Name, Subject
FROM Teachers
ORDER BY dbo.GetAverageGrade(Subject) DESC;
```

Sorts teachers by their subject's average grade.

**In a computed column or `CHECK` constraint:**

```sql
ALTER TABLE Teachers
ADD AvgGradeForSubject AS dbo.GetAverageGrade(Subject);
```

Now `AvgGradeForSubject` is a persisted-in-metadata computed column. Note: it's evaluated on read, not stored (unless `PERSISTED` — and even then, only for deterministic functions).

---

## <mark><u>**7. Performance — The Critical Section**</u></mark>

**Pre-SQL Server 2019:** <mark>**<u>scalar UDFs are a well-known performance trap</u>**</mark>. They execute **row-by-row**, <mark><u>**break parallelism, and produce plans that don't reflect their real cost**</u></mark>. A query touching 1 million rows and calling a scalar UDF once per row means 1 million separate function invocations — <mark><u>**each with overhead**</u></mark>.

**SQL Server 2019+:** <mark>**Scalar UDF Inlining (FROID)** rewrites simple scalar UDFs into inline expressions</mark>, removing the row-by-row penalty. **<u>Not all UDFs qualify</u>**:

**Inlining fails if the UDF:**

- Contains `BEGIN...END` with certain control flow (`IF`, `WHILE`, `TRY/CATCH`)
- Uses non-deterministic built-ins (`GETDATE()`, `NEWID()`, `RAND()`)
- References table variables, temp tables, or table-valued functions
- Uses `EXEC`, dynamic SQL, or error handling
- Returns `VARCHAR(MAX)`, `NVARCHAR(MAX)`, or other large types
- Uses aggregates in certain ways
- Is recursive

**The example in the source material uses `BEGIN...END` with a `SELECT` — this *can* be inlined by 2019+ FROID, but only if the body is simple enough.** Adding a single `IF` or `TRY/CATCH` kills inlining and you're back to row-by-row.

**How to check:** look at the execution plan. If you see a `Compute Scalar` with an inlined expression, it worked. If you see a `UDF` operator or a `Table Valued Function` (for scalar calls, oddly named in some plans), it didn't.

**The takeaway:** <mark><u>**don't reach for scalar UDFs casually**</u></mark>. If the same logic can be a `CASE` expression or inline arithmetic, write it inline. <mark>Use a scalar UDF only when</mark>:

- The logic is genuinely reused across many queries.
- It encodes a business rule you want centralized.
- You're on 2019+ and inlining succeeds (verify the plan).

---

## 8. <mark><u>**When to Use a Scalar UDF**</u></mark>

**Good fits:**

- A reused pure computation: `dbo.CalculateTax(amount, region)`, `dbo.FormatFullName(first, last)`.
- A business rule you want to centralize rather than duplicate across queries.
- A simple predicate reused in many `WHERE` clauses.

**Bad fits:**

- Row-by-row lookups that could be joins — use `JOIN` or `APPLY` instead.
- Aggregations over the same table — a subquery or join to a derived table is almost always faster.
- Complex procedural logic — that belongs in a stored procedure.
- A one-off calculation — just inline it.
- In a `WHERE` clause against a large table on a pre-2019 server — this is the classic performance disaster. Each row triggers a function call that itself likely queries the same table. It's an unintentional N+1.

**The `WHERE dbo.GetAverageGrade(Subject) > 80` example from the source material is exactly this trap.** It runs the function once per teacher, and each run re-aggregates `Students`. For 10,000 teachers and 1 million students, that's 10,000 passes over `Students`. Writing it as a join is orders of magnitude faster:

```sql
SELECT t.Name, t.Subject
FROM Teachers AS t
JOIN (
    SELECT Subject, AvgGrade = AVG(Grade)
    FROM Students
    GROUP BY Subject
) AS s
    ON s.Subject = t.Subject
WHERE s.AvgGrade > 80;
```

The scalar function version *works*, but the join version is what you'd actually ship. The UDF version is a teaching example — it demonstrates composability, not good performance practice.

---

## 9. Common Pitfalls

**1. `AVG` over `INT` truncates.** Covered above. Cast to `DECIMAL` if you need precision.

**2. `NULL` propagation.** If the function's `SELECT` returns no row, the variable stays `NULL`. Callers get `NULL`. This can silently drop rows in `WHERE` clauses (since `NULL > 80` is UNKNOWN, not FALSE — no, wait, it filters out, but you might not expect it).

**3. Determinism.** A scalar UDF that calls `GETDATE()` or `NEWID()` is non-deterministic. It can't be used in computed columns (unless marked) or indexed views, and it defeats inlining.

**4. Naming parameter same as column.** `@Subject` vs `Subject` inside the function body — works, but reads poorly. Consider `@SubjectFilter`.

**5. Schema qualification.** Always write `dbo.GetAverageGrade(...)` at the call site. Unqualified calls depend on the caller's default schema.

**6. Inlining silently fails.** Even on 2019+, if the body isn't simple, inlining is skipped and you get pre-2019 performance. Check the plan.

**7. Using scalar UDFs where an iTVF fits.** If the caller needs a set, an iTVF is strictly better. Scalar UDFs only make sense when you genuinely need one value per call.

---

## 10. Scalar UDF vs. Other Forms

| Need                         | Prefer                           |
| ---------------------------- | -------------------------------- |
| Single value per row, reused | **Scalar UDF** (verify inlining) |
| Set of rows, reused          | **iTVF**                         |
| Set with procedural body     | **mTVF** (rarely)                |
| Side effects, transactions   | **Stored procedure**             |
| One-off calculation          | **Inline expression**            |
| Reused filter/join logic     | **iTVF**                         |

---

## 11. Summary

| Aspect                                       | Scalar UDF                                                 |
| -------------------------------------------- | ---------------------------------------------------------- |
| Modifies data                                | No                                                         |
| Body                                         | `BEGIN...END` with `RETURN @value`                         |
| Returns                                      | One scalar value                                           |
| Callable from `SELECT` / `WHERE` / `JOIN ON` | Yes                                                        |
| Inlined by optimizer                         | Only on 2019+, only if simple                              |
| Pre-2019 performance                         | Row-by-row, often 10–100× slower than inline               |
| Post-2019 performance                        | Fast if inlined; slow if not                               |
| Parallelizable                               | Only when inlined                                          |
| Preferred over iTVF                          | No, when the caller needs a set                            |
| Preferred over stored procedure              | Only for pure scalar computation                           |
| When to use                                  | Reused pure scalar logic, centrally defined business rules |
| When NOT to use                              | Row-by-row lookups, aggregations, anything procedural      |

---

## 12. Next Steps

- Compare `SELECT dbo.GetAverageGrade(Subject) FROM Teachers` against the equivalent join-based version. Look at the execution plan and the actual execution time.
- Test the function on SQL Server 2019+ and check whether inlining kicked in (`Compute Scalar` in the plan vs. a UDF operator).
- Add an `IF` or `TRY/CATCH` to the function body and observe that inlining is disabled — this is the moment "simple UDF" becomes "slow UDF."
- Rewrite the same logic as an iTVF returning one row per subject, and join to it. This is usually the better design.
- Try to use the scalar UDF in a computed column with `PERSISTED` — note that it fails unless the function is deterministic.

---
