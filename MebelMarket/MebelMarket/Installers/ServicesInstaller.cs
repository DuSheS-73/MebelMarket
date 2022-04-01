using MebelMarket.DAL.Helpers;
using MebelMarket.Services.Abstract;
using MebelMarket.Services.Implementation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MebelMarket.Installers
{
    internal sealed class ServicesInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<ConnectionHelper>();

            services.AddScoped<IIdentityService, IdentityService>();
        }
    }
}
