using Framework.Core.comman;
using System.Net;
using System.Net.Mime;
using System.Text.Json;
using InvalidOperation = Framework.Core.comman.InvalidOperationException;

namespace Service.API.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionHandlerMiddleware> logger;

        public ExceptionHandlerMiddleware(RequestDelegate next,ILogger<ExceptionHandlerMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                await HandleError(context, ex);
            }
        }

        private async Task HandleError(HttpContext context, Exception ex)
        {
            if(!context.Response.HasStarted)
            {
                context.Response.ContentType = MediaTypeNames.Application.Json;
                // Defalut
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var response = new CustomError();

                switch(ex)
                {
                    case BadRequestException badRequestEx:
                        response.StatusCode = context.Response.StatusCode;
                        response.Message = badRequestEx.Message;
                        response.Details = "Bad Request Exception";
                        response.Error = 400;
                        break;
                    case ItemAlreadyExistException itemAlreadyExistEx:
                        response.StatusCode = context.Response.StatusCode;
                        response.Message = itemAlreadyExistEx.Message;
                        response.Details = "Item Already Exist Exception";
                        response.Error = 409;
                        break;
                    case ItemNotFoundException itemNotFound:
                        response.StatusCode = context.Response.StatusCode;
                        response.Message = itemNotFound.Message;
                        response.Details = "Item Not Found Exception";
                        response.Error = 404;
                        break;
                    case InvalidOperation invalidOperation:
                        response.StatusCode = context.Response.StatusCode;
                        response.Message = invalidOperation.Message;
                        response.Details = "Invalid Operation Exception";
                        response.Error = 405;
                        break;
                    case UnAuthorizedException unAuthorized:
                        response.StatusCode = context.Response.StatusCode;
                        response.Message = unAuthorized.Message;
                        response.Details = "this user UnAuthenticated";
                        response.Error = 401;
                        break;
                    case ForbiddenException forbidden:
                        response.StatusCode = context.Response.StatusCode;
                        response.Message = forbidden.Message;
                        response.Details = "this user UnAuthorized";
                        response.Error = 403;
                        break;
                    default:
                        response.StatusCode = context.Response.StatusCode;
                        response.Message = ex.Message;
                        response.Details = "Internal Server Error";
                        response.Error = 500;
                        break;
                }
                var json = JsonSerializer.Serialize(response);
                await context.Response.WriteAsync(json);
            }
            else
            {
                await context.Response.WriteAsync(string.Empty);
            }
        }
    }
}
