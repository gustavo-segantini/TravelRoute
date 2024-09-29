using MediatR;
using TravelRouteLib.Models;

namespace TravelRouteApi.Commands
{
    public record RegisterCommand(RouteTrip Route) : IRequest;
}
