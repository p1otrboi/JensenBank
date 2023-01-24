using JensenBank.Core.Dto;
using JensenBank.Repository.Interfaces;
using Models.Domain;

namespace JensenBank.Service.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepo _customerRepo;
        private readonly IAccountRepo _accountRepo;

        public CustomerService(ICustomerRepo cuistomerRepo, IAccountRepo accountRepo)
        {
            _customerRepo = cuistomerRepo;
            _accountRepo = accountRepo;
        }

        public async Task<List<AccountSummaryDto>> GetAccountSummary(int customerId)
        {
            var result = await _accountRepo.GetAccountSummary(customerId);

            return result;
        }
    }
}
