using IdentityManager.Data.Repositories;
using IdentityManager.Domain.Roles;
using IdentityManager.Infrastructure.Time;
using MediatR;
using Microsoft.Extensions.Logging;

namespace IdentityManager.Core.Roles.Commands
{
    internal class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, CreateRoleCommandResult>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly ITimeService _timeService;
        private readonly ILogger<CreateRoleCommandHandler> _logger;

        public CreateRoleCommandHandler(IRoleRepository roleRepository,
            ITimeService timeService,
            ILogger<CreateRoleCommandHandler> logger)
        {
            _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
            _timeService = timeService ?? throw new ArgumentNullException(nameof(timeService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<CreateRoleCommandResult> Handle(CreateRoleCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var creationTime = _timeService.NowUtc;
                var entity = Role.Create(command.Name, creationTime);
                await _roleRepository.SaveAsync(entity);
                await _roleRepository.UnitOfWork.CommitChangesAsync();

                return new CreateRoleCommandResult
                {
                    IsSuccess = true,
                    Id = entity.Id,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error on creating new role.");

                return new CreateRoleCommandResult
                {
                    IsSuccess = false,
                    Error = ex.Message,
                    ExceptionThrown = ex
                };
            }
        }
    }
}
