using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MagicVilla.MagicApi.Filter
{
    public class CustomExceptionFIlter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is BadImageFormatException)
            {
                context.Result = new ObjectResult("File not found  but handled")
                {
                    StatusCode = 503
                };
                //context.ExceptionHandled = true;
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }
    }
}
