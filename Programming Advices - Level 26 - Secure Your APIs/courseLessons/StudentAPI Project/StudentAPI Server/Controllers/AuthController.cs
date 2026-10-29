using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using StudentApi.DataSimulation;
using StudentApi.DTOs.Auth;
using StudentApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.AspNetCore.RateLimiting;
using System.Text;


namespace StudentApi.Controllers
{
    // This controller is responsible for authentication-related actions,
    // such as logging in and issuing JWT tokens.
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;


        public AuthController(ILogger<AuthController> logger)
        {
            _logger = logger;
        }


        private static string GenerateAccessToken(Student student)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, student.Id.ToString()),
                new Claim(ClaimTypes.Email, student.Email!),
                new Claim(ClaimTypes.Role, student.Role!)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("THIS_IS_A_VERY_SECRET_KEY_123456"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "StudentApi",
                audience: "StudentApiUsers",
                claims: claims,
                //expires: DateTime.UtcNow.AddMinutes(30),
                expires: DateTime.UtcNow.AddSeconds(10),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private static string GenerateRefreshToken()
        {
            var bytes = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(bytes);
            return Convert.ToBase64String(bytes);
        }

        [HttpPost("login")]
        [EnableRateLimiting("AuthLimiter")]
        [ProducesResponseType(typeof(TokenResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status429TooManyRequests)]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var ip = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";
            var student = StudentDataSimulation.StudentsList
                .FirstOrDefault(s => s.Email == request.Email);

            if (student == null)
            {
                _logger.LogWarning(
                    "Failed login attempt (email not found). Email={Email}, IP={IP}",
                    request.Email,
                    ip
                );

                return Unauthorized("Invalid credentials");
            }

            bool isValidPassword = BCrypt.Net.BCrypt.Verify(request.Password, student.PasswordHash);
            if (!isValidPassword)
            {
                _logger.LogWarning(
                    "Failed login attempt (bad password). Email={Email}, IP={IP}",
                    request.Email,
                    ip
                );

                return Unauthorized("Invalid credentials");
            }

            var accessToken = GenerateAccessToken(student);
            var refreshToken = GenerateRefreshToken();

            student.RefreshTokenHash = BCrypt.Net.BCrypt.HashPassword(refreshToken);
            student.RefreshTokenExpiresAt = DateTime.UtcNow.AddDays(7);
            student.RefreshTokenRevokedAt = null;

            _logger.LogInformation(
                "Successful login. UserId={UserId}, Email={Email}, IP={IP}",
                student.Id,
                student.Email,
                ip
            );

            return Ok(new TokenResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            });
        }

        [HttpPost("refresh")]
        [EnableRateLimiting("AuthLimiter")]
        [ProducesResponseType(typeof(TokenResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status429TooManyRequests)]
        public IActionResult Refresh([FromBody] RefreshRequest request)
        {
            var ip = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";
            var student = StudentDataSimulation.StudentsList
                .FirstOrDefault(s => s.Email == request.Email);

            // 2. Generic Unauthorized to prevent enumeration
            if (student == null ||
                student.RefreshTokenHash == null ||
                student.RefreshTokenRevokedAt != null ||
                student.RefreshTokenExpiresAt == null ||
                student.RefreshTokenExpiresAt <= DateTime.UtcNow)
            {
                _logger.LogWarning(
                    "Invalid refresh token attempt. Email={Email}, IP={IP}",
                    request.Email,
                    ip
                );
                return Unauthorized();
            }

            bool refreshValid = BCrypt.Net.BCrypt.Verify(request.RefreshToken, student.RefreshTokenHash);
            if (!refreshValid)
            {
                _logger.LogWarning(
                    "Invalid refresh token attempt. Email={Email}, IP={IP}",
                    request.Email,
                    ip
                );
                return Unauthorized();
            }

            if (string.IsNullOrEmpty(student.Email) || string.IsNullOrEmpty(student.Role))
            {
                _logger.LogWarning(
                    "Invalid refresh token attempt due to missing email or role. Email={Email}, IP={IP}",
                    request.Email,
                    ip
                );
                return Unauthorized();
            }

            var newAccessToken = GenerateAccessToken(student);
            var newRefreshToken = GenerateRefreshToken();

            student.RefreshTokenHash = BCrypt.Net.BCrypt.HashPassword(newRefreshToken);
            student.RefreshTokenExpiresAt = DateTime.UtcNow.AddDays(7);
            student.RefreshTokenRevokedAt = null;

            return Ok(new TokenResponse
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            });
        }

        [HttpPost("logout")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Logout([FromBody] LogoutRequest request)
        {
            var student = StudentDataSimulation.StudentsList
                .FirstOrDefault(s => s.Email == request.Email);

            if (student == null || student.RefreshTokenHash == null)
                return Ok();

            bool refreshValid = BCrypt.Net.BCrypt.Verify(request.RefreshToken, student.RefreshTokenHash);
            if (!refreshValid)
                return Ok();

            student.RefreshTokenRevokedAt = DateTime.UtcNow;
            return Ok("Logged out successfully");
        }
    }
}
