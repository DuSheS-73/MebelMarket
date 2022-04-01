using Microsoft.AspNetCore.Identity;

namespace MebelMarket.DAL.IdentityModels
{
    public sealed class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
