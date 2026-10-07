using System.Net.Http.Json;


namespace StudentApiClient
{
    public class Student
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Age { get; set; }
        public int Grade { get; set; }
    }

    class Program
    {
        static readonly HttpClient httpClient = new HttpClient();

        static async Task GetAllStudents()
        {
            try
            {
                Console.WriteLine("--------------------------");
                Console.WriteLine("Fetching all students.....");
                var students = await httpClient.GetFromJsonAsync<List<Student>>("api/Students/All");

                if (students != null)
                {
                    foreach (var student in students)
                    {
                        Console.WriteLine($"ID: {student.Id}, Name: {student.Name}, Age: {student.Age}, Age: {student.Grade}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
        static async Task GetPassedStudents()
        {
            try
            {
                Console.WriteLine("-----------------------------");
                Console.WriteLine("Fetching Passed students.....");
                var students = await httpClient.GetFromJsonAsync<List<Student>>("api/Students/Passed");

                if (students != null)
                {
                    foreach (var student in students)
                        Console.WriteLine($"ID: {student.Id}, Name: {student.Name}, Age: {student.Age}, Age: {student.Grade}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
        static async Task<double> GetAverageGrade()
        {
            try
            {
                Console.WriteLine("-----------------------------");
                Console.WriteLine("Fetching average grade .....");
                return await httpClient.GetFromJsonAsync<Double>("api/Students/AverageGrade");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return 0.0;
        }


        static async Task Main(string[] args)
        {
            // [NEVER USE],
            // httpClient.BaseAddress = new Uri("http://localhost:5151/api/Students");
            // the base address should be set to the API's base URL, not a specific endpoint.
            // The correct way is to set it to "http://localhost:5151/api/" and then use relative paths for specific endpoints.

            // Set this to the correct URI for your API
            httpClient.BaseAddress = new Uri("http://localhost:5151/");

            await GetAllStudents();
            await GetPassedStudents();
            double averageGrade = await GetAverageGrade();
            Console.WriteLine($"Average Grade: {averageGrade}");
        }
    }
}
