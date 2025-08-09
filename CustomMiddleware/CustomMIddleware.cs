using System.Security.Claims;
using Microsoft.Identity.Client;

namespace PetGrubBakcend.CustomMiddleware
{
    public class CustomMIddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomMIddleware> _logger;
        public CustomMIddleware(RequestDelegate next,ILogger<CustomMIddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        //public async Task InvokeAsync(HttpContext httpContext)
        //{
        //    if (httpContext.User.Identity?.IsAuthenticated == true)
        //    {
        //        var userId = httpContext.User.FindFirstValue("userId")
        //                      ?? httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

        //        if (userId != null)
        //        {
        //            httpContext.Items["UserId"] = userId;
        //        }
        //        else
        //        {
        //            _logger.LogWarning("No userId found in JWT claims.");
        //        }
        //    }

        //    await _next(httpContext);
        //}


        public async Task InvokeAsync(HttpContext httpContext)
        {
            if (httpContext.User.Identity?.IsAuthenticated == true)
            {
                var userId = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userId != null)
                {
                    httpContext.Items["UserId"] = userId;
                }
                else
                {
                    _logger.LogInformation("No NameIdentifier found in the jwt token");
                }
            }
            await _next(httpContext);
        }
    }
}
