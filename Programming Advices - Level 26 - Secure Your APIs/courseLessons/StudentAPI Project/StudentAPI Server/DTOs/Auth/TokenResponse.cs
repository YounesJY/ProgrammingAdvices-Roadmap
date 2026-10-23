using System.ComponentModel.DataAnnotations;

namespace StudentApi.DTOs.Auth
{
    public class TokenResponse
    {
        [Required]
        public required string AccessToken { get; set; }

        [Required]
        public required string RefreshToken { get; set; }
    }
}