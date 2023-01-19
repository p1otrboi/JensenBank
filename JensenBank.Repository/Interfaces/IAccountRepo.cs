using JensenBank.Core.Dto;

namespace JensenBank.Repository.Interfaces
{
    public interface IAccountRepo
    {
        public Task<int> AddAsync(AccountForCreationDto account);
        public Task<int> SetAccountType(int accountId, int accountTypeId);
    }
}
