using System.ComponentModel.DataAnnotations;

namespace MebelMarket.Contracts.V1.Requests
{
    public sealed class UserRegistrationRequest
    {
        [Required] public string Name { get; set; }
        [Required] public string Surname { get; set; }
        [Required] public string Email { get; set; }
        [Required] public string Password { get; set; }
        [Required] public string Phone { get; set; }
    }
}
