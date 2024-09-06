using IdentityManager.Domain.AccessRights;
using IdentityManager.Domain.Roles;

namespace IdentityManager.Data.Repositories
{
    public interface IAccessRightRepository : IRepository<AccessRight>
    {
        Task SaveAsync(AccessRight accessRight, CancellationToken cancellationToken = default);
        Task<AccessRight?> GetAsync(Guid id, CancellationToken cancellationToken = default);
        Task<AccessRight?> GetByCodeAsync(string code, bool includeRoles = false, CancellationToken cancellationToken = default);
        Task<AccessRight?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
        void Delete(AccessRight accessRight);
    }
}
