namespace Final.Authorization;

using Final.Data;
using Final.Services;
using Microsoft.Extensions.Primitives;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;

    public JwtMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, IAuthRepo customerService, IJwtUtils jwtUtils, HomezillaContext _context)
    {
        
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        var userData = jwtUtils.ValidateToken(token);
        if (userData != null)
        {
            // attach user to context on successful jwt validation
            context.Items["User"] = userData;  
        }

        await _next(context);
    }

}