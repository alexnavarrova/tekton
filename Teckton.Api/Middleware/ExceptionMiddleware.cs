using System.Net;
using Tekton.Api.Errors;
using Newtonsoft.Json;
using Tekton.Application.Exceptions;

namespace Tekton.Api.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                var statusCode = (int)HttpStatusCode.InternalServerError;
                var result = string.Empty;

                switch (ex)
                {
                    case NotFoundException:
                        statusCode = (int)HttpStatusCode.NotFound;
                        break;

                    case ValidationException validationException:
                        statusCode = (int)HttpStatusCode.BadRequest;
                        result = JsonConvert.SerializeObject(new CodeErrorException<IDictionary<string, string[]>>(statusCode, ex.Message, validationException.Errors));
                        break;

                    case BadRequestException:
                        statusCode = (int)HttpStatusCode.BadRequest;
                        break;

                    default:
                        break;
                }

                if (string.IsNullOrEmpty(result))
                    result = JsonConvert.SerializeObject(new CodeErrorException<string>(statusCode, ex.Message, ex?.StackTrace ?? ""));


                context.Response.StatusCode = statusCode;

                await context.Response.WriteAsync(result);

            }

        }

    }
}

