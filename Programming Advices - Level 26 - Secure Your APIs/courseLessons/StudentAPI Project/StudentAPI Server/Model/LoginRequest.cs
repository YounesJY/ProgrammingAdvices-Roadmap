namespace StudentApi.Models
{
    using System.ComponentModel.DataAnnotations;

    namespace StudentApi.Models
    {
        public class LoginRequest
        {
            [Required]
            [EmailAddress]
            public required string Email { get; set; }

            [Required]
            public required string Password { get; set; }
        }
    }
}
