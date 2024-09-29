using MediatR;
using TravelRouteApi.Commands;
using TravelRouteLib.Models;
using TravelRouteLib.Services;

namespace TravelRouteApi.Handlers
{
    public class BestRouteHandler(IDijkstra dijkstra) : IRequestHandler<BestRouteQuery, BestRoute>
    {
        public async Task<BestRoute> Handle(BestRouteQuery request, CancellationToken cancellationToken)
        {
            return await dijkstra.FindBestRoute(request.From, request.To);
        }
    }
}
