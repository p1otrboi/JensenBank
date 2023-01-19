using JensenBank.Core.Domain;
using JensenBank.Core.Dto;

namespace JensenBank.Repository.Interfaces;

public interface IUserRepo
{
    public Task<int> AddAsync(DbUserForCreationDto user);
    public Task<User> GetByUsername(string username);
}
