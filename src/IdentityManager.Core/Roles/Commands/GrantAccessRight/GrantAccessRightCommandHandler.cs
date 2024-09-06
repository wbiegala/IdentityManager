using IdentityManager.Data.Repositories;
using IdentityManager.Infrastructure.Time;
using MediatR;
using Microsoft.Extensions.Logging;

namespace IdentityManager.Core.Roles.Commands.GrantAccessRight
{
    internal class GrantAccessRightCommandHandler : IRequestHandler<GrantAccessRightCommand, GrantAccessRightCommandResult>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IAccessRightRepository _accessRightRepository;
        private readonly ITimeService _timeService;
        private readonly ILogger<GrantAccessRightCommandHandler> _logger;

        public GrantAccessRightCommandHandler(IRoleRepository roleRepository,
            IAccessRightRepository accessRightRepository,
            ITimeService timeService,
            ILogger<GrantAccessRightCommandHandler> logger)
        {
            _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
            _accessRightRepository = accessRightRepository ?? throw new ArgumentNullException(nameof(accessRightRepository));
            _timeService = timeService ?? throw new ArgumentNullException(nameof(timeService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<GrantAccessRightCommandResult> Handle(GrantAccessRightCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var timestamp = _timeService.NowUtc;

                var role = await _roleRepository.GetByNameAsync(command.RoleName, cancellationToken);
                if (role is null)
                    return new GrantAccessRightCommandResult { IsSuccess = false, Error = "Role with given name not found." };

                var accessRight = await _accessRightRepository.GetByCodeAsync(command.AccessRightCode, cancellationToken: cancellationToken);
                if (accessRight is null)
                    return new GrantAccessRightCommandResult { IsSuccess = false, Error = "Access right with given code not found." };

                role.GrantAccessRight(accessRight, timestamp);

                await _roleRepository.SaveAsync(role);
                await _roleRepository.UnitOfWork.CommitChangesAsync();

                return new GrantAccessRightCommandResult { IsSuccess = true };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error on granting access right with code '{code}' to role with name '{name}'",
                    command.AccessRightCode, command.RoleName);

                return new GrantAccessRightCommandResult
                {
                    IsSuccess = false,
                    Error = ex.Message,
                    ExceptionThrown = ex
                };
            }
        }
    }
}
