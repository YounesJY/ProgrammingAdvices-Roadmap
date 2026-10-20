using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("StudentApiCorsPolicy", policy =>
    {
        policy
            .WithOrigins(
                "https://localhost:7062",
                "http://localhost:5151"
            )
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});
// ===============================
// JWT Authentication Configuration
// ===============================
// Register authentication services in the dependency injection container.
// JwtBearerDefaults.AuthenticationScheme tells ASP.NET Core that
// JWT Bearer authentication will be the default authentication method.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        // TokenValidationParameters define how incoming JWTs will be validated.
        options.TokenValidationParameters = new TokenValidationParameters
        {
            // Ensures the token was issued by a trusted issuer.
            ValidateIssuer = true,


            // Ensures the token is intended for this API (audience check).
            ValidateAudience = true,


            // Ensures the token has not expired.
            ValidateLifetime = true,


            // Ensures the token signature is valid and was signed by the API.
            ValidateIssuerSigningKey = true,


            // The expected issuer value (must match the issuer used when creating the JWT).
            ValidIssuer = "StudentApi",


            // The expected audience value (must match the audience used when creating the JWT).
            ValidAudience = "StudentApiUsers",


            // The secret key used to validate the JWT signature.
            // This must be the same key used when generating the token.
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("THIS_IS_A_VERY_SECRET_KEY_123456"))
        };
    });


// ===============================
// Authorization Configuration
// ===============================
// Register authorization services.
// This enables attributes like [Authorize] and role-based authorization.
builder.Services.AddAuthorization();


// ===============================
// Swagger Configuration
// ===============================
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();


// ===============================
// HTTP Request Pipeline
// ===============================
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("StudentApiCorsPolicy");
/*  IMPORTANT:
    Authentication middleware must run BEFORE authorization middleware.
    Authentication identifies the user.
    Authorization decides what the user is allowed to do.
*/
app.UseAuthentication();
app.UseAuthorization();
/*  Map controller routes (e.g., /api/Students, /api/Auth). 
*/
app.MapControllers();

app.Run();
