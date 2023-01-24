using JensenBank.Core.Dto;

namespace JensenBank.Service.Services
{
    public interface ICustomerService
    {
        public Task<List<AccountSummaryDto>> GetAccountSummary(int customerId);
    }
}
