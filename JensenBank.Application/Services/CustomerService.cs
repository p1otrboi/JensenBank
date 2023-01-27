using JensenBank.Core.Dto;
using JensenBank.Infrastructure.Interfaces;

namespace JensenBank.Application.Services;

public class CustomerService : ICustomerService
{
    private readonly IAccountRepo _accountRepo;
    private readonly IDispositionRepo _dispositionRepo;
    private readonly ICustomerRepo _customerRepo;
    private readonly ITransactionRepo _transactionRepo;


    public CustomerService(ICustomerRepo customerRepo, IAccountRepo accountRepo,
        IDispositionRepo dispositionRepo, ITransactionRepo transactionRepo)
    {
        _customerRepo = customerRepo;
        _accountRepo = accountRepo;
        _dispositionRepo = dispositionRepo;
        _transactionRepo = transactionRepo;
    }

    public async Task<List<AccountSummaryDto>> GetAccountSummary(int customerId)
    {
        var result = await _accountRepo.GetAccountSummary(customerId);

        return result;
    }

    public async Task<List<AccountSummaryDto>> CreateAccount(int customerId, AccountForCreationDto details)
    {
        var accountId = await _accountRepo.AddAsync(details);

        await _dispositionRepo.AddAsync(customerId, accountId, "OWNER");

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

    public async Task<MoneyTransferDto> TransferMoney(int customerId, MoneyTransferDto details)
    {
        var accounts = await _accountRepo.GetAccountSummary(customerId);

        // check ownership of given customer account.no
        if (!accounts.Any(x => x.AccountId == details.From_Account))
        {
            throw new Exception("Transaction failed. This is not your bank account.");
        }

        // account lookup
        var recipentAccount = await _accountRepo.GetByIdAsync(details.To_Account);

        if (recipentAccount is null)
        {
            throw new Exception("Recipent account with given account.no does not exist.");
        }

        // check limits and funds
        if (details.Amount < 1)
        {
            throw new Exception("Transaction failed. Smallest amount to transfer is $1.");
        }
        if (details.Amount > accounts.First(x => x.AccountId == details.From_Account).Balance)
        {
            throw new Exception("Non-sufficent funds.");
        }

        // withdraw from senders account
        var senderBalance = await _accountRepo.SubAmountFromAccountBalanceAsync(details.From_Account, details.Amount);

        // create transaction for senders account
        var senderTransaction = new TransactionForCreationDto()
        {
            AccountId = details.From_Account,
            Type = "Debit",
            Operation = "Transfer Between Accounts",
            Amount = details.Amount,
            Balance = senderBalance,
            Account = $"To account #{details.To_Account}"
        };
        await _transactionRepo.AddAsync(senderTransaction);

        // deposit to recipent account
        var recipentBalance = await _accountRepo.AddAmountToAccountBalanceAsync(details.To_Account, details.Amount);

        // create transaction for recipent account
        var recipentTransaction = new TransactionForCreationDto()
        {
            AccountId = details.To_Account,
            Type = "Credit",
            Operation = "Transfer Between Accounts",
            Amount = details.Amount,
            Balance = recipentBalance,
            Account = $"From account #{details.From_Account}"
        };
        await _transactionRepo.AddAsync(recipentTransaction);

        return details;
    }
}
