using System.ComponentModel.DataAnnotations;

namespace MebelMarket.Contracts.V1.Requests
{
    public sealed class UserLoginRequest
    {
        [Required] public string Login { get; set; }
        [Required] public string Password { get; set; }
    }
}
