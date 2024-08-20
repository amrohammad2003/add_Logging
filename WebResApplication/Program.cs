using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Application.MappingProfiles;
using Application.Services;
using Contract.Interfaces.IServices;
using Efcore.DBContext;
using Microsoft.Extensions.Options;

namespace WebResApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add configuration
            builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            // Get the connection string from configuration
            var connectionString = builder.Configuration.GetConnectionString("constr");

            // Register DbContext with SQL Server
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString, sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly("Efcore")
                               .EnableRetryOnFailure(
                                   maxRetryCount: 5,
                                   maxRetryDelay: TimeSpan.FromSeconds(10),
                                   errorNumbersToAdd: null
                               );
                })
            );

            // Register custom logging provider
          
            // Configure logging
            builder.Logging.ClearProviders(); // Remove default providers
            builder.Logging.AddConsole(); // Add console logging
            builder.Logging.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Warning); // Filter EF Core logs

            // Register services
            builder.Services.AddScoped<IDrinkService, DrinkService>();
            builder.Services.AddScoped<IFoodService, FoodService>();
            builder.Services.AddScoped<ILoggerService, LoggerService>();


            // Register AutoMapper profiles
            builder.Services.AddAutoMapper(typeof(DrinkMappingProfile));
            builder.Services.AddAutoMapper(typeof(FoodMappingProfile));

            // Add controllers and Swagger
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebResApplication API", Version = "v1" });
            });

            var app = builder.Build();

            // Apply migrations
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                dbContext.Database.Migrate();
            }

            // Configure middleware
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebResApplication API v1");
                });
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
