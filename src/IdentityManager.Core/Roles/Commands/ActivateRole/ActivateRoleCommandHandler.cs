using IdentityManager.Data.Repositories;
using IdentityManager.Infrastructure.Time;
using MediatR;
using Microsoft.Extensions.Logging;

namespace IdentityManager.Core.Roles.Commands.ActivateRole
{
    internal class ActivateRoleCommandHandler : IRequestHandler<ActivateRoleCommand, ActivateRoleCommandResult>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly ITimeService _timeService;
        private readonly ILogger<ActivateRoleCommandHandler> _logger;

        public ActivateRoleCommandHandler(IRoleRepository roleRepository,
            ITimeService timeService,
            ILogger<ActivateRoleCommandHandler> logger)
        {
            _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
            _timeService = timeService ?? throw new ArgumentNullException(nameof(timeService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<ActivateRoleCommandResult> Handle(ActivateRoleCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var role = await _roleRepository.GetByNameAsync(command.RoleName, cancellationToken);
                if (role is null)
                    return new ActivateRoleCommandResult { IsSuccess = false, Error = "Role with given name not found." };

                var timestamp = _timeService.NowUtc;

                role.Activate(timestamp);

                await _roleRepository.SaveAsync(role);
                await _roleRepository.UnitOfWork.CommitChangesAsync();


                return new ActivateRoleCommandResult { IsSuccess = true };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error on creating new role.");

                return new ActivateRoleCommandResult
                {
                    IsSuccess = false,
                    Error = ex.Message,
                    ExceptionThrown = ex
                };
            }
        }
    }
}
