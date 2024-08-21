namespace IdentityManager.Service.Validation
{
    public sealed record ValidationResult
    {
        public bool IsValid { get; init; }
        public bool HasValidator { get; init; }
        public IEnumerable<ValidationError>? ValidationErrors { get; init; }

        public sealed record ValidationError
        {
            public string PropertyName { get; init; }
            public string Code { get; init; }
            public string Message { get; init; }
        }
    }

    public static class ValidationResultExtensions
    {
        public static void ThrowOnValidationViolation(this ValidationResult result)
        {
            if (!result.IsValid)
                throw new ValidationViolationException(result.ValidationErrors!);
        }
    }
}
