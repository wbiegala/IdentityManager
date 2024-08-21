using FluentValidation;
using IdentityManager.Core.Roles.Queries.GetRoleByName;
using IdentityManager.Service.Contract.Roles;
using MediatR;

namespace IdentityManager.Service.Validation.Validators
{
    public static class RolesValidators
    {
        internal static IServiceCollection AddRolesValidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<CreateRoleRequest>, CreateRoleRequestValidator>();

            return services;
        }

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
