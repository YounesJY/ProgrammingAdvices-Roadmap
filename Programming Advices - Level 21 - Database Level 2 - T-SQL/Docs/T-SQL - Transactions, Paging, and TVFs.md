# T-SQL — Transactions, Paging, and TVFs

---

## Part 1 — Transactions

### 1.1 What is Locking, and is it related to keeping transactions small?

**Locking** is how SQL Server prevents two sessions from interfering with each other while they read or write the same data. When a session modifies a row inside a transaction, the engine takes a lock on that row (and often on its page, and sometimes its table) so that concurrent sessions either wait or see the pre-modification state.

Lock types you'll encounter:

| Lock                | What it does                                                                                                                     |
| ------------------- | -------------------------------------------------------------------------------------------------------------------------------- |
| **Shared (S)**      | Taken by readers. Multiple readers can hold S locks on the same row.                                                             |
| **Exclusive (X)**   | Taken by writers. Blocks everyone else.                                                                                          |
| **Update (U)**      | Middle ground — taken by a writer before promoting to X, preventing two writers from deadlocking on the read-then-write pattern. |
| **Intent (IS, IX)** | Placed on higher-level objects (page, table) to signal that a lower-level lock exists.                                           |

**Lock granularity matters too:**

- **Row-level** — finer, more concurrent, more overhead.
- **Page-level** — middle ground.
- **Table-level** — coarse, blocks everyone, less overhead.

SQL Server picks granularity based on cost, or you can hint it (`WITH (ROWLOCK)`, `WITH (TABLOCK)`).

**Yes, locking is directly tied to transaction size.** The longer a transaction stays open and the more rows it touches:

- The more locks it holds.
- The longer other sessions must wait.
- The higher the chance of a **deadlock** (two sessions each holding a lock the other wants).

So the rule:

> **Keep transactions as short as possible. Open late, commit early. Never hold a transaction open while waiting on user input, external services, or UI.**

Concrete example of the wrong pattern:

```sql
BEGIN TRAN;
    -- do some updates
    -- ... wait for user confirmation in the application ...
    -- ... call an external API ...
    -- ... do more updates ...
COMMIT;
```

While that transaction is open, other sessions block on the rows it touched. On a busy system, one slow transaction can queue dozens of others.

The right pattern:

```sql
BEGIN TRAN;
    -- do all the updates in one pass
COMMIT;
```

No user interaction, no external calls, no time spent thinking — the transaction exists only as long as the data modification takes.

**Related concept: isolation levels.** Locking behavior depends on the session's isolation level:

| Level                            | Reads block writers? | Writers block readers? | Notes                                                                                    |
| -------------------------------- | -------------------- | ---------------------- | ---------------------------------------------------------------------------------------- |
| `READ UNCOMMITTED`               | No                   | No                     | Dirty reads possible. Almost never use.                                                  |
| `READ COMMITTED` (default)       | Yes                  | Yes                    | Standard behavior. Readers wait for writers.                                             |
| `REPEATABLE READ`                | Yes                  | Yes                    | Locks held for the whole transaction.                                                    |
| `SERIALIZABLE`                   | Yes                  | Yes                    | Range locks. Most restrictive.                                                           |
| `SNAPSHOT`                       | No                   | No                     | Readers see a committed snapshot. Row versioning instead of locking. Requires DB option. |
| `READ COMMITTED SNAPSHOT` (RCSI) | No                   | No                     | Like snapshot, but only for read committed sessions.                                     |

Snapshot and RCSI trade locking for **row versioning** stored in `tempdb`. They eliminate reader-writer blocking at the cost of `tempdb` pressure.

---

### 1.2 Should `BEGIN TRAN` go inside `BEGIN TRY`?

**Yes — in most cases.** The pattern is:

```sql
BEGIN TRY
    BEGIN TRAN;
        -- do work
    COMMIT;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK;
    THROW;
END CATCH;
```

**Why this ordering:**

- `BEGIN TRAN` inside `TRY` means any error raised between `BEGIN TRAN` and `COMMIT` is catchable by the `CATCH` block.
- If `BEGIN TRAN` itself fails (rare — usually a resource issue), the `CATCH` block's `IF @@TRANCOUNT > 0` guard prevents trying to roll back a transaction that was never opened.
- `COMMIT` inside `TRY` means if commit fails, the `CATCH` runs.
- `ROLLBACK` inside `CATCH` ensures the transaction is cleaned up before `THROW` re-raises the error.

**The `@@TRANCOUNT > 0` guard is important.** Without it:

- If an error occurs *before* `BEGIN TRAN`, `@@TRANCOUNT` is 0 and `ROLLBACK` would error: `The ROLLBACK TRANSACTION request has no corresponding BEGIN TRANSACTION`.
- That new error would obscure the original error you were trying to surface.

**Variation — `BEGIN TRAN` before `BEGIN TRY`:**

Some code puts `BEGIN TRAN` *before* `TRY`:

```sql
BEGIN TRAN;
BEGIN TRY
    -- work
    COMMIT;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0 ROLLBACK;
    THROW;
END CATCH;
```

This is fine too, but slightly worse: if `BEGIN TRAN` itself errors (e.g., nested transaction issues with `XACT_ABORT`), the `TRY` doesn't catch it. Putting `BEGIN TRAN` inside `TRY` gives you one more layer of error coverage.

**The trade-off is minor.** Both forms are widely used. Pick one and be consistent.

**One more thing:** `SET XACT_ABORT ON` interacts with all of this. With `XACT_ABORT ON`, any runtime error automatically rolls back the whole transaction and aborts the batch. With it OFF (default), some errors roll back only the failing statement, leaving the transaction open and potentially in a partial state. For robust error handling, `SET XACT_ABORT ON` at the top of the procedure is often recommended:

```sql
CREATE PROCEDURE dbo.usp_TransferMoney
    @FromAccount INT,
    @ToAccount   INT,
    @Amount      DECIMAL(18,2)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;   -- any error → auto rollback

    BEGIN TRY
        BEGIN TRAN;
            UPDATE Accounts SET Balance = Balance - @Amount WHERE AccountID = @FromAccount;
            UPDATE Accounts SET Balance = Balance + @Amount WHERE AccountID = @ToAccount;
        COMMIT;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK;
        THROW;
    END CATCH;
END;
```

With `XACT_ABORT ON`, the `CATCH` block usually doesn't even need to `ROLLBACK` — the engine has already done it. But the `IF @@TRANCOUNT > 0 ROLLBACK` guard is harmless and protects against edge cases.

---

### 1.3 Why is there no `END TRANSACTION`?

Because `COMMIT` and `ROLLBACK` *are* the end. A transaction has exactly two terminal states:

- **`COMMIT`** — make the changes permanent.
- **`ROLLBACK`** — discard the changes.

There's no third path. `END TRANSACTION` would be redundant — "end" would have to mean either commit or rollback, and you'd still need to say which one.

The syntax is:

```sql
BEGIN TRAN;   -- or BEGIN TRANSACTION
    ...
COMMIT;       -- or COMMIT TRANSACTION, or COMMIT WORK
-- or
ROLLBACK;     -- or ROLLBACK TRANSACTION, or ROLLBACK WORK
```

`BEGIN`/`COMMIT`/`ROLLBACK` are the three verbs. There's no matching `END`.

**Why the asymmetry with, say, `BEGIN...END` blocks?**

`BEGIN...END` (without `TRAN`) is a **block delimiter** for grouping statements — like `{ }` in C-family languages. It has a matching `END`.

`BEGIN TRAN` is not a block delimiter. It starts a **transaction state**. The state ends via `COMMIT` or `ROLLBACK`, not via an `END` keyword.

Two different `BEGIN`s:

| Syntax       | Meaning           | Paired with            |
| ------------ | ----------------- | ---------------------- |
| `BEGIN`      | Statement block   | `END`                  |
| `BEGIN TRAN` | Transaction start | `COMMIT` or `ROLLBACK` |

They look similar but are unrelated constructs. That's why one has an `END` and the other doesn't.

**Sidenote on nesting.** T-SQL supports "nested" transactions via `BEGIN TRAN` inside another `BEGIN TRAN`, but it's not real nesting — it just increments `@@TRANCOUNT`. Only the outermost `COMMIT` actually commits. Any `ROLLBACK` (even from an inner "transaction") rolls back the entire stack. This is why in practice you should avoid nested transactions unless you really understand the semantics.

---

### 1.4 Downsides of Transactions — why "only when necessary"?

Transactions are not free. Downsides:

**1. Locking and blocking.** Every open transaction holds locks. Long transactions → long lock waits → slow system. Covered above.

**2. Deadlock risk.** Two transactions each holding a lock the other needs → deadlock. One gets chosen as the victim and rolled back. Deadlocks are inherent to locking; shorter transactions reduce the window but don't eliminate it.

**3. Log growth.** Every modification inside a transaction is written to the transaction log. A long transaction with millions of row changes can balloon the log. If the log fills and can't grow, the transaction fails.

**4. Rollback cost.** Rolling back a transaction with millions of row changes can take longer than the original work — the engine has to undo each change, and it can't stop partway.

**5. Concurrency reduction.** Even without blocking, open transactions reduce overall throughput because other sessions can't see the uncommitted changes (at most isolation levels).

**6. Resource pinning.** Locks hold pages in memory; versioned rows (in snapshot modes) hold space in `tempdb`. Long transactions keep both alive longer.

**When is a transaction **necessary**?

- **Multi-statement atomicity** — several statements must all succeed or all fail together (e.g., debit + credit).
- **Consistency** across reads and writes — you read something, then write based on that read, and need the read to still be valid.
- **Implicit single-statement transactions** — every `UPDATE`/`INSERT`/`DELETE` is *already* wrapped in an implicit transaction automatically. You don't need to open one explicitly for a single statement.

**When is a transaction **not** necessary?**

- A single `SELECT` — implicitly wrapped, no explicit transaction needed.
- A single `INSERT`/`UPDATE`/`DELETE` — implicitly wrapped.
- Read-only queries that don't need consistency across statements (though consistent reads can be useful).

**The real guidance:**

> **Use explicit transactions only when multiple statements must be atomic. Keep them as short as possible. Avoid holding them open across user interaction, network calls, or any external wait.**

You don't need to wrap every query in a transaction. The engine already does that for you. Explicit transactions are for the case where the engine's automatic per-statement transaction isn't enough — when you need a *group* of statements to succeed or fail together.

**"Always use transactions" is a bad recommendation** because it leads to long-running transactions on trivial queries that don't need them, which causes more blocking, not less. The rule is: use them where atomicity matters, nowhere else.

---

## Part 2 — OFFSET and PAGING

### 2.1 What is `OFFSET ... FETCH`?

`OFFSET` and `FETCH` are the SQL standard way of doing paging in T-SQL (SQL Server 2012+):

```sql
SELECT *
FROM Orders
ORDER BY OrderDate DESC
OFFSET 20 ROWS
FETCH NEXT 10 ROWS ONLY;
```

Translation: sort by `OrderDate` descending, skip the first 20 rows, return the next 10.

Key points:

- `ORDER BY` is **mandatory** — paging without a defined order is meaningless.
- `OFFSET n ROWS` skips `n` rows.
- `FETCH NEXT m ROWS ONLY` takes `m` rows after the offset.
- `FETCH FIRST m ROWS ONLY` is a synonym — `FIRST` and `NEXT` are interchangeable here.
- `OFFSET` can appear without `FETCH` (skip rows, return the rest).
- `FETCH` cannot appear without `OFFSET`.

### 2.2 The Pattern

Standard paging formula:

- Page size: `@PageSize` (e.g., 10)
- Page number: `@PageNumber` (1-based, e.g., 3)
- Offset: `(@PageNumber - 1) * @PageSize` = 20

```sql
DECLARE @PageSize   INT = 10;
DECLARE @PageNumber INT = 3;

SELECT *
FROM Orders
ORDER BY OrderDate DESC
OFFSET (@PageNumber - 1) * @PageSize ROWS
FETCH NEXT @PageSize ROWS ONLY;
```

### 2.3 Performance Notes

**`OFFSET n` still scans the first n rows.** The engine has to find the nth row in sorted order, and if there's no useful index, that means scanning and discarding. For page 1, that's cheap. For page 10,000, it's expensive — you're still paying for all 99,990 rows you skipped.

**The fix is a good index on the `ORDER BY` column.** With a covering index, the engine can seek directly to the offset position. Without one, it sorts and discards.

**Keyset pagination (the fast alternative):**

Instead of `OFFSET`, remember the last row from the previous page and filter:

```sql
-- First page
SELECT TOP 10 *
FROM Orders
ORDER BY OrderDate DESC, OrderID DESC;

-- Next page — remember the last OrderDate + OrderID from the previous result
SELECT TOP 10 *
FROM Orders
WHERE (OrderDate, OrderID) < (@LastOrderDate, @LastOrderID)
ORDER BY OrderDate DESC, OrderID DESC;
```

This is **O(page size)** regardless of page number — the index seeks directly to where the last page ended. It's what production systems use when they need deep paging.

**Trade-offs of keyset pagination:**

- Can't jump to "page 57" — you can only go to the "next page."
- Requires a **unique** ordering column (or combination) to break ties. `OrderDate` alone has ties; `(OrderDate, OrderID)` is unique.
- Useless for "jump to last page" UI.

`OFFSET` is fine when:

- Pages are shallow (first few pages of results).
- You have a covering index on the `ORDER BY` column.
- Random access matters more than deep paging.

### 2.4 Real-World Scenarios

**Web grids.** Every grid with pagination (e-commerce product listings, admin dashboards, log viewers) uses `OFFSET`/`FETCH` under the hood, whether you write it or an ORM writes it for you. EF Core's `.Skip(n).Take(m)` generates exactly this.

**API pagination.** REST endpoints like `/api/orders?page=3&size=20` map directly to `OFFSET`/`FETCH`.

**"Load more" UI.** Often uses keyset pagination because it only ever goes forward, and deep pages need to stay fast.

**Report paging.** Large reports shown one screen at a time. Usually `OFFSET` is fine because the page count is small.

**Infinite scroll.** Always keyset. You never need to jump to page 5,000; you just need the next batch fast.

**Batch processing.** `OFFSET`/`FETCH` in a loop to process rows in chunks. But usually `TOP` with a `WHERE` filter is better (process by ID range) — `OFFSET` gets slower as the loop progresses.

### 2.5 The `ORDER BY` Requirement

You cannot write:

```sql
SELECT * FROM Orders OFFSET 20 ROWS FETCH NEXT 10 ROWS ONLY;  -- error
```

You must have `ORDER BY`. Without deterministic ordering, "page 2" isn't well-defined — SQL Server could return rows in any order, and the "same" row might appear on two pages or none.

`ORDER BY` must be on columns that give a **total order**. If ties are possible on the sort column(s), rows with the same sort key might shift between pages. Add a unique tiebreaker (`ORDER BY OrderDate DESC, OrderID DESC`) to stabilize paging.

---

## Part 3 — Table-Valued Functions vs. Result Sets from a Procedure

### 3.1 The Core Difference

A **TVF** (table-valued function) is an expression that produces a table. It's called from `FROM`, `JOIN`, or `APPLY`.

A **stored procedure** is a program that *can* return one or more result sets. It's called via `EXEC`.

| Aspect                   | TVF                                            | Result set from procedure       |
| ------------------------ | ---------------------------------------------- | ------------------------------- |
| **Called from**          | `FROM`, `JOIN`, `APPLY`                        | `EXEC` (standalone)             |
| **Composable**           | Yes — can be joined, filtered, aggregated      | No — the result is the endpoint |
| **Can modify data**      | No                                             | Yes                             |
| **Transactions**         | No                                             | Yes                             |
| **`TRY/CATCH`**          | No                                             | Yes                             |
| **Dynamic SQL**          | No                                             | Yes                             |
| **Temp tables**          | No (iTVF); local table variable only (mTVF)    | Yes                             |
| **Multiple result sets** | No — one table                                 | Yes — many                      |
| **OUTPUT parameters**    | No                                             | Yes                             |
| **Return code**          | Via `RETURN` (scalar UDF / mTVF) or N/A (iTVF) | Via `RETURN` (INT)              |
| **Inlined by optimizer** | iTVF yes; mTVF no                              | N/A — own plan                  |

### 3.2 When Each Wins

**TVF wins when:**

- The result needs to be **joined** with other tables or reused in a larger query.
- The logic is a pure transformation — filter, project, aggregate.
- You want the optimizer to inline and optimize it with the caller (iTVF).
- Encapsulation matters — a parameterized view.

**Procedure wins when:**

- You need to **modify data**.
- You need to return **multiple result sets**.
- You need `TRY/CATCH`, transactions, or dynamic SQL.
- You need temp tables for intermediate results.
- You need to return data via `OUTPUT` parameters.

### 3.3 The Practical Split

If you're designing a data-access layer:

- **Reads with parameters** → TVF, usually iTVF.
- **Writes / transactional operations** → stored procedure.
- **Complex reports returning multiple grids** → stored procedure (multiple result sets).
- **Reusable filter/join logic** → iTVF.

The rule of thumb:

> **Functions produce values. Procedures produce effects.**

If your "read" procedure is just `SELECT ... FROM Table WHERE @Param = Something`, it could just as well be an iTVF — and the iTVF would be more composable.

---

## Part 4 — Can a TVF be Considered a Parameterized View?

**Yes — specifically an iTVF.** That's exactly what it is.

A **view** is a named query without parameters:

```sql
CREATE VIEW dbo.ActiveOrders AS
SELECT * FROM Orders WHERE Status = 'Active';
```

You can filter it further, join it, aggregate it — but you can't parameterize the `WHERE` clause.

An **iTVF** is the same idea, but parameterized:

```sql
CREATE FUNCTION dbo.OrdersByStatus(@Status NVARCHAR(20))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM Orders WHERE Status = @Status
);
```

You call it as `dbo.OrdersByStatus('Active')` and treat the result as a table. From the caller's perspective, it *is* a view that accepts parameters.

**Why the comparison holds:**

- Same shape (`RETURNS TABLE ... SELECT`).
- Same usage (`FROM`, `JOIN`, `APPLY`).
- Same optimization behavior — the optimizer sees through both.
- Same limitations — read-only, no procedural logic.

**Why the comparison breaks for mTVFs:**

An mTVF is *not* a parameterized view. It's a materialized result produced by procedural code. The optimizer can't see through it, estimates are wrong, and it behaves more like a black-box data source than a view.

So the statement "TVF is a parameterized view" is true for **iTVFs** and false for **mTVFs**. If you want to be precise: **iTVF = parameterized view. mTVF = procedural table producer.**

---

## Part 5 — iTVF vs. mTVF

### 5.1 Side-by-Side

| Aspect                   | iTVF                        | mTVF                                   |
| ------------------------ | --------------------------- | -------------------------------------- |
| **Body**                 | Single `SELECT`             | `BEGIN...END` with multiple statements |
| **Return declaration**   | `RETURNS TABLE`             | `RETURNS @t TABLE (...) AS BEGIN`      |
| **Body ends with**       | `RETURN (SELECT ...)`       | `RETURN` (bare)                        |
| **Table variable**       | Not allowed                 | Required (the return variable)         |
| **Intermediate steps**   | No — single query           | Yes — multiple statements              |
| **CTE inside**           | Yes                         | Yes                                    |
| **Optimizer visibility** | Full (inlined)              | None (black box)                       |
| **Cardinality estimate** | Real (from stats)           | Always 1 row                           |
| **Parallelism**          | Yes                         | Usually no                             |
| **Predicate pushdown**   | Yes                         | No                                     |
| **Performance**          | As fast as inline query     | Often slow                             |
| **When to use**          | 95% of "I need a TVF" cases | Rare, genuinely procedural cases       |

### 5.2 What the Difference Actually Means

**iTVF** — the optimizer treats it as a **macro**. Before optimization, it substitutes the function body into the calling query. From the engine's perspective, it's as if you typed the `SELECT` inline. This is why:

- Predicates from the caller get pushed into the function's `WHERE`.
- Joins can be reordered across the function boundary.
- Statistics on underlying tables are used for cardinality estimates.
- Parallelism works normally.

**mTVF** — the optimizer treats it as a **black box**. It runs first, produces a materialized table variable, and the caller sees the result as a table with **1 estimated row**. From the engine's perspective:

- Predicates from the caller cannot be pushed into the function.
- The function runs once, fully, producing all rows.
- Downstream joins plan around the 1-row estimate — often catastrophically wrong.
- Parallelism is usually blocked.

### 5.3 The "Can I Rewrite It As an iTVF?" Test

Most mTVFs can be rewritten as iTVFs. Example: an mTVF that does

```sql
INSERT @Result SELECT ... FROM A WHERE ...
INSERT @Result SELECT ... FROM B WHERE ...
RETURN;
```

is usually expressible as:

```sql
RETURN
(
    SELECT ... FROM A WHERE ...
    UNION ALL
    SELECT ... FROM B WHERE ...
);
```

The `UNION ALL` replaces the two `INSERT`s. Most procedural-looking bodies collapse into a single `SELECT` with `UNION`/`JOIN`/`CASE`/CTEs.

**When it truly can't collapse:** when you need iterative logic (a `WHILE` loop building rows step by step), or conditional control flow that changes which query runs. These are rare. And even then, ask whether a stored procedure or app-side logic would be more appropriate.

**The rule:** if you're about to write an mTVF, first try to express the same logic as a single `SELECT`. If you can, use an iTVF. If you can't, you probably don't need a TVF at all — a stored procedure or inline query is often better.

---

## Summary Table

| Question                                  | Answer                                                                                                                                                            |
| ----------------------------------------- | ----------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| What is locking?                          | SQL Server's mechanism for preventing concurrent sessions from interfering — shared locks for readers, exclusive for writers, intent locks at higher granularity. |
| Is locking related to short transactions? | Yes. Longer transactions → more locks held longer → more blocking, more deadlocks. Keep transactions short.                                                       |
| `BEGIN TRAN` inside `BEGIN TRY`?          | Yes, in most cases. Guard `ROLLBACK` with `IF @@TRANCOUNT > 0`. Consider `SET XACT_ABORT ON`.                                                                     |
| Why no `END TRANSACTION`?                 | `COMMIT` and `ROLLBACK` are the two terminal states. No third state, so no `END` keyword.                                                                         |
| Downside of transactions?                 | Locking, blocking, deadlocks, log growth, rollback cost, reduced concurrency. Use only when multiple statements must be atomic.                                   |
| Should you "always" use transactions?     | No. Single statements are implicitly transactional. Explicit transactions only when atomicity across statements is required.                                      |
| What is `OFFSET ... FETCH`?               | SQL standard paging. Skips rows and returns a window. Requires `ORDER BY`.                                                                                        |
| Real-world paging?                        | Web grids, APIs, reports, "load more." `OFFSET` for shallow pages; keyset pagination for deep pages.                                                              |
| TVF vs. procedure result set?             | TVF is composable (joinable, filterable), read-only. Procedure can modify data, return multiple result sets, has full error handling and transactions.            |
| Is a TVF a parameterized view?            | Yes, for iTVFs. mTVFs are procedural black boxes, not views.                                                                                                      |
| iTVF vs. mTVF?                            | iTVF = single `SELECT`, inlined, fast, estimable. mTVF = multi-statement, black box, 1-row estimate, slow. Prefer iTVF.                                           |

---

---

### OG Qs

Tramsations Qs

What's is Locking in transactions , is that related to keeping trasactions small and simple ?

write tthe begin Transaction statement inside the begin try statement in better for more error handling and ensure that the entire transaction lifecycle is entirely managed within the try-catch ??

why there's no END Transaction statment ?

is thre any downsides for using trasnations ? why only to use it whne neccessary or alwyas as reccomdatios ?

SQL OFFEST AND PAGINF ?

real world scianrios ?

Table-vuluaed function vs Result set retrun by a procedur ?

can we consier a TVF as a paramerterized view ?

diff beween ITVF and MTVF
