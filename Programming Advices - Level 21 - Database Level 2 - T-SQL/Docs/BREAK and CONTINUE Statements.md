## BREAK and CONTINUE Statements

---

    `BREAK` and `CONTINUE` statements in Transact-SQL (T-SQL), which is used with Microsoft SQL Server. These statements are primarily used within loops to control the flow of execution.

---

### Understanding `BREAK` and `CONTINUE` in T-SQL

#### 1. Introduction to Loops in T-SQL

- Loops in T-SQL, like in other programming languages, are used to execute a set of statements repeatedly until a specified condition is met.
- The most common loop in T-SQL is the `WHILE` loop.

#### 2. `BREAK` Statement

- Purpose: The `BREAK` statement is used to immediately exit the loop, regardless of whether the loop condition is still true.
- Usage: Typically used when a certain condition is met inside the loop, and there is no need to continue looping.

**Example:**

![](https://uploads.teachablecdn.com/attachments/cVsbA1rATM2NdiPNuj5i_1.JPG)

#### 3. `CONTINUE` Statement

- Purpose: The `CONTINUE` statement is used to skip the rest of the loop body and immediately start the next iteration of the loop.
- Usage: Commonly used to skip certain iterations based on a condition.

**Example:**

![](https://uploads.teachablecdn.com/attachments/UfYEQaiuQzqlALtZDflW_2.JPG)

---

#### 4. Key Differences and Usage Scenarios

- Use `BREAK` when you want to exit the loop entirely.
- Use `CONTINUE` when you want to skip the current iteration and proceed with the next iteration.

---
