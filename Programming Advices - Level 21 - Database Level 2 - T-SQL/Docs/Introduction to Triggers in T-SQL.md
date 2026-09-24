# Introduction to Triggers in T-SQL

---

## 1. What is a Trigger?

A **trigger** is<mark> <u>a special kind of stored procedure</u> that fires **automatically** <u>**in response to an event**</u> on a table or database</mark>. <u>**You don't call it**</u> — <mark><u>**the engine does**</u></mark>, when the triggering event happens.

Mental model:

| Object           | Called by                                        |
| ---------------- | ------------------------------------------------ |
| Stored procedure | Application code, explicitly via `EXEC`          |
| Function         | Queries, as an expression                        |
| **Trigger**      | **The database engine, in response to an event** |

**<u>Triggers are attached to a specific target</u>** (a table, a view, or the database itself) and a specific event (`INSERT`, `UPDATE`, `DELETE`, `CREATE TABLE`, `DROP`, login events, etc.).

---

## 2. What Triggers Are Used For

Triggers exist to react to data or schema changes that have already been requested — you can't stop the caller from issuing the statement, but you can *respond* to it.

Common use cases:

- <mark>**Enforcing business rules**</mark> that can't be expressed as `CHECK` constraints (e.g., cross-table validation).
- <mark>**Auditing and logging**</mark> — capture who changed what, when, and what the old/new values were.
- <mark>**Data synchronization**</mark> — propagate changes to related tables or a history table.
- <mark>**Automatic updates**</mark> — derive values on one table based on changes to another.

All four are legitimate, but <mark><u>**trigger-based logic is *hard to see***</u></mark> — <u>**it doesn't appear in the caller's code**</u>. <mark>The trade-off is</mark> always: <u>**enforcement and encapsulation vs. invisibility and debuggability**</u>.

---

## 3. Anatomy of a Trigger

A trigger has three parts:

| Component                  | Meaning                                                            |
| -------------------------- | ------------------------------------------------------------------ |
| <mark>**Event**</mark>     | What fires it — `INSERT`, `UPDATE`, `DELETE`, or DDL events        |
| <mark>**Condition**</mark> | Optional logic inside the trigger body that decides whether to act |
| <mark>**Action**</mark>    | The SQL that runs when the trigger fires                           |

Created via `CREATE TRIGGER`. Unlike stored procedures, <mark><u>**triggers take no parameters**</u></mark> — the engine supplies the affected rows through the `inserted` and `deleted` pseudo-tables.

---

## 4. Types of Triggers

### 4.1 By timing

| Type                   | When it fires                                        | Typical use                                                                                                     |
| ---------------------- | ---------------------------------------------------- | --------------------------------------------------------------------------------------------------------------- |
| **AFTER** (a.k.a. FOR) | After the triggering statement has modified the data | <u>**Auditing, cascading updates, logging**</u>                                                                 |
| **INSTEAD OF**         | *Instead of* the triggering statement                | <mark>Override default behavior</mark>; <u>**used on views to make them updatable**</u>; complex business rules |

### 4.2 By scope

| Type              | Attached to          | Events                                   |
| ----------------- | -------------------- | ---------------------------------------- |
| **DML trigger**   | A table or view      | `INSERT`, `UPDATE`, `DELETE`             |
| **DDL trigger**   | A database or server | `CREATE`, `ALTER`, `DROP`, `GRANT`, etc. |
| **Logon trigger** | The server           | `LOGON`                                  |

### 4.3 By scope of execution

| Type                                            | Fires once per statement | Fires once per row |
| ----------------------------------------------- | ------------------------ | ------------------ |
| **Statement-level** (T-SQL default)             | ✅                        | ❌                  |
| **Row-level** (not directly supported in T-SQL) | ❌                        | ✅                  |

<mark>**This is the important one.**</mark> SQL Server <mark><u>triggers are **statement-level**, not row-level</u></mark>. A single `UPDATE` that affects 10,000 rows <u>fires the trigger **once**</u>, and the `inserted`/`deleted` pseudo-tables contain **all 10,000 rows**. <mark>Oracle</mark>, in contrast, <mark><u>**supports both statement-level and row-level triggers**</u></mark> (`FOR EACH ROW`). This is a common source of bugs when developers coming from Oracle write T-SQL triggers expecting one call per row.

---

## 5. The `inserted` and `deleted` Pseudo-Tables

Inside an `AFTER` DML trigger, two read-only tables are available:

| Pseudo-table | Contains                           |
| ------------ | ---------------------------------- |
| `inserted`   | New rows (after the modification)  |
| `deleted`    | Old rows (before the modification) |

Behavior per operation:

| Operation | `inserted` | `deleted`  |
| --------- | ---------- | ---------- |
| `INSERT`  | New rows   | Empty      |
| `DELETE`  | Empty      | Old rows   |
| `UPDATE`  | New values | Old values |

For an `UPDATE`, you can join `inserted` to `deleted` on the primary key to see old-vs-new per row. This is how audit triggers capture before/after values.

For `INSTEAD OF` triggers, the same pseudo-tables exist, but since the underlying modification hasn't happened yet, `inserted`/`deleted` reflect what the caller was *trying* to do.

---

## 6. Why Triggers Are Tricky

Triggers are powerful but carry real hazards.

<mark>**6.1 They're invisible at the call site.**</mark>
An `INSERT` in application code can trigger side effects the developer never sees. Debugging "why did this row appear in `AuditLog`?" can take hours.

<mark>**6.2 Statement-level, not row-level.**</mark>
A trigger must handle **all rows** in `inserted` / `deleted`. Writing trigger logic as if it runs once per row is the classic bug. Example: a trigger that does `SELECT @id = ID FROM inserted` is broken — it silently uses only one of the affected rows.

<mark>**6.3 Recursion and nesting.**</mark>
<mark><u>**A trigger can fire another trigger, which fires another**</u></mark>. SQL Server limits nesting (32 levels by default) and has options to control recursion (`RECURSIVE_TRIGGERS`), but <mark><u>**runaway cascades are possible**</u></mark>.

**6.4 Performance.**
Triggers run inside the caller's transaction. <mark>A slow trigger slows every</mark> `INSERT`/`UPDATE`/`DELETE` on the table. <mark>It can also hold locks longer</mark>, blocking other sessions.

<mark>**6.5 Error handling.**</mark>
If a trigger raises an error, the entire statement (and transaction, if applicable) rolls back. Sometimes that's what you want; sometimes it silently masks the caller's intended operation.

**6.6 Hard to test.**
Trigger logic **<u>runs as a side effect of data changes</u>**. <mark><u>**Unit-testing triggers is awkward**</u></mark> compared to testing functions or procedures.

**6.7 Deployment and version control.**
Triggers live in the database, so <mark><u>**they're harder to diff and review than application code**</u></mark>. Changes go through `ALTER TRIGGER` on each environment.

---

## 7. When to Use a Trigger vs. Alternatives

| Need                                                  | Prefer                                                                |
| ----------------------------------------------------- | --------------------------------------------------------------------- |
| Enforce a simple value constraint                     | `CHECK` constraint                                                    |
| Enforce a foreign-key relationship                    | `FOREIGN KEY`                                                         |
| Default value for a column                            | `DEFAULT` constraint                                                  |
| Compute a derived column                              | Computed column or view                                               |
| <mark>Enforce a cross-table business rule</mark>      | Trigger (if declarative constraints can't express it)                 |
| <mark>Audit who changed what</mark>                   | Trigger (writes to an audit table)                                    |
| <mark><u>**Cascade updates across tables**</u></mark> | `FOREIGN KEY ... ON UPDATE CASCADE` where possible; trigger otherwise |
| Application-side validation                           | Application code (usually clearer and testable)                       |

> <mark><u>**Rule of thumb:**</u></mark> reach for a trigger only when the constraint genuinely *cannot* be expressed declaratively and the enforcement must be at the database level. Otherwise, prefer a constraint or application code.

---

<img title="" src="https://uploads.teachablecdn.com/attachments/8lS041bYQQmxpM54gcVh_t3.png" alt="" data-align="center" width="366">

---

## 8. Summary

| Aspect              | Trigger                                                      |
| ------------------- | ------------------------------------------------------------ |
| Fires               | Automatically in response to an event                        |
| Called by           | The engine, not the application                              |
| Parameters          | None — uses `inserted` / `deleted` pseudo-tables             |
| Types               | AFTER, INSTEAD OF                                            |
| Scope               | Table / view (DML), database / server (DDL, LOGON)           |
| Granularity         | Statement-level in T-SQL (not row-level)                     |
| Transaction context | Runs inside the caller's transaction                         |
| Common uses         | Business rules, auditing, synchronization, auto-updates      |
| Trade-offs          | Invisible at call site, hard to debug, performance-sensitive |
| Created by          | `CREATE TRIGGER`                                             |
| Best practice       | Use sparingly; prefer declarative constraints when possible  |

---

## 9. Next Steps

- Write an `AFTER INSERT` trigger that logs new rows to an audit table — verify that a single multi-row `INSERT` produces multiple rows in the log (proving statement-level, not row-level, behavior).
- Write the same trigger as `INSTEAD OF INSERT` and observe how it must explicitly perform the insert the caller wanted.
- Test what happens when the trigger raises an error: does the caller's statement roll back? Does the transaction roll back?
- Write a DDL trigger that blocks `DROP TABLE` and observe it firing on schema changes.
- Compare trigger-based auditing against Change Data Capture (CDC) and temporal tables — both are built-in mechanisms Microsoft provides specifically for change tracking, and they're often better than rolling your own.
