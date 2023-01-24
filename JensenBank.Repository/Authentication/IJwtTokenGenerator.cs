using JensenBank.Core.Domain;
using Models.Domain;

namespace JensenBank.Infrastructure.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(Customer c, User u);
}
