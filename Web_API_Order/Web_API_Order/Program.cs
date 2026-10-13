using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Web_API_Order.Repository;
using Web_API_Order.Models;
using Web_API_Order;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ApplicationDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
// Configure Swagger and add a security definition so the "Authorize" button appears in UI.
// This lets you paste "Bearer <token>" into Swagger to call protected endpoints.
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Enter: \"Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddTransient<IOrder, Order>();

// Bind JwtSettings from configuration (appsettings.json).
// Ensure you have a "JwtSettings" section with SecretKey, Issuer, Audience, ExpiryMinutes.
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
var jwtSection = builder.Configuration.GetSection("JwtSettings");

// Read the SecretKey and create the signing key.
// SecretKey must match the key used when issuing tokens (AuthController).
var secret = jwtSection.GetValue<string>("SecretKey") ?? throw new InvalidOperationException("Missing JWT SecretKey");
var key = Encoding.UTF8.GetBytes(secret);

// Register authentication with JWT Bearer handler.
// - DefaultAuthenticateScheme and DefaultChallengeScheme ensure JWT is used for auth challenges.
// - AddJwtBearer configures token validation parameters.
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    // RequireHttpsMetadata = true enforces HTTPS for metadata (recommended).
    // Set false only for local HTTP development/testing.
    options.RequireHttpsMetadata = true;
    options.SaveToken = true;

    // Token validation parameters:
    // - ValidateIssuer/ValidateAudience ensure token was issued for your API.
    // - ValidateLifetime checks token expiry.
    // - ValidateIssuerSigningKey ensures signature is valid using the configured key.
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSection.GetValue<string>("Issuer"),
        ValidAudience = jwtSection.GetValue<string>("Audience"),
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };

    // Optional diagnostics callbacks to log why authentication failed or succeeded.
    // Useful when you see 401 + WWW-Authenticate: Bearer to determine cause.
    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            var logger = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger("JwtAuth");
            logger.LogError(context.Exception, "Authentication failed");
            return Task.CompletedTask;
        },
        OnTokenValidated = context =>
        {
            var logger = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger("JwtAuth");
            logger.LogInformation("Token validated for {user}", context.Principal?.Identity?.Name);
            return Task.CompletedTask;
        },
        OnChallenge = context =>
        {
            // OnChallenge runs when middleware issues a 401 challenge.
            var logger = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger("JwtAuth");
            logger.LogWarning("OnChallenge: error={error} description={desc}", context.Error, context.ErrorDescription);
            return Task.CompletedTask;
        }
    };
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// IMPORTANT: Authentication middleware must run BEFORE Authorization middleware.
// This ensures the HttpContext.User is set from the validated token before policies/Authorize run.
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();