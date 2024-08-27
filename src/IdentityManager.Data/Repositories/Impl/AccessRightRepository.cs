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

        public async Task<AccessRight?> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.AccessRights.FindAsync(id, cancellationToken);
        }

        public async Task<AccessRight?> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
        {
            return await _dbContext.AccessRights.SingleOrDefaultAsync(ac => ac.Code.ToUpper() == code.ToUpper(), cancellationToken);
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
