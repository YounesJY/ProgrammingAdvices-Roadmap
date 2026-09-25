# Inserted and Deleted Tables in T-SQL Triggers

---

## 1. What They Are

`inserted` and `deleted` <mark>are **virtual tables** that SQL Server creates automatically inside a DML trigger</mark>. <mark>**<u>They exist only</u>** for the duration of the trigger's execution</mark> — <u>**you can't query them outside a trigger**</u>, and <u>**they aren't stored anywhere in the database**</u>.

> They <mark><u>give the trigger visibility into **what changed**</u></mark> during the statement that fired it.

| Pseudo-table | Contains                                      |
| ------------ | --------------------------------------------- |
| `inserted`   | The **new** version of each affected row      |
| `deleted`    | The **original** version of each affected row |

---

## 2. Key Characteristics

- <mark>**Virtual.**</mark> Not stored — materialized only while the trigger is running.<mark></mark>

- <mark>**Schema mirrors the trigger's base table.**</mark> Same columns, same types.

- <mark>**Read-only.**</mark> You can `SELECT` from them, `JOIN` them, and insert their rows into other tables. You can't `INSERT`, `UPDATE`, or `DELETE` them directly.

- <mark>**Statement-scoped.**</mark> They hold **all rows** affected by the triggering statement,not one row at a time. A single `UPDATE` touching 10,000 rows puts 10,000 rows in each pseudo-table, and the trigger fires **once**

- <mark><u>**Available in both `AFTER` and `INSTEAD OF` triggers**</u></mark> — not in functions, procedures, or ad-hoc queries.

---

## 3. Behavior per DML Operation

| Operation | `inserted`                         | `deleted`                          |
| --------- | ---------------------------------- | ---------------------------------- |
| `INSERT`  | <mark>New rows</mark>              | <u>**Empty**</u> (nothing removed) |
| `DELETE`  | <u>**Empty**</u> (nothing added)   | <mark>Removed rows</mark>          |
| `UPDATE`  | <mark><u>**New values**</u></mark> | <mark><u>**Old values**</u></mark> |

<mark><u>**For `UPDATE`,** both tables are populated</u></mark>. <u>**Joining them on the primary key**</u> gives the before/after pair per row — <mark><u>**the foundation of every audit trigger**</u></mark>.

---

## 4. The Example Scenario

### Table

```sql
CREATE TABLE [dbo].[Students](
    [StudentID] [int] NOT NULL,
    [Name]      [nvarchar](50) NULL,
    [Subject]   [nvarchar](50) NULL,
    [Grade]     [int] NULL,
    [IsActive]  [bit] NULL,
    PRIMARY KEY CLUSTERED ([StudentID] ASC)
);
```

Goal: log every change to `Grade` into an audit table.

### Step 1 — Audit Table

```sql
CREATE TABLE StudentGradeAudit (
    AuditID        INT IDENTITY PRIMARY KEY,
    StudentID      INT,
    OldGrade       INT,
    NewGrade       INT,
    ChangeDateTime DATETIME DEFAULT GETDATE()
);
```

### Step 2 — The Trigger

```sql
CREATE TRIGGER trg_StudentGradeChange 
ON [dbo].[Students]
AFTER UPDATE
AS
BEGIN
    IF UPDATE(Grade)
    BEGIN
        INSERT INTO StudentGradeAudit (StudentID, OldGrade, NewGrade)
        SELECT 
            I.StudentID, 
            D.Grade AS OldGrade, 
            I.Grade AS NewGrade
        FROM inserted I
        INNER JOIN deleted D ON I.StudentID = D.StudentID;
    END
END;
```

### How it works

1. Fires **after** an `UPDATE` on `Students`.
2. `IF UPDATE(Grade)` checks whether the `Grade` column appeared in the `SET` clause of the update. If the caller only updated `Name`, the trigger does nothing.
3. The `INNER JOIN` between `inserted` and `deleted` on `StudentID` pairs each row's before and after values.
4. Those pairs get inserted into `StudentGradeAudit`.

---

## 5. Details Worth Understanding

### 5.1 `IF UPDATE(col)` <mark>is column-level, not value-level</mark>

`UPDATE(Grade)` returns `TRUE` if the column appears in the `SET` clause — <mark>regardless of whether the values actually changed</mark>.

```sql
UPDATE Students SET Grade = Grade WHERE StudentID = 1;
```

This fires the trigger even though the grade is identical before and after. To log only **actual** changes, compare the values:

```sql
IF EXISTS (
    SELECT 1
    FROM inserted I
    JOIN deleted D ON I.StudentID = D.StudentID
    WHERE I.Grade <> D.Grade
       OR (I.Grade IS NULL AND D.Grade IS NOT NULL)
       OR (I.Grade IS NOT NULL AND D.Grade IS NULL)
)
BEGIN
    -- ... do the insert
END
```

The `NULL`-safe comparison is tedious because `NULL <> NULL` is `UNKNOWN`. On SQL Server 2022+ you can use `IS DISTINCT FROM`:

```sql
WHERE I.Grade IS DISTINCT FROM D.Grade
```

### 5.2 Never assume a single row

The most common trigger bug is treating `inserted` like it holds exactly one row:

```sql
-- WRONG: silently uses one arbitrary row
DECLARE @id INT;
SELECT @id = StudentID FROM inserted;
```

If the triggering statement affected 500 rows, this picks one at random and ignores the rest. Always write trigger logic **set-based** — `INSERT ... SELECT ... FROM inserted JOIN deleted`, `UPDATE ... FROM inserted`, `DELETE ... FROM deleted`. Never feed scalar variables from the pseudo-tables.

### 5.3 `UPDATE` guarantees pairing between `inserted` and `deleted`

In an `UPDATE`, every affected row has an entry in both `inserted` and `deleted`. `INNER JOIN` on the primary key is safe. You don't need `LEFT JOIN` — the pairing is guaranteed by the engine.

### 5.4 `inserted` and `deleted` in `INSTEAD OF` triggers

In an `AFTER` trigger, <mark>the modification has already happened</mark>. In an `INSTEAD OF` trigger, it hasn't — the trigger itself performs the modification, using `inserted`/`deleted` as instructions from the caller. Same pseudo-tables, different timing.

### 5.5 `deleted` in `INSERT`, `inserted` in `DELETE`

Referencing `deleted` in an `INSERT` trigger or `inserted` in a `DELETE` trigger doesn't error — the table exists but has zero rows. Still, it usually indicates confused logic, since those pseudo-tables don't carry meaning for those operations.

### 5.6 Triggers run once per statement, not once per row

T-SQL triggers are **statement-level**. This differs from Oracle's row-level triggers (`FOR EACH ROW`) and from many developers' intuitions. A trigger designed around "one row at a time" logic will misbehave on multi-row statements. Always write for N rows.

---

## 6. Common Pitfalls

| Pitfall                                      | Why it's wrong                                    | Fix                                                                   |
| -------------------------------------------- | ------------------------------------------------- | --------------------------------------------------------------------- |
| `SELECT @var = col FROM inserted`            | Uses an arbitrary row when multiple are affected  | Write set-based logic with `INSERT ... SELECT` / `JOIN`               |
| Assuming the trigger runs once per row       | T-SQL is statement-level                          | Design for multi-row                                                  |
| Using `UPDATE(col)` as "value changed" check | It only checks the `SET` clause, not values       | Compare `inserted` vs. `deleted` values                               |
| Not handling `NULL` comparisons              | `NULL <> NULL` is `UNKNOWN`                       | `IS DISTINCT FROM` (2022+) or explicit `IS NULL` checks               |
| `INSERT` trigger referencing `deleted`       | Table exists but is empty                         | Nothing to fix, but clean up the logic                                |
| Heavy logic in the trigger                   | Runs inside the caller's transaction, holds locks | Move expensive work out of the trigger (queue table + background job) |
| Trigger fires inside an app loop             | Each `INSERT` runs the trigger separately         | Batch statements where possible                                       |

---

## 7. `inserted` / `deleted` Outside Triggers

<mark><u>**You can't query these pseudo-tables outside a trigger**</u></mark>. In an `OUTPUT` clause, the same logical concepts appear under the same names:

```sql
UPDATE Students
SET Grade = @NewGrade
OUTPUT inserted.StudentID, deleted.Grade AS OldGrade, inserted.Grade AS NewGrade
WHERE StudentID = @ID;
```

`OUTPUT` gives access to `inserted` and `deleted` in regular DML — often a cleaner alternative to a trigger when you only need logging on one statement. <mark>Triggers are better when the logging must be unconditional across every statement against the table</mark>, from any caller.

---

## 8. Summary

| Aspect                 | `inserted`                           | `deleted`                         |
| ---------------------- | ------------------------------------ | --------------------------------- |
| Purpose                | New version of affected rows         | Original version of affected rows |
| Populated by `INSERT`  | Yes                                  | No (empty)                        |
| Populated by `DELETE`  | No (empty)                           | Yes                               |
| Populated by `UPDATE`  | Yes (new values)                     | Yes (old values)                  |
| Available in           | DML triggers (`AFTER`, `INSTEAD OF`) | Same                              |
| Granularity            | All rows affected by the statement   | Same                              |
| Schema                 | Mirrors the trigger's base table     | Same                              |
| Modifiable             | No — read-only                       | No                                |
| Equivalent in `OUTPUT` | `inserted.*`                         | `deleted.*`                       |

---

## 9. Next Steps

- Write a trigger that handles a **multi-row** `UPDATE` (say, 100 students in one statement) and verify the audit table gets 100 rows.
- Rewrite the same audit logic using `OUTPUT` instead of a trigger. Compare readability, testability, and performance.
- Write an `INSTEAD OF UPDATE` trigger and observe that the caller's update does *not* happen unless the trigger performs it explicitly.
- Try `UPDATE Students SET Grade = Grade` (no actual value change) and check whether your trigger logs a row. If it does, extend the `WHERE` clause to filter out no-op updates.
- Test what happens when the trigger raises an error — does the caller's statement roll back? Does an outer transaction roll back? This is the trigger's transaction semantics in action.
