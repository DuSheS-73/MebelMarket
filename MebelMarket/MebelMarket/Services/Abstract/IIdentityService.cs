using MebelMarket.Models;
using System.Threading.Tasks;

namespace MebelMarket.Services.Abstract
{
    public interface IIdentityService
    {
        Task<AuthenticationResult> RegisterAsync(string email, string phone, string password, string name, string surname);
        Task<AuthenticationResult> LoginAsync(string login, string password);
    }
}
