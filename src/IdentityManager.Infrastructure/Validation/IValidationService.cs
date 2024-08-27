namespace IdentityManager.Infrastructure.Validation
{
    public interface IValidationService
    {
        Task<ValidationResult> ValidateAsync<TModel>(TModel model, CancellationToken cancellationToken = default)
            where TModel : class;
    }
}
