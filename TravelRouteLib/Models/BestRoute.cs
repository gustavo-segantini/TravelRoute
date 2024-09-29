namespace TravelRouteLib.Models
{
    public class BestRoute(string route, int cost)
    {
        public string Route { get; set; } = route;
        public int Cost { get; set; } = cost;
    }
}
