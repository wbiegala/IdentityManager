using IdentityManager.Domain.Roles;

namespace IdentityManager.Data.Repositories
{
    public interface IRoleRepository
    {
        Task SaveAsync(Role role, CancellationToken cancellationToken = default);
        Task<Role?> GetAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
