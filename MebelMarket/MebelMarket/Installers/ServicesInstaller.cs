using MebelMarket.DAL.Helpers;
using MebelMarket.DAL.Repository.Abstract;
using MebelMarket.DAL.Repository.Dapper;
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
            #region Helpers
            services.AddSingleton<ConnectionHelper>();
            #endregion

            #region Services
            services.AddScoped<IIdentityService, IdentityService>();
            #endregion

            #region Repositories
            services.AddScoped<IProductsRepository, ProductsRepository>();
            services.AddScoped<IProductFilesRepository, ProductFilesRepository>();
            services.AddScoped<ICategoriesRepository, CategoriesRepository>();
            #endregion
        }
    }
}
