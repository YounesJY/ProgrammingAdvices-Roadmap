# Introduction to Cursors in T-SQL

---

## 1. What is a Cursor?

A **cursor** is <mark>a database object that <u>lets you process a result set **one row at a time**</u>, <u>instead of in one set-based</u> operation</mark>.

- Normal SQL: "update all rows where X" — <mark>one statement, all rows</mark>.
- Cursor: "for each row in this result set, do something" — <mark><u>**one iteration per row**</u></mark>.

It bridges set-based SQL and procedural row-by-row programming (like a `foreach` loop in C#).

![](https://uploads.teachablecdn.com/attachments/4eFmR7i1TFu3zQOue9kK_t3.png)

---

## 2. Why Use Cursors?

<mark>SQL is designed for set-based operations, <u>**so cursors should be rare**</u></mark>. But there are cases where they fit:

- <mark>**Sequential processing**</mark> — the data must be processed in a specific order, one row at a time.
- <mark>**Complex per-row logic** </mark>— each row needs decision-making that's hard or impossible to express in one query.
- <mark>**Interactivity**</mark> — the application steps through rows individually (e.g., scrolling through records).

Outside these cases, a set-based solution is almost always faster and cleaner.

---

## 3. Types of Cursors

| Type             | Behavior                                                                                                           |
| ---------------- | ------------------------------------------------------------------------------------------------------------------ |
| **Static**       | <mark>Snapshot of the data</mark> when the cursor opens. Later changes to the base table <mark>aren't seen.</mark> |
| **Dynamic**      | <mark>Reflects changes</mark> made to the data <mark>while the cursor is open.</mark>                              |
| **Forward-Only** | Can <mark>only move forward</mark> through the rows. <mark>Fastest and most common.</mark>                         |
| **Scrollable**   | Can move forward, backward, and jump to specific rows.                                                             |

> <mark>The default in T-SQL is forward-only, read-only — **<u>usually the least expensive option</u>**.</mark>

---

## 4. <mark><u>**Performance Considerations**</u></mark>

<mark><u>**Cursors are heavier than set-based operations**</u></mark>. Main issues:

- **Overhead** — the engine processes rows one at a time, with per-row bookkeeping. Large datasets amplify this.
- **Locking and concurrency** — cursors can hold locks longer, blocking other sessions.
- **Alternatives usually exist** — most cursor problems can be rewritten as a single `UPDATE`/`INSERT`/`DELETE` with `JOIN`, or as a set-based query.

Rule of thumb: before writing a cursor, ask whether a set-based rewrite is possible. If yes, do that.

---

## 5. <mark><u>**Best Practices**</u></mark>

- <mark><u>**Minimize cursor use.**</u></mark> Only when set-based alternatives truly don't fit.
- **Keep transactions short.** If the cursor runs inside a transaction, don't let it hold the transaction open across many iterations.
- **Pick the cheapest cursor type.** Forward-only + read-only is usually enough.
- **Always close and deallocate.** Leaving cursors open wastes server resources and can hold locks.

The standard lifecycle:

```sql
DECLARE cursor_name CURSOR FOR <query>;
OPEN cursor_name;
FETCH NEXT FROM cursor_name INTO @vars;
WHILE @@FETCH_STATUS = 0
BEGIN
    -- process the current row
    FETCH NEXT FROM cursor_name INTO @vars;
END
CLOSE cursor_name;
DEALLOCATE cursor_name;
```

---

## 6. Summary

| Aspect          | Cursor                                                      |
| --------------- | ----------------------------------------------------------- |
| Purpose         | Process a result set row by row                             |
| When to use     | Sequential processing, complex per-row logic, interactivity |
| When not to use | Anything a single set-based statement can do                |
| Common types    | Static, Dynamic, Forward-Only, Scrollable                   |
| Cost            | High overhead, locking, concurrency impact                  |
| Best practice   | Rarely; close and deallocate always                         |
| Trade-off       | Procedural flexibility vs. set-based performance            |

---

## 7. Next Steps

- Write a simple forward-only cursor over `Students` and print each row.
- Rewrite the same loop as a single set-based query and compare the execution plans.
- Try the same cursor as `STATIC` vs `DYNAMIC` and observe how each responds to a concurrent `UPDATE` on the base table.
- Inspect `sys.dm_exec_cursors` while a cursor is open to see what's happening under the hood.
