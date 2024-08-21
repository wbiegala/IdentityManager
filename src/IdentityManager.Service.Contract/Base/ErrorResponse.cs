namespace IdentityManager.Service.Contract.Base
{
    public class ErrorResponse
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public string? StackTrace { get; set; }
    }
}
