using JensenBank.Core.Domain;
using Microsoft.IdentityModel.Tokens;
using Models.Domain;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JensenBank.Repository.Authentication;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    public string GenerateToken(Customer c, User u)
    {
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("JensenBankAppSecret")),
            SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim("CustomerId", c.CustomerId.ToString()),
            new Claim(ClaimTypes.Role, u.Role_Type),
            new Claim("Username", u.Username),
            new Claim("Givenname", c.Givenname),
            new Claim("Surname", c.Surname),
            new Claim("Country", c.Country)
        };

        var securityToken = new JwtSecurityToken(
            issuer: "http://localhost:5281/",
            audience: "http://localhost:5281/",
            expires: DateTime.Now.AddMinutes(20),
            claims: claims,
            signingCredentials: signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
}
