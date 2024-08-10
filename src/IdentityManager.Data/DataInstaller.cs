using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityManager.Data
{
    public static class DataInstaller
    {
        public static IServiceCollection AddData(this IServiceCollection services, string dbConnectionString)
        {
            services.AddDbContext<IdentityManagerContext>(cfg =>
            {
                cfg.UseSqlServer(dbConnectionString);
            });

            return services;
        }
    }
}
