using IdentityManager.API.Admin.Contract.AccessManagement.Roles;
using IdentityManager.Core.Roles.Commands;
using IdentityManager.Infrastructure.Validation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IdentityManager.API.Admin.AccessManagement.Endpoints
{
    [Route(AdminApiCfg.Routing)]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IValidationService _validationService;

        public RolesController(IMediator mediator, IValidationService validationService)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _validationService = validationService ?? throw new ArgumentNullException(nameof(validationService));
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoleAsync([FromBody] CreateRoleRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validationService.ValidateAsync(request, cancellationToken);
            validationResult.ThrowOnValidationViolation();

            var result = await _mediator.Send(new CreateRoleCommand { Name = request.Name, CreatorId = Guid.NewGuid() });

            if (result.IsSuccess)
                return Ok(new CreateRoleResponse { Id = result.Id!.Value });

            return BadRequest();
        }
    }
}
