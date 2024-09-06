using IdentityManager.API.Admin.Contract.AccessManagement.Roles;
using IdentityManager.Core.Roles.Commands;
using IdentityManager.Core.Roles.Commands.ActivateRole;
using IdentityManager.Core.Roles.Commands.DeactivateRole;
using IdentityManager.Core.Roles.Commands.DeleteRole;
using IdentityManager.Core.Roles.Commands.GrantAccessRight;
using IdentityManager.Core.Roles.Commands.RevokeAccessRight;
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

            return BadRequest(result.MapCommandError());
        }

        [HttpDelete("{roleName}")]
        public async Task<IActionResult> DeleteRoleAsync([FromRoute]string roleName, CancellationToken cancellationToken)
        {
            var command = new DeleteRoleCommand { RoleName = roleName };
            var result = await _mediator.Send(command, cancellationToken);

            if (result.IsSuccess)
                return Ok();

            return BadRequest(result.MapCommandError());
        }

        [HttpPost("{roleName}/activate")]
        public async Task<IActionResult> ActivateRoleAsync([FromRoute]string roleName, CancellationToken cancellationToken)
        {
            var command = new ActivateRoleCommand { RoleName = roleName };
            var result = await _mediator.Send(command, cancellationToken);

            if (result.IsSuccess)
                return Ok();

            return BadRequest(result.MapCommandError());
        }

        [HttpPost("{roleName}/deactivate")]
        public async Task<IActionResult> DeactivateRoleAsync([FromRoute] string roleName, CancellationToken cancellationToken)
        {
            var command = new DeactivateRoleCommand { RoleName = roleName };
            var result = await _mediator.Send(command, cancellationToken);

            if (result.IsSuccess)
                return Ok();

            return BadRequest(result.MapCommandError());
        }

        [HttpPost("{roleName}/grant/{accessRightCode}")]
        public async Task<IActionResult> GrantAccessRightAsync(
            [FromRoute] string roleName,
            [FromRoute] string accessRightCode,
            CancellationToken cancellationToken)
        {
            var command = new GrantAccessRightCommand { RoleName = roleName, AccessRightCode = accessRightCode };
            var result = await _mediator.Send(command, cancellationToken);

            if (result.IsSuccess)
                return Ok();

            return BadRequest(result.MapCommandError());
        }

        [HttpPost("{roleName}/revoke/{accessRightCode}")]
        public async Task<IActionResult> RevokeAccessRightAsync(
            [FromRoute] string roleName,
            [FromRoute] string accessRightCode,
            CancellationToken cancellationToken)
        {
            var command = new RevokeAccessRightCommand { RoleName = roleName, AccessRightCode = accessRightCode };
            var result = await _mediator.Send(command, cancellationToken);

            if (result.IsSuccess)
                return Ok();

            return BadRequest(result.MapCommandError());
        }
    }
}
