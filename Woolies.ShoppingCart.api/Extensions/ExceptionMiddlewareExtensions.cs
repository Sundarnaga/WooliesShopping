using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Woolies.Shopping.Infra;
using Woolies.Shopping.Models;

namespace Woolies.Shopping.api.Extensions
{
    /// <summary>
    ///Class to handle exception
    /// </summary>
    public static class ExceptionMiddlewareExtensions
    {
        /// <summary>
        ///Extension method to handle exception and to log the error
        /// </summary>
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILoggerManager loggerManager)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                            //Logging the error
                            loggerManager.LogError($"Error Occured: {contextFeature.Error}");
                            //Sending error details response
                            await context.Response.WriteAsync(JsonConvert.SerializeObject(new ErrorDetails
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = "Internal Server Error Occured. Check Log for more info"
                        }));
                        ;
                    }
                });
            });
        }
    }
}

