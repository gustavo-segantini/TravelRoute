using MediatR;
using TravelRouteApi.Commands;
using TravelRouteLib.Services;

namespace TravelRouteApi.Handlers
{
    public class RegisterRouteHandler(IDijkstra dijkstra) : IRequestHandler<RegisterCommand>
    {
        public async Task<Unit> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            await dijkstra.AddRoute(request.Route);


            return Unit.Value;
        }
    }
}
