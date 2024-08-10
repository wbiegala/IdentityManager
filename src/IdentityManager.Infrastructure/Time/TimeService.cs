namespace IdentityManager.Infrastructure.Time
{
    internal class TimeService : ITimeService
    {
        public DateTimeOffset Now => DateTimeOffset.Now;

        public DateTimeOffset NowUtc => DateTimeOffset.UtcNow;

        public DateOnly Today => DateOnly.FromDateTime(DateTime.UtcNow);

        public TimeOnly ThisMoment => TimeOnly.FromDateTime(DateTime.UtcNow);
    }
}
