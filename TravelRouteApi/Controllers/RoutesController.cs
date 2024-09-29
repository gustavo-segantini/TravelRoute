using MediatR;
using Microsoft.AspNetCore.Mvc;
using TravelRouteApi.Commands;
using TravelRouteLib.Models;

namespace TravelRouteApi.Controllers
{

    [ApiController]
    public class RoutesController(IMediator mediator) : Controller
    {
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorEventArgs), StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [Consumes("application/json")]

        public async Task<OkResult> RegisterRoute([FromBody] RouteTrip route)
        {
            await mediator.Send(new RegisterCommand(route));

            return Ok();
        }

        [HttpGet("best-route")]
        [ProducesResponseType(typeof(BestRoute), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [Consumes("application/json")]

        public async Task<ObjectResult> GetBestRoute(string from, string to)
        {
            if (string.IsNullOrEmpty(from) && string.IsNullOrEmpty(to))
            {
                return BadRequest("From and To are required");
            }

            var bestRoute = await mediator.Send(new BestRouteQuery { From = from, To = to });

            return Ok(new
            {
                bestRoute.Route,
                bestRoute.Cost
            });
        }
    }
}
