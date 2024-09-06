using IdentityManager.Data.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace IdentityManager.Core.AccessRights.Commands.DeleteAccessRight
{
    internal class DeleteAccessRightCommandHandler : IRequestHandler<DeleteAccessRightCommand, DeleteAccessRightCommandResult>
    {
        private readonly IAccessRightRepository _accessRightRepository;
        private readonly ILogger<DeleteAccessRightCommandHandler> _logger;

        public DeleteAccessRightCommandHandler(IAccessRightRepository accessRightRepository, ILogger<DeleteAccessRightCommandHandler> logger)
        {
            _accessRightRepository = accessRightRepository ?? throw new ArgumentNullException(nameof(accessRightRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<DeleteAccessRightCommandResult> Handle(DeleteAccessRightCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var accessRight = await _accessRightRepository.GetByCodeAsync(command.Code, true, cancellationToken);
                if (accessRight is null)
                    return new DeleteAccessRightCommandResult { IsSuccess = false, Error = "Access right with given code not found." };

                if (accessRight.IsGrantedToAnyRole())
                    return new DeleteAccessRightCommandResult { IsSuccess = false, Error = "Access right cannot be delete while is granted to any role." };

                _accessRightRepository.Delete(accessRight);
                await _accessRightRepository.UnitOfWork.CommitChangesAsync(cancellationToken);

                return new DeleteAccessRightCommandResult { IsSuccess = true };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error on access right delete.");

                return new DeleteAccessRightCommandResult
                {
                    IsSuccess = false,
                    Error = ex.Message,
                    ExceptionThrown = ex
                };
            }
        }
    }
}
