using IdentityManager.Data.Repositories;
using IdentityManager.Data.Repositories.Impl;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityManager.Data
{
    public static class DataInstaller
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IAccessRightRepository, AccessRightRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();

            return services;
        }
    }
}
