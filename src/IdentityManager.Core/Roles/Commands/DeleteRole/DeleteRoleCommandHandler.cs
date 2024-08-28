using IdentityManager.Data.Repositories;
using IdentityManager.Domain.AccessRights;
using IdentityManager.Domain.Roles;
using IdentityManager.Infrastructure.Time;
using MediatR;
using Microsoft.Extensions.Logging;

namespace IdentityManager.Core.Roles.Commands.DeleteRole
{
    internal class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, DeleteRoleCommandResult>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly ITimeService _timeService;
        private readonly ILogger<DeleteRoleCommandHandler> _logger;

        public DeleteRoleCommandHandler(IRoleRepository roleRepository,
            ITimeService timeService,
            ILogger<DeleteRoleCommandHandler> logger)
        {
            _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
            _timeService = timeService ?? throw new ArgumentNullException(nameof(timeService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<DeleteRoleCommandResult> Handle(DeleteRoleCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var role = await _roleRepository.GetByNameAsync(command.RoleName, cancellationToken);
                if (role is null)
                    return new DeleteRoleCommandResult { IsSuccess = false, Error = "Role with given name not found." };

                var timestamp = _timeService.NowUtc;

                await RevokeAllAccessRightsAsync(role, timestamp, cancellationToken);
                await DeactivateRoleAsync(role, timestamp, cancellationToken);

                _roleRepository.Delete(role);
                await _roleRepository.UnitOfWork.CommitChangesAsync();

                return new DeleteRoleCommandResult { IsSuccess = true };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error on deleting new role.");

                return new DeleteRoleCommandResult
                {
                    IsSuccess = false,
                    Error = ex.Message,
                    ExceptionThrown = ex
                };
            }
        }

        private async Task RevokeAllAccessRightsAsync(Role role, DateTimeOffset timestamp, CancellationToken cancellationToken)
        {
            var accessRights = new List<AccessRight>();
            accessRights.AddRange(role.AccessRights);

            accessRights.ForEach(ar => role.RevokeAccessRight(ar, timestamp));

            await _roleRepository.SaveAsync(role);
            await _roleRepository.UnitOfWork.CommitChangesAsync();
        }

        private async Task DeactivateRoleAsync(Role role, DateTimeOffset timestamp, CancellationToken cancellationToken)
        {
            role.Deactivate(timestamp);
            await _roleRepository.SaveAsync(role);
            await _roleRepository.UnitOfWork.CommitChangesAsync();
        }
    }
}
