## CASE Statement

### CASE Statement in T-SQL

> REMEMBER: CASE statemnt <mark>WILL ALWAYS retrun a scalar value, a signle field</mark>, so it can be used anywhere in many ways

---

#### **Introduction**

<mark><u>T-SQL does not have a dedicated </u> `SWITCH` <u> statement</u></mark> as found in many programming languages. Instead, the `CASE` statement serves a similar purpose, allowing for conditional logic based on specific values or conditions. It's the closest equivalent to a `SWITCH` statement in T-SQL.



> In T-SQL, the `CASE` statement is <mark><u>primarily used within the context of queries</u></mark>, such as `SELECT`, `UPDATE`, `INSERT`, and `DELETE` statements. <mark><u>**It is not used as a standalone control-of-flow**</u></mark> structure like `IF` or `WHILE`.

In T-SQL, the `CASE` statement is specifically <mark><u>designed for conditional logic within the set-based operations of SQL queries</u></mark>. <mark>**It's not a general-purpose control-of-flow statement**</mark> like those found in procedural programming languages. Therefore, it cannot be used in the same way as an `if-else` or `switch` statement in languages like C# or Java, which control the flow of the program.

If you need to implement control-of-flow logic in T-SQL that is not directly tied to a query, you would typically use:

- IF...ELSE Statements: For conditional execution of T-SQL statements.
- WHILE Loops: For executing a set of statements repeatedly based on a condition.

---

#### **Understanding the CASE Statement as a SWITCH Equivalent**

The CASE statement can be used in two forms, which can mimic the behavior of a SWITCH statement **but only inside queries**:

1. Simple CASE (Equivalent to SWITCH): Compares an expression to a set of specific values.
2. Searched CASE: Evaluates a set of Boolean expressions.

---

#### Syntax of <mark>Simple CASE</mark> (SWITCH Equivalent)

```plsql
CASE input_expression
    WHEN expression1 THEN result1
    WHEN expression2 THEN result2
    ...
    ELSE default_result
END
```

- `input_expression`: The expression to compare against the `WHEN` expressions.

#### Syntax of <mark>Searched CASE</mark>

```plsql
CASE
    WHEN boolean_expression1 THEN result1
    WHEN boolean_expression2 THEN result2
    ...
    ELSE default_result
END
```

- Each `WHEN` clause contains a Boolean expression.

---

#### **Best Practices**

- <mark>Avoid Complexity</mark>: Keep CASE statements simple for better readability.
- <mark>Performance Consideration</mark>: Be cautious with performance on large datasets.
- NULL Handling: CASE returns NULL <mark>if no conditions are met and there is no ELSE clause</mark>.
- <mark>Consistent Data Types</mark>: Ensure consistent data types in THEN and ELSE clauses.  

#### **Summary**

    In T-SQL, the `CASE` statement functions as the closest equivalent to a `SWITCH` statement. It's a flexible tool for conditional logic, adaptable for various scenarios in SELECT, UPDATE, and ORDER BY clauses, enhancing SQL query functionality and dynamism.

---
