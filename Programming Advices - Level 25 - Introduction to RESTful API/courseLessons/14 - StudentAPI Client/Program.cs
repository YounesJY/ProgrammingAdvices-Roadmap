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

        static async Task GetAllStudentsAsync()
        {
            try
            {
                Console.WriteLine("--------------------------");
                Console.WriteLine("Fetching all students...\n");

                var students = await httpClient.GetFromJsonAsync<List<Student>>("api/Students/");
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
        static async Task Main(string[] args)
        {
            // Set this to the correct URI for your API
            // [NEVER USE], the base address should be set to the API's base URL, not a specific endpoint.
            // The correct way is to set it to "http://localhost:5151/api/" and then use relative paths for specific endpoints.
            // httpClient.BaseAddress = new Uri("http://localhost:5151/api/Students");

            httpClient.BaseAddress = new Uri("http://localhost:5151/");
            await GetAllStudentsAsync();
        }
    }
}
