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

        // Make ID column the primary key column.
        // Use this - cleanest and most readable:
        // 1. Explicit DataColumn type (very clear)
        employeesDataTable.PrimaryKey = new DataColumn[] { employeesDataTable.Columns["ID"] };

        // 2. Implicit type (modern C# - cleanest)
        /*
        employeesDataTable.PrimaryKey = new[] { employeesDataTable.Columns["ID"] };
            // =========================================
            // ============= CLARIFICATION =============
            // =========================================
            //
            // 'new []' uses implicit array type inference
            // The compiler figures out the type from the elements
            //
            // Since employeesDataTable.Columns["ID"] returns DataColumn
            // The compiler infers: new DataColumn[]
            //
        */

        /*
            // For composite primary key (multiple columns)
        employeesDataTable.PrimaryKey = new DataColumn[] {
            employeesDataTable.Columns["ID"],
            employeesDataTable.Columns["Name"]
        };
        */

        //Add rows 
        employeesDataTable.Rows.Add(1, "Mohammed Abu-Hadhoud", "Jordan", 5000, DateTime.Now);
        employeesDataTable.Rows.Add(2, "Ali Maher", "KSA", 525.5, DateTime.Now);
        employeesDataTable.Rows.Add(3, "Lina Kamal", "Jordan", 730.5, DateTime.Now);
        employeesDataTable.Rows.Add(4, "Fadi JAmeel", "Egypt", 800, DateTime.Now);
        employeesDataTable.Rows.Add(2, "Omar Mahmoud", "Lebanon", 7000, DateTime.Now);


        Console.WriteLine(" -> Employees List:");
        foreach (DataRow recordRow in employeesDataTable.Rows)
        {
            //Using Index
            // Console.WriteLine("ID: {0}\t Name : {1} \t Country: {2} \t Salary: {3} Date: {4} \t ", RecordRow[0], RecordRow[1], RecordRow[2], RecordRow[3], RecordRow[4]);

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