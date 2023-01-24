using JensenBank.Core.Dto;
using JensenBank.Infrastructure.Interfaces;
using Models.Domain;

namespace JensenBank.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IAccountRepo _accountRepo;
        private readonly IDispositionRepo _dispositionRepo;
        private readonly ICustomerRepo _customerRepo;


        public CustomerService(ICustomerRepo customerRepo, IAccountRepo accountRepo, IDispositionRepo dispositionRepo)
        {
            _customerRepo = customerRepo;
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

        public async Task<AccountTransactionsDto> GetAccountWithTransactions(int customerId, int accountId)
        {
            var accounts = await _accountRepo.GetAccountSummary(customerId);

            if (accounts.Find(x => x.AccountId == accountId) is null) 
            {
                throw new Exception("This is not your account.");
            }

            var result = await _accountRepo.GetAccountWithTransactions(accountId);

            return result;
        }
    }
}
