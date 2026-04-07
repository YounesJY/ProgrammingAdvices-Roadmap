using System;
using System.Data;


public class Example
{
    public static void Main()
    {
        DataTable employeesDataTable = new DataTable();


        /*
        employeesDataTable.Columns.Add("ID", typeof(int));
        employeesDataTable.Columns.Add("Name", typeof(string));
        employeesDataTable.Columns.Add("Country", typeof(string));
        employeesDataTable.Columns.Add("Salary", typeof(Double));
        employeesDataTable.Columns.Add("Date", typeof(DateTime));
         */

        // =========================================
        // ============= HIGH PRIORITY =============
        // =========================================
        // Creating columns using helper method GenerateDataColumn() --> DRY principle
        // Each column can have custom properties (Unique, ReadOnly, AutoIncrement, etc.)

        DataColumn dataColumnID = GenerateDataColumn("ID", typeof(int), isUnique: true, isReadOnly: false, columnCaption: "EmployeeID",
            isAutoIncrement: true, autoIncrementSeed: 1, autoIncrementStep: 1);
        employeesDataTable.Columns.Add(dataColumnID);

        DataColumn dataColumnFullName = GenerateDataColumn("FullName", typeof(string), isUnique: false, isReadOnly: false, columnCaption: "Employee Name");
        employeesDataTable.Columns.Add(dataColumnFullName);

        DataColumn dataColumnCountry = GenerateDataColumn("Country", typeof(string), isUnique: false, isReadOnly: false, columnCaption: "Country");
        employeesDataTable.Columns.Add(dataColumnCountry);

        DataColumn dataColumnSalary = GenerateDataColumn("Salary", typeof(double), isUnique: false, isReadOnly: false, columnCaption: "Salary");
        employeesDataTable.Columns.Add(dataColumnSalary);

        DataColumn dataColumnStartDate = GenerateDataColumn("StartDate", typeof(DateTime), isUnique: false, isReadOnly: false, columnCaption: "StartDate");
        employeesDataTable.Columns.Add(dataColumnStartDate);

        DataColumn dataColumnEndDate = GenerateDataColumn("EndDate", typeof(DateTime), isUnique: false, isReadOnly: false, columnCaption: "EndDate");
        employeesDataTable.Columns.Add(dataColumnEndDate);

        // Make ID column the primary key column
        employeesDataTable.PrimaryKey = new[] { employeesDataTable.Columns["ID"] };

        // Add rows 
        employeesDataTable.Rows.Add(1, "Mohammed Abu-Hadhoud", "Jordan", 5000, DateTime.Now);
        employeesDataTable.Rows.Add(2, "Ali Maher", "KSA", 525.5, DateTime.Now);
        employeesDataTable.Rows.Add(3, "Lina Kamal", "Jordan", 730.5, DateTime.Now);
        employeesDataTable.Rows.Add(4, "Fadi Jameel", "Egypt", 800, DateTime.Now);
        employeesDataTable.Rows.Add(5, "Omar Mahmoud", "Lebanon", 7000, DateTime.Now); // Duplicate ID = 2

        Console.WriteLine(" -> Employees List:");
        foreach (DataRow recordRow in employeesDataTable.Rows)
        {
            // Alternative: Using Index
            // Console.WriteLine("ID: {0}\t Name : {1} \t Country: {2} \t Salary: {3} Date: {4} \t ", recordRow[0], recordRow[1], recordRow[2], recordRow[3], recordRow[4]);

            Console.WriteLine($@"
                                ID         : {recordRow["ID"]}
                                Name       : {recordRow["FullName"]} 
                                Country    : {recordRow["Country"]}
                                Salary     : {recordRow["Salary"]} 
                                Start Date : {recordRow["StartDate"]}
                                End Date   : {(recordRow["EndDate"] == DBNull.Value ? "NULL" : recordRow["EndDate"])}"
            );
        }

        Console.ReadKey();
    }

    static DataColumn GenerateDataColumn(string columnName, Type dataType, bool isUnique = false, bool isReadOnly = false, string columnCaption = null,
        bool allowNull = true, bool isAutoIncrement = false, int autoIncrementSeed = 0, int autoIncrementStep = 1)
    {
        DataColumn dataColumn = new DataColumn();


        dataColumn.ColumnName = columnName;
        dataColumn.DataType = dataType;
        dataColumn.AutoIncrement = isAutoIncrement;
        dataColumn.Unique = isUnique;
        dataColumn.ReadOnly = isReadOnly;
        dataColumn.AllowDBNull = allowNull;

        if (isAutoIncrement)
        {
            dataColumn.AutoIncrementSeed = autoIncrementSeed;
            dataColumn.AutoIncrementStep = autoIncrementStep;
        }

        if (!string.IsNullOrWhiteSpace(columnCaption))
            dataColumn.Caption = columnCaption;

        return dataColumn;
    }
}