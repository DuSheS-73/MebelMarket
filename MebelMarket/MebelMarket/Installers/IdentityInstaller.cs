using MebelMarket.DAL.IdentityModels;
using MebelMarket.Models.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MebelMarket.Installers
{
    internal sealed class IdentityInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
            })
            .AddUserStore<UserStore>()
            .AddRoleStore<RoleStore>();

            services.AddAuthentication();
            services.AddAuthorization();

            services.Configure<JwtSettings>(configuration.GetSection(nameof(JwtSettings)));
        }
    }
}
