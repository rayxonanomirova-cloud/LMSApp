
using System.Net;
using LMSApp.Infrastructure.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace LMSApp.Application.Middlewares;

    public static class TokenValidationMiddleware
    {
        public static IApplicationBuilder CustomTokenValidationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ValidateToken>();
        }
    }
    public class ValidateToken
    {
        private readonly RequestDelegate _next;

        public ValidateToken(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context,LMSAppContext dbContext)
        {
            try
            {
                var userId = context.User.FindFirst("Id")?.Value;

                if (!string.IsNullOrEmpty(userId))
                {
                    var user = await dbContext.Users.AnyAsync(x=>x.Id == Guid.Parse(userId) && x.RefreshToken == null &&
                    x.RefreshTokenExpiryTime.HasValue && x.RefreshTokenExpiryTime.Value < DateTime.UtcNow);

                    if (user)
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        await context.Response.WriteAsync("Unauthorized: Refresh token expired.");
                        return;                      
                    }
                }

                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                await context.Response.WriteAsync(new
                {
                    StatusCode = context.Response.StatusCode,
                    Message = ex.Message
                }.ToString());
            }
        }
    }
    

