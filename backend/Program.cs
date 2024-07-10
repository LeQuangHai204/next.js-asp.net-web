using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

using Api.Services;
using Api.Model;
using Api.Model.Daos;
using Api.Model.Repositories;
using Api.Model.Dtos;

namespace Api
{
    public class Program
    {
        private static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            ConfigurationManager configuration = builder.Configuration;
            IServiceCollection services = builder.Services;

            // Add database context service
            IConfigurationSection? connectionInfo = configuration
                .GetSection("Databases")
                .GetChildren()
                .FirstOrDefault(db => db["Name"] == "Default");

            if (connectionInfo == null)
            {
                HandleError("Database connection info not found");
                return;
            }

            MySqlConnectionStringBuilder connectionStringBuilder = new()
            {
                Server = connectionInfo["Server"],
                Port = uint.TryParse(connectionInfo["Port"], out uint port) ? port : 3306,
                Database = connectionInfo["Database"],
                UserID = connectionInfo["Uid"],
                Password = connectionInfo["Pwd"],
            };

            string connectionString = connectionStringBuilder.ConnectionString;

            try // Check database connection
            {
                new MySqlConnection(connectionString).Open();
            }
            catch (Exception ex)
            {
                HandleError("Database connection failed: " + ex.Message);
                return;
            }

            try // Services registration
            {
                // Set up logging service for console
                services.AddLogging(loggingBuilder => loggingBuilder.AddConsole());

                // Add Swagger service for API documentation
                services.AddEndpointsApiExplorer();
                services.AddSwaggerGen(swaggerGenOptions =>
                {
                    swaggerGenOptions.SwaggerDoc("v0", new() { Title = "API", Version = "v0" });
                    swaggerGenOptions.AddSecurityDefinition("Bearer", new()
                    {
                        Type = SecuritySchemeType.Http,
                        Description = "JWT Authorization header using the Bearer scheme",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Scheme = "Bearer",
                        BearerFormat = "JWT",
                    });
                    swaggerGenOptions.AddSecurityRequirement(new() {{ new() { Reference = new() {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }}, new string[] { }}});
                });

                // Add identity service
                services.AddIdentity<AppUser, IdentityRole>(identityOptions =>
                {

                    identityOptions.Password.RequireDigit = false;
                    identityOptions.Password.RequireLowercase = false;
                    identityOptions.Password.RequireUppercase = false;
                    identityOptions.Password.RequireNonAlphanumeric = false;
                    identityOptions.Password.RequiredLength = 3;

                    identityOptions.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                    identityOptions.Lockout.MaxFailedAccessAttempts = 5;
                    identityOptions.Lockout.AllowedForNewUsers = true;

                    identityOptions.User.RequireUniqueEmail = false;
                    identityOptions.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

                    identityOptions.SignIn.RequireConfirmedAccount = false;
                    identityOptions.SignIn.RequireConfirmedPhoneNumber = false;
                    identityOptions.SignIn.RequireConfirmedEmail = false;
                })
                    .AddEntityFrameworkStores<AppDbContext>();

                // Add authentication service
                services.AddAuthentication(authenticationOptions =>
                {
                    authenticationOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    authenticationOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    authenticationOptions.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    authenticationOptions.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                    authenticationOptions.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
                    authenticationOptions.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                    .AddJwtBearer(jwtBearerOptions =>
                    {
                        jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateIssuerSigningKey = true,

                            ValidIssuer = configuration["Jwt:Issuer"],
                            ValidAudience = configuration["Jwt:Audience"],
                            LifetimeValidator = (notBefore, expires, token, param) => expires > DateTime.UtcNow,
                            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                                configuration["Jwt:SecretKey"] ?? "No JWT key found")),
                        };
                    });

                // Register database context service
                services.AddDbContext<AppDbContext>(dbContextOptionsBuilder =>
                {
                    dbContextOptionsBuilder.UseMySQL(connectionString);
                });

                // Register controller service
                services.AddControllers();

                // Register data access objects components
                services.AddScoped<IEntityDao<Customer>, CustomerDao>();

                // Register entity repository components
                services.AddScoped<IEntityRepository<Customer>, CustomerRepository>();

                // Register services components
                services.AddScoped<IJwtService, JwtService>();
                services.AddScoped<CustomerService>();

                // Register middleware components
                services.AddScoped<JwtCookieMiddleware>();
            }
            catch (Exception ex)
            {
                HandleError("Error durring service registration: " + ex.Message);
                return;
            }

            // Create app from builder
            WebApplication app;
            try
            {
                app = builder.Build();
            }
            catch (Exception ex)
            {
                HandleError("Application initialzation failed: " + ex.Message);
                return;
            }

            // Configure the HTTP request pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            try
            {
                app.UseHttpsRedirection();
                app.UseMiddleware<JwtCookieMiddleware>();
                app.UseAuthentication();
                app.UseAuthorization();
                app.MapControllers();
            }
            catch (Exception ex)
            {
                HandleError("Error configuring request pipeline: " + ex.Message);
                return;
            }

            app.Run();
        }

        private static void HandleError(string message)
        {
            Console.WriteLine(message);
            Environment.ExitCode = 1;
        }
    }
}
