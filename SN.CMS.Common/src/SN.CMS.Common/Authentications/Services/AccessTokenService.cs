using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SN.CMS.Common.Authentications
{
    public class AccessTokenService : IAccessTokenService
    {
        private readonly IDistributedCache _cache;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IOptions<JwtOptions> _jwtOptions;

        public AccessTokenService(IDistributedCache cache,
               IHttpContextAccessor httpContextAccessor,
               IOptions<JwtOptions> jwtOptions)
        {
            _cache = cache;
            _httpContextAccessor = httpContextAccessor;
            _jwtOptions = jwtOptions;
        }

        public async Task DeactivateAsync(string userId, string token)
        {
            await _cache.SetStringAsync(GetKey(token),
                    "deactivated", new DistributedCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow =
                            TimeSpan.FromMinutes(_jwtOptions.Value.ExpiryMinutes)
                    });
        }

        public async Task DeactivateCurrentAsync(string userId)
        {
            await DeactivateAsync(userId, GetCurrentAsync());
        }

        public async Task<bool> IsActiveAsync(string token)
        {
            var tokenCache = await _cache.GetStringAsync(GetKey(token));
            return string.IsNullOrWhiteSpace(tokenCache);
        }

        public async Task<bool> IsCurrentActiveToken()
        {
            var result = await IsActiveAsync(GetCurrentAsync());
            return result;
        }

        private static string GetKey(string token)
        {
            var result = $"tokens:{token}";
            return result;
        }

        private string GetCurrentAsync()
        {
            var authorizationHeader = _httpContextAccessor
                .HttpContext.Request.Headers["authorization"];

            return authorizationHeader == StringValues.Empty
                ? string.Empty
                : authorizationHeader.Single().Split(' ').Last();
        }


    }
}
