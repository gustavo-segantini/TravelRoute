
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TravelRouteLib.Configuration;
using TravelRouteLib.Services;

namespace TravelRouteConsole;

[ExcludeFromCodeCoverage]
internal class Program
{
    private static async Task Main()
    {
        var host = Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration((_, config) =>
            {
                config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            })
            .ConfigureServices((context, services) =>
            {
                services.Configure<PathCsvFile>(context.Configuration.GetSection("PathCsvFile"));

                services.AddSingleton<IDijkstra, Dijkstra>();
            })
            .Build();

        var dijkstra = host.Services.GetRequiredService<IDijkstra>();

        dijkstra.LoadRoutes();

        while (true)
        {
            Console.WriteLine("Please enter the route (format: DE-PARA):");

            var input = Console.ReadLine();

            if (input == "exit")
                break;

            var route = input?.Split('-');
            var from = route?[0];
            var to = route?[1];

            if (from == null || to == null)
            {
                Console.WriteLine("Invalid route format.");
                continue;
            }

            var result = await dijkstra.FindBestRoute(from, to);

            Console.WriteLine($"Best route: {result.Route} > ${result.Cost} {Environment.NewLine}");
        }
    }
}