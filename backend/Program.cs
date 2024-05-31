using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System.Reflection;

using Api.Models;

namespace Api
{
    public class Program
    {
        private static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            ConfigurationManager configuration = builder.Configuration;
            IServiceCollection services = builder.Services;

            // Add services (default by asp)
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

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

            try // Register custom services
            {
                // Add database context service
                services.AddDbContext<AppDbContext>(dbContextOptionsBuilder =>
                {
                    dbContextOptionsBuilder.UseMySQL(connectionStringBuilder.ConnectionString);
                });

                // Add controller service
                services.AddControllers();

                // Register data access objects components
                services.AddScoped<IEntityDao<Customer>, CustomerDao>();

                // Register entity mapper components
                services.AddScoped<CustomerDtoMapper>();
            }
            catch (Exception ex)
            {
                HandleError("Service registration failed: " + ex.Message);
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

            app.UseHttpsRedirection();
            app.MapControllers();
            app.Run();
        }

        private static void HandleError(string message)
        {
            Console.WriteLine(message);
            Environment.ExitCode = 1;
        }
    }
}