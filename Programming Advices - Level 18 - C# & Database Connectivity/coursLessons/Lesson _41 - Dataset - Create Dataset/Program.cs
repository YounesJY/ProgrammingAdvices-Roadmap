using System;
using System.Data;
using System.Linq;

public class Example
{
    public static void Main()
    {
        DataTable employeesDataTable = new DataTable();
        DataTable departmentsDataTable = new DataTable();
        DataSet enterpriseDataSet = new DataSet();


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

        Console.WriteLine(" -> Employees List: ");
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


        departmentsDataTable.Columns.Add("DepartmentID", typeof(int));
        departmentsDataTable.Columns.Add("Name", typeof(string));

        //Add rows 
        departmentsDataTable.Rows.Add(1, "Marketing");
        departmentsDataTable.Rows.Add(2, "IT");
        departmentsDataTable.Rows.Add(3, "HR");


        Console.WriteLine(" -> Departments List: ");
        foreach (DataRow recordRow in departmentsDataTable.Rows)
        {
            //Using Field Name
            Console.WriteLine($@"
                                DepartmentID : {recordRow["DepartmentID"]}
                                Name         : {recordRow["Name"]}"
            );
        }


        //Adding DataTables into DataSet
        enterpriseDataSet.Tables.Add(employeesDataTable);
        enterpriseDataSet.Tables.Add(departmentsDataTable);

        // =================== DEBUG TIP ======================
        // USE DEBUG MODE TO OBSERVE CHANGES DURING RUNTIME ON:
        // enterpriseDataSet
        // employeesDataTable
        // departmentsDataTable
        // ====================================================


        Console.WriteLine(" -> Printing Employees Data form the Dataset: ");
        foreach (DataRow recordRow in enterpriseDataSet.Tables[0].Rows)
        {
            Console.WriteLine($@"
                                ID      : {recordRow["ID"]}
                                Name    : {recordRow["Name"]} 
                                Country : {recordRow["Country"]}
                                Salary  : {recordRow["Salary"]} 
                                Date    : {recordRow["Date"]}"
            );
        }

        Console.WriteLine(" -> Printing Departments Data form the Dataset: ");
        foreach (DataRow recordRow in enterpriseDataSet.Tables[1].Rows)
        {
            //Using Field Name
            Console.WriteLine($@"
                                DepartmentID : {recordRow["DepartmentID"]}
                                Name         : {recordRow["Name"]}"
            );
        }

        Console.ReadKey();
    }
}