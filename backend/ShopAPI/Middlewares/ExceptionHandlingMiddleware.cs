using System.Net;
using System.Text.Json;
using FluentValidation;

namespace ShopAPI.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

  
        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

    
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
          
                await _next(httpContext);
            }
      
            catch (ValidationException ex)
            {
               
                await HandleValidationExceptionAsync(httpContext, ex);
            }
         
        }

        private async Task HandleValidationExceptionAsync(HttpContext context, ValidationException exception)
        {
           
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Response.ContentType = "application/json";

           
            var errors = exception.Errors
                .Select(e => new { e.PropertyName, e.ErrorMessage });

         
            var json = JsonSerializer.Serialize(new { errors });
            await context.Response.WriteAsync(json);
        }
    }

}
