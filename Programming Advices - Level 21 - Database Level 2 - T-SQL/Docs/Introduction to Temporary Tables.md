## Introduction to Temporary Tables

---

#### **Introduction to Temporary Tables**

    Temporary tables in T-SQL are <mark>**<u>used to store and process intermediate results</u>**</mark>. <mark>These tables are **<u>created in the tempdb database</u>** and are automatically deleted when they are no longer used</mark>. Temporary tables are particularly useful in complex SQL operations where intermediate results need to be stored temporarily.

---

#### **Types of Temporary Tables**

1. <mark>Local Temporary Tables</mark>: Created with a single hash (`#`) symbol. <mark>**<u>Visible only to the connection that creates it</u>**</mark> and <u>are deleted when the connection is closed</u>.
2. Syntax: `CREATE TABLE #TempTable (...)`
3. <mark>Global Temporary Tables</mark>: Created with a double hash (`##`) symbol. <mark>**<u>Visible to all connections</u>**</mark> and <u>are deleted when the last connection using it is closed</u>.
4. Syntax: `CREATE TABLE ##TempTable (...)`

---

#### **Advantages of Temporary Tables**

1. **Performance**: Can improve performance in complex queries <mark>**by breaking them into simpler parts**</mark>.
2. **Complex Data Processing**: <mark>Useful for storing intermediate results in complex data processing</mark>.
3. **Transaction Management**: <mark>Changes in a temporary table **are not logged extensively**</mark>, which can be beneficial in large transactions.

---

#### **Cleaning Up**

 Temporary tables are <mark>**<u>automatically deleted when the session that created them ends</u>**</mark>. However, <mark>**<u>it's often considered good practice to explicitly drop them when they are no longer needed</u>**</mark>.

---

#### **Conclusion:**

    Temporary tables are a powerful feature in T-SQL, allowing for efficient handling of complex queries and data processing tasks. Their ability to store intermediate results and their scope of visibility make them a versatile tool for database developers and administrators. Understanding when and how to use temporary tables can significantly optimize SQL operations.

---
