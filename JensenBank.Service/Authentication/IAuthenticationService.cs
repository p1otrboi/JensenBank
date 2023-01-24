using JensenBank.Core.Dto;
using Microsoft.Identity.Client;

namespace JensenBank.Application.Authentication;

public interface IAuthenticationService
{
    Task<string> Login(LoginRequestDto request);
}
