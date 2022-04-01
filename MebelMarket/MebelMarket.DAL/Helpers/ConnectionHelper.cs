using Microsoft.Extensions.Configuration;

namespace MebelMarket.DAL.Helpers
{
    public sealed class ConnectionHelper
    {
        private readonly string ConnectionString;
        public ConnectionHelper(IConfiguration configuration)
        {
            #if DEBUG
                ConnectionString = configuration["ConnectionStrings:DevelopmentConnectionString"];
            #else
                ConnectionString = configuration["ConnectionStrings:ProductionConnectionString"];
            #endif
        }

        public string CnnVal => ConnectionString;
    }
}
