using MediatR;
using TravelRouteLib.Models;

namespace TravelRouteLib.Services
{
    public interface IDijkstra
    {
        void LoadRoutes();

        Task<Unit> AddRoute(RouteTrip routeTrip);

        Task<BestRoute> FindBestRoute(string from, string to);
        void Dispose();
    }
}
