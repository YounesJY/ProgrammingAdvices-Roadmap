# The rules for updatable views in SQL Server

### 1. Single-table views

**True.** <mark>A view that <u>selects from one base table</u></mark> (with optional `WHERE`, column list, computed expressions excluded) <mark><u>**is updatable**</u></mark>. `INSERT`/`UPDATE`/`DELETE` **against the view <mark>propagate to the base table</mark>**, subject to the same rules as direct DML.

Limitations:

- Can't update columns derived from expressions, aggregates, or `CASE`.
- Can't insert into columns without a value (no default).
- `WITH CHECK OPTION` can block rows that fall outside the view's filter.

### 2. Multi-table views

**Not quite right as stated.** The rule isn't "one table at a time." It's more specific:

- **`UPDATE` and `DELETE`** against a multi-table view modify **only one base table** — the one whose columns are being updated, or (for DELETE) the one the view is "primarily" derived from. The engine picks this based on the query shape.
- <mark>**`INSERT`** against a multi-table view is **not allowed at all** </mark>(except in specific outer-join cases where one table is nullable). You can't insert into a view that spans multiple tables with `JOIN`, because the engine can't infer which base table each column belongs to.

So "one table at a time" is accurate for `UPDATE`/`DELETE`, but misses that **`INSERT` simply can't happen on a multi-table view**.

### 3. If you need to update both tables

**True.** Use an `INSTEAD OF` trigger. This is the classic reason `INSTEAD OF` exists — to make a view that spans multiple base tables fully updatable (`INSERT`, `UPDATE`, `DELETE`), by splitting the operation across the underlying tables in whatever way makes sense for your data model.

---

## The corrected version

Here's what I'd write in your MD:

> **Single-table views are updatable.** `INSERT`, `UPDATE`, and `DELETE` against the view propagate to the underlying base table, subject to the usual rules.
> 
> **Multi-table views are partially updatable.**
> 
> - `UPDATE` and `DELETE` modify exactly one base table — the one the affected columns belong to (for `UPDATE`) or the primary table in the view's `FROM` (for `DELETE`). You can't update columns from multiple base tables in one statement.
> - `INSERT` is generally **not allowed** on a multi-table view, because the engine can't determine which base table each column targets.
> 
> **If you need to modify multiple base tables through a single view operation, use an `INSTEAD OF` trigger.** The trigger intercepts the caller's statement and performs the necessary `INSERT`/`UPDATE`/`DELETE` on each base table explicitly.

---

## Why this matters

The distinction is:

- **Single-table view** = transparent proxy. Reads and writes pass through.
- **Multi-table view** = partially transparent. Reads work; writes work for `UPDATE`/`DELETE` on the "primary" table; `INSERT` doesn't work; multi-table writes don't work.
- **INSTEAD OF trigger** = you implement the write logic yourself. Full control.

If your MD is going into a "views" section, adding this clarifies why `INSTEAD OF` triggers exist as a distinct tool, rather than being a generic "triggers" curiosity.
