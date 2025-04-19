using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SchoolManagmentSystem.Core.Bases;
//using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;

namespace SchoolManagmentSystem.Core.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {

                var response = context.Response;
                response.ContentType = "application/json";
                var responseModel = new GeneralResponse<string>()
                {
                    Succeeded = false,
                    Message = error.Message,
                };
                switch (error)
                {

                    case UnauthorizedAccessException e:
                        responseModel.Message = e.Message;
                        responseModel.statusCode = HttpStatusCode.Unauthorized;
                        response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        break;

                    case ValidationException e:
                        responseModel.Message = e.Message;
                        responseModel.statusCode = HttpStatusCode.BadRequest;
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;

                    case DbUpdateConcurrencyException e:
                        responseModel.Message = e.Message;
                        responseModel.statusCode = HttpStatusCode.Conflict;
                        response.StatusCode = (int)HttpStatusCode.Conflict;
                        break;

                    case DbUpdateException e:
                        responseModel.Message = e.Message;
                        responseModel.statusCode = HttpStatusCode.Conflict;
                        response.StatusCode = (int)HttpStatusCode.Conflict;
                        break;

                    case KeyNotFoundException e:
                        responseModel.Message = e.Message;
                        responseModel.statusCode = HttpStatusCode.NotFound;
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;

                    case Exception e:
                        if (e.GetType().ToString() == "ApiException")
                        {
                            responseModel.Message = e.Message;
                            response.StatusCode = (int)HttpStatusCode.BadRequest;
                        }
                        else
                        {
                            responseModel.Message = "Internal Server Error";
                            response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        }
                        responseModel.Message = e.Message;
                        responseModel.Message += e.InnerException != null ? e.InnerException.Message : string.Empty;
                        break;

                }
                var result = JsonSerializer.Serialize(responseModel);
                await response.WriteAsync(result);

            }
        }
    }
}
