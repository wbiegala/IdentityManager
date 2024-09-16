namespace IdentityManager.Domain.Shared
{
    internal static class TimestampGenerator
    {
        private static Func<DateTimeOffset> _getNow = () => DateTimeOffset.Now;
        private static Func<DateTimeOffset> _getUtcNow = () => DateTimeOffset.UtcNow;

        public static DateTimeOffset Now => _getNow();
        public static DateTimeOffset UtcNow => _getUtcNow();

        // for test mocking
        internal static void Redefine(Func<DateTimeOffset> getNow, Func<DateTimeOffset> getUtcNow)
        {
            _getNow = getNow;
            _getUtcNow = getUtcNow;
        }
    }
}
