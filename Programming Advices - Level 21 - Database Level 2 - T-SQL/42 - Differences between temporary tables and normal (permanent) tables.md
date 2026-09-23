## Differences between temporary tables and normal (permanent) tables

---

    The differences between temporary tables and normal (permanent) tables in SQL are significant in terms of scope, lifespan, usage, and physical storage. Here's a breakdown of the key differences:

### 1. Lifespan and Scope

- Temporary Tables: <mark>They are created in the tempdb database and exist only for the eduration of the session or connection that created them</mark>. **<u>Local temporary tables</u>** (prefixed with `#`) are visible only to the connection that created them, while **<u>global temporary tables</u>** (prefixed with `##`) are visible to all connections but still exist only until the last connection using them is closed.
- Normal Tables: <mark>Permanent tables are created in a user-defined database and persist until they are explicitly dropped by a user</mark>. **<u>They are visible and accessible to any user with the appropriate permissions</u>**, regardless of the user session or connection.

---

### 2. Performance and Storage

- Temporary Tables: They are <mark>stored in the tempdb database</mark>, which is a system database recreated every time SQL Server restarts. Operations on temporary tables generally <mark><u>**have less logging and lower locking overhead**</u></mark>, which can lead to performance benefits, especially for complex queries and large data manipulations.
- Normal Tables: Permanent tables are stored in the database in which they are created and <mark>**<u>are subject to more extensive logging and locking</u>**</mark>. This ensures data integrity and durability, which are critical for persistent data storage.

---

### 3. Usage

- Temporary Tables: <mark><u>**Ideal for storing intermediate results in complex queries**</u></mark>, for data processing within stored procedures, and <mark><u>**for situations where data needs to be isolated to a single session or connection**</u></mark>.
- Normal Tables: Used <mark>for storing data that needs to persist beyond the current session</mark>, <mark>for shared access among multiple users</mark>, and <u>**for data that forms the core structure of the application’s database schema**</u>.

---

### 4. Transaction Logging

- Temporary Tables: <mark><u>**They have minimal transaction logging**</u></mark>. This means that while they do participate in transactions, rollbacks and other transactional controls might have less overhead compared to normal tables.
- Normal Tables: <mark><u>**Fully participate in transactions with complete logging**</u></mark>, ensuring data integrity and supporting complex transactional controls.

---

### 5. Backup and Recovery

- Temporary Tables: They are not included in database backups and <mark><u>**cannot be recovered after a server restart or crash**</u></mark>.
- Normal Tables: <mark><u>**They are included in database backups and can be recovered in case of server restarts or database failures**</u></mark>.

---

### Conclusion

Choosing between temporary and normal tables depends on the specific requirements of the task at hand. Temporary tables are ideal for transient data and quick, session-specific operations, whereas normal tables are suited for storing persistent data that requires full transactional support, backup, and recovery.

---
