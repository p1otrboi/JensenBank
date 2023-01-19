using JensenBank.Core.Dto;
using Microsoft.Identity.Client;

namespace JensenBank.Service.Authentication;

public interface IAuthenticationService
{
    Task<string> Login(LoginRequestDto request);
}
