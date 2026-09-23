## Using the RANK() Function with PARTITION BY in SQL

---

#### **Introduction**

In SQL, the `PARTITION BY` clause is used in conjunction with window functions like `RANK()`. This clause allows you to divide the result set into partitions and apply the ranking function within each partition.

---

#### Scenario

Continuing with our `Students` table, we will now rank students within each subject. This means that the rank will restart for each subject.

---

#### **SQL Concepts: PARTITION BY Clause**

- PARTITION BY: This clause divides the result set into partitions where the ranking function restarts its count for each partition.

#### **Using RANK() with PARTITION BY**

Let's modify our previous query to include `PARTITION BY`:

```sql
SELECT 
    StudentID, 
    Name, 
    Subject, 
    Grade,
    RANK() OVER (PARTITION BY Subject ORDER BY Grade DESC) AS GradeRank
FROM 
    Students;
```

In this query:

- `PARTITION BY Subject` means the ranking will be reset for each subject.
- `ORDER BY Grade DESC` still orders the students by grade within each subject.
- `GradeRank` shows the rank of students within each specific subject.

---

#### Understanding the Output

- Students are ranked within each subject based on their grades.
- If students in the same subject have the same grade, they will have the same rank. The next rank is incremented based on the total number of students with the previous grade within that subject.
- The rank resets for each different subject.

---

#### Conclusion

Using `RANK()` with `PARTITION BY` allows for more nuanced analysis of data, enabling ranking within specific subsets of data. This is particularly useful in scenarios where comparative ranking is required within categories.

---

---

#### Exercise

1. Insert sample data into the `Students` table, ensuring multiple subjects are represented.
2. Run the modified query and observe how students are ranked within each subject.
3. Experiment by changing the partitioning column to something else, like `Grade`.

This lesson highlights the utility of partitioning in SQL queries, specifically for tasks that require categorized analysis or sorting within specific groups. The `PARTITION BY` clause, when used with functions like `RANK()`, offers a powerful tool for sophisticated data manipulation and analysis in SQL.

---
