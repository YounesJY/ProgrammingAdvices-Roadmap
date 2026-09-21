```csharp
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_Mery_Buisness;
namespace ConsoleAppTest
{
     static class Program
     {
         static string ConnectionString = @"Server=.\MSSQLSERVER2022;Database=C21_DB1;User Id=sa;Password=sa123456;";
         
         static DataTable GetAllEmployees2()
         {
             DataTable employeesTable = new DataTable();
             using (SqlConnection connection = new SqlConnection(ConnectionString))
             {
                 connection.Open();
                 string Query = "SELECT * FROM Employees2;";
                
                SqlCommand command = new SqlCommand(Query, connection);
                 using (SqlDataReader reader = command.ExecuteReader())
                     employeesTable.Load(reader);
             }
             
             return employeesTable;
         }
         
         public static bool UpdateEmployeeSalary(float Salary, string Name)
         {
             int rowsAffected = 0;
             bool isUpdated = false;
             using (SqlConnection Connection = new SqlConnection(ConnectionString))
             {
                 Connection.Open();
                 string Query = @"UPDATE Employees2 
                    SET Salary = @Salary 
                    WHERE Name = @Name";
                 using (SqlCommand Command = new SqlCommand(Query, Connection))
                 {
                     Command.Parameters.AddWithValue("@Salary", Salary);
                     Command.Parameters.AddWithValue("@Name", Name);
                     rowsAffected = Command.ExecuteNonQuery();
                     isUpdated = Command.ExecuteNonQuery() > 0;
                     Console.WriteLine($"RowsAffected: {rowsAffected}");
                 }
             }
             
             return isUpdated;
         }

     static void Main(string[] args)
     {
         Stopwatch watch = new Stopwatch();
         float Salary = 0;
         int PerformanceRating = 0;

         watch.Start();
         DataTable dtAllEmployees2 = GetAllEmployees2();

        foreach(DataRow EmployeeRow in dtAllEmployees2.Rows)
         {
             Salary = Convert.ToSingle(EmployeeRow["Salary"]);
             PerformanceRating = (int)EmployeeRow["PerformanceRating"];
             
             if (PerformanceRating > 90)
             Salary *= 1.15F;
             else if (PerformanceRating < 90 && PerformanceRating > 75)
             Salary *= 1.10F;
             else if (PerformanceRating < 74 && PerformanceRating >50)
             Salary *= 1.05F;
             else
             Salary = Salary; ;
             UpdateEmployeeSalary(Salary, (string)EmployeeRow["Name"]);
         }
             Console.WriteLine($"Time : {watch.ElapsedMilliseconds}");
             Console.ReadLine();
         }
     }
}
```
