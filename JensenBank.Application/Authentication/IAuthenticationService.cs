using JensenBank.Core.Dto;

namespace JensenBank.Application.Authentication;

public interface IAuthenticationService
{
    Task<string> Login(LoginRequestDto request);
}
