# Recursive CTEs — What We've Covered

---

## 1. Syntax

**Fixed template — no flexibility:**

```sql
WITH CTE_Name AS (
    <anchor: SELECT ...>
    UNION ALL
    <recursive member: SELECT ... FROM CTE_Name ...>
)
SELECT ... FROM CTE_Name;
```

- `WITH` at the top
- Anchor runs first (produces the starting rows)
- `UNION ALL` between the two members
- Recursive member references the CTE's own name
- Only **one** recursive reference allowed per member
- `UNION ALL` is required (not `UNION`)

---

## 2. Execution Model

**Iterative loop, not function recursion. No call stack.**

```
accumulator = anchor result
working     = accumulator

while working is not empty:
    new_rows = recursive_member(working)
    accumulator = accumulator UNION ALL new_rows
    working     = new_rows

return accumulator
```

- **Anchor** runs once, populates both accumulator and working set.
- **Recursive member** runs against the **working set** (previous iteration's output) — **not** the accumulator.
- Each iteration produces **one level deeper** in the hierarchy.
- Termination: when the recursive member produces zero rows.

**No stack, no frames, no unwinding.** The engine implements this as a loop with two internal worktables in `tempdb`.

---

## 3. The Two Hidden Sets

| Concept         | Meaning                                                             |
| --------------- | ------------------------------------------------------------------- |
| **Working set** | Last iteration's output. Input for the next iteration.              |
| **Accumulator** | Union of all rows produced so far. What the CTE returns.            |
| **Worktable**   | Internal `tempdb` storage for the above. Real pages, not queryable. |

The recursive reference resolves to the **working set**, not the accumulator. This is why the recursion walks one level at a time instead of re-processing earlier rows.

---

## 4. Column Matching Rules

Anchor and recursive member must match **three ways**:

| Rule                          | Enforced by SQL Server?           |
| ----------------------------- | --------------------------------- |
| Same number of columns        | ✅ Yes — parse error if not        |
| Same data type per position   | ✅ Yes — "Types don't match" error |
| Same **meaning** per position | ❌ No — silent wrong behavior      |

The third rule is the dangerous one. The engine lines up columns by **ordinal position**, not by name. If position 1 means `EmployeeID` in the anchor but `ManagerID` in the recursive member, the query runs but loops forever — and you get `MAXRECURSION` exhausted.

---

## 5. Type Matching in Detail

Types must match **exactly**, not just be compatible.

- `VARCHAR(50)` ≠ `VARCHAR(MAX)` — error
- `VARCHAR` ≠ `NVARCHAR` — error
- `INT` ≠ `BIGINT` — error

**Common fix:** explicitly `CAST` both sides to the same type. For string-building hierarchies, cast both to `VARCHAR(MAX)` or `NVARCHAR(MAX)`.

```sql
-- Anchor
Hierarchy = CAST(Name AS NVARCHAR(MAX))

-- Recursive member
Hierarchy = CONCAT(ETH.Hierarchy, ' -> ', e.Name)   -- CONCAT returns NVARCHAR(MAX)
```

`CONCAT` returns `NVARCHAR(MAX)`, so the anchor must be cast to `NVARCHAR(MAX)` (not `VARCHAR(MAX)`) to match.

---

## 6. Recursion Limits

- Default `MAXRECURSION = 100` iterations.
- If the recursive member keeps producing rows beyond 100 iterations → error: *"The statement terminated. The maximum recursion 100 has been exhausted."*
- Override with `OPTION (MAXRECURSION n)` in the outer query.
- `MAXRECURSION 0` = unlimited (dangerous if the query can loop).

**This is a safety cap, not a design target.** If your query naturally needs 100+ levels, raise the cap. If it *hits* 100 and errors, your query has a bug or the data has a cycle.

---

## 7. Common Causes of Infinite Recursion

| Cause                                                                                              | Fix                                                                      |
| -------------------------------------------------------------------------------------------------- | ------------------------------------------------------------------------ |
| Join condition reversed (`e.EmployeeID = ETH.ManagerID` instead of `e.ManagerID = ETH.EmployeeID`) | Flip to walk down the hierarchy                                          |
| Column order swapped between anchor and recursive member                                           | Make the order consistent                                                |
| Actual cycle in the data (A manages B, B manages A)                                                | Add cycle detection (path column + `WHERE`) or use `MAXRECURSION` to cap |
| Termination condition missing or wrong                                                             | Ensure the recursive member eventually produces zero rows                |

---

## 8. The Debugging Habit

When a recursive CTE hits `MAXRECURSION`, check in this order:

1. **Column order** — does position N mean the same thing in both members?
2. **Join direction** — is the join walking down (correct) or sideways (loop)?
3. **Data cycles** — does the hierarchy actually have a cycle?
4. **Termination** — can the recursive member always produce new rows?

Numbers 1 and 2 are the most common silent bugs. They don't produce errors — just loops.

---

## 9. What Recursive CTEs Are Good For

- **Hierarchies:** org charts, category trees, bill-of-materials.
- **Graph traversal:** following edges through a graph (with cycle detection).
- **Number/date series generation:** 1..N, calendar dates between two dates.
- **Running totals with complex logic:** when window functions won't fit.
- **Any "keep applying a rule until nothing new"** problem.

---

## 10. Key Mental Model

The single most important idea:

> **The recursive member reads from the previous iteration's output, not from the accumulated result.**

This is why:

- The recursion walks one level at a time.
- Termination happens naturally when a level produces no rows.
- Column meaning must stay consistent across iterations.
- Reversing the join condition sends the walk in the wrong direction.

If you remember only one thing, remember this.

---

## 11. Practical Rules to Carry Forward

- **Anchor first, `UNION ALL`, recursive member.** Fixed syntax.
- **Anchor and recursive member must list columns in the same order, with the same types.**
- **Cast explicitly** when the two sides might infer different types.
- **The recursive reference is the working set**, not the accumulator.
- **`MAXRECURSION` default is 100** — raise it only if the query legitimately needs more depth.
- **If it hits `MAXRECURSION`, suspect a bug** before raising the cap.
- **Termination happens when the recursive member returns no rows** — nothing else.

---

## 12. What We Haven't Covered (If You Want to Go Deeper)

- Cycle detection in recursive CTEs (the path-tracking pattern).
- Recursive CTEs with multiple anchor members (`UNION ALL` between two anchors).
- Using recursive CTEs to generate date ranges.
- `OPTION (MAXRECURSION 0)` trade-offs.
- Performance: recursive CTEs are `tempdb`-heavy; alternatives exist for many problems (window functions, `STRING_AGG`, closure tables).
- Comparing recursive CTE to other hierarchy techniques (`hierarchyid`, nested sets, materialized paths).

If you want, we can drill into any of those next.
