using Employee.Data;
using Employee.Service.Mapping;
using Employee.Service.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
var connectionString = configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// Register DbContext from Employee.Data
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// Register AutoMapper with the profile from Employee.Service
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<EmployeeMappingProfile>());

// Register the employee service
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

var tenantId = configuration["Authentication:TenantId"];

// ── CORS: allow the Blazor WASM client ──
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorClient", policy =>
    {
        policy.WithOrigins("https://localhost:5200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// ── OpenID Connect JWT Bearer authentication ──
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = configuration["Authentication:Authority"];
        options.Audience  = configuration["Authentication:Audience"];

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer           = true,
            ValidateAudience         = true,
            ValidateLifetime         = true,
            ValidateIssuerSigningKey = true,

            ValidIssuers =
            [
                $"https://login.microsoftonline.com/{tenantId}/v2.0",
                $"https://sts.windows.net/{tenantId}/"
            ]
        };

        // ── Diagnostic events: log the exact issuer mismatch ──
        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                var logger = context.HttpContext.RequestServices
                    .GetRequiredService<ILoggerFactory>()
                    .CreateLogger("JwtBearer");

                logger.LogError(context.Exception,
                    "Authentication failed: {Message}", context.Exception.Message);

                if (context.Exception is SecurityTokenInvalidIssuerException issuerEx)
                {
                    logger.LogError(
                        "Token issuer: '{InvalidIssuer}' | Valid issuers: {ValidIssuers}",
                        issuerEx.InvalidIssuer,
                        string.Join(", ", options.TokenValidationParameters.ValidIssuers ?? []));
                }

                return Task.CompletedTask;
            },
            OnTokenValidated = context =>
            {
                var logger = context.HttpContext.RequestServices
                    .GetRequiredService<ILoggerFactory>()
                    .CreateLogger("JwtBearer");

                var issuer = context.SecurityToken?.Issuer;
                logger.LogInformation("Token validated successfully. Issuer: '{Issuer}'", issuer);

                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddAuthorization();

// Add controllers
builder.Services.AddControllers();

var app = builder.Build();

// CORS must come before auth and endpoints
app.UseCors("AllowBlazorClient");

// Order matters: Authentication → Authorization → endpoints
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();