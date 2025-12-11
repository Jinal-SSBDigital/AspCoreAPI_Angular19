using Microsoft.AspNetCore.Builder;

namespace BSEB_CoreAPI.Middleware
{
   
        public static class AuthenticationMiddlewareExtensions
        {
            public static IApplicationBuilder UseCustomAuthentication(this IApplicationBuilder builder)
            {
                return builder.UseMiddleware<AuthenticationMiddleware>();
            }
        }
    
}
