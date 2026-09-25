## Table Variables in T-SQL

---

#### **Introduction to Table Variables**

    Table variables in T-SQL are <mark>used to store a set of records temporarily</mark>, <mark>similar to temporary tables</mark>. However, <mark>**<u>they have some distinct characteristics and are suitable for different scenarios</u>**</mark>. Table variables are declared using the `DECLARE` statement and are scoped to the batch, stored procedure, or function in which they are defined.

---

#### **Advantages of Table Variables**

1. Performance: <mark>**<u>For small datasets</u>**</mark>, table variables <mark>can be faster **<u>since they are stored in memory</u>** and not written to disk</mark>.
   1. > For small datasets, table variables can be faster than temp tables because their writes are not logged to the transaction log and they don't maintain statistics — so there's less per-operation overhead. <mark><u>**They are still stored in `tempdb` on disk, the same as temp tables**</u></mark>. The speed advantage disappears as data grows, because the lack of statistics leads the optimizer to assume 1 row, which produces bad execution plans for larger sets.
2. Transaction Log: <mark>Operations on table variables **<u>generate fewer log records</u>**</mark>. This can be beneficial in terms of performance.
3. Scope: The scope of a table variable is <mark>limited to the batch, stored procedure, or function in which it is defined</mark>. This can simplify transaction management and error handling.

---

#### **Differences Between Table Variables and Temporary Tables**

- <mark><u>**Logging and Transactions**</u></mark>: <mark>**Table variables have minimal logging for modifications**</mark>, which can result in performance benefits for certain types of workloads. However, they don't participate fully in transactions. For example, <u>*if a transaction is rolled back, changes to a table variable made within that transaction <mark>are not rolled back</mark>*</u>.
  - > <mark>**<u>NO LOGS = NO ROLLBACK</u>**</mark>, if there's no logs about your transactions, how do you expect from the server to rollback your tables ?
- <mark>**<u>Statistics</u>**</mark>: **<u>SQL Server does not create statistics on table variables</u>**, which can affect the performance of queries involving large table variables.
- Scope: Temporary tables exist until they are explicitly dropped or the session/connection is closed, whereas table variables exist only within the batch, stored procedure, or function.

#### **Limitations of Table Variables**

1. <mark>Indexing</mark>: By default, you can only create a primary key index at the time of declaration. <u>**Additional indexing options are limited**</u>.
2. <mark>Statistics</mark>: Lack of statistics <mark>can lead to suboptimal query plans for large data sets</mark>.

---

#### **Best Practices**

- Data Size Consideration: <mark>**<u>Prefer table variables for small datasets or simple operations</u>**</mark>.
- Scope and Lifetime: Use table variables <mark>**when you need a temporary storage mechanism within a single batch or stored procedure**</mark>.

---

#### **Conclusion**

    Table variables in T-SQL provide a convenient way to temporarily store and manipulate small sets of data. They are particularly <mark>**<u>useful for quick operations and in scenarios where minimal logging and transactional scope are important</u>**</mark>. Understanding when and how to use table variables, as opposed to temporary tables or other types of temporary storage, is an important skill in SQL programming and database design.

---
