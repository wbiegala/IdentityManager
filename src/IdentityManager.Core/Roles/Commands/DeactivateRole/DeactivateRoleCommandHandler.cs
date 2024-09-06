using IdentityManager.Core.Roles.Commands.DeactivateRole;
using IdentityManager.Data.Repositories;
using IdentityManager.Infrastructure.Time;
using MediatR;
using Microsoft.Extensions.Logging;

namespace IdentityManager.Core.Roles.Commands.ActivateRole
{
    internal class DeactivateRoleCommandHandler : IRequestHandler<DeactivateRoleCommand, DeactivateRoleCommandResult>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly ITimeService _timeService;
        private readonly ILogger<DeactivateRoleCommandHandler> _logger;

        public DeactivateRoleCommandHandler(IRoleRepository roleRepository,
            ITimeService timeService,
            ILogger<DeactivateRoleCommandHandler> logger)
        {
            _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
            _timeService = timeService ?? throw new ArgumentNullException(nameof(timeService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<DeactivateRoleCommandResult> Handle(DeactivateRoleCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var role = await _roleRepository.GetByNameAsync(command.RoleName, cancellationToken);
                if (role is null)
                    return new DeactivateRoleCommandResult { IsSuccess = false, Error = "Role with given name not found." };

                var timestamp = _timeService.NowUtc;

                role.Deactivate(timestamp);

                await _roleRepository.SaveAsync(role);
                await _roleRepository.UnitOfWork.CommitChangesAsync();


                return new DeactivateRoleCommandResult { IsSuccess = true };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error on deactivating role.");

                return new DeactivateRoleCommandResult
                {
                    IsSuccess = false,
                    Error = ex.Message,
                    ExceptionThrown = ex
                };
            }
        }
    }
}
