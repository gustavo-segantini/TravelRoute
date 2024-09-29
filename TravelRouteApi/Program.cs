using System.Diagnostics.CodeAnalysis;
using Serilog;

namespace TravelRouteApi;

[ExcludeFromCodeCoverage]
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Configure Serilog
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();

        builder.Host.UseSerilog();

        // Add services to the container.
        var startup = new Startup(builder.Configuration);
        startup.ConfigureServices(builder.Services);

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        startup.Configure(app, app.Environment);


        app.Run();
    }
}