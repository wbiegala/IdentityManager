﻿using IdentityManager.Domain.Roles;
using Microsoft.EntityFrameworkCore;

namespace IdentityManager.Data.Repositories.Impl
{
    internal class RoleRepository : IRoleRepository
    {
        private readonly IdentityManagerContext _dbContext;

        public RoleRepository(IdentityManagerContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public IUnitOfWork UnitOfWork => _dbContext;

        public void Delete(Role role)
        {
            _dbContext.Roles.Remove(role);
        }

        public async Task<Role?> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Roles.FindAsync(id, cancellationToken);
        }

        public async Task<Role?> GetByNameAsync(string searchingName, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Roles
                .Include(role => role.AccessRights)
                .SingleOrDefaultAsync(r => r.Name.ToUpper() == searchingName.ToUpper(), cancellationToken);
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
