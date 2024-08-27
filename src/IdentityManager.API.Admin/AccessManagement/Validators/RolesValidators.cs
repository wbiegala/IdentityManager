using FluentValidation;
using IdentityManager.API.Admin.Contract.AccessManagement.Roles;
using IdentityManager.Core.Roles.Queries.GetRoleByName;
using MediatR;

namespace IdentityManager.API.Admin.AccessManagement.Validators
{
    public static class RolesValidators
    {
        public class CreateRoleRequestValidator : AbstractValidator<CreateRoleRequest>
        {
            public const string Error_NonUniqueName = "Role with given name already exists.";

            public CreateRoleRequestValidator(IMediator mediator)
            {
                RuleFor(req => req.Name)
                    .NotEmpty()
                    .CustomAsync(async (name, ctx, ct) =>
                        {
                            var roleWithGivenName = await mediator.Send(new GetRoleByNameQuery(name), ct);
                            if (roleWithGivenName is not null)
                                ctx.AddFailure(nameof(CreateRoleRequest.Name), Error_NonUniqueName);
                        });
            }
        }
    }
}
