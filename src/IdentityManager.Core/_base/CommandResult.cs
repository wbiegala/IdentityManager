namespace IdentityManager.Core.Base
{
    public abstract record CommandResult
    {
        public bool IsSuccess { get; init; }
        public string? Error { get; init; }
        public IEnumerable<string>? ErrorDetails { get; init; }
        public Exception? ExceptionThrown { get; init; }
    }
}
