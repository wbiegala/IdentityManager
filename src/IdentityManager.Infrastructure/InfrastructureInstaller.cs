using IdentityManager.Infrastructure.Time;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityManager.Infrastructure
{
    public static class InfrastructureInstaller
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<ITimeService, TimeService>();

            return services;
        }
    }
}
