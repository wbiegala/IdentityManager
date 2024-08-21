using IdentityManager.Service.Validation.Validators;

namespace IdentityManager.Service.Validation
{
    public static class ValidationInstaller
    {
        public static IServiceCollection AddValidation(this IServiceCollection services)
        {
            services.AddRolesValidators();
            services.AddSingleton<IValidationService, ValidationService>();

            return services;
        }
    }
}
