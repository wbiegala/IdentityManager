using IdentityManager.Domain.AccessRights;

namespace IdentityManager.Core.AccessRights.Queries
{
    public sealed record GetAccessRightQueryResult
    {
        public Guid Id { get; init; }
        public string Code { get; init; }
        public string Name { get; init; }
        public DateTimeOffset CreatedAt { get; init; }
    }


    internal static class GetAccessRightQueryResultMapper
    {
        public static GetAccessRightQueryResult Map(this AccessRight aggregate) =>
            new()
            {
                Id = aggregate.Id,
                Code = aggregate.Code,
                Name = aggregate.Name,
                CreatedAt = aggregate.CreatedAt,
            };
    }
}
