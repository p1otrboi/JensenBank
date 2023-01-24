using JensenBank.Infrastructure.Authentication;
using JensenBank.Infrastructure.Context;
using JensenBank.Infrastructure.Interfaces;
using JensenBank.Infrastructure.Repos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JensenBank.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
            services.AddSingleton<DapperContext>();
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddScoped<IPasswordEncryption, PasswordEncryption>();
            services.AddScoped<ICustomerRepo, CustomerRepo>();
            services.AddScoped<IAccountRepo, AccountRepo>();
            services.AddScoped<IUserRepo, UserRepo>();
            services.AddScoped<IDispositionRepo, DispositionRepo>();
            services.AddScoped<ILoanRepo, LoanRepo>();
            services.AddScoped<ITransactionRepo, TransactionRepo>();

            return services;
        }
    }
}
