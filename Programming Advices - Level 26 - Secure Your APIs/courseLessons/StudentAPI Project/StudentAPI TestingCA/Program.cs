using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.Security;
using System.Threading.Tasks;


namespace StudentApiConsoleClient
{
    class LoginRequest
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
    class TokenResponse
    {
        public required string Token { get; set; }
    }
    class StudentDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int Age { get; set; }
        public int Grade { get; set; }
    }

    class Program
    {
        private const string BaseUrl = "https://localhost:7062/";
        // Test credentials that already exist in the API's in-memory data store.
        private const string Email = "ali.ahmed@student.com";
        private const string Password = "password1";

        static HttpClient CreateHttpClientForLocalDev(string baseUrl)
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback =
                    (message, certificate, chain, sslErrors) =>
                        sslErrors == SslPolicyErrors.None ||
                        sslErrors == SslPolicyErrors.RemoteCertificateChainErrors
            };

            return new HttpClient(handler)
            {
                BaseAddress = new Uri(baseUrl)
            };
        }
        static async Task<string> LoginAndGetTokenAsync(HttpClient http, string email, string password)
        {
            var request = new LoginRequest
            {
                Email = email,
                Password = password
            };
            var response = await http.PostAsJsonAsync("/api/Auth/login", request);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                Console.WriteLine("Invalid credentials.");
                return "";
            }

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Login failed: {response.StatusCode}");
                return "";
            }

            var tokenResponse = await response.Content.ReadFromJsonAsync<TokenResponse>();
            return tokenResponse?.Token ?? "";
        }
        static async Task CallGetAllStudentsAsync(HttpClient http, string? token)
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, "api/Students/All");

            if (!string.IsNullOrWhiteSpace(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await http.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                Console.WriteLine("401 Unauthorized");
                return;
            }

            var students = await response.Content.ReadFromJsonAsync<List<StudentDto>>();
            if (students == null || students.Count == 0)
            {
                Console.WriteLine("No students returned.");
                return;
            }

            Console.WriteLine($"{students.Count} students returned:");
            foreach (var s in students)
            {
                Console.WriteLine($"- {s.Name} (Age: {s.Age}, Grade: {s.Grade})");
            }
        }

        static async Task Main(string[] args)
        {
            Console.WriteLine("=== Student API Console Client (JWT) ===");
            Console.WriteLine();

            using var http = CreateHttpClientForLocalDev(BaseUrl);
            var token = await LoginAndGetTokenAsync(http, Email, Password);

            if (string.IsNullOrWhiteSpace(token))
            {
                Console.WriteLine("Login failed.");
                return;
            }

            Console.WriteLine("Login succeeded.");
            Console.WriteLine($"Token (first 30 chars): {token[..30]}...");
            Console.WriteLine();

            // Step 2: Call a secured endpoint without sending a token.
            // This is expected to fail with 401 Unauthorized.
            Console.WriteLine("Calling GET /api/Students WITHOUT token (expected 401)...");
            await CallGetAllStudentsAsync(http, null);
            Console.WriteLine();

            // Step 3: Call the same secured endpoint with a valid JWT token.
            // This is expected to succeed.
            Console.WriteLine("Calling GET /api/Students WITH token (expected 200)...");
            await CallGetAllStudentsAsync(http, token);
            Console.WriteLine();
        }
    }
}
