using FluentValidation;

namespace IdentityManager.Infrastructure.Validation
{
    public static class ValidationRules
    {
        public static IRuleBuilderOptions<T, string> AlphanumericOnly<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Matches("^[a-zA-Z0-9]*$")
                .WithErrorCode(nameof(AlphanumericOnly))
                .WithMessage("Only latin letters and digits are acceptable");
        }
    }
}
