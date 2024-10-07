using Microsoft.EntityFrameworkCore;
using RoleBasedAuthenticationCodeDemo.Models;
using Microsoft.AspNetCore.Authentication;
using System.Text.Json.Serialization;

namespace RoleBasedAuthenticationCodeDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    // Use ReferenceHandler.Preserve to handle circular references
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
                    options.JsonSerializerOptions.PropertyNamingPolicy = null;
                });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Add scoped repository service
            builder.Services.AddScoped<IUserRepository, UserRepository>();

            // Configure Basic Authentication
            builder.Services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", options => { });

            // Configure Entity Framework Core
            builder.Services.AddDbContext<UserDBContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("ConStr")));

            // Configure CORS policy (if needed)
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            // Use CORS
            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
