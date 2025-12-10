using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace BSEB_CoreAPI.Middleware
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<AuthenticationMiddleware> _logger;

        public AuthenticationMiddleware(RequestDelegate next, ILogger<AuthenticationMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            string path = context.Request.Path.Value?.ToLower() ?? "";

            // Let preflight requests pass through immediately
            if (string.Equals(context.Request.Method, "OPTIONS", StringComparison.OrdinalIgnoreCase))
            {
                // Optionally you can short-circuit with 200 here; but CORS middleware already handles it if registered earlier
                await _next(context);
                return;
            }

            // Allow the login API (public endpoint)
            if (path.Contains("/api/loginauth/login"))
            {
                await _next(context);
                return;
            }
            if (path.Contains("/api/dwnldregform/bindfaculty"))
            {
                await _next(context);
                return;
            }
            if (path.Contains("/api/dwnldregform/dwnldregform"))
            {
                await _next(context);
                return;
            }
          

            // ...existing auth header checks...
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (string.IsNullOrWhiteSpace(token))
            {
                _logger.LogWarning("Unauthorized access attempt on {URL}", context.Request.Path);
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Unauthorized access. Missing token.");
                return;
            }

            await _next(context);
        }

        //public async Task InvokeAsync(HttpContext context)
        //{
        //    string path = context.Request.Path.Value?.ToLower() ?? "";

        //    // -----------------------------
        //    // 1️⃣ SKIP PUBLIC ENDPOINTS
        //    // -----------------------------
        //    if (path.Contains("/api/loginauth/login"))     // login API
        //    {
        //        await _next(context);
        //        return;
        //    }

        //    // ADD MORE PUBLIC ENDPOINTS HERE IF NEEDED
        //    // if (path.Contains("/api/public"))
        //    // {
        //    //     await _next(context);
        //    //     return;
        //    // }

        //    // -----------------------------
        //    // 2️⃣ CHECK AUTHORIZATION HEADER
        //    // -----------------------------
        //    var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        //    if (string.IsNullOrWhiteSpace(token))
        //    {
        //        _logger.LogWarning("Unauthorized access attempt on {URL}", context.Request.Path);

        //        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        //        await context.Response.WriteAsync("Unauthorized access. Missing token.");
        //        return;
        //    }

        //    // -----------------------------
        //    // 3️⃣ TOKEN VALIDATION (Optional – placeholder)
        //    // -----------------------------
        //    // TODO: Replace with JWT / database validation
        //    // if (!ValidateToken(token)) { ... }

        //    await _next(context);
        //}
    }
}
