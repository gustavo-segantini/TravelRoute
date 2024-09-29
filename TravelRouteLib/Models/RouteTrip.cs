using System.ComponentModel.DataAnnotations;

namespace TravelRouteLib.Models;

public class RouteTrip(string from, string to, int cost)
{
    [Required(ErrorMessage = "O From é obrigatório")]
    [MinLength(3, ErrorMessage = "O From deve ter no mínimo 3 caracteres")]
    [MaxLength(3, ErrorMessage = "O From deve ter no máximo 3 caracteres")]
    [RegularExpression("^[A-Z]{3}$", ErrorMessage = "O From deve conter apenas letras maiúsculas")]
    public string From { get; set; } = from;

    [Required(ErrorMessage = "O To é obrigatório")]
    [MinLength(3, ErrorMessage = "O To deve ter no mínimo 3 caracteres")]
    [MaxLength(3, ErrorMessage = "O To deve ter no máximo 3 caracteres")]
    [RegularExpression("^[A-Z]{3}$", ErrorMessage = "O To deve conter apenas letras maiúsculas")]
    public string To { get; set; } = to;

    [Required(ErrorMessage = "O Cost é obrigatório")]
    public int Cost { get; set; } = cost;
}