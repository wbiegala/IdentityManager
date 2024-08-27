using IdentityManager.Infrastructure.Time;
using IdentityManager.Infrastructure.Validation;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityManager.Infrastructure
{
    public static class InfrastructureInstaller
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<ITimeService, TimeService>();
            services.AddSingleton<IValidationService, ValidationService>();

            return services;
        }
    }
}
