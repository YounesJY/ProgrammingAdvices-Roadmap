using System;
using System.Data;
using System.Linq;

public class Example
{
    public static void Main()
    {
        DataTable employeesDataTable = new DataTable();


        employeesDataTable.Columns.Add("ID", typeof(int));
        employeesDataTable.Columns.Add("Name", typeof(string));
        employeesDataTable.Columns.Add("Country", typeof(string));
        employeesDataTable.Columns.Add("Salary", typeof(Double));
        employeesDataTable.Columns.Add("Date", typeof(DateTime));

        //Add rows 
        employeesDataTable.Rows.Add(1, "Mohammed Abu-Hadhoud", "Jordan", 5000, DateTime.Now);
        employeesDataTable.Rows.Add(2, "Ali Maher", "KSA", 525.5, DateTime.Now);
        employeesDataTable.Rows.Add(3, "Lina Kamal", "Jordan", 730.5, DateTime.Now);
        employeesDataTable.Rows.Add(4, "Fadi JAmeel", "Egypt", 800, DateTime.Now);
        employeesDataTable.Rows.Add(5, "Omar Mahmoud", "Lebanon", 7000, DateTime.Now);

        Console.WriteLine("\nEmployees List:\n");
        foreach (DataRow recordRow in employeesDataTable.Rows)
        {
            //Using Field Name
            Console.WriteLine($@"
                                ID      : {recordRow["ID"]}
                                Name    : {recordRow["Name"]} 
                                Country : {recordRow["Country"]}
                                Salary  : {recordRow["Salary"]} 
                                Date    : {recordRow["Date"]}"
            );
        }


        // Delete employees with ID >= 4
        Console.WriteLine(" -> Employees List After Deletion:");

        // Step 1: Filter employees where ID >= 4
        // =========================================
        // ============= HIGH PRIORITY =============
        // =========================================

        // --------------------------------------------------------------------------
        // The Select() method returns an array (DataRow[]) of references 
        // to the original rows in the DataTable, not copies.
        // Any changes made to these rows will directly affect the original data.
        // --------------------------------------------------------------------------
        DataRow[] results = employeesDataTable.Select("ID >= 4");


        // ==================== DEBUG TIP =====================
        // USE DEBUG MODE TO OBSERVE CHANGES DURING RUNTIME ON:
        // results (DataRow[] array) and recordRow (DataRow)
        // ====================================================
        foreach (var recordRow in results)
        {
            recordRow.Delete();
        }

        // =========================================
        // ============= HIGH PRIORITY =============
        // =========================================
        // Sync with database after being online again
        // employeesDataTable.AcceptChanges();
        //
        // AcceptChanges() commits all pending changes (Insert, Update, Delete)
        // to the database by resetting RowState to Unchanged for all rows.
        // Call this only AFTER the database connection is restored and
        // changes have been successfully pushed to the database.

        foreach (DataRow recordRow in employeesDataTable.Rows)
        {
            //Using Field Name
            Console.WriteLine($@"
                                ID      : {recordRow["ID"]}
                                Name    : {recordRow["Name"]} 
                                Country : {recordRow["Country"]}
                                Salary  : {recordRow["Salary"]} 
                                Date    : {recordRow["Date"]}"
            );
        }

        Console.ReadKey();
    }
}