## BEGIN...END Blocks in T-SQL

---

#### **Introduction to BEGIN...END Blocks in T-SQL**

    The `BEGIN...END` block in T-SQL is a control-of-flow language construct used to group a series of T-SQL statements into a single block. This is particularly useful for <mark>defining the body of control-of-flow statements</mark> like `IF...ELSE`, `WHILE`, and others.

>  `BEGIN...END` blocks <mark>are similar to { }</mark> in other programming languages.

### **Syntax**

The basic syntax of a `BEGIN...END` block is:

```plsql
BEGIN
    -- Series of T-SQL statements
END
```

#### Usage of BEGIN...END Blocks

1. <mark>In Control-of-Flow Constructs</mark>: To define the scope of statements within control structures like `IF`, `ELSE`, and `WHILE`.

```plsql
IF @condition = TRUE
BEGIN
    -- Statements to execute if condition is true
END
```

1. <mark><u>**To Group Statements**</u></mark>: Group multiple T-SQL statements so that they are executed together as a unit.
2. <mark>In Stored Procedures and Triggers</mark>: To define the set of statements that make up a stored procedure or trigger.

#### **Characteristics**

- Scope Definition: `BEGIN...END` blocks define the start and end of a statement group.
- <mark>Nested Blocks</mark>: These blocks can be nested inside one another.

#### **Examples**

- Simple BEGIN...END Block:

```plsql
BEGIN
    PRINT 'Hello, World!';
    SELECT * FROM Employees;
END
```

- Nested Blocks:

```plsql
IF @x > 10
BEGIN
    PRINT 'X is greater than 10';
    IF @y < 20
    BEGIN
        PRINT 'Y is less than 20';
    END
END
```

- In WHILE Loops:

```plsql
WHILE @counter < 10
BEGIN
    PRINT @counter;
    SET @counter = @counter + 1;
END
```

### 
