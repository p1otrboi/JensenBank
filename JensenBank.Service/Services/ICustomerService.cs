using JensenBank.Core.Dto;

namespace JensenBank.Application.Services
{
    public interface ICustomerService
    {
        public Task<List<AccountSummaryDto>> GetAccountSummary(int customerId);
        public Task<List<AccountSummaryDto>> CreateAccount(int customerId, AccountForCreationDto details);
    }
}
