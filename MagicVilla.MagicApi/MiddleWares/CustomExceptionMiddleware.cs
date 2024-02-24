namespace MagicVilla.MagicApi.MiddleWares
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _requestDelegate;

        public CustomExceptionMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        private async Task InvokeAsync(HttpContext context)
        {
            try
            {


                await _requestDelegate(context);
            }catch (Exception ex)
            {
                await ProcessException(context, ex);
            }
        }

        private async Task ProcessException(HttpContext context, Exception ex)
        {
            //throw new NotImplementedException();
         await   context.Response.WriteAsync("There was an exception");
        }
    }
}
