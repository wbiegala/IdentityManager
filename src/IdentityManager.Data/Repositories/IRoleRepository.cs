using IdentityManager.Domain.Roles;

namespace IdentityManager.Data.Repositories
{
    public interface IRoleRepository : IRepository<Role>
    {
        Task SaveAsync(Role role, CancellationToken cancellationToken = default);
        Task<Role?> GetAsync(Guid id, CancellationToken cancellationToken = default);
        Task<Role?> GetByNameAsync(string searchingName, CancellationToken cancellationToken = default);
        void Delete(Role role);
    }
}
