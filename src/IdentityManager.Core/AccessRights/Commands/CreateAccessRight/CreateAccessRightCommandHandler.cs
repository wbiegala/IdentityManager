using IdentityManager.Core.Roles.Commands;
using IdentityManager.Data.Repositories;
using IdentityManager.Domain.AccessRights;
using IdentityManager.Infrastructure.Time;
using MediatR;
using Microsoft.Extensions.Logging;

namespace IdentityManager.Core.AccessRights.Commands.CreateAccessRight
{
    internal class CreateAccessRightCommandHandler : IRequestHandler<CreateAccessRightCommand, CreateAccessRightCommandResult>
    {
        private readonly IAccessRightRepository _accessRightRepository;
        private readonly ITimeService _timeService;
        private readonly ILogger<CreateRoleCommandHandler> _logger;

        public CreateAccessRightCommandHandler(IAccessRightRepository accessRightRepository,
            ITimeService timeService,
            ILogger<CreateRoleCommandHandler> logger)
        {
            _accessRightRepository = accessRightRepository ?? throw new ArgumentNullException(nameof(accessRightRepository));
            _timeService = timeService ?? throw new ArgumentNullException(nameof(timeService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<CreateAccessRightCommandResult> Handle(CreateAccessRightCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var creationTime = _timeService.NowUtc;
                var entity = AccessRight.Create(request.Code, request.Name, request.Description, creationTime);
                await _accessRightRepository.SaveAsync(entity, cancellationToken);
                await _accessRightRepository.UnitOfWork.CommitChangesAsync(cancellationToken);

                return new CreateAccessRightCommandResult
                {
                    IsSuccess = true,
                    Id = entity.Id,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error on creating new access right.");

                return new CreateAccessRightCommandResult
                {
                    IsSuccess = false,
                    Error = ex.Message,
                    ExceptionThrown = ex
                };
            }
        }
    }
}
