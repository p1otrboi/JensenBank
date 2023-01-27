using Microsoft.AspNetCore.Http;

namespace JensenBank.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int GetCustomerId()
        {
            var result = 0;
            if (_httpContextAccessor.HttpContext != null)
            {
                var claims = _httpContextAccessor.HttpContext.User.FindFirst("CustomerId");
                result = int.Parse(claims.Value);
            }
            return result;
        }
    }
}
