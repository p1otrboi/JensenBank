using JensenBank.Core.Domain;
using Models.Domain;

namespace JensenBank.Repository.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(Customer c, User u);
}
