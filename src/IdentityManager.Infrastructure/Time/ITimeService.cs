namespace IdentityManager.Infrastructure.Time
{
    public interface ITimeService
    {
        DateTimeOffset Now { get; }
        DateTimeOffset NowUtc { get; }
        DateOnly Today { get; }
        TimeOnly ThisMoment { get; }
    }
}
