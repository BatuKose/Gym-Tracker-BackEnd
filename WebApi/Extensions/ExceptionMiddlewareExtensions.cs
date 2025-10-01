using Entites.Exceptions;
using System.Text.Json;

namespace WebApi.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureGlobalExceptionMiddleware(this WebApplication app)
        {
            app.Use(async (context, next) =>
            {
                try
                {
                    await next();
                }
                catch (Exception ex)
                {
                    context.Response.ContentType="application/json";
                    int statusCode;
                    string message;
                    if (ex is CustomExeption customEx)
                    {
                        statusCode= customEx.StatusCode;
                        message= customEx.Message;
                    }
                    else
                    {
                        statusCode=StatusCodes.Status500InternalServerError;
                        message="Beklenmeyen bir hata oluştu";
                    }
                    context.Response.StatusCode = statusCode;
                    var json = JsonSerializer.Serialize(
                        new
                        {
                            statusCode,
                            message
                        }
                        );
                    await context.Response.WriteAsync(json);
                }
            });
        }
    }
}

