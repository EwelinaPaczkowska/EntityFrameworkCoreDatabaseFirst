using apbd12.DAL;
using apbd12.Middlewares;
using apbd12.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace apbd12;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

        builder.Services.AddControllers();
        builder.Services.AddAuthorization();

        builder.Services.AddDbContext<TripsContext>(opt =>
        {
            opt.UseSqlServer(connectionString)
               .EnableSensitiveDataLogging()
               .LogTo(Console.WriteLine, LogLevel.Information);
        });

        builder.Services.AddScoped<ITripsService, TripsService>();
        builder.Services.AddScoped<IClientService, ClientService>();
        
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "TripsApi",
                Description = "API for managing trips",
                Contact = new OpenApiContact
                {
                    Name = "API Support",
                    Email = "apiSupport@gmail.com",
                    Url = new Uri("https://github.com/apiSupport")
                },
                License = new OpenApiLicense
                {
                    Name = "MIT License",
                    Url = new Uri("https://opensource.org/licenses/MIT")
                }
            });
        });

        var app = builder.Build();

        app.UseGlobalExceptionHandling();

        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "TripsApi");
            c.DocExpansion(DocExpansion.List);
            c.DefaultModelExpandDepth(0);
            c.DisplayRequestDuration();
            c.EnableFilter();
        });

        app.MapGet("/", () => Results.Redirect("/swagger"));

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}
