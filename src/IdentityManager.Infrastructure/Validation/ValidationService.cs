using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityManager.Infrastructure.Validation
{
    internal class ValidationService : IValidationService
    {
        private readonly IServiceProvider _container;

        public ValidationService(IServiceProvider container)
        {
            _container = container ?? throw new ArgumentNullException(nameof(container));
        }

        public async Task<ValidationResult> ValidateAsync<TModel>(TModel model, CancellationToken cancellationToken = default)
            where TModel : class
        {
            using var scope = _container.CreateAsyncScope();
            var validator = scope.ServiceProvider.GetService<IValidator<TModel>>();

            if (validator is null)
                return CreateResultForNullValidator();

            var result = await validator.ValidateAsync(model, cancellationToken);

            return CreateResult(result);
        }

        private static ValidationResult CreateResultForNullValidator()
        {
            return new ValidationResult
            {
                HasValidator = false,
                IsValid = true,
            };
        }

        private static ValidationResult CreateResult(FluentValidation.Results.ValidationResult result)
        {
            return new ValidationResult
            {
                IsValid = result.IsValid,
                HasValidator = true,
                ValidationErrors = result.Errors.Any()
                    ? result.Errors.ConvertAll(f => new ValidationResult.ValidationError
                        {
                            PropertyName = f.PropertyName,
                            Code = f.ErrorCode,
                            Message = f.ErrorMessage
                        })
                    : null
            };
        }
    }
}
