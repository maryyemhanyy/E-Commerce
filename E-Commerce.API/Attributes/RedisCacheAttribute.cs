using E_Commerce.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;
using System.Text.Json;

namespace E_Commerce.API.Attributes
{
    public class RedisCacheAttribute : ActionFilterAttribute
    {
        private readonly int _durationInSeconds;
        public RedisCacheAttribute(int durationInSeconds = 90)
        {
            _durationInSeconds = durationInSeconds;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cacheService = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();

            var cacheKey = GenerateCacheKeyFromRequest(context.HttpContext.Request);

            var cachedResponse =await cacheService.GetAsync(cacheKey);

            if(!string.IsNullOrEmpty(cachedResponse))
            {
                context.Result = new ContentResult
                {
                    Content = cachedResponse,
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status200OK
                };
                return;
            }

            var executedContext = await next.Invoke();

            if (executedContext.Result is OkObjectResult { Value: { } value } )
            {
                var json = JsonSerializer.Serialize(value);
                await cacheService.SetAsync(cacheKey , json , TimeSpan.FromSeconds(_durationInSeconds));
            }
        }

        private string GenerateCacheKeyFromRequest(HttpRequest request)
        {
            var keyBuilder = new StringBuilder();
             keyBuilder.Append(request.Path).Append('?');

            foreach(var (key , value) in request.Query.OrderBy(q => q.Key))
            {
                keyBuilder.Append(key).Append('=').Append(value).Append('&');
            }

            return keyBuilder.ToString();
        }
    }
}
