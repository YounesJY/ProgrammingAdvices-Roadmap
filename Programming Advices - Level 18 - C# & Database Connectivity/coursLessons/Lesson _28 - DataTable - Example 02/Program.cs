using System;
using System.Data;
using System.Linq;

public class Example
{
    public static void Main()
    {
        DataTable employeesDataTable = new DataTable();
        int employeesCount = 0;
        double totalSalaries = 0;
        double averageSalaries = 0;
        double minSalary = 0;
        double maxSalary = 0;


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

        // Aggregatio functions - LINQ ?
        employeesCount = employeesDataTable.Rows.Count;
        totalSalaries = Convert.ToDouble(employeesDataTable.Compute("SUM(Salary)", string.Empty));
        averageSalaries = Convert.ToDouble(employeesDataTable.Compute("AVG(Salary)", string.Empty));
        minSalary = Convert.ToDouble(employeesDataTable.Compute("Min(Salary)", string.Empty));
        maxSalary = Convert.ToDouble(employeesDataTable.Compute("Max(Salary)", string.Empty));

        Console.WriteLine(" Employees List:");
        foreach (DataRow recordRow in employeesDataTable.Rows)
        {
            //Using Index
            // Console.WriteLine("ID: {0}\t Name : {1} \t Name: {2} \t Salary: {3} Date: {4} \t ", RecordRow[0], RecordRow[1], RecordRow[2], RecordRow[3], RecordRow[4]);

            //Using Field Name
            //Console.WriteLine("ID: {0}\t Name : {1} \t Country: {2} \t Salary: {3} Date: {4} \t ", RecordRow["ID"], RecordRow["Name"], RecordRow["Country"], RecordRow["Salary"], RecordRow["Date"]);
            Console.WriteLine($@"
                                ID      : {recordRow["ID"]}
                                Name    : {recordRow["Name"]} 
                                Country : {recordRow["Country"]}
                                Salary  : {recordRow["Salary"]} 
                                Date    : {recordRow["Date"]}"
            );
        }

        Console.WriteLine("\n==============================\n");
        Console.WriteLine($" Count of Employees        = {employeesCount}");
        Console.WriteLine($" Total Employee Salaries   = {totalSalaries}");
        Console.WriteLine($" Average Employee Salaries = {averageSalaries}");
        Console.WriteLine($" Minimum Salary            = {maxSalary}");
        Console.WriteLine($" Maximum Salary            = {minSalary}");
        Console.WriteLine("\n==============================\n");

        Console.ReadKey();
    }
}