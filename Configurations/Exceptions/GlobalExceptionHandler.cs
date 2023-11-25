using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Metadata;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Task_Manager.Configurations.Exceptions;
public class GlobalExceptionHandler
{
    private readonly RequestDelegate _next;
    public GlobalExceptionHandler(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {

            await HandleExceptionAsync(context, ex);
        }

    }
    public static Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        HttpStatusCode statusCode;
        var stackTrace = string.Empty;
        var message = "";

        var exceptionType = ex.GetType();

        if (exceptionType == typeof(BadHttpRequestException))
        {
            stackTrace = ex.StackTrace;
            message = ex.Message;
            statusCode = HttpStatusCode.BadRequest;
        }
        else if (exceptionType == typeof(UnauthorizedAccessException))
        {
            stackTrace = ex.StackTrace;
            message = ex.Message;
            statusCode = HttpStatusCode.Unauthorized;
        }
        else if (exceptionType == typeof(NotFound))
        {
            stackTrace = ex.StackTrace;
            message = ex.Message;
            statusCode = HttpStatusCode.NotFound;
        }
        else
        {
            stackTrace = ex.StackTrace;
            message = ex.Message;
            statusCode = HttpStatusCode.InternalServerError;
        }

        var exceptionResult = JsonSerializer.Serialize(new { error = $"Error Occured is: {message}", stackTrace });
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        return context.Response.WriteAsync(exceptionResult);
    }
}
