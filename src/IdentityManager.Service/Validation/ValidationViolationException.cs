using static IdentityManager.Service.Validation.ValidationResult;

namespace IdentityManager.Service.Validation
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
