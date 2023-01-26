using JensenBank.Infrastructure.Authentication;
using JensenBank.Infrastructure.Context;
using JensenBank.Infrastructure.Interfaces;
using JensenBank.Infrastructure.Repos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace JensenBank.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
            services.AddSingleton<DapperContext>();
            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddScoped<IPasswordEncryption, PasswordEncryption>();
            services.AddScoped<ICustomerRepo, CustomerRepo>();
            services.AddScoped<IAccountRepo, AccountRepo>();
            services.AddScoped<IUserRepo, UserRepo>();
            services.AddScoped<IDispositionRepo, DispositionRepo>();
            services.AddScoped<ILoanRepo, LoanRepo>();
            services.AddScoped<ITransactionRepo, TransactionRepo>();
            services.AddAuth(configuration);

            return services;
        }

        public static IServiceCollection AddAuth(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                         .GetBytes(configuration.GetSection("JwtSettings:Secret").Value)),
                    ValidIssuer = configuration.GetSection("JwtSettings:Issuer").Value,
                    ValidAudience = configuration.GetSection("JwtSettings:Audience").Value
                };
        });
            return services;
        }
    }
}
