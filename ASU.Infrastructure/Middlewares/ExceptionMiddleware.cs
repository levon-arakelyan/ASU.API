using ASU.Core.Enums;
using ASU.Infrastructure.Exceptions;
using MedMinder.Caregivers.Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch.Exceptions;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;

namespace ASU.Infrastructure.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(_logger, httpContext, ex);
            }
        }

        private static async Task HandleExceptionAsync(ILogger<ExceptionMiddleware> logger, HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = Core.Constants.AppConstants.CustomErrorCode;

            var logToFile = false;
            var errorId = Guid.NewGuid().ToString();

            var errorDetails = new ErrorDetailsDTO();

            if (exception is BaseException)
            {
                var baseException = exception as BaseException;
                errorDetails.Message = baseException.Message;
                errorDetails.ExceptionLevel = baseException.ExceptionLevel;
                errorDetails.HttpStatusCode = baseException.HttpStatusCode;
                logToFile = baseException.Log;
            }
            else if (exception is JsonPatchException)
            {
                errorDetails.Message = $"Json patch failed, {exception.Message}";
                errorDetails.ExceptionLevel = ExceptionLevel.Danger;
                errorDetails.HttpStatusCode = HttpStatusCode.BadRequest;
                logToFile = false;
            }
            else
            {
                //unhandled exception
                errorDetails.HttpStatusCode = HttpStatusCode.InternalServerError;
                errorDetails.ExceptionLevel = ExceptionLevel.Danger;
                logToFile = true;
                errorDetails.Message = $"Something is wrong. Please contact system administrator. Id={errorId}";
            }

            if (logToFile)
            {
                if (errorDetails.ExceptionLevel == ExceptionLevel.Info)
                {
                    logger.LogInformation($"Log Info: {exception}");
                }
                else if (errorDetails.ExceptionLevel == ExceptionLevel.Warning)
                {
                    logger.LogWarning($"Log Warning: {exception}");
                }
                else if (errorDetails.ExceptionLevel == ExceptionLevel.Danger)
                {
                    logger.LogError($"Log Error: {exception}");
                }
            }

            var json = JsonConvert.SerializeObject(errorDetails, new JsonSerializerSettings
            {
                ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver(),
                Formatting = Newtonsoft.Json.Formatting.None
            });

            await context.Response.WriteAsync(json);
        }
    }
}
