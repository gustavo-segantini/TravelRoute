using MediatR;
using Microsoft.Extensions.Options;
using TravelRouteLib.Configuration;
using TravelRouteLib.Models;

namespace TravelRouteLib.Service;

public class Dijkstra(IOptions<PathCsvFile> config) : IDijkstra
{
    public static readonly Dictionary<string, List<(string, int)>> Graph = new();

    public async void LoadRoutes()
    {
        foreach (var line in await File.ReadAllLinesAsync(config.Value.Path))
        {
            var parts = line.Split(',');
            var from = parts[0];
            var to = parts[1];
            var cost = int.Parse(parts[2]);

            if (!Graph.ContainsKey(from))
            {
                Graph[from] = [];
            }
            Graph[from].Add((to, cost));
        }
    }

    public async Task<Unit> AddRoute(RouteTrip routeTrip)
    {
        if (!Graph.ContainsKey(routeTrip.From))
        {
            Graph[routeTrip.From] = [];
        }
        Graph[routeTrip.From].Add((routeTrip.To, routeTrip.Cost));

        await using var writer = new StreamWriter(config.Value.Path, true);

        await writer.WriteLineAsync($"{routeTrip.From},{routeTrip.To},{routeTrip.Cost}");

        return await Task.FromResult(Unit.Value);
    }

    public async Task<BestRoute> FindBestRoute(string from, string to)
    {
        var pq = new List<(string node, int cost, string path)>();
        var visited = new HashSet<string>();
        pq.Add((from, 0, from));

        while (pq.Count > 0)
        {
            pq = pq
                .OrderBy(x => x.cost)
                .ToList();

            var (current, currentCost, path) = pq.First();

            pq.RemoveAt(0);

            if (current == to)
            {
                return await Task.Run(() =>
                {
                    var bestRoute = new BestRoute(path, currentCost);

                    return bestRoute;
                });
            }

            if (!visited.Add(current)) continue;

            foreach (var (neighbor, cost) in Graph[current])
            {
                if (!visited.Contains(neighbor))
                {
                    pq.Add((neighbor, currentCost + cost, path + " - " + neighbor));
                }
            }
        }

        return await Task.Run(() =>
        {
            var bestRoute = new BestRoute("No route found", int.MaxValue);

            return bestRoute;
        });
    }
}