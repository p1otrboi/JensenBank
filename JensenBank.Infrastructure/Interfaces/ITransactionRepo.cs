using JensenBank.Core.Dto;

namespace JensenBank.Infrastructure.Interfaces
{
    public interface ITransactionRepo
    {
        public Task<int> AddAsync(TransactionForCreationDto transaction);
    }
}
