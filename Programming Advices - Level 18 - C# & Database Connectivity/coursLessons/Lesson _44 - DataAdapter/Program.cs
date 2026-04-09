using System;
using System.Data;
using System.Data.SqlClient;

class Program
{
    static void Main()
    {
        string connectionString = "Server=.;Database=HR_Database;User Id=sa;Password=JY0912;";
        string query = "SELECT * FROM Employees;";
        SqlConnection connection = null;
        SqlDataAdapter dataAdapter = null;
        DataSet dataSet = new DataSet();


        connection = new SqlConnection(connectionString);
        dataAdapter = new SqlDataAdapter(query, connectionString);
        SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);

        dataAdapter.SelectCommand.Connection = connection;
        dataAdapter.Fill(dataSet, "Employees");

        DataTable customersTable = dataSet.Tables["Employees"];
        foreach (DataRow row in customersTable.Rows)
            Console.WriteLine($"Customer ID: {row["ID"]}, Name: {row["FirstName"]}, LastName: {row["LastName"]}");

        // Make changes to the DataSet (add, modify, or delete rows)

        /*
        // ========================================================
        // ============= EXTREMELY HIGH PRIORITY NOTE =============
        // ========================================================
        //
        // THE CODE BELOW WILL THROW NullReferenceException BECAUSE:
        // UpdateCommand, InsertCommand, DeleteCommand are ALL NULL!
        // Only SelectCommand was created by the constructor.
        //
        // =========================================
        // FIX 1: Using SqlCommandBuilder (EASIEST)
        // =========================================
        //
        // ADD THIS ONE LINE AFTER creating the dataAdapter:
        // SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
        //
        // Then REPLACE all the connection Open/Set/Close code below with:
        // dataAdapter.Update(dataSet, "Employees");
        //
        // That's it! No connection.Open(), no setting UpdateCommand.Connection,
        // no connection.Close() - Update() handles everything automatically!
        //
        // =========================================
        // FIX 2: Manual Commands (FULL CONTROL)
        // =========================================
        //
        // REPLACE the adapter creation with:
        // SqlDataAdapter dataAdapter = new SqlDataAdapter();
        //
        // Then AFTER creating connection, add ALL commands manually:
        // dataAdapter.SelectCommand = new SqlCommand(query, connection);
        //
        // dataAdapter.UpdateCommand = new SqlCommand(
        //     "UPDATE Employees SET Fir}stName = @FirstName, LastName = @LastName WHERE ID = @ID",
        //     connection);
        // dataAdapter.UpdateCommand.Parameters.Add("@FirstName", SqlDbType.NVarChar, 50, "FirstName");
        // dataAdapter.UpdateCommand.Parameters.Add("@LastName", SqlDbType.NVarChar, 50, "LastName");
        // dataAdapter.UpdateCommand.Parameters.Add("@ID", SqlDbType.Int, 4, "ID");
        //
        // =========================================
        */

        // Comment that line below since we used SqlCommandBuilder before once
        // Update the data source with the changes made to the DataSet
        dataAdapter.UpdateCommand.Connection = connection;  // ← NullReferenceException! UpdateCommand is NULL!

        // Update the data source with the changes
        dataAdapter.Update(dataSet, "Employees");
    }
}