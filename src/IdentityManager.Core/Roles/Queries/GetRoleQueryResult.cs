using IdentityManager.Domain.Roles;

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


    internal static class GetRoleQueryResultMapper
    {
        public static GetRoleQueryResult Map(this Role aggregate) =>
            new()
            {
                Id = aggregate.Id,
                Name = aggregate.Name,
                CreatedAt = aggregate.CreatedAt,
                ModifiedAt = aggregate.ModifiedAt,
                IsActive = aggregate.IsActive,
            };
    }
}
