using IdentityManager.API.Admin.Contract;
using IdentityManager.Core.Base;

namespace IdentityManager.API.Admin
{
    public static class ErrorResponseMappers
    {
        public static AdminApiErrorResponse MapCommandError(this CommandResult result) =>
            new()
            {
                Message = result.Error!,
                ErrorDetails = result.ErrorDetails,
            };
    }
}
