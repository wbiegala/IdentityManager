using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityManager.API.Admin
{
    public static class AdminApiInstaller
    {
        public static IMvcBuilder AddAdminApiEndpoints(this IMvcBuilder mvcBuilder)
        {
            mvcBuilder.AddApplicationPart(typeof(AdminApiInstaller).Assembly);

            return mvcBuilder;
        }

        public static IServiceCollection AddAdminApi(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining(typeof(AdminApiCfg), includeInternalTypes: true);
            return services;
        }
    }
}
