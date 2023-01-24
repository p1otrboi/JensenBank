using JensenBank.Core.Domain;
using JensenBank.Core.Dto;

namespace JensenBank.Infrastructure.Interfaces;

public interface IUserRepo
{
    public Task<int> AddAsync(DbUserForCreationDto user);
    public Task<User> GetByUsername(string username);
}
