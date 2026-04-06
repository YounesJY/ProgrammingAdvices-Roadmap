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
        employeesDataTable.Rows.Add(6, "Omar Mahmoud", "Jordan", 4000, DateTime.Now);
        employeesDataTable.Rows.Add(7, "Omar Mahmoud", "KSA", 8000, DateTime.Now);


        sortThenPrint(employeesDataTable, string.Empty);
        sortThenPrint(employeesDataTable, "ID ASC");
        sortThenPrint(employeesDataTable, "ID DESC");
        sortThenPrint(employeesDataTable, "NAME ASC");
        sortThenPrint(employeesDataTable, "NAME DESC");
        sortThenPrint(employeesDataTable, "SALARY ASC");
        sortThenPrint(employeesDataTable, "SALARY DESC");

        sortThenPrint(employeesDataTable, "NAME ASC, SALARY ASC");
        sortThenPrint(employeesDataTable, "NAME ASC, SALARY DESC");

        Console.ReadKey();
    }

    public static void sortThenPrint(DataTable dataTable, string filter)
    {
        dataTable.DefaultView.Sort = filter;
        dataTable = dataTable.DefaultView.ToTable();


        Console.WriteLine("-------------------------------------------------------------------");
        Console.WriteLine($" -> Employees List Sorted By [{((filter == string.Empty) ? "No filter" : filter)}] \n");
        foreach (DataRow recordRow in dataTable.Rows)
        {
            Console.WriteLine($@"
                                ID      : {recordRow["ID"]}
                                Name    : {recordRow["Name"]} 
                                Country : {recordRow["Country"]}
                                Salary  : {recordRow["Salary"]} 
                                Date    : {recordRow["Date"]}"
            );
        }
        Console.WriteLine("-------------------------------------------------------------------");
    }
}