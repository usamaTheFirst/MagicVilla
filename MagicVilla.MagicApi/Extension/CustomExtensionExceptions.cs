using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;

namespace MagicVilla.MagicApi.Extension
{
    public  static class CustomExtensionExceptions
    {
         public static void ErrorHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(error =>
            {
                error.Run(async context =>
                {
                    context.Response.StatusCode = 500;
                    context.Response.ContentType = "application/json";
                    var feature = context.Features.Get<IExceptionHandlerFeature>();


                    if (feature != null)
                    {
                        


                        await context.Response.WriteAsync(JsonConvert.SerializeObject(new
                        {
                            StatusCode = context.Response.StatusCode,
                            ErrorMessage = "Hello  From program.cs exception handler"
                        }));
                    }
                });
            });

        }
    }
}
