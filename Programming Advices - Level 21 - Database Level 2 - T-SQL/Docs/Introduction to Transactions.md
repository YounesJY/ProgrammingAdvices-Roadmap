## Introduction to Transactions

---

#### **Introduction to Transactions**

- Definition: A transaction in SQL is <mark><u>a series of database operations that are **treated as a single logical unit**</u></mark>. It ensures that either all operations within it are executed or none are.
- ACID Properties: <mark>**<u>Transactions in SQL adhere to ACID properties</u>**</mark> - Atomicity, Consistency, Isolation, Durability.

---

#### **Why Use Transactions?**

- <mark>Data Integrity</mark>: Critical for operations that must not be partially completed, such as bank transfers.
- <mark>Error Handling</mark>: Transactions help in managing errors and maintaining database consistency.

**ACID** is an acronym that stands for Atomicity, Consistency, Isolation, and Durability. It's a set of properties that guarantee that database transactions are processed reliably.

1. Atomicity: This ensures that <mark>all operations within a transaction are treated as a single unit</mark>. <mark>**<u>Either all of them are executed successfully, or none are</u>**</mark>. If any part of the transaction fails, the entire transaction is rolled back (undone), maintaining data integrity.
2. Consistency: Consistency ensures that a transaction brings the database from one valid state to another. Integrity constraints are maintained so that <mark>the database **<u>remains consistent</u>** **<u>before and after</u>** the transaction</mark>.
3. Isolation: Isolation <mark>ensures that transactions are **securely and independently** processed at the same time without interference</mark>, but the results of the transaction are as if the transactions were processed sequentially. This prevents transactions from reading intermediate (and possibly inconsistent) data.
4. Durability: Durability guarantees that once a transaction has been committed, it will remain so, even in the event of a system failure. <mark>**<u>This means that the changes made by the transaction are permanently stored in the database</u>**</mark>. <u>In practical terms, this means that the database system has mechanisms in place, <mark>such as writing to a transaction log</mark>, that ensure the permanence of the transaction's effects.</u>

Together, these properties ensure that database transactions are executed safely, reliably, and in a way that preserves the integrity of the database.

---

#### <mark>**<u>Best Practices</u>**</mark>

- Short and Concise: Keep transactions as brief as possible.
- Error Handling: Use `TRY...CATCH` for robust error handling.
- Testing: Always test transactions thoroughly in a non-production environment.

#### **Conclusion**

Transactions are fundamental in ensuring data integrity, especially in scenarios like bank transfers. They provide a way to group multiple operations into a single, <mark>atomic unit</mark>, ensuring that either all operations succeed or none do, thus maintaining the consistency and reliability of your database.

---

> This lesson now accurately represents the concept and implementation of transactions in T-SQL, particularly highlighting a practical example of a bank transfer.

---


