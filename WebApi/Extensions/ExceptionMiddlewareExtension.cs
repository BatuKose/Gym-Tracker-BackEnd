using Entites.ErrorModel;
using Entites.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Services.Contracts;

namespace WebApi.Extensions
{
    public static class ExceptionMiddlewareExtension
    {
        public static void ConfigureExceptionHandler( this WebApplication app,ILoggerService logger)
        {
            app.UseExceptionHandler(
                appError=>
                {
                    appError.Run(
                        async context=>
                        {
                            context.Response.ContentType = "application/json";
                            var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                            if (contextFeature != null)
                            {
                                context.Response.StatusCode = contextFeature.Error switch
                                {
                                    NotFoundException hata => StatusCodes.Status404NotFound,
                                    DBExceptions dbhata=>StatusCodes.Status500InternalServerError,
                                    _ => StatusCodes.Status500InternalServerError
                                };
                                logger.LogError($"Beklenmeyen bir hata meydana geldi: {contextFeature.Error}");
                                await context.Response.WriteAsync(new ErrorDetails()
                                {
                                    StatusCode = context.Response.StatusCode, 
                                    Message = contextFeature.Error.Message
                                }.ToString()); 
                            }
                        }
                        );
                }
                );
        }
    }
}
