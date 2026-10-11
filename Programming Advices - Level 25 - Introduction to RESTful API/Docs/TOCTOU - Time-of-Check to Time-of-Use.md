# TOCTOU — Time-of-Check to Time-of-Use

## What TOCTOU means

<mark>**TOCTOU** = **T**ime-**O**f-**C**heck **T**o **T**ime-**O**f-**U**se.</mark>

It's a race condition where:

1. Your code **checks** some condition (the "check").
2. Later, your code **acts** on that check (the "use").
3. <mark>**In between**, **<u>the world can change</u>**</mark> — so the check is stale by the time you use it.

<mark>The check and the use are supposed to be atomic</mark>, <mark>**<u>but they're not</u>**</mark>. <mark>**Something** **<u>can slip in between</u>**</mark>.

---

## The classic non-code example

You check your bank account balance before buying something. The app says: "You have $500." You click Buy for a $400 item. In the time between the check and the purchase, another charge hits your account for $300. Now you have $200, and the $400 purchase fails — or worse, overdraws you.

You checked. The value was correct **at the time**. By the time you acted, the world had changed. The check was stale.

That's TOCTOU.

---

## The classic code example

```csharp
if (File.Exists(path))
{
    // between here and the next line, another process could delete the file
    File.Delete(path);   // ← might throw FileNotFoundException
}
```

`File.Exists` returned `true`. By the time `File.Delete` runs, the file might be gone. The check and the use weren't atomic.

The "correct" version:

```csharp
try
{
    File.Delete(path);   // attempt; catch if it fails
}
catch (FileNotFoundException)
{
    // handle the case where the file is already gone
}
```

No check. Just try. Handle failure.

---

## Where TOCTOU shows up in your project

This is the code we discussed:

```csharp
Student? student = Student.Find(id);       // ← CHECK: does the student exist?
if (student == null)
    return NotFound($"Student with ID {id} not found.");

if (!Student.DeleteStudent(id))            // ← USE: delete it
    return StatusCode(500, $"Failed to delete student with ID {id}.");
```

**The check:**

`Find(id)` runs `SELECT * FROM Students WHERE Id = @id`. It returns a student — the row exists **at this moment**.

**The use:**

`DeleteStudent(id)` runs `DELETE FROM Students WHERE Id = @id`. It expects the row to still be there.

**The gap:**

Between the `SELECT` and the `DELETE`, another request could delete the same student. What happens then?

**The outcome:**

The `DELETE` affects 0 rows. `DeleteStudent(id)` returns `false`. Your code sees `false` and returns `500 Internal Server Error`.

But `500` is wrong. The row didn't disappear because of a server error. It disappeared because **someone else deleted it first**. The correct answer is `404 Not Found` — the student doesn't exist (anymore).

You returned the wrong status code because your code assumed the world was the same between the check and the use. It wasn't.

---

## Why this is called a "race"

Two requests "race" to modify the same data. Depending on which wins:

- **Case A:** Request 1 finds the student, deletes it. Request 2 finds nothing → `404`. Correct.
- **Case B:** Request 1 finds the student. Request 2 also finds the student. Request 1 deletes. Request 2 tries to delete → 0 rows → your code returns `500`. **Wrong.**

The outcome depends on timing. Sometimes it's correct, sometimes it's not. That's what "race" means — the result depends on the interleaving.

---

## The fix — make the check and the use atomic

The delete already tells you whether the row existed. Zero rows affected = didn't exist. So you don't need the separate check.

```csharp
if (!Student.DeleteStudent(id))
    return NotFound($"Student with ID {id} not found.");

return NoContent();
```

**One operation.** The delete is atomic from the database's perspective. It either removes 1 row (it existed) or 0 rows (it didn't). No gap, no race.

The result — "was the row there when I deleted it?" — is answered by the delete itself, not by a separate read that can go stale.

---

## The general principle

**Don't check-then-act if you can act-and-check.**

| Pattern                                          | Race-prone?  |
| ------------------------------------------------ | ------------ |
| `Find` then `Delete`                             | ✅ Race-prone |
| `Delete` and check row count                     | ❌ Race-free  |
| `Exists` then `Create`                           | ✅ Race-prone |
| `Create` and handle duplicate-key error          | ❌ Race-free  |
| `GetBalance` then `Withdraw`                     | ✅ Race-prone |
| `Withdraw` with a WHERE balance >= amount clause | ❌ Race-free  |

<mark>The pattern "act-and-check" means</mark>: perform the operation with a constraint that captures your condition, then interpret the result. The database (or the OS, or the file system) enforces the check atomically.

---

## When you can't avoid check-then-act

Sometimes you genuinely need to read data before acting on it:

- **Authorization.** You need the student's owner to decide if the caller can delete it.
- **Audit logging.** You want to log the student's state before deleting.
- **Cascade logic.** You need fields from the loaded entity to update related rows.

In those cases, you **have to** do `Find` first. But you must handle the race:

```csharp
Student? student = Student.Find(id);
if (student == null)
    return NotFound($"Student with ID {id} not found.");

// ... use student for authorization, logging, cascades ...

if (!Student.DeleteStudent(id))
    return NotFound($"Student with ID {id} was already deleted.");
    //   ^ 404, not 500 — the row is gone either way

return NoContent();
```

The status is still `404`. Because the truth is: "this student doesn't exist." Whether it never existed or was deleted a millisecond ago, the caller gets the same answer.

<mark>**Rule:**</mark> if the check and the use are separate, and the use fails because the state changed, return the status that matches the *current* reality — not the status that matches what the check *said* earlier.

---

## Database transactions and TOCTOU

<mark>**A common misconception**</mark>: "if I wrap the `Find` and `Delete` in a transaction, I'm safe."

**Not always true.** It depends on the isolation level.

**`READ COMMITTED`** (the SQL Server default): the transaction sees committed data. If another transaction deletes the row between your `SELECT` and `DELETE`, your `SELECT` already happened with the row present, and your `DELETE` sees 0 rows. Same race.

**`SERIALIZABLE` or `REPEATABLE READ`:** the `SELECT` locks the row until the transaction ends. Now the race is prevented — no other transaction can delete the row between your `SELECT` and your `DELETE`. But: this holds locks longer, blocks other writers, increases deadlock risk, and is usually overkill.

**The cheaper fix:** don't do the separate check. Act-and-check, one statement. No transaction needed.

For the case where you genuinely need the read (authorization, logging), a transaction with `READ COMMITTED` still has the race — you'd need `UPDLOCK` or higher isolation to close it. That's a bigger discussion. For now, know that transactions aren't a free fix.

---

## Other TOCTOU examples you'll run into

**User registration:**

```csharp
var user = db.Users.FirstOrDefault(u => u.Email == email);
if (user != null) return BadRequest("Email already registered.");
db.Users.Add(new User { Email = email });   // ← race: another request could insert the same email
```

Fix: add a unique index on `Email`. The database rejects the duplicate. Catch the constraint violation and return `400`.

**File operations:**

```csharp
if (File.Exists(path)) File.Delete(path);   // ← race
```

Fix: just `try { File.Delete(path); } catch (FileNotFoundException) { }`.

**Inventory decrement:**

```csharp
var stock = db.Products.Where(p => p.Id == id).Select(p => p.Stock).First();
if (stock >= quantity)
    product.Stock -= quantity;
    db.SaveChanges();
```

Fix: atomic UPDATE with a `WHERE Stock >= quantity` clause. Check the row count.

**Cache invalidation:**

```csharp
var cached = cache.Get(key);
if (cached == null)
{
    cached = await FetchFromDatabase(key);
    cache.Set(key, cached);   // ← another thread could have already cached a newer value
}
```

Fix: use `GetOrAdd` (atomic) or accept that the "redundant fetch" is fine (idempotent).

Same pattern everywhere: check, gap, use. If the gap matters, eliminate it by making the operation itself the check.

---

## Summary

| Concept                              | Meaning                                                                               |
| ------------------------------------ | ------------------------------------------------------------------------------------- |
| **TOCTOU**                           | Time-of-Check to Time-of-Use — a race where state changes between checking and acting |
| **Root cause**                       | The check and the use aren't atomic; something can change in between                  |
| **Symptom**                          | Wrong status codes, spurious failures, inconsistent behavior under load               |
| **Fix (preferred)**                  | Act-and-check: perform the operation atomically, interpret the result                 |
| **Fix (when you must read first)**   | Handle the failure case explicitly; return the status matching current reality        |
| **Not a fix**                        | `READ COMMITTED` transactions — they don't prevent the race by default                |
| **Correct fix for high-concurrency** | `SERIALIZABLE`, `UPDLOCK`, or a unique constraint — depends on the operation          |

---

## Applied to your project

**Your `DeleteStudent`:**

```csharp
// BEFORE — check-then-act, race-prone
Student? student = Student.Find(id);
if (student == null)
    return NotFound(...);

if (!Student.DeleteStudent(id))
    return StatusCode(500, ...);   // ← wrong status on race
```

**AFTER — act-and-check, race-free**

```csharp
// The DELETE itself tells us if the row existed.
if (!Student.DeleteStudent(id))
    return NotFound($"Student with ID {id} not found.");

return NoContent();
```

Same change applies to any other endpoint where you have a separate read before a write, and the read is only used to check existence.

---

## The mental rule

> **<mark>If the operation you're about to perform already tells you the answer, <u>don't ask a separate question first</u>.</mark>**

`DELETE` tells you if the row existed. `INSERT` with a unique constraint tells you if it was already there. `UPDATE ... WHERE balance >= amount` tells you if the balance was sufficient. <mark><u>The operation **is** the check</u></mark>.

<mark>The check-then-act pattern **feels natural** because **it mirrors <u>how you think</u>**</mark> ("first I check, then I act"). <mark>**But <u>it's the wrong shape for concurrent systems</u>**</mark>. Act, then react to what happened.

---
