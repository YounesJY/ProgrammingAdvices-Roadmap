## WHILE Loops - Example 1 - Simple Counter

---

#### **Introduction to WHILE Loops in T-SQL**

    A WHILE loop in T-SQL is a control-of-flow language construct that allows the execution of a specified block of SQL statements repeatedly as long as a specified condition is true.

----

#### **Basic Syntax**

WHILE [condition]
BEGIN
    -- SQL statements to be executed
END

- `condition`: A Boolean expression. If it evaluates to true, the loop continues; if false, the loop stops.  

---

#### **Using a WHILE Loop**

WHILE loops are often used for repetitive tasks where the number of iterations isn't known beforehand or to iterate through records in a table one row at a time.

### <mark>There is No For loop or Do .. While loop In T-SQL</mark>

In T-SQL (Transact-SQL, used with Microsoft SQL Server), there are no `FOR` or `DO WHILE` statements as you would find in many other programming languages. The primary looping constructs available in T-SQL are the `WHILE` loop <mark>and the `CURSOR`</mark>, which is used to iterate over a result set row by row.

### Only WHILE Loop

The `WHILE` loop is the primary means for performing repeated actions in T-SQL and it works similarly to `WHILE` loops in other programming languages. It executes a block of statements as long as a specified condition is true.

---
