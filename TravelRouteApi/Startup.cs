using System.Diagnostics.CodeAnalysis;
using MediatR;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using TravelRouteLib.Configuration;
using TravelRouteLib.Services;

namespace TravelRouteApi;

[ExcludeFromCodeCoverage]
public class Startup(IConfiguration configuration)
{
    public IConfiguration Configuration { get; } = configuration;

    public void ConfigureServices(IServiceCollection services)
    {
        services
            .AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
            });

        services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));

        services.AddSwaggerGen(c =>
        {
            c.UseInlineDefinitionsForEnums();
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Travel Route API", Version = "v1" });
        });

        services.Configure<PathCsvFile>(Configuration.GetSection("PathCsvFile"));

        services.AddSingleton<IDijkstra, Dijkstra>();

        services.AddMediatR(Assembly.GetExecutingAssembly());

        ReadCsv(services);
    }
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Travel Route API V1");
            c.RoutePrefix = string.Empty;
        });

        app.UseSerilogRequestLogging(c =>
        {
            c.IncludeQueryInRequestPath = true;
        }); // Enable Serilog request logging

        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }

    public void ReadCsv(IServiceCollection services)
    {
        var dijkstra = services.BuildServiceProvider().GetService<IDijkstra>();

        dijkstra?.LoadRoutes();
    }
}
