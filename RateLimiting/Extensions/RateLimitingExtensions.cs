using Microsoft.AspNetCore.RateLimiting;

namespace RateLimiting.Extensions;

public static class RateLimitingExtensions
{
    public static void AddRateLimiting(this IServiceCollection services)
    {
        services.AddRateLimiter(options =>
        {
            options.RejectionStatusCode=StatusCodes.Status429TooManyRequests;
            options.OnRejected= async (context,token)=>
                await context.HttpContext.Response.WriteAsJsonAsync("baned for 5 min", token);
            
            options.AddTokenBucketLimiter("bucket", opt =>
            {
                opt.TokensPerPeriod = 20;
                opt.TokenLimit = 100;
                opt.ReplenishmentPeriod = TimeSpan.FromMinutes(5);
            });
        });
    }
}