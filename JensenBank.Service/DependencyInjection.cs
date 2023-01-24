using JensenBank.Application.Authentication;
using JensenBank.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace JensenBank.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
