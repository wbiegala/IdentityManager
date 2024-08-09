using Microsoft.Extensions.DependencyInjection;

namespace IdentityManager.Core
{
    public static class CoreInstaller
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(CoreInstaller).Assembly);
            });

            return services;
        }
    }
}
