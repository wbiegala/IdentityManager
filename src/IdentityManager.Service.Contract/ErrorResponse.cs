namespace IdentityManager.Service.Contract
{
    public class ErrorResponse
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public string? StackTrace { get; set; }
    }
}
