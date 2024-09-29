using MediatR;
using Microsoft.AspNetCore.Mvc;
using TravelRouteApi.Commands;
using TravelRouteLib.Service;
using TravelRouteLib.Models;

namespace TravelRouteApi.Controllers
{
    public class RoutesController(IMediator mediator) : Controller
    {

        [HttpPost("register")]

        public async Task<OkResult> RegisterRoute([FromBody] RouteTrip route)
        {
            await mediator.Send(new RegisterCommand(route));

            return Ok();
        }

        [HttpGet("best-route")]
        public async Task<OkObjectResult> GetBestRoute(string from, string to)
        {
            var bestRoute = await mediator.Send(new BestRouteQuery { From = from, To = to });

            return Ok(new
            {
                bestRoute.Route,
                bestRoute.Cost
            });
        }
    }
}
