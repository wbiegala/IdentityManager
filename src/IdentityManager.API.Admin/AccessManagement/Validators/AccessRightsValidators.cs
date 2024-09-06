using FluentValidation;
using IdentityManager.API.Admin.Contract.AccessManagement.AccessRights;
using IdentityManager.Core.AccessRights;
using IdentityManager.Core.AccessRights.Queries.GetAccessRightByCode;
using IdentityManager.Core.AccessRights.Queries.GetAccessRightByName;
using IdentityManager.Infrastructure.Validation;
using MediatR;

namespace IdentityManager.API.Admin.AccessManagement.Validators
{
    public static class AccessRightsValidators
    {
        internal class CreateAccessRightValidator : AbstractValidator<CreateAccessRightRequest>
        {
            public const string Error_NonUniqueCode = "Access right with given code already exists.";
            public const string Error_NonUniqueName = "Access right with given name already exists.";

            public CreateAccessRightValidator(IMediator mediator)
            {
                RuleFor(req => req.Code)
                    .NotEmpty()
                    .Length(AccessRightsValidationRules.Code_MinLength, AccessRightsValidationRules.Code_MaxLength)
                    .AlphanumericOnly()
                    .CustomAsync(async (code, ctx, ct) =>
                        {
                            var accessRightWithGivenCode = await mediator.Send(new GetAccessRightByCodeQuery(code), ct);
                            if (accessRightWithGivenCode is not null)
                                ctx.AddFailure(nameof(CreateAccessRightRequest.Code), Error_NonUniqueCode);
                        });

                RuleFor(req => req.Name)
                    .NotEmpty()
                    .Length(AccessRightsValidationRules.Name_MinLength, AccessRightsValidationRules.Name_MaxLength)
                    .AlphanumericOnly()
                    .CustomAsync(async (name, ctx, ct) =>
                        {
                            var accessRightWithGivenName = await mediator.Send(new GetAccessRightByNameQuery(name), ct);
                            if (accessRightWithGivenName is not null)
                                ctx.AddFailure(nameof(CreateAccessRightRequest.Name), Error_NonUniqueName);
                        });
            }
        }
    }
}
