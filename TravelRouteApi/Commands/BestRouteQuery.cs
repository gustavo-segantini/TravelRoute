using MediatR;
using TravelRouteLib.Models;

namespace TravelRouteApi.Commands
{
    public class BestRouteQuery : IRequest<BestRoute>
    {
        public string From { get; set; }

        public string To { get; set; }
    }
}
