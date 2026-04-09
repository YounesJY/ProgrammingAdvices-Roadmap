using System;
using System.Data;

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

        Console.WriteLine(" -> Employees List:\n");
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


        // First You filter for Employee ID = 4
        DataRow[] results = employeesDataTable.Select("ID = 4");

        // ==================== DEBUG TIP =====================
        // USE DEBUG MODE TO OBSERVE CHANGES DURING RUNTIME ON:
        // results (DataRow[] array) and recordRow (DataRow)
        // ====================================================

        foreach (var recordRow in results)
        {
            recordRow["Name"] = "Maha Ahmed";
            /*
            // The DataRow indexer returns object
            object value = recordRow["Salary"];  // Returns object

            // When you assign:
            recordRow["Salary"] = "900";  // Takes object (string is an object)
            recordRow["Salary"] = 900;    // Takes object (int is an object - boxed)
            recordRow["Salary"] = 900.0;  // Takes object (double is an object - boxed)    
            
            recordRow["Salary"] = "900";   // Works - but involves internal parsing
            */
            recordRow["Salary"] = 900;   // Better - type-safe, no parsing overhead
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

        Console.WriteLine(" -> Updating Employee ID = 4 record:\n");
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