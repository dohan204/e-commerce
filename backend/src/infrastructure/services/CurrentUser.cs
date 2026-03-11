using System.Security.Claims;
using application.interfaces;
using Microsoft.AspNetCore.Http;

namespace infrastructure.services
{
    public class CurrentUser : ICurrentUser
    {
        private readonly IHttpContextAccessor _httpContext;
        public CurrentUser(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }
        public string UserId 
         => _httpContext.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
    }
}