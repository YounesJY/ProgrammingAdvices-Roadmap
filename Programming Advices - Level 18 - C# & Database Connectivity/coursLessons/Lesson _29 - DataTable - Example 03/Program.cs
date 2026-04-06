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

        //  Add rows 
        employeesDataTable.Rows.Add(1, "Mohammed Abu-Hadhoud", "Jordan", 5000, DateTime.Now);
        employeesDataTable.Rows.Add(2, "Ali Maher", "KSA", 525.5, DateTime.Now);
        employeesDataTable.Rows.Add(3, "Lina Kamal", "Jordan", 730.5, DateTime.Now);
        employeesDataTable.Rows.Add(4, "Fadi JAmeel", "Egypt", 800, DateTime.Now);
        employeesDataTable.Rows.Add(5, "Omar Mahmoud", "Lebanon", 7000, DateTime.Now);

        //  -------------------------------------------------
        //  No Filter
        //  -------------------------------------------------
        filterThenPrint(employeesDataTable, string.Empty);

        //  -------------------------------------------------
        //  Filter 01 : Filter By Country Jordan
        //  -------------------------------------------------
        filterThenPrint(employeesDataTable, "COUNTRY='Jordan'");

        //  -------------------------------------------------
        //  Filter 02 : Filter By Country "Jordan or Egypt"
        //  -------------------------------------------------
        filterThenPrint(employeesDataTable, "COUNTRY='Jordan' OR COUNTRY='Egypt'");

        //  -------------------------------------------------
        //  Filter 03 : Filter By ID "1"
        //  -------------------------------------------------
        filterThenPrint(employeesDataTable, "ID=1");

        Console.ReadKey();
    }

    public static void filterThenPrint(DataTable dataTable, string filter)
    {
        DataRow[] filteredResultRows;
        int employeesCount = 0;
        double totalSalaries = 0;
        double averageSalaries = 0;
        double minSalary = 0;
        double maxSalary = 0;


        filteredResultRows = dataTable.Select(filter);

        Console.WriteLine("\n-------------------------------------------------------------------\n");
        Console.WriteLine($"Filter Employees with [{((filter == string.Empty) ? "No filter" : filter)}] \n");
        foreach (DataRow recordRow in filteredResultRows)
        {
            Console.WriteLine($@"
                                ID      : {recordRow["ID"]}
                                Name    : {recordRow["Name"]} 
                                Country : {recordRow["Country"]}
                                Salary  : {recordRow["Salary"]} 
                                Date    : {recordRow["Date"]}"
            );
        }

        employeesCount = filteredResultRows.Count();
        totalSalaries = Convert.ToDouble(dataTable.Compute("SUM(Salary)", filter));
        averageSalaries = Convert.ToDouble(dataTable.Compute("AVG(Salary)", filter));
        minSalary = Convert.ToDouble(dataTable.Compute("Min(Salary)", filter));
        maxSalary = Convert.ToDouble(dataTable.Compute("Max(Salary)", filter));

        Console.WriteLine("\n==============================\n");
        Console.WriteLine($" Count of Employees        = {employeesCount}");
        Console.WriteLine($" Total Employee Salaries   = {totalSalaries}");
        Console.WriteLine($" Average Employee Salaries = {averageSalaries}");
        Console.WriteLine($" Minimum Salary            = {maxSalary}");
        Console.WriteLine($" Maximum Salary            = {minSalary}");
        Console.WriteLine("\n==============================\n");
        Console.WriteLine("\n-------------------------------------------------------------------\n");
    }
}