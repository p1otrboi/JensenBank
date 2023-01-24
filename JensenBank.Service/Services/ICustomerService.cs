using JensenBank.Core.Dto;

namespace JensenBank.Application.Services
{
    public interface ICustomerService
    {
        public Task<List<AccountSummaryDto>> GetAccountSummary(int customerId);
        public Task<AccountTransactionsDto> GetAccountWithTransactions(int customerId, int accountId);
        public Task<List<AccountSummaryDto>> CreateAccount(int customerId, AccountForCreationDto details);
    }
}
