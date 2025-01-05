﻿using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Utilities.Exceptions.Handler
{
    public class CustomExceptionHandler
        (ILogger<CustomExceptionHandler> logger)
        : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            logger.LogError($"Error Message: {exception.Message}, Time of occurence {DateTime.Now}");
            (string Detail, string Title, int StatusCode) details = exception switch
            {
                ServerException => (exception.Message, exception.GetType().Name, httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError),
                BadRequestException => (exception.Message, exception.GetType().Name, httpContext.Response.StatusCode = StatusCodes.Status400BadRequest),
                ValidationException => (exception.Message, exception.GetType().Name, httpContext.Response.StatusCode = StatusCodes.Status400BadRequest),
                NotFoundException => (exception.Message, exception.GetType().Name, httpContext.Response.StatusCode = StatusCodes.Status404NotFound),
                _ => (exception.Message, "Internal Server Error", 500)
            };
            var problemDetails = new ProblemDetails
            {
                Title = details.Title,
                Status = details.StatusCode,
                Detail = details.Detail,
                Instance = httpContext.Request.Path
            };
            problemDetails.Extensions.Add("traceId", httpContext.TraceIdentifier);
            if (exception is ValidationException validationException)
            {
                problemDetails.Extensions.Add("ValidationErrors", validationException.Errors);
            }

            httpContext.Response.ContentType = "application/problem+json";
            await httpContext.Response.WriteAsJsonAsync(problemDetails);
            return true;
        }
    }
}
