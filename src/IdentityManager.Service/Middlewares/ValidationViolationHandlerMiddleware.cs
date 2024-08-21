using IdentityManager.Service.Contract.Base;
using IdentityManager.Service.Validation;
using System.Text.Json;

namespace IdentityManager.Service.Middlewares
{
    public class ValidationViolationHandlerMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (ValidationViolationException exception)
            {
                var model = new ValidationErrorResponse
                {
                    Failures = exception.Errors.Select(e => new ValidationErrorResponse.ValidationFailure
                    {
                        PropertyName = e.PropertyName,
                        Message = e.Message,
                        Code = e.Code,
                    })
                };

                context.Response.StatusCode = 422;
                context.Response.Headers.Add("Content-Type", "Application/Json");
                await context.Response.WriteAsync(JsonSerializer.Serialize(model, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    WriteIndented = true,
                }));
            }
        }
    }
}
