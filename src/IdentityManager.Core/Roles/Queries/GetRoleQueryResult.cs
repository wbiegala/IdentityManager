namespace IdentityManager.Core.Roles.Queries
{
    public sealed record GetRoleQueryResult
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public DateTimeOffset CreatedAt { get; init; }
        public DateTimeOffset ModifiedAt { get; init; }
        public bool IsActive { get; init; }
    }
}
