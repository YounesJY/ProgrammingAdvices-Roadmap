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


        /*
        // Display all records in the view.
        */


        // =================== DEBUG TIP ======================
        // USE DEBUG MODE TO OBSERVE CHANGES DURING RUNTIME ON:
        // employeesDataView
        // ====================================================

        DataView employeesDataView = employeesDataTable.DefaultView;

        Console.WriteLine(" -> Employees List from data view: ");
        foreach (DataRowView recordRow in employeesDataView)
        {
            //Using Field Name
            Console.WriteLine($@"
                                {recordRow["ID"]}, {recordRow["Name"]},
                                {recordRow["Country"]}, {recordRow["Salary"]}"
            );
        }

        // ========================
        // Now Filter.
        // ========================

        employeesDataView.RowFilter = "COUNTRY = 'Jordan' OR COUNTRY = 'Egypt'";

// =========================================
// ============= HIGH PRIORITY =============
// =========================================
// Setting RowFilter IMMEDIATELY filters the view
// At this EXACT moment, the DataView updates its internal row collection
// The view now only contains rows matching the filter

// =================== DEBUG TIP =======================
// USE DEBUG MODE TO OBSERVE CHANGES DURING RUNTIME ON:
// employeesDataView.RowFilter - employeesDataView.Count
// =====================================================

        Console.WriteLine(" -> Employees List from data view after filtering \"Jodan or Egypt\": ");
        foreach (DataRowView recordRow in employeesDataView)
        {
            //Using Field Name
            Console.WriteLine($@"
                                {recordRow["ID"]}, {recordRow["Name"]},
                                {recordRow["Country"]}, {recordRow["Salary"]}"
            );
        }

        Console.ReadKey();
    }
}