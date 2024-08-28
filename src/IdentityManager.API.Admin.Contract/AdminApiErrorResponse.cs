namespace IdentityManager.API.Admin.Contract
{
    public class AdminApiErrorResponse
    {
        public string Message { get; set; }
        public IEnumerable<string>? ErrorDetails { get; set; }
    }
}
