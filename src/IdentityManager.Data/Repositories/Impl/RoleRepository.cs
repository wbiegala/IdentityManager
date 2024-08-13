using IdentityManager.Domain.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityManager.Data.Repositories.Impl
{
    internal class RoleRepository : IRoleRepository
    {
        private readonly IdentityManagerContext _dbContext;

        public RoleRepository(IdentityManagerContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<Role?> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Roles.FindAsync(id, cancellationToken);
        }

        public async Task SaveAsync(Role role, CancellationToken cancellationToken = default)
        {
            if (role.Id == default)
            {
                await _dbContext.Roles.AddAsync(role, cancellationToken);
            }
            else
            {
                _dbContext.Roles.Update(role);
            }
        }
    }
}
