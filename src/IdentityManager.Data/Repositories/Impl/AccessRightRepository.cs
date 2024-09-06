using IdentityManager.Domain.AccessRights;
using Microsoft.EntityFrameworkCore;

namespace IdentityManager.Data.Repositories.Impl
{
    internal class AccessRightRepository : IAccessRightRepository
    {
        private readonly IdentityManagerContext _dbContext;


        public AccessRightRepository(IdentityManagerContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public IUnitOfWork UnitOfWork => _dbContext;

        public void Delete(AccessRight accessRight)
        {
            _dbContext.AccessRights.Remove(accessRight);
        }

        public async Task<AccessRight?> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.AccessRights.FindAsync(id, cancellationToken);
        }

        public async Task<AccessRight?> GetByCodeAsync(string code, bool includeRoles = false, CancellationToken cancellationToken = default)
        {
            var query = _dbContext.AccessRights.AsQueryable();
            query = includeRoles ? query.Include(ar => ar.Roles) : query;

            return await query.SingleOrDefaultAsync(ac => ac.Code.ToUpper() == code.ToUpper(), cancellationToken);
        }

        public async Task<AccessRight?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await _dbContext.AccessRights.SingleOrDefaultAsync(ac => ac.Name.ToUpper() == name.ToUpper(), cancellationToken);
        }

        public async Task SaveAsync(AccessRight accessRight, CancellationToken cancellationToken = default)
        {
            if (accessRight.Id == default)
            {
                await _dbContext.AccessRights.AddAsync(accessRight, cancellationToken);
            }
            else
            {
                 _dbContext.AccessRights.Update(accessRight);
            }
        }
    }
}
