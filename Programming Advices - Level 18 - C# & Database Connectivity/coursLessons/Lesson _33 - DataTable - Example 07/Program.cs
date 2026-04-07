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


        //Clear all Data
        employeesDataTable.Clear();

        // ====================================================
        // =================== HIGH PRIORITY ==================
        // ====================================================
        //
        // Clear() removes ALL rows from the DataTable
        // The column structure (schema) remains INTACT
        //
        // Before Clear():
        // - Table has 5 rows and 5 columns
        //   (ID, Name, Country, Salary, Date)
        //
        // After Clear():
        // - Table has 0 rows
        // - Table still has 5 columns (ID, Name, Country, Salary, Date)
        // - Table is empty but ready to accept new rows
        // ====================================================

        // ====================================================
        // ==================== DEBUG TIP =====================
        // ====================================================
        // USE DEBUG MODE TO OBSERVE CHANGES DURING RUNTIME ON:
        //
        // employeesDataTable.Rows      
        // employeesDataTable.Columns   
        //
        // Watch how data changes step by step at runtime!
        // ====================================================


        Console.WriteLine(" -> Clearing data:\n");
        Console.WriteLine("  - Employees List after Clear:\n");

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