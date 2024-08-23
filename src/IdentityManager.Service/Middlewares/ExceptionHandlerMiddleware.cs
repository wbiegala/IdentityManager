using IdentityManager.Service.Contract.Base;
using IdentityManager.Service.Validation;
using System.Text.Json;

namespace IdentityManager.Service.Middlewares
{
    public class ExceptionHandlerMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (ValidationViolationException ex)
            {
                await HandleValidationViolentionAsync(context, ex);
            }
            catch (Exception ex)
            {
                await HandleAsync(context, ex);
            }
        }

        private static async Task HandleValidationViolentionAsync(HttpContext context, ValidationViolationException exception)
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
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonSerializer.Serialize(model, ResponseOptions));
        }

        private static async Task HandleAsync(HttpContext context, Exception exception)
        {
            var model = new ErrorResponse
            {
                Message = exception.Message,
                Code = exception.GetType().Name,
                StackTrace = exception.StackTrace
            };

            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonSerializer.Serialize(model, ResponseOptions));
        }

        private static JsonSerializerOptions ResponseOptions =>
            new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
            };
    }
}
