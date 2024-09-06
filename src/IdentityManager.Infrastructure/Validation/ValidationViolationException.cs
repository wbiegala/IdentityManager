using static IdentityManager.Infrastructure.Validation.ValidationResult;

namespace IdentityManager.Infrastructure.Validation
{
    public class ValidationViolationException : Exception
    {
        private readonly List<ValidationError> _errors;

        public IReadOnlyCollection<ValidationError> Errors => _errors;

        public ValidationViolationException(IEnumerable<ValidationError> errors)
        {
            _errors = errors.ToList();
        }
    }
}
