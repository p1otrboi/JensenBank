using Dapper;
using JensenBank.Core.Dto;
using JensenBank.Infrastructure.Context;
using JensenBank.Infrastructure.Interfaces;
using Models.Domain;
using System.Data;

namespace JensenBank.Infrastructure.Repos;

public class AccountRepo : IAccountRepo
{
    private readonly DapperContext _context;

    public AccountRepo(DapperContext context)
    {
        _context = context;
    }
    public async Task<decimal> GetBalanceAsync(int accountId)
    {
        var sp = "AccountGetBalance";
        var param = new DynamicParameters();
        param.Add("@AccountId", accountId);

        using (var db = _context.CreateConnection())
        {
            var balance = await db.QueryFirstOrDefaultAsync<decimal>(sp, param, commandType: CommandType.StoredProcedure);

            return balance;
        }
    }
    public async Task<Account> GetByIdAsync(int accountId)
    {
        var sp = "AccountGetById";
        var param = new DynamicParameters();
        param.Add("@AccountId", accountId);

        using (var db = _context.CreateConnection())
        {
            var account = await db.QuerySingleOrDefaultAsync<Account>(sp, param, commandType: CommandType.StoredProcedure);

            return account;
        }
    }
    public async Task<int> AddAsync(AccountForCreationDto account)
    {
        var sp = "AccountGetById";
        var param = new DynamicParameters();
        param.Add("@Frequency", account.Frequency);
        param.Add("@AccountTypesId", account.AccountTypeId);

        using (var db = _context.CreateConnection())
        {
            var id = await db.ExecuteScalarAsync<int>(sp, param, commandType: CommandType.StoredProcedure);

            return id;
        }
    }

    public async Task<int> SetAccountType(int accountId, int accountTypeId)
    {
        var sp = "AccountSetType";
        var param = new DynamicParameters();
        param.Add("@AccountId", accountId);
        param.Add("@AccountTypesId", accountTypeId);

        using (var db = _context.CreateConnection())
        {
            var id = await db.ExecuteScalarAsync<int>(sp, param, commandType: CommandType.StoredProcedure);

            return id;
        }
    }

    public async Task<decimal> AddAmountToAccountBalanceAsync(int accountId, decimal amount)
    {
        var sp = "AccountAddBalance";
        var param = new DynamicParameters();
        param.Add("@AccountId", accountId);
        param.Add("@Amount", amount);

        using (var db = _context.CreateConnection())
        {
            var balance = await db.QuerySingleAsync<decimal>(sp, param, commandType: CommandType.StoredProcedure);

            return balance;
        }
    }

    public async Task<decimal> SubAmountFromAccountBalanceAsync(int accountId, decimal amount)
    {
        var sp = "AccountSubBalance";
        var param = new DynamicParameters();
        param.Add("@AccountId", accountId);
        param.Add("@Amount", amount);

        using (var db = _context.CreateConnection())
        {
            var balance = await db.QuerySingleAsync<decimal>(sp, param, commandType: CommandType.StoredProcedure);

            return balance;
        }
    }

    public async Task<List<AccountSummaryDto>> GetAccountSummary(int customerId)
    {
        var sp = "AccountGetSummary";
        var param = new DynamicParameters();
        param.Add("@CustomerId", customerId);

        using (var db = _context.CreateConnection())
        {
            var result = await db.QueryAsync<AccountSummaryDto>(sp, param, commandType: CommandType.StoredProcedure);

            return result.ToList();
        }
    }

    public async Task<AccountTransactionsDto> GetAccountWithTransactions(int accountId)
    {
        var sp = "AccountGetWithTransactions";
        var param = new DynamicParameters();
        param.Add("@AccountId", accountId);

        using (var db = _context.CreateConnection())
        {
            var lookup = new Dictionary<int, AccountTransactionsDto>();

            var result = await db.QueryAsync<AccountTransactionsDto, Transaction, AccountTransactionsDto>(sp, (account, transaction) =>
            {
                if (!lookup.TryGetValue(account.AccountId, out AccountTransactionsDto? a))
                    lookup.Add(account.AccountId, a = account);

                if (account.Transactions is null)
                    a.Transactions = new List<Transaction>();

                a.Transactions.Add(transaction);

                return a;
            }, param, splitOn: "TransactionId", commandType: CommandType.StoredProcedure);

            return result.First();
        }
    }
}