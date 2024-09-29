namespace TravelRouteLib.Models;

public class RouteTrip(string from, string to, int cost)
{
    public string From { get; set; } = from;
    public string To { get; set; } = to;
    public int Cost { get; set; } = cost;
}