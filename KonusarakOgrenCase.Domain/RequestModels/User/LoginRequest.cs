using System.ComponentModel.DataAnnotations;

namespace KonusarakOgrenCase.Domain.RequestModels.User;

public class LoginRequest
{
    [Required] public string Mail { get; set; }

    [Required] public string Password { get; set; }
}