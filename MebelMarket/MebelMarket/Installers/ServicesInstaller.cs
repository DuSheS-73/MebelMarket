using MebelMarket.DAL.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MebelMarket.Installers
{
    internal sealed class ServicesInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<ConnectionHelper>();
        }
    }
}
