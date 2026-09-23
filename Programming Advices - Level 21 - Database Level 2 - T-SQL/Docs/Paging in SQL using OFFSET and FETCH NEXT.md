## Paging in SQL using OFFSET and FETCH NEXT

---

#### **Introduction**

<mark><u>**Paging is a critical feature in database management**</u></mark>, particularly <mark><u>**useful when dealing with large datasets**</u></mark>. It <mark>allows for displaying data in segmented chunks</mark>, <mark><u>**enhancing performance and user experience**</u></mark>. SQL Server offers the `OFFSET` and `FETCH NEXT` clauses to implement paging efficiently.

---

#### **Scenario**

We have a `Students` table with multiple records. <mark><u>**Our objective is to display this data in a paginated format**</u></mark>. In this example, each page will show 3 students, and we'll focus on retrieving the second page of results.

---

#### SQL Concepts: OFFSET and FETCH NEXT

- OFFSET: Skips a set number of rows in the data.
- FETCH NEXT: Retrieves a specific number of rows after the offset.

---

#### **Setting Up Variables**

We use variables for dynamic paging control:

DECLARE @PageNumber AS INT, @RowsPerPage AS INT;
SET @PageNumber = 2;  -- Set to the second page
SET @RowsPerPage = 3; -- Displaying 3 rows per page

`@PageNumber` is now set to 2, indicating we want the second page of data. `@RowsPerPage` remains at 3.

---

#### **Implementing Paging**

To fetch the data for the second page:

```sql
SELECT StudentID, Name, Subject, Grade
FROM Students
ORDER BY StudentID
OFFSET (@PageNumber - 1) * @RowsPerPage ROWS
FETCH NEXT @RowsPerPage ROWS ONLY;
```

- We use `ORDER BY StudentID` for consistent ordering.
- `OFFSET (@PageNumber - 1) * @RowsPerPage ROWS`: This now skips the first 3 rows (since `@PageNumber` is 2, so `(2-1)*3 = 3`).
- `FETCH NEXT @RowsPerPage ROWS ONLY`: Retrieves the next 3 rows after the offset, which constitutes the second page of data.

> <mark>OFFSET and FETCH **<u>don't work without order by</u>** statement</mark>: 
> 
> ---
> 
> The reason the query doesn't work when the ORDER BY statement is removed is because the OFFSET and FETCH NEXT clauses require an explicit ordering of the result set. Without ORDER BY, SQL Server does not have a defined sequence of rows to apply the pagination logic.
> Why This Is Important:
> 
> ---
> 
> <mark><u>**Pagination without ORDER BY would be meaningless because there’s no defined sequence for displaying rows**</u></mark>.
> <mark><u>**SQL Server enforces this rule to prevent unreliable and inconsistent query results**</u></mark>.

#### **Example**

- Page 1: Shows the first 3 students.
- Page 2 (current): Skips the first 3 students, showing students 4 to 6.
- Page 3: Would skip the first 6 students, showing students 7 to 9, and so on.

---

#### **Conclusion**

By adjusting `@PageNumber`, you can navigate through different pages in a dataset. `OFFSET` and `FETCH NEXT` provide a straightforward method to implement paging in SQL Server, which is essential for managing and displaying large volumes of data efficiently.

---

---

#### Exercises

1. Change `@PageNumber` to access different pages (e.g., 1, 3, 4).
2. Experiment with `@RowsPerPage` to display different numbers of rows per page.
3. Try different `ORDER BY` clauses to understand their impact on the data presentation.

This lesson emphasizes the flexibility and importance of paging in database systems, particularly for improving data manageability and user interface design.

---
