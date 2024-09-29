using MediatR;
using TravelRouteLib.Models;

namespace TravelRouteLib.Service
{
    public interface IDijkstra
    {
        void LoadRoutes();

        Task<Unit> AddRoute(RouteTrip routeTrip);

        Task<BestRoute> FindBestRoute(string from, string to);
    }
}
