using IdentityManager.Core.Roles.Commands.GrantAccessRight;
using IdentityManager.Data.Repositories;
using IdentityManager.Infrastructure.Time;
using MediatR;
using Microsoft.Extensions.Logging;

namespace IdentityManager.Core.Roles.Commands.RevokeAccessRight
{
    internal class RevokeAccessRightCommandHandler : IRequestHandler<RevokeAccessRightCommand, RevokeAccessRightCommandResult>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IAccessRightRepository _accessRightRepository;
        private readonly ITimeService _timeService;
        private readonly ILogger<RevokeAccessRightCommandHandler> _logger;

        public RevokeAccessRightCommandHandler(IRoleRepository roleRepository,
            IAccessRightRepository accessRightRepository,
            ITimeService timeService,
            ILogger<RevokeAccessRightCommandHandler> logger)
        {
            _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
            _accessRightRepository = accessRightRepository ?? throw new ArgumentNullException(nameof(accessRightRepository));
            _timeService = timeService ?? throw new ArgumentNullException(nameof(timeService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<RevokeAccessRightCommandResult> Handle(RevokeAccessRightCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var timestamp = _timeService.NowUtc;

                var role = await _roleRepository.GetByNameAsync(command.RoleName, cancellationToken);
                if (role is null)
                    return new RevokeAccessRightCommandResult { IsSuccess = false, Error = "Role with given name not found." };

                var accessRight = await _accessRightRepository.GetByCodeAsync(command.AccessRightCode, cancellationToken);
                if (accessRight is null)
                    return new RevokeAccessRightCommandResult { IsSuccess = false, Error = "Access right with given code not found." };

                role.RevokeAccessRight(accessRight, timestamp);

                await _roleRepository.SaveAsync(role);
                await _roleRepository.UnitOfWork.CommitChangesAsync();

                return new RevokeAccessRightCommandResult { IsSuccess = true };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error on revoking access right with code '{code}' to role with name '{name}'",
                    command.AccessRightCode, command.RoleName);

                return new RevokeAccessRightCommandResult
                {
                    IsSuccess = false,
                    Error = ex.Message,
                    ExceptionThrown = ex
                };
            }
        }
    }
}
