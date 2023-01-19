using JensenBank.Core.Dto;
using JensenBank.Repository.Authentication;
using JensenBank.Repository.Interfaces;

namespace JensenBank.Service.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepo _userRepo;
    private readonly ICustomerRepo _customerRepo;
    private readonly IPasswordEncryption _pwEncryption;

    public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator,
        IUserRepo userRepository,
        IPasswordEncryption pwEncryption, ICustomerRepo customerRepo)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepo = userRepository;
        _pwEncryption = pwEncryption;
        _customerRepo = customerRepo;
    }
    public async Task<string> Login(LoginRequestDto request)
    {
        var dbUser = await _userRepo.GetByUsername(request.Username);

        if (dbUser is null)
        {
            throw new Exception("Invalid username.");
        }

        var salt = Convert.FromHexString(dbUser.PW_Salt);

        bool loginResult = _pwEncryption.VerifyPassword(request.Password, dbUser.PW_Hash, salt);

        if (loginResult)
        {
            var dbCustomer = await _customerRepo.GetByIdAsync(dbUser.CustomerId);

            var token = _jwtTokenGenerator.GenerateToken(dbCustomer, dbUser);

            return token;
        }
        else
        {
            throw new Exception("Login failed.");
        }
    }
}
