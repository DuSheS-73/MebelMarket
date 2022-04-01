using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MebelMarket.Installers
{
    internal interface IInstaller
    {
        void InstallServices(IServiceCollection services, IConfiguration configuration);
    }
}
