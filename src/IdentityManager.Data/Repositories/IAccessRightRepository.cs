using IdentityManager.Domain.AccessRights;

namespace IdentityManager.Data.Repositories
{
    public interface IAccessRightRepository : IRepository<AccessRight>
    {
        Task SaveAsync(AccessRight accessRight, CancellationToken cancellationToken = default);
        Task<AccessRight?> GetAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
