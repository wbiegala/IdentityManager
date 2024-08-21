namespace IdentityManager.Service.Contract.Base
{
    public class ValidationErrorResponse
    {
        public string Code { get; set; } = "ValidationError";
        public IEnumerable<ValidationFailure> Failures { get; set; }

        public class ValidationFailure
        {
            public string PropertyName { get; set; }
            public string Code { get; set; }
            public string Message { get; set; }
        }
    }
}
