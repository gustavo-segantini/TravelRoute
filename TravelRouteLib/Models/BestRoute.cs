using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace TravelRouteLib.Models
{
    public class BestRoute(string route, int cost)
    {
        public string Route { get; set; } = route;
        public int Cost { get; set; } = cost;
    }
}
