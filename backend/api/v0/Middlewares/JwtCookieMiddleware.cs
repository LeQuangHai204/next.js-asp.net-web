// This middleware is used to get the JWT token from the cookie and append it to the request headers.
public class JwtCookieMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        string? token = context.Request.Cookies["AuthToken"];
        if (!string.IsNullOrEmpty(token)) context.Request.Headers.Append("Authorization", "Bearer " + token);
        await next(context);
    }
}
