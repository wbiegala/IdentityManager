using IdentityManager.API.Admin.Contract.AccessManagement.AccessRights;
using IdentityManager.Core.AccessRights.Commands.CreateAccessRight;
using IdentityManager.Infrastructure.Validation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IdentityManager.API.Admin.AccessManagement.Endpoints
{
    [Route(AdminApiCfg.Routing)]
    [ApiController]
    public class AccessRightsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IValidationService _validationService;

        public AccessRightsController(IMediator mediator, IValidationService validationService)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _validationService = validationService ?? throw new ArgumentNullException(nameof(validationService));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccessRightAsync([FromBody]CreateAccessRightRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validationService.ValidateAsync(request, cancellationToken);
            validationResult.ThrowOnValidationViolation();

            var command = new CreateAccessRightCommand
            {
                Code = request.Code,
                Name = request.Name,
                Description = request.Description,
            };
            var result = await _mediator.Send(command, cancellationToken);

            if (result.IsSuccess)
                return Ok(new CreateAccessRightResponse { Id = result.Id!.Value });

            return BadRequest();
        }
    }
}
