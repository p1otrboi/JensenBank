using JensenBank.Core.Dto;
using JensenBank.Repository.Interfaces;
using Models.Domain;

namespace JensenBank.Service.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepo _customerRepo;
        private readonly IAccountRepo _accountRepo;
        private readonly IDispositionRepo _dispositionRepo;

        public CustomerService(ICustomerRepo cuistomerRepo, IAccountRepo accountRepo, IDispositionRepo dispositionRepo)
        {
            _customerRepo = cuistomerRepo;
            _accountRepo = accountRepo;
            _dispositionRepo = dispositionRepo;
        }

        public async Task<List<AccountSummaryDto>> GetAccountSummary(int customerId)
        {
            var result = await _accountRepo.GetAccountSummary(customerId);

            return result;
        }

        public async Task<List<AccountSummaryDto>> CreateAccount(int customerId, AccountForCreationDto details)
        {
            var id = await _accountRepo.AddAsync(details);

            await _dispositionRepo.AddAsync(customerId, id, "OWNER");

            var accounts = await _accountRepo.GetAccountSummary(customerId);

            return accounts;
        }
    }
}
